// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Models;
using AzureMcp.Core.Options;
using AzureMcp.AzureSignalR.Commands.Key;
using AzureMcp.AzureSignalR.Models;
using AzureMcp.AzureSignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.AzureSignalR.UnitTests.Key;

public class KeyListCommandTests
{
    private readonly IAzureSignalRService _signalRService;
    private readonly KeyListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";

    public KeyListCommandTests()
    {
        _signalRService = Substitute.For<IAzureSignalRService>();
        var logger = Substitute.For<ILogger<KeyListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);
        var serviceProvider = collection.BuildServiceProvider();

        _command = new(logger);
        _context = new(serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsKeys()
    {
        // Arrange
        var expectedKeys = new SignalRKeyModel
        {
            KeyType = "Both",
            PrimaryKey = "primary-key-value",
            SecondaryKey = "secondary-key-value",
            PrimaryConnectionString = "Endpoint=https://signalr123.service.signalr.net;AccessKey=primary-key-value;Version=1.0;",
            SecondaryConnectionString = "Endpoint=https://signalr123.service.signalr.net;AccessKey=secondary-key-value;Version=1.0;",
            ConnectionString = "Endpoint=https://signalr123.service.signalr.net;AccessKey=primary-key-value;Version=1.0;"
        };

        _signalRService.ListKeysAsync(
            _knownSubscriptionId,
            _knownResourceGroup,
            _knownSignalRName,
            Arg.Any<string?>(),
            Arg.Any<AuthMethod?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedKeys);

        // Act
        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --name {_knownSignalRName}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Serialize the entire ResponseResult to JSON and then deserialize to verify content
        var json = System.Text.Json.JsonSerializer.Serialize(response.Results);
        var resultData = System.Text.Json.JsonSerializer.Deserialize<KeyListCommand.KeyListCommandResult>(
            json, AzureSignalRJsonContext.Default.KeyListCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal("Both", resultData.Keys.KeyType);
        Assert.Equal("primary-key-value", resultData.Keys.PrimaryKey);
        Assert.Equal("secondary-key-value", resultData.Keys.SecondaryKey);
        Assert.Contains("primary-key-value", resultData.Keys.PrimaryConnectionString);
        Assert.Contains("secondary-key-value", resultData.Keys.SecondaryConnectionString);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        _signalRService.ListKeysAsync(
            _knownSubscriptionId,
            _knownResourceGroup,
            _knownSignalRName,
            Arg.Any<string?>(),
            Arg.Any<AuthMethod?>(),
            Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Exception("Service error"));

        // Act
        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --name {_knownSignalRName}");
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
    [InlineData("list")]
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
