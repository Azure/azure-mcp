// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Postgres.Commands.Server;
using AzureMcp.Postgres.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Postgres.UnitTests.Server;

public class ServerListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPostgresService _postgresService;
    private readonly ILogger<ServerListCommand> _logger;
    private readonly ServerListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";
    private readonly string _knownResourceGroup = "rg1";
    private readonly string _knownUser = "user1";

    public ServerListCommandTests()
    {
        _postgresService = Substitute.For<IPostgresService>();
        _logger = Substitute.For<ILogger<ServerListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_postgresService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsServers_WhenServersExist()
    {
        var expectedServers = new List<string> { "server1", "server2" };
        _postgresService.ListServersAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser)).Returns(expectedServers);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ServerListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedServers, result.Servers);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoServers()
    {
        _postgresService.ListServersAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser)).Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ServerListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Servers);
        Assert.Empty(result.Servers);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";

        _postgresService.ListServersAsync(Arg.Is(_knownSubscriptionId), Arg.Is(_knownResourceGroup), Arg.Is(_knownUser))
            .ThrowsAsync(new Exception("Test error"));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--user-name", _knownUser
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--user")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var args = _parser.Parse(new string[]
        {
            missingParameter == "--subscription" ? "" : "--subscription", _knownSubscriptionId,
            missingParameter == "--resource-group" ? "" : "--resource-group", _knownResourceGroup,
            missingParameter == "--user-name" ? "" : "--user-name", _knownUser,
        });

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }

    private class ServerListResult
    {
        [JsonPropertyName("Servers")]
        public List<string> Servers { get; set; } = new List<string>();
    }
}
