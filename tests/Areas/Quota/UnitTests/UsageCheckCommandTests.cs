// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Areas.Quota.Commands;
using AzureMcp.Areas.Quota.Services;
using AzureMcp.Areas.Quota.Services.Util;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Quota.UnitTests;

[Trait("Area", "Quota")]
public sealed class UsageCheckCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IQuotaService _quotaService;
    private readonly ILogger<UsageCheckCommand> _logger;
    private readonly UsageCheckCommand _command;
    private readonly Parser _parser;

    public UsageCheckCommandTests()
    {
        _quotaService = Substitute.For<IQuotaService>();
        _logger = Substitute.For<ILogger<UsageCheckCommand>>();
        
        var services = new ServiceCollection();
        services.AddSingleton(_quotaService);
        _serviceProvider = services.BuildServiceProvider();
        
        _command = new UsageCheckCommand(_logger);
        _parser = new Parser(_command.GetCommand());
    }

    [Fact]
    public async Task Should_check_azure_quota_success()
    {
        // Arrange
        var subscriptionId = "test-subscription-id";
        var region = "eastus";
        var resourceTypes = "Microsoft.App, Microsoft.Storage/storageAccounts";
        
        var expectedQuotaInfo = new Dictionary<string, List<UsageInfo>>
        {
            {
                "Microsoft.App",
                new List<UsageInfo>
                {
                    new("ContainerApps", 100, 5, "Count"),
                    new("ContainerAppsEnvironments", 10, 2, "Count")
                }
            },
            {
                "Microsoft.Storage/storageAccounts",
                new List<UsageInfo>
                {
                    new("StorageAccounts", 250, 15, "Count"),
                    new("TotalStorageSize", 500, 150, "TB")
                }
            }
        };

        _quotaService.GetAzureQuotaAsync(
                Arg.Is<List<string>>(list => 
                    list.Count == 2 && 
                    list.Contains("Microsoft.App") && 
                    list.Contains("Microsoft.Storage/storageAccounts")),
                subscriptionId,
                region)
            .Returns(expectedQuotaInfo);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--region", region,
            "--resource-types", resourceTypes
        ]);

        var context = new CommandContext(_serviceProvider);

        // Act
        var result = await _command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);

        // Verify the service was called with the correct parameters
        await _quotaService.Received(1).GetAzureQuotaAsync(
            Arg.Is<List<string>>(list => 
                list.Count == 2 && 
                list.Contains("Microsoft.App") && 
                list.Contains("Microsoft.Storage/storageAccounts")),
            subscriptionId,
            region);

        // Verify the response structure
        var json = JsonSerializer.Serialize(result.Results);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        };

        var response = JsonSerializer.Deserialize<UsageCheckCommand.UsageCheckCommandResult>(json, options);
        Assert.NotNull(response);
        Assert.NotNull(response.UsageInfo);
        Assert.Equal(2, response.UsageInfo.Count);
        
        // Verify Microsoft.App quotas
        Assert.True(response.UsageInfo.ContainsKey("Microsoft.App"));
        var appQuotas = response.UsageInfo["Microsoft.App"];
        Assert.Equal(2, appQuotas.Count);
        Assert.Contains(appQuotas, q => q.Name == "ContainerApps" && q.Limit == 100 && q.Used == 5);
        Assert.Contains(appQuotas, q => q.Name == "ContainerAppsEnvironments" && q.Limit == 10 && q.Used == 2);
        
        // Verify Microsoft.Storage/storageAccounts quotas
        Assert.True(response.UsageInfo.ContainsKey("Microsoft.Storage/storageAccounts"));
        var storageQuotas = response.UsageInfo["Microsoft.Storage/storageAccounts"];
        Assert.Equal(2, storageQuotas.Count);
        Assert.Contains(storageQuotas, q => q.Name == "StorageAccounts" && q.Limit == 250 && q.Used == 15);
        Assert.Contains(storageQuotas, q => q.Name == "TotalStorageSize" && q.Limit == 500 && q.Used == 150);
    }

    [Fact]
    public async Task Should_ReturnError_empty_resource_types()
    {
        // Arrange
        var subscriptionId = "test-subscription-id";
        var region = "eastus";
        var resourceTypes = "";

        _quotaService.GetAzureQuotaAsync(
                Arg.Any<List<string>>(),
                subscriptionId,
                region)
            .Returns(new Dictionary<string, List<UsageInfo>>());

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--region", region,
            "--resource-types", resourceTypes
        ]);

        var context = new CommandContext(_serviceProvider);

        // Act
        var result = await _command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.Status);
    }

    [Fact]
    public async Task Should_handle_service_exception()
    {
        // Arrange
        var subscriptionId = "test-subscription-id";
        var region = "eastus";
        var resourceTypes = "Microsoft.App";
        var expectedException = new Exception("Service error occurred");

        _quotaService.GetAzureQuotaAsync(
                Arg.Any<List<string>>(),
                subscriptionId,
                region)
            .ThrowsAsync(expectedException);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--region", region,
            "--resource-types", resourceTypes
        ]);

        var context = new CommandContext(_serviceProvider);

        // Act
        var result = await _command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.Status);
        Assert.Contains("Service error occurred", result.Message);
    }

    [Fact]
    public async Task Should_parse_resource_types_with_spaces()
    {
        // Arrange
        var subscriptionId = "test-subscription-id";
        var region = "westus2";
        var resourceTypes = " Microsoft.Web/sites , Microsoft.Storage/storageAccounts , Microsoft.Compute/virtualMachines ";
        
        var expectedQuotaInfo = new Dictionary<string, List<UsageInfo>>
        {
            { "Microsoft.Web/sites", new List<UsageInfo> { new("WebApps", 10, 3, "Count") } },
            { "Microsoft.Storage/storageAccounts", new List<UsageInfo> { new("StorageAccounts", 250, 15, "Count") } },
            { "Microsoft.Compute/virtualMachines", new List<UsageInfo> { new("VMs", 50, 10, "Count") } }
        };

        _quotaService.GetAzureQuotaAsync(
                Arg.Is<List<string>>(list => 
                    list.Count == 3 && 
                    list.Contains("Microsoft.Web/sites") && 
                    list.Contains("Microsoft.Storage/storageAccounts") &&
                    list.Contains("Microsoft.Compute/virtualMachines")),
                subscriptionId,
                region)
            .Returns(expectedQuotaInfo);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--region", region,
            "--resource-types", resourceTypes
        ]);

        var context = new CommandContext(_serviceProvider);

        // Act
        var result = await _command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);

        // Verify the service was called with correctly parsed resource types
        await _quotaService.Received(1).GetAzureQuotaAsync(
            Arg.Is<List<string>>(list => 
                list.Count == 3 && 
                list.Contains("Microsoft.Web/sites") && 
                list.Contains("Microsoft.Storage/storageAccounts") &&
                list.Contains("Microsoft.Compute/virtualMachines")),
            subscriptionId,
            region);
    }

    [Fact]
    public async Task Should_return_null_results_when_no_quotas_found()
    {
        // Arrange
        var subscriptionId = "test-subscription-id";
        var region = "eastus";
        var resourceTypes = "Microsoft.App";

        _quotaService.GetAzureQuotaAsync(
                Arg.Any<List<string>>(),
                subscriptionId,
                region)
            .Returns(new Dictionary<string, List<UsageInfo>>());

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--region", region,
            "--resource-types", resourceTypes
        ]);

        var context = new CommandContext(_serviceProvider);

        // Act
        var result = await _command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Null(result.Results); // Should be null when no quotas are found
    }
}
