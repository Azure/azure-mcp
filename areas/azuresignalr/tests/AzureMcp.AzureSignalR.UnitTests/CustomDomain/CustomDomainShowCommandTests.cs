// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Models;
using AzureMcp.Core.Options;
using AzureMcp.AzureSignalR.Commands.CustomDomain;
using AzureMcp.AzureSignalR.Models;
using AzureMcp.AzureSignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.AzureSignalR.UnitTests.CustomDomain;

public class CustomDomainShowCommandTests
{
    private readonly IAzureSignalRService _signalRService;
    private readonly CustomDomainShowCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";
    private readonly string _knownCustomDomainName = "api.example.com";

    public CustomDomainShowCommandTests()
    {
        _signalRService = Substitute.For<IAzureSignalRService>();
        var logger = Substitute.For<ILogger<CustomDomainShowCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);
        var serviceProvider = collection.BuildServiceProvider();

        _command = new(logger);
        _context = new(serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsCustomDomain()
    {
        // Arrange
        var expectedCustomDomain = new SignalRCustomDomainModel
        {
            Name = _knownCustomDomainName,
            Id =
                $"/subscriptions/{_knownSubscriptionId}/resourceGroups/{_knownResourceGroup}/providers/Microsoft.SignalRService/signalR/{_knownSignalRName}/customDomains/{_knownCustomDomainName}",
            Type = "Microsoft.SignalRService/signalR/customDomains",
            ProvisioningState = "Succeeded",
            DomainName = _knownCustomDomainName,
            CustomCertificate =
                "/subscriptions/sub123/resourceGroups/rg123/providers/Microsoft.SignalRService/signalR/signalr123/customCertificates/cert1"
        };

        _signalRService.GetCustomDomainAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                _knownCustomDomainName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedCustomDomain);

        // Act
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCustomDomainName}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Serialize the entire ResponseResult to JSON and then deserialize to verify content
        var json = System.Text.Json.JsonSerializer.Serialize(response.Results);
        var resultData = System.Text.Json.JsonSerializer
            .Deserialize<CustomDomainShowCommand.CustomDomainShowCommandResult>(
                json, AzureSignalRJsonContext.Default.CustomDomainShowCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal(_knownCustomDomainName, resultData.CustomDomain.Name);
        Assert.Equal(_knownCustomDomainName, resultData.CustomDomain.DomainName);
        Assert.Equal("Succeeded", resultData.CustomDomain.ProvisioningState);
    }

    [Fact]
    public async Task ExecuteAsync_CustomDomainNotFound_Returns404()
    {
        // Arrange
        _signalRService.GetCustomDomainAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                _knownCustomDomainName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns((SignalRCustomDomainModel?)null);

        // Act
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCustomDomainName}");
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
        _signalRService.GetCustomDomainAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                _knownCustomDomainName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Exception("Service error"));

        // Act
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCustomDomainName}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotEqual(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_MissingRequiredParameters_ReturnsValidationError()
    {
        // Act
        var parseResult =
            _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup}");
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
