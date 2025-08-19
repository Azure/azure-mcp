// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using Azure;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Models;
using AzureMcp.AzureSignalR.Commands.Certificate;
using AzureMcp.AzureSignalR.Models;
using AzureMcp.AzureSignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.AzureSignalR.UnitTests.Certificate;

public class CertificateShowCommandTests
{
    private readonly IAzureSignalRService _signalRService;
    private readonly CertificateShowCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";
    private readonly string _knownCertificateName = "cert123";

    public CertificateShowCommandTests()
    {
        _signalRService = Substitute.For<IAzureSignalRService>();
        var logger = Substitute.For<ILogger<CertificateShowCommand>>();

        var collection = new ServiceCollection().AddSingleton(_signalRService);
        var serviceProvider = collection.BuildServiceProvider();

        _command = new(logger);
        _context = new(serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsCertificate()
    {
        // Arrange
        var expectedCertificate = new SignalRCertificateModel
        {
            Name = _knownCertificateName,
            Id = $"/subscriptions/{_knownSubscriptionId}/resourceGroups/{_knownResourceGroup}/providers/Microsoft.SignalRService/signalR/{_knownSignalRName}/customCertificates/{_knownCertificateName}",
            Type = "Microsoft.SignalRService/signalR/customCertificates",
            ProvisioningState = "Succeeded",
            KeyVaultBaseUri = "https://keyvault.vault.azure.net/",
            KeyVaultSecretName = "certificate-secret",
            KeyVaultSecretVersion = "v1"
        };

        _signalRService.GetCertificateAsync(
            _knownSubscriptionId,
            _knownResourceGroup,
            _knownSignalRName,
            _knownCertificateName,
            Arg.Any<string?>(),
            Arg.Any<AuthMethod?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(expectedCertificate);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --certificate-name {_knownCertificateName}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Serialize the entire ResponseResult to JSON and then deserialize to verify content
        var json = System.Text.Json.JsonSerializer.Serialize(response.Results);
        var resultData = System.Text.Json.JsonSerializer.Deserialize<CertificateShowCommand.CertificateShowCommandResult>(
            json, AzureSignalRJsonContext.Default.CertificateShowCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal(expectedCertificate.Name, resultData.Certificate.Name);
        Assert.Equal(expectedCertificate.Id, resultData.Certificate.Id);
        Assert.Equal(expectedCertificate.ProvisioningState, resultData.Certificate.ProvisioningState);
        Assert.Equal(expectedCertificate.KeyVaultBaseUri, resultData.Certificate.KeyVaultBaseUri);
        Assert.Equal(expectedCertificate.KeyVaultSecretName, resultData.Certificate.KeyVaultSecretName);
        Assert.Equal(expectedCertificate.KeyVaultSecretVersion, resultData.Certificate.KeyVaultSecretVersion);
    }

    [Fact]
    public async Task ExecuteAsync_CertificateNotFound_Returns404()
    {
        // Arrange
        _signalRService.GetCertificateAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<AuthMethod?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns((SignalRCertificateModel?)null);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --certificate-name {_knownCertificateName}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.Status);
        Assert.Contains("not found", response.Message);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_RequestFailedException404_HandledByGenericExceptionHandler()
    {
        // Arrange
        var requestException = new RequestFailedException(404, "Not Found");
        _signalRService.GetCertificateAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<AuthMethod?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(requestException);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --certificate-name {_knownCertificateName}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        // The generic exception handler will set an error status (not necessarily 404)
        Assert.NotEqual(200, response.Status);
        Assert.NotNull(response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_RequestFailedException403_HandledByGenericExceptionHandler()
    {
        // Arrange
        var requestException = new RequestFailedException(403, "Forbidden");
        _signalRService.GetCertificateAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<AuthMethod?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(requestException);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --certificate-name {_knownCertificateName}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        // The generic exception handler will set an error status (not necessarily 403)
        Assert.NotEqual(200, response.Status);
        Assert.NotNull(response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_MissingRequiredParameters_ReturnsValidationError()
    {
        // Arrange - missing certificate name
        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_HandledByGenericExceptionHandler()
    {
        // Arrange
        var exception = new InvalidOperationException("Service error");
        _signalRService.GetCertificateAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<AuthMethod?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(exception);

        var parseResult = _parser.Parse($"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --certificate-name {_knownCertificateName}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        // The generic exception handler will set an error status
        Assert.NotEqual(200, response.Status);
        Assert.NotNull(response.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public async Task ExecuteAsync_InvalidCertificateName_ReturnsValidationError(string certificateName)
    {
        // Arrange
        var args = $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --certificate-name \"{certificateName}\"";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
    }

    [Fact]
    public void Command_Properties_AreCorrect()
    {
        // Assert
        Assert.Equal("show", _command.Name);
        Assert.Equal("Show SignalR Certificate", _command.Title);
        Assert.Contains("Show details of a custom certificate", _command.Description);
        Assert.False(_command.Metadata.Destructive);
        Assert.True(_command.Metadata.ReadOnly);
    }
}
