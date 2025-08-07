// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.AzureSignalR.Commands.Certificate;
using AzureMcp.AzureSignalR.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.AzureSignalR.LiveTests.Certificate;

[Collection("LiveTestCollection")]
public class CertificateShowCommandLiveTests(ITestOutputHelper output) : LiveTestBase(output)
{
    private readonly ILogger<CertificateShowCommand> _logger = CreateLogger<CertificateShowCommand>();

    [Fact(Skip = "Live test - requires Azure resources")]
    public async Task ExecuteAsync_WithRealAzureResources_ReturnsExpectedCertificate()
    {
        // Arrange
        var subscriptionId = GetRequiredEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        var resourceGroupName = GetRequiredEnvironmentVariable("AZURE_SIGNALR_RESOURCE_GROUP");
        var signalRName = GetRequiredEnvironmentVariable("AZURE_SIGNALR_SERVICE_NAME");
        var certificateName = GetRequiredEnvironmentVariable("AZURE_SIGNALR_CERTIFICATE_NAME");

        var serviceProvider = CreateServiceProvider(services =>
        {
            services.AddSingleton<IAzureSignalRService, AzureSignalRService>();
        });

        var command = new CertificateShowCommand(_logger);
        var context = new CommandContext(serviceProvider);
        var parser = new Parser(command.GetCommand());

        var parseResult = parser.Parse($"--subscription {subscriptionId} --resource-group {resourceGroupName} --signalr-name {signalRName} --certificate-name {certificateName}");

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var result = response.Results.GetData<CertificateShowCommand.CertificateShowCommandResult>();
        Assert.NotNull(result);
        Assert.Equal(certificateName, result.Certificate.Name);
        Assert.NotEmpty(result.Certificate.Id);
        Assert.NotEmpty(result.Certificate.Type);
        Assert.NotEmpty(result.Certificate.ProvisioningState);

        // Log the actual certificate details for verification
        Output.WriteLine($"Certificate Name: {result.Certificate.Name}");
        Output.WriteLine($"Certificate ID: {result.Certificate.Id}");
        Output.WriteLine($"Provisioning State: {result.Certificate.ProvisioningState}");
        Output.WriteLine($"Key Vault Base URI: {result.Certificate.KeyVaultBaseUri}");
        Output.WriteLine($"Key Vault Secret Name: {result.Certificate.KeyVaultSecretName}");
    }

    [Fact(Skip = "Live test - requires Azure resources")]
    public async Task ExecuteAsync_WithNonExistentCertificate_Returns404()
    {
        // Arrange
        var subscriptionId = GetRequiredEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        var resourceGroupName = GetRequiredEnvironmentVariable("AZURE_SIGNALR_RESOURCE_GROUP");
        var signalRName = GetRequiredEnvironmentVariable("AZURE_SIGNALR_SERVICE_NAME");
        var nonExistentCertificateName = "non-existent-certificate-" + Guid.NewGuid().ToString("N")[..8];

        var serviceProvider = CreateServiceProvider(services =>
        {
            services.AddSingleton<IAzureSignalRService, AzureSignalRService>();
        });

        var command = new CertificateShowCommand(_logger);
        var context = new CommandContext(serviceProvider);
        var parser = new Parser(command.GetCommand());

        var parseResult = parser.Parse($"--subscription {subscriptionId} --resource-group {resourceGroupName} --signalr-name {signalRName} --certificate-name {nonExistentCertificateName}");

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(404, response.Status);
        Assert.Contains("not found", response.Message, StringComparison.OrdinalIgnoreCase);
        Assert.Null(response.Results);
    }

    [Fact(Skip = "Live test - requires Azure resources")]
    public async Task ExecuteAsync_WithNonExistentSignalRService_ReturnsError()
    {
        // Arrange
        var subscriptionId = GetRequiredEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        var resourceGroupName = GetRequiredEnvironmentVariable("AZURE_SIGNALR_RESOURCE_GROUP");
        var nonExistentSignalRName = "non-existent-signalr-" + Guid.NewGuid().ToString("N")[..8];
        var certificateName = "any-certificate";

        var serviceProvider = CreateServiceProvider(services =>
        {
            services.AddSingleton<IAzureSignalRService, AzureSignalRService>();
        });

        var command = new CertificateShowCommand(_logger);
        var context = new CommandContext(serviceProvider);
        var parser = new Parser(command.GetCommand());

        var parseResult = parser.Parse($"--subscription {subscriptionId} --resource-group {resourceGroupName} --signalr-name {nonExistentSignalRName} --certificate-name {certificateName}");

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
        Assert.NotNull(response.Message);
    }
}
