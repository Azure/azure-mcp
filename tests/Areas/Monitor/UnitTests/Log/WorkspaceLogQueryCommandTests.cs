// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json.Nodes;
using AzureMcp.Areas.Monitor.Commands.Log;
using AzureMcp.Areas.Monitor.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Monitor.UnitTests.Log;

[Trait("Area", "Monitor")]
public sealed class WorkspaceLogQueryCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMonitorService _monitorService;
    private readonly ILogger<WorkspaceLogQueryCommand> _logger;
    private readonly WorkspaceLogQueryCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private const string _knownSubscriptionId = "knownSubscription";
    private const string _knownWorkspaceId = "knownWorkspace";
    private const string _knownResourceGroupId = "knownResourceGroup";
    private const string _knownTableName = "knownTable";
    private const string _knownTenantId = "knownTenant";
    private const string _knownHours = "24";
    private const string _knownLimit = "100";
    private const string _knownQuery = "| limit 10";

    public WorkspaceLogQueryCommandTests()
    {
        _monitorService = Substitute.For<IMonitorService>();
        _logger = Substitute.For<ILogger<WorkspaceLogQueryCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_monitorService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Theory]
    [InlineData($"--subscription {_knownSubscriptionId} --workspace {_knownWorkspaceId} --resource-group {_knownResourceGroupId} --table-name {_knownTableName} --query {_knownQuery}", true)]
    [InlineData($"--subscription {_knownSubscriptionId} --workspace {_knownWorkspaceId} --resource-group {_knownResourceGroupId} --table-name {_knownTableName} --query {_knownQuery} --hours {_knownHours} --limit {_knownLimit}", true)]
    [InlineData($"--subscription {_knownSubscriptionId} --workspace {_knownWorkspaceId} --table-name {_knownTableName} --query {_knownQuery}", false)] // missing resource-group
    [InlineData($"--subscription {_knownSubscriptionId}", false)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var mockResults = new List<JsonNode>
            {
                JsonNode.Parse(@"{""TimeGenerated"": ""2023-01-01T12:00:00Z"", ""Message"": ""Test log entry""}") ?? JsonNode.Parse("{}") ?? new JsonObject(),
                JsonNode.Parse(@"{""TimeGenerated"": ""2023-01-01T12:01:00Z"", ""Message"": ""Another log entry""}") ?? JsonNode.Parse("{}") ?? new JsonObject()
            };
            _monitorService.QueryWorkspaceLogs(
                Arg.Any<string>(), 
                Arg.Any<string>(), 
                Arg.Any<string>(), 
                Arg.Any<string>(), 
                Arg.Any<int?>(), 
                Arg.Any<int?>(), 
                Arg.Any<string>(), 
                Arg.Any<RetryPolicyOptions>())
                .Returns(mockResults);
        }

        // Act
        var response = await _command.ExecuteAsync(_context, _parser.Parse(args));

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsQueryResults()
    {
        // Arrange
        var mockResults = new List<JsonNode>
        {
            JsonNode.Parse(@"{""TimeGenerated"": ""2023-01-01T12:00:00Z"", ""Message"": ""Test log entry"", ""Level"": ""Info""}") ?? new JsonObject(),
            JsonNode.Parse(@"{""TimeGenerated"": ""2023-01-01T12:01:00Z"", ""Message"": ""Another log entry"", ""Level"": ""Warning""}") ?? new JsonObject(),
            JsonNode.Parse(@"{""TimeGenerated"": ""2023-01-01T12:02:00Z"", ""Message"": ""Error occurred"", ""Level"": ""Error""}") ?? new JsonObject()
        };
        _monitorService.QueryWorkspaceLogs(
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<int?>(), 
            Arg.Any<int?>(), 
            Arg.Any<string>(), 
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResults);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--workspace", _knownWorkspaceId,
            "--resource-group", _knownResourceGroupId,
            "--table-name", _knownTableName,
            "--query", _knownQuery
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Verify the mock was called
        await _monitorService.Received(1).QueryWorkspaceLogs(
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<int?>(), 
            Arg.Any<int?>(), 
            Arg.Any<string>(), 
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceWithCorrectParameters()
    {
        // Arrange
        var mockResults = new List<JsonNode> { JsonNode.Parse(@"{""result"": ""data""}") ?? new JsonObject() };
        _monitorService.QueryWorkspaceLogs(
            _knownSubscriptionId,
            _knownWorkspaceId,
            _knownQuery,
            _knownTableName,
            int.Parse(_knownHours),
            int.Parse(_knownLimit),
            _knownTenantId,
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResults);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--workspace", _knownWorkspaceId,
            "--resource-group", _knownResourceGroupId,
            "--table-name", _knownTableName,
            "--query", _knownQuery,
            "--hours", _knownHours,
            "--limit", _knownLimit,
            "--tenant", _knownTenantId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _monitorService.Received(1).QueryWorkspaceLogs(
            _knownSubscriptionId,
            _knownWorkspaceId,
            _knownQuery,
            _knownTableName,
            int.Parse(_knownHours),
            int.Parse(_knownLimit),
            _knownTenantId,
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithDefaultParameters_UsesExpectedDefaults()
    {
        // Arrange
        var mockResults = new List<JsonNode> { JsonNode.Parse(@"{""result"": ""data""}") ?? new JsonObject() };
        _monitorService.QueryWorkspaceLogs(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<int?>(),
            Arg.Any<int?>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(mockResults);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--workspace", _knownWorkspaceId,
            "--resource-group", _knownResourceGroupId,
            "--table-name", _knownTableName,
            "--query", _knownQuery
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _monitorService.Received(1).QueryWorkspaceLogs(
            _knownSubscriptionId,
            _knownWorkspaceId,
            _knownQuery,
            _knownTableName,
            Arg.Any<int?>(), // Default hours
            Arg.Any<int?>(), // Default limit
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _monitorService.QueryWorkspaceLogs(
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<string>(), 
            Arg.Any<int?>(), 
            Arg.Any<int?>(), 
            Arg.Any<string>(), 
            Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<List<JsonNode>>(new Exception("Test error")));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--workspace", _knownWorkspaceId,
            "--resource-group", _knownResourceGroupId,
            "--table-name", _knownTableName,
            "--query", _knownQuery
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }
}
