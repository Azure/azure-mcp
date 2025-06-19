// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Postgres.Commands.Database;
using AzureMcp.Postgres.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Postgres.UnitTests.Database;

public class DatabaseListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPostgresService _postgresService;
    private readonly ILogger<DatabaseListCommand> _logger;
    private readonly DatabaseListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";
    private readonly string _knownResourceGroup = "rg1";
    private readonly string _knownUser = "user1";
    private readonly string _knownServer = "server1";

    public DatabaseListCommandTests()
    {
        _postgresService = Substitute.For<IPostgresService>();
        _logger = Substitute.For<ILogger<DatabaseListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_postgresService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsDatabases_WhenDatabasesExist()
    {
        var expectedDatabases = new List<string> { "db1", "db2" };
        _postgresService.ListDatabasesAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser), Arg.Is(_knownServer)).Returns(expectedDatabases);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser,
            "--server", _knownServer
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<DatabaseListResult>(json);
        Assert.NotNull(result);
        Assert.Equal(expectedDatabases, result.Databases);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsMessage_WhenNoDatabasesExist()
    {
        _postgresService.ListDatabasesAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser), Arg.Is(_knownServer)).Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser,
            "--server", _knownServer
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<DatabaseListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Databases);
        Assert.Empty(result.Databases);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        _postgresService.ListDatabasesAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser), Arg.Is(_knownServer)).ThrowsAsync(new Exception("Test exception"));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser,
            "--server", _knownServer
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Test exception", response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--user")]
    [InlineData("--server")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var args = _parser.Parse(new string[]
        {
            missingParameter == "--subscription" ? "" : "--subscription", _knownSubscriptionId,
            missingParameter == "--resource-group" ? "" : "--resource-group", _knownResourceGroup,
            missingParameter == "--user-name" ? "" : "--user-name", _knownUser,
            missingParameter == "--server" ? "" : "--server", _knownServer,
        });

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }


    private class DatabaseListResult
    {
        [JsonPropertyName("Databases")]
        public List<string> Databases { get; set; } = new List<string>();
    }
}
