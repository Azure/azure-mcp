// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
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

public class RuntimeShowCommandTests
{
    private readonly ISignalRService _signalRService;
    private readonly RuntimeShowCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";

    public RuntimeShowCommandTests()
    {
        _signalRService = Substitute.For<ISignalRService>();
        var logger = Substitute.For<ILogger<RuntimeShowCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);
        var serviceProvider = collection.BuildServiceProvider();

        _command = new(logger);
        _context = new(serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsSignalRService()
    {
        // Arrange
        var expectedService = new SignalRRuntimeModel
        {
            Name = _knownSignalRName,
            ResourceGroupName = _knownResourceGroup,
            Location = "East US",
            SkuName = "Standard_S1",
            SkuTier = "Standard",
            ProvisioningState = "Succeeded",
            HostName = "signalr123.service.signalr.net",
            PublicPort = 443,
            ServerPort = 443
        };

        _signalRService.GetRuntimeAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedService);

        // Act
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Serialize the entire ResponseResult to JSON and then deserialize to verify content
        var json = System.Text.Json.JsonSerializer.Serialize(response.Results);
        var resultData = System.Text.Json.JsonSerializer.Deserialize<RuntimeShowCommand.RuntimeShowCommandResult>(
            json, SignalRJsonContext.Default.RuntimeShowCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal(_knownSignalRName, resultData.Runtime.Name);
        Assert.Equal(_knownResourceGroup, resultData.Runtime.ResourceGroupName);
        Assert.Equal("East US", resultData.Runtime.Location);
        Assert.Equal("Standard_S1", resultData.Runtime.SkuName);
        Assert.Equal("Succeeded", resultData.Runtime.ProvisioningState);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceNotFound_Returns404()
    {
        // Arrange
        _signalRService.GetRuntimeAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns((SignalRRuntimeModel?)null);

        // Act
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(404, response.Status);
        Assert.Contains("not found", response.Message ?? string.Empty);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        _signalRService.GetRuntimeAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Exception("Service error"));

        // Act
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotEqual(200, response.Status);
        Assert.Contains("Service error", response.Message ?? string.Empty);
    }

    [Fact]
    public async Task ExecuteAsync_MissingRequiredParameters_ReturnsValidationError()
    {
        // Act
        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotEqual(200, response.Status);
    }

    [Theory]
    [InlineData("show")]
    public void Name_ReturnsCorrectValue(string expectedName)
    {
        // Assert
        Assert.Equal(expectedName, _command.Name);
    }

    [Fact]
    public void Description_ReturnsNonEmptyString()
    {
        // Assert
        Assert.False(string.IsNullOrWhiteSpace(_command.Description));
    }

    [Fact]
    public void Title_ReturnsNonEmptyString()
    {
        // Assert
        Assert.False(string.IsNullOrWhiteSpace(_command.Title));
    }

    [Fact]
    public void Metadata_ReturnsReadOnlyNonDestructive()
    {
        // Assert
        Assert.True(_command.Metadata.ReadOnly);
        Assert.False(_command.Metadata.Destructive);
    }
}
