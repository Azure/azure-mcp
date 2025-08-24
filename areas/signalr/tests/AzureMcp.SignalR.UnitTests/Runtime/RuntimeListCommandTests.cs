// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using Azure;
using AzureMcp.Core.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.SignalR;
using AzureMcp.SignalR.Commands.Runtime;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.SignalR.UnitTests.SignalR;

public class RuntimeListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISignalRService _signalRService;
    private readonly ILogger<RuntimeListCommand> _logger;
    private readonly RuntimeListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";

    public RuntimeListCommandTests()
    {
        _signalRService = Substitute.For<ISignalRService>();
        _logger = Substitute.For<ILogger<RuntimeListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsSignalRRuntimes()
    {
        // Arrange
        var expectedServices = new List<SignalRRuntimeModel>
        {
            new()
            {
                Name = "signalr1",
                ResourceGroupName = "rg1",
                Location = "East US",
                SkuName = "Standard_S1",
                SkuTier = "Standard",
                ProvisioningState = "Succeeded",
                HostName = "signalr1.service.signalr.net",
                PublicPort = 443,
                ServerPort = 443
            },
            new()
            {
                Name = "signalr2",
                ResourceGroupName = "rg2",
                Location = "West US",
                SkuName = "Free_F1",
                SkuTier = "Free",
                ProvisioningState = "Succeeded",
                HostName = "signalr2.service.signalr.net",
                PublicPort = 443,
                ServerPort = 443
            }
        };

        _signalRService.ListRuntimesAsync(
                _knownSubscriptionId,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(expectedServices);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Serialize the entire ResponseResult to JSON and then deserialize to verify content
        var json = System.Text.Json.JsonSerializer.Serialize(response.Results);
        var resultData = System.Text.Json.JsonSerializer
            .Deserialize<RuntimeListCommand.RuntimeListCommandResult>(
                json, SignalRJsonContext.Default.RuntimeListCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal(2, resultData.Runtimes.Count());

        var services = resultData.Runtimes.ToList();
        Assert.Equal("signalr1", services[0].Name);
        Assert.Equal("rg1", services[0].ResourceGroupName);
        Assert.Equal("Standard_S1", services[0].SkuName);
        Assert.Equal("signalr2", services[1].Name);
        Assert.Equal("rg2", services[1].ResourceGroupName);
        Assert.Equal("Free_F1", services[1].SkuName);
    }

    [Fact]
    public async Task ExecuteAsync_NoSignalRRuntimes_ReturnsNull()
    {
        // Arrange
        var emptyRuntimes = new List<SignalRRuntimeModel>();

        _signalRService.ListRuntimesAsync(
                _knownSubscriptionId,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(emptyRuntimes);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_RequestFailedException403_ReturnsAuthorizationError()
    {
        // Arrange
        var requestException = new RequestFailedException(403, "Forbidden");
        _signalRService.ListRuntimesAsync(
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(requestException);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(403, response.Status);
        Assert.NotNull(response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_MissingRequiredParameters_ReturnsValidationError()
    {
        // Arrange - missing subscription
        var parseResult = _parser.Parse("");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_HandlesGracefully()
    {
        // Arrange
        var exception = new InvalidOperationException("Service error");
        _signalRService.ListRuntimesAsync(
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(exception);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
        Assert.NotNull(response.Message);
    }

    [Fact]
    public void Command_Properties_AreCorrect()
    {
        // Assert
        Assert.Equal("list", _command.Name);
        Assert.Equal("List all Services", _command.Title);
        Assert.Contains("List all SignalR Service resources", _command.Description);
        Assert.False(_command.Metadata.Destructive);
        Assert.True(_command.Metadata.ReadOnly);
    }
}
