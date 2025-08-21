// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.SignalR;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Models;
using AzureMcp.Core.Options;
using AzureMcp.SignalR.Commands.CustomDomain;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.SignalR.UnitTests.CustomDomain;

public class CustomDomainListCommandTests
{
    private readonly ISignalRService _signalRService;
    private readonly CustomDomainListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";

    public CustomDomainListCommandTests()
    {
        _signalRService = Substitute.For<ISignalRService>();
        var logger = Substitute.For<ILogger<CustomDomainListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);
        var serviceProvider = collection.BuildServiceProvider();

        _command = new(logger);
        _context = new(serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsCustomDomains()
    {
        // Arrange
        var expectedCustomDomains = new List<SignalRCustomDomainModel>
        {
            new()
            {
                Name = "domain1",
                Id =
                    $"/subscriptions/{_knownSubscriptionId}/resourceGroups/{_knownResourceGroup}/providers/Microsoft.SignalRService/signalR/{_knownSignalRName}/customDomains/domain1",
                Type = "Microsoft.SignalRService/signalR/customDomains",
                ProvisioningState = "Succeeded",
                DomainName = "api.example.com",
                CustomCertificate =
                    "/subscriptions/sub123/resourceGroups/rg123/providers/Microsoft.SignalRService/signalR/signalr123/customCertificates/cert1"
            },
            new()
            {
                Name = "domain2",
                Id =
                    $"/subscriptions/{_knownSubscriptionId}/resourceGroups/{_knownResourceGroup}/providers/Microsoft.SignalRService/signalR/{_knownSignalRName}/customDomains/domain2",
                Type = "Microsoft.SignalRService/signalR/customDomains",
                ProvisioningState = "Succeeded",
                DomainName = "signalr.contoso.com",
                CustomCertificate =
                    "/subscriptions/sub123/resourceGroups/rg123/providers/Microsoft.SignalRService/signalR/signalr123/customCertificates/cert2"
            }
        };

        _signalRService.ListCustomDomainsAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedCustomDomains);

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
        var resultData = System.Text.Json.JsonSerializer
            .Deserialize<CustomDomainListCommand.CustomDomainListCommandResult>(
                json, SignalRJsonContext.Default.CustomDomainListCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal(2, resultData.CustomDomains.Count);
        Assert.Equal("domain1", resultData.CustomDomains[0].Name);
        Assert.Equal("api.example.com", resultData.CustomDomains[0].DomainName);
        Assert.Equal("domain2", resultData.CustomDomains[1].Name);
        Assert.Equal("signalr.contoso.com", resultData.CustomDomains[1].DomainName);
    }

    [Fact]
    public async Task ExecuteAsync_NoCustomDomains_ReturnsNull()
    {
        // Arrange
        _signalRService.ListCustomDomainsAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(new List<SignalRCustomDomainModel>());

        // Act
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName}");
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        _signalRService.ListCustomDomainsAsync(
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
