// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.AzureSignalR.Commands.SignalR;
using AzureMcp.AzureSignalR.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace AzureMcp.AzureSignalR.LiveTests.SignalR;

[Collection("LiveTestCollection")]
public class SignalRServiceListCommandLiveTests(ITestOutputHelper output) : LiveTestBase(output)
{
    private readonly ILogger<SignalRServiceListCommand> _logger = CreateLogger<SignalRServiceListCommand>();

    [Fact(Skip = "Live test - requires Azure resources")]
    public async Task ExecuteAsync_WithRealAzureSubscription_ReturnsSignalRServices()
    {
        // Arrange
        var subscriptionId = GetRequiredEnvironmentVariable("AZURE_SUBSCRIPTION_ID");

        var serviceProvider = CreateServiceProvider(services =>
        {
            services.AddSingleton<IAzureSignalRService, AzureSignalRService>();
        });

        var command = new SignalRServiceListCommand(_logger);
        var context = new CommandContext(serviceProvider);
        var parser = new Parser(command.GetCommand());

        var parseResult = parser.Parse($"--subscription {subscriptionId}");

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);

        if (response.Results != null)
        {
            var result = response.Results.GetData<SignalRServiceListCommand.SignalRServiceListCommandResult>();
            Assert.NotNull(result);

            // Log the found services for verification
            var services = result.SignalRServices.ToList();
            Output.WriteLine($"Found {services.Count} SignalR service(s):");

            foreach (var service in services)
            {
                Output.WriteLine($"  - Name: {service.Name}");
                Output.WriteLine($"    Resource Group: {service.ResourceGroupName}");
                Output.WriteLine($"    Location: {service.Location}");
                Output.WriteLine($"    SKU: {service.SkuName} ({service.SkuTier})");
                Output.WriteLine($"    State: {service.ProvisioningState}");
                Output.WriteLine($"    Host: {service.HostName}");
                Output.WriteLine();
            }

            // Verify basic properties
            foreach (var service in services)
            {
                Assert.NotEmpty(service.Name);
                Assert.NotEmpty(service.ResourceGroupName);
                Assert.NotEmpty(service.Location);
                Assert.NotEmpty(service.ProvisioningState);
            }
        }
        else
        {
            Output.WriteLine("No SignalR services found in subscription");
        }
    }

    [Fact(Skip = "Live test - requires Azure resources")]
    public async Task ExecuteAsync_WithInvalidSubscription_ReturnsError()
    {
        // Arrange
        var invalidSubscriptionId = "00000000-0000-0000-0000-000000000000";

        var serviceProvider = CreateServiceProvider(services =>
        {
            services.AddSingleton<IAzureSignalRService, AzureSignalRService>();
        });

        var command = new SignalRServiceListCommand(_logger);
        var context = new CommandContext(serviceProvider);
        var parser = new Parser(command.GetCommand());

        var parseResult = parser.Parse($"--subscription {invalidSubscriptionId}");

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
        Assert.NotNull(response.Message);

        Output.WriteLine($"Expected error received: {response.Message}");
    }
}
