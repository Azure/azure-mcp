// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using Azure;
using AzureMcp.SignalR;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Models;
using AzureMcp.SignalR.Commands.CustomCertificate;
using AzureMcp.SignalR.Models;
using AzureMcp.SignalR.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.SignalR.UnitTests.Certificate;

public class CustomCertificateShowCommandTests
{
    private readonly ISignalRService _signalRService;
    private readonly CustomCertificateShowCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string _knownResourceGroup = "rg123";
    private readonly string _knownSignalRName = "signalr123";
    private readonly string _knownCertificateName = "cert123";

    public CustomCertificateShowCommandTests()
    {
        _signalRService = Substitute.For<ISignalRService>();
        var logger = Substitute.For<ILogger<CustomCertificateShowCommand>>();

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
        var expectedCertificate = new SignalRCustomCertificateModel
        {
            Name = _knownCertificateName,
            Id =
                $"/subscriptions/{_knownSubscriptionId}/resourceGroups/{_knownResourceGroup}/providers/Microsoft.SignalRService/signalR/{_knownSignalRName}/customCertificates/{_knownCertificateName}",
            Type = "Microsoft.SignalRService/signalR/customCertificates",
            ProvisioningState = "Succeeded",
            KeyVaultBaseUri = "https://keyvault.vault.azure.net/",
            KeyVaultSecretName = "certificate-secret",
            KeyVaultSecretVersion = "v1"
        };

        _signalRService.GetCustomCertificateAsync(
                _knownSubscriptionId,
                _knownResourceGroup,
                _knownSignalRName,
                _knownCertificateName,
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(expectedCertificate);

        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCertificateName}");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Serialize the entire ResponseResult to JSON and then deserialize to verify content
        var json = System.Text.Json.JsonSerializer.Serialize(response.Results);
        var resultData = System.Text.Json.JsonSerializer
            .Deserialize<CustomCertificateShowCommand.CertificateShowCommandResult>(
                json, SignalRJsonContext.Default.CertificateShowCommandResult);
        Assert.NotNull(resultData);
        Assert.Equal(expectedCertificate.Name, resultData.CustomCertificate.Name);
        Assert.Equal(expectedCertificate.Id, resultData.CustomCertificate.Id);
        Assert.Equal(expectedCertificate.ProvisioningState, resultData.CustomCertificate.ProvisioningState);
        Assert.Equal(expectedCertificate.KeyVaultBaseUri, resultData.CustomCertificate.KeyVaultBaseUri);
        Assert.Equal(expectedCertificate.KeyVaultSecretName, resultData.CustomCertificate.KeyVaultSecretName);
        Assert.Equal(expectedCertificate.KeyVaultSecretVersion, resultData.CustomCertificate.KeyVaultSecretVersion);
    }

    [Fact]
    public async Task ExecuteAsync_CertificateNotFound_Returns404()
    {
        // Arrange
        _signalRService.GetCustomCertificateAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns((SignalRCustomCertificateModel?)null);

        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCertificateName}");

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
        _signalRService.GetCustomCertificateAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(requestException);

        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCertificateName}");

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
        _signalRService.GetCustomCertificateAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(requestException);

        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCertificateName}");

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
        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName}");

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
        _signalRService.GetCustomCertificateAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<AuthMethod?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
            .ThrowsAsync(exception);

        var parseResult =
            _parser.Parse(
                $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name {_knownCertificateName}");

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
        var args =
            $"--subscription {_knownSubscriptionId} --resource-group {_knownResourceGroup} --signalr-name {_knownSignalRName} --name \"{certificateName}\"";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
    }
}
