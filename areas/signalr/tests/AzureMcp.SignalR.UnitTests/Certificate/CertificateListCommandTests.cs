// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.SignalR;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Models;
using AzureMcp.Core.Options;
using AzureMcp.SignalR.Commands.Certificate;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.SignalR.UnitTests.Certificate;

public class CertificateListCommandTests
{
    private readonly ISignalRService _signalRService;
    private readonly CertificateListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";

    public CertificateListCommandTests()
    {
        _signalRService = Substitute.For<ISignalRService>();
        var logger = Substitute.For<ILogger<CertificateListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);
        var serviceProvider = collection.BuildServiceProvider();

        _command = new(logger);
        _context = new(serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsCertificates()
    {
        // Arrange
        var expectedCertificates = new List<SignalRCertificateModel>
        {
            new()
            {
                Name = "cert1",
                Id =
                    $"/subscriptions/{_knownSubscriptionId}/resourceGroups/{_knownResourceGroup}/providers/Microsoft.SignalRService/signalR/{_knownSignalRName}/customCertificates/cert1",
                Type = "Microsoft.SignalRService/signalR/customCertificates",
                ProvisioningState = "Succeeded",
                KeyVaultBaseUri = "https://vault1.vault.azure.net/",
                KeyVaultSecretName = "cert1-secret",
                KeyVaultSecretVersion = "v1"
            },
            new()
            {
                Name = "cert2",
                Id =
                    $"/subscriptions/{_knownSubscriptionId}/resourceGroups/{_knownResourceGroup}/providers/Microsoft.SignalRService/signalR/{_knownSignalRName}/customCertificates/cert2",
                Type = "Microsoft.SignalRService/signalR/customCertificates",
                ProvisioningState = "Succeeded",
                KeyVaultBaseUri = "https://vault2.vault.azure.net/",
                KeyVaultSecretName = "cert2-secret",
                KeyVaultSecretVersion = "v2"
            }
        };

        _signalRService.ListCertificatesAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedCertificates);

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
            .Deserialize<CertificateListCommand.CertificateListCommandResult>(
                json, SignalRJsonContext.Default.CertificateListCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal(2, resultData.Certificates.Count);
        Assert.Equal("cert1", resultData.Certificates[0].Name);
        Assert.Equal("https://vault1.vault.azure.net/", resultData.Certificates[0].KeyVaultBaseUri);
        Assert.Equal("cert2", resultData.Certificates[1].Name);
        Assert.Equal("https://vault2.vault.azure.net/", resultData.Certificates[1].KeyVaultBaseUri);
    }

    [Fact]
    public async Task ExecuteAsync_NoCertificates_ReturnsNull()
    {
        // Arrange
        _signalRService.ListCertificatesAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<RetryPolicyOptions?>())
            .Returns(new List<SignalRCertificateModel>());

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
        _signalRService.ListCertificatesAsync(
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
