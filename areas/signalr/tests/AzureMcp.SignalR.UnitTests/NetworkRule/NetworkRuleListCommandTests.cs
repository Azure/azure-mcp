// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using AzureMcp.Core.Models.Command;
using AzureMcp.SignalR.Commands.NetworkRule;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.CommandLine.Parsing;
using AzureMcp.Core.Models;
using AzureMcp.Core.Options;
using Xunit;

namespace AzureMcp.SignalR.UnitTests.NetworkRule;

public class NetworkRuleListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISignalRService _service;
    private readonly ILogger<NetworkRuleListCommand> _logger;
    private readonly NetworkRuleListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public NetworkRuleListCommandTests()
    {
        _service = Substitute.For<ISignalRService>();
        _logger = Substitute.For<ILogger<NetworkRuleListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("network access control rules", command.Description);
    }

    [Theory]
    [InlineData("--signalr-name testSignalR --resource-group testRG --subscription testSub", true)]
    [InlineData("--signalr-name testSignalR --resource-group testRG", false)] // Missing subscription
    [InlineData("--resource-group testRG --subscription testSub", false)] // Missing signalr-name
    [InlineData("", false)] // Missing all required params
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        var networkRules = new SignalRNetworkAclModel
        {
            DefaultAction = "Allow",
            PublicNetwork = new SignalRNetworkRuleModel { Allow = new[] { "ClientConnection", "RESTAPI" } }
        };

        // Set up service mock for all cases to avoid NSubstitute issues
        _service.GetNetworkRulesAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions>())
            .Returns(networkRules);

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        if (shouldSucceed)
        {
            Assert.Equal(200, response.Status);
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            // Validation failures return 400 status with error message about missing required options
            Assert.Equal(400, response.Status);
            Assert.Contains("Missing Required options", response.Message);
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange - Use a more realistic Azure exception
        var azureException = new RequestFailedException(500, "Test error", "InternalServerError", null);
        _service.GetNetworkRulesAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(azureException);

        var parseResult = _parser.Parse([
            "--signalr-name", "testSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNullWhenNoNetworkRules()
    {
        // Arrange
        _service.GetNetworkRulesAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns((SignalRNetworkAclModel?)null);

        var parseResult = _parser.Parse([
            "--signalr-name", "testSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNetworkRulesSuccessfully()
    {
        // Arrange
        var expectedNetworkRules = new SignalRNetworkAclModel
        {
            DefaultAction = "Deny",
            PublicNetwork =
                new SignalRNetworkRuleModel
                {
                    Allow = new[] { "ServerConnection", "ClientConnection", "RESTAPI", "Trace" }, Deny = null
                },
            PrivateEndpoints = new[]
            {
                new SignalRPrivateEndpointModel { Name = "test-pe", Allow = new[] { "ClientConnection" } }
            }
        };

        _service.GetNetworkRulesAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedNetworkRules);

        var parseResult = _parser.Parse([
            "--signalr-name", "testSignalR", "--resource-group", "testRG", "--subscription", "testSub"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Equal("Success", response.Message);
    }
}
