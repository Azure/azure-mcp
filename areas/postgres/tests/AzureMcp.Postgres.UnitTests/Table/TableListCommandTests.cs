// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Postgres.Commands.Table;
using AzureMcp.Postgres.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Postgres.UnitTests.Table;

public class TableListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPostgresService _postgresService;
    private readonly ILogger<TableListCommand> _logger;
    private readonly TableListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";
    private readonly string _knownResourceGroup = "rg1";
    private readonly string _knownUser = "user1";
    private readonly string _knownServer = "server1";
    private readonly string _knownDatabase = "db123";

    public TableListCommandTests()
    {
        _postgresService = Substitute.For<IPostgresService>();
        _logger = Substitute.For<ILogger<TableListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_postgresService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsTables_WhenTablesExist()
    {
        var expectedTables = new List<string> { "table1", "table2" };
        _postgresService.ListTablesAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser), Arg.Is(_knownServer), Arg.Is(_knownDatabase)).Returns(expectedTables);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser,
            "--server", _knownServer,
            "--database", _knownDatabase
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<TableListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedTables, result.Tables);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoTablesExist()
    {
        _postgresService.ListTablesAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser), Arg.Is(_knownServer), Arg.Is(_knownDatabase)).Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser,
            "--server", _knownServer,
            "--database", _knownDatabase
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<TableListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Tables);
        Assert.Empty(result.Tables);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--user")]
    [InlineData("--server")]
    [InlineData("--database")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var args = _parser.Parse(new string[]
        {
            missingParameter == "--subscription" ? "" : "--subscription", _knownSubscriptionId,
            missingParameter == "--resource-group" ? "" : "--resource-group", _knownResourceGroup,
            missingParameter == "--user-name" ? "" : "--user-name", _knownUser,
            missingParameter == "--server" ? "" : "--server", _knownServer,
            missingParameter == "--database" ? "" : "--database", _knownDatabase
        });

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }

    private class TableListResult
    {
        [JsonPropertyName("Tables")]
        public List<string> Tables { get; set; } = [];
    }
}
