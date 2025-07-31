// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using AzureMcp.Foundry.Commands;
using AzureMcp.Foundry.Services;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Foundry.UnitTests;

public class AgentsConnectCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IFoundryService _foundryService;

    public AgentsConnectCommandTests()
    {
        _foundryService = Substitute.For<IFoundryService>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_foundryService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ConnectsToAgent_WhenValidOptionsProvided()
    {
        var agentId = "test-agent-id";
        var query = "What is the weather today?";
        var endpoint = "https://test-endpoint.azure.com";

        var expectedResponse = new Dictionary<string, object>
        {
            { "success", true },
            { "thread_id", "thread-123" },
            { "run_id", "run-456" },
            { "result", "The weather is sunny today." },
            { "citations", new List<string>() }
        };

        _foundryService.ConnectAgent(
                Arg.Is<string>(s => s == agentId),
                Arg.Is<string>(s => s == query),
                Arg.Is<string>(s => s == endpoint),
                Arg.Any<string?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedResponse);

        var command = new AgentsConnectCommand();
        var args = command.GetCommand().Parse(["--agent-id", agentId, "--query", query, "--endpoint", endpoint]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var agentId = "test-agent-id";
        var query = "What is the weather today?";
        var endpoint = "https://test-endpoint.azure.com";
        var expectedError = "Failed to connect to agent: Service unavailable";

        _foundryService.ConnectAgent(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Exception(expectedError));

        var command = new AgentsConnectCommand();
        var args = command.GetCommand().Parse(["--agent-id", agentId, "--query", query, "--endpoint", endpoint]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidArguments_ReturnsValidationError()
    {
        var query = "What is the weather today?";
        var endpoint = "https://test-endpoint.azure.com";

        var command = new AgentsConnectCommand();
        var args = command.GetCommand().Parse(["--query", query, "--endpoint", endpoint]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
        Assert.Contains("AgentId", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithFailedAgentRun_ReturnsErrorResult()
    {
        var agentId = "test-agent-id";
        var query = "What is the weather today?";
        var endpoint = "https://test-endpoint.azure.com";

        var errorResponse = new Dictionary<string, object>
        {
            { "success", false },
            { "error", "Agent run failed: Internal server error" },
            { "thread_id", "thread-123" },
            { "run_id", "run-456" },
            { "result", "Error: Agent run failed: Internal server error" }
        };

        _foundryService.ConnectAgent(
                Arg.Is<string>(s => s == agentId),
                Arg.Is<string>(s => s == query),
                Arg.Is<string>(s => s == endpoint),
                Arg.Any<string?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(errorResponse);

        var command = new AgentsConnectCommand();
        var args = command.GetCommand().Parse(["--agent-id", agentId, "--query", query, "--endpoint", endpoint]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);
    }
}
