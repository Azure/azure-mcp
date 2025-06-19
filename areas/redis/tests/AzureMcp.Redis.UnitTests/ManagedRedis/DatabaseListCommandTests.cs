// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.Redis.Commands.ManagedRedis;
using AzureMcp.Redis.Models.ManagedRedis;
using AzureMcp.Redis.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Redis.UnitTests.ManagedRedis;

public class DatabaseListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisService _redisService;
    private readonly ILogger<DatabaseListCommand> _logger;
    private readonly DatabaseListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";
    private readonly string _knownResourceGroup = "rg1";
    private readonly string _knownCluster = "cluster1";

    public DatabaseListCommandTests()
    {
        _redisService = Substitute.For<IRedisService>();
        _logger = Substitute.For<ILogger<DatabaseListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_redisService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsDatabases_WhenDatabasesExist()
    {
        var expectedDatabases = new Database[]
        {
            new()
            {
                Name = "db1",
                ClusterName = "cluster1",
                ResourceGroupName = "rg1",
                SubscriptionId = "sub123",
                Port = 10000,
                ProvisioningState = "Succeeded"
            },
            new()
            {
                Name = "db2",
                ClusterName = "cluster1",
                ResourceGroupName = "rg1",
                SubscriptionId = "sub123",
                Port = 10001,
                ProvisioningState = "Succeeded"
            }
        };

        _redisService.ListDatabasesAsync(
            Arg.Is(_knownCluster),
            Arg.Is(_knownResourceGroup),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<AuthMethod>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions>())
            .Returns(expectedDatabases);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--cluster", _knownCluster
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<DatabaseListCommandResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Equal(expectedDatabases.Length, result.Databases.Count());
        Assert.Collection(result.Databases,
            item => Assert.Equal("db1", item.Name),
            item => Assert.Equal("db2", item.Name));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoDatabases()
    {
        _redisService.ListDatabasesAsync(
            Arg.Is(_knownCluster),
            Arg.Is(_knownResourceGroup),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<AuthMethod>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions>())
            .Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--cluster", _knownCluster
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<DatabaseListCommandResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Databases);
        Assert.Empty(result.Databases);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        _redisService.ListDatabasesAsync(
            Arg.Is(_knownCluster),
            Arg.Is(_knownResourceGroup),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<AuthMethod>(),
            Arg.Any<AzureMcp.Core.Options.RetryPolicyOptions>())
            .ThrowsAsync(new Exception("Test error"));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--resource-group", _knownResourceGroup,
            "--cluster", _knownCluster
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--cluster")]
    public async Task ExecuteAsync_ReturnsError_WhenRequiredParameterIsMissing(string parameterToKeep)
    {
        var command = new DatabaseListCommand(_logger);

        var options = new List<string>();
        if (parameterToKeep == "--subscription")
            options.AddRange(["--subscription", _knownSubscriptionId]);
        if (parameterToKeep == "--resource-group")
            options.AddRange(["--resource-group", _knownResourceGroup]);
        if (parameterToKeep == "--cluster")
            options.AddRange(["--cluster", _knownCluster]);

        var parseResult = _parser.Parse(options.ToArray());

        var response = await _command.ExecuteAsync(_context, parseResult);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message, StringComparison.OrdinalIgnoreCase);
    }

    private record DatabaseListCommandResult(IEnumerable<Database> Databases)
    {
        public IEnumerable<Database> Databases { get; set; } = Databases ?? [];
    }
}
