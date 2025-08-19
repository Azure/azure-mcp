// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.FunctionApp.LiveTests;

public sealed class FunctionAppCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{

    [Fact]
    public async Task Should_list_function_apps_by_subscription()
    {
        var result = await CallToolAsync(
            "azmcp_functionapp_list",
            new()
            {
                { "subscription", Settings.SubscriptionId }
            });

        var functionApps = result.AssertProperty("results");
        Assert.Equal(JsonValueKind.Array, functionApps.ValueKind);

        Assert.True(functionApps.GetArrayLength() >= 2, "Expected at least two Function Apps in the test environment");

        foreach (var functionApp in functionApps.EnumerateArray())
        {
            Assert.Equal(JsonValueKind.Object, functionApp.ValueKind);

            Assert.True(functionApp.TryGetProperty("name", out var nameProperty));
            Assert.False(string.IsNullOrEmpty(nameProperty.GetString()));

            Assert.True(functionApp.TryGetProperty("resourceGroupName", out var rgProperty));
            Assert.False(string.IsNullOrEmpty(rgProperty.GetString()));

            Assert.True(functionApp.TryGetProperty("appServicePlanName", out var aspProperty));
            Assert.False(string.IsNullOrEmpty(aspProperty.GetString()));

            if (functionApp.TryGetProperty("location", out var locationProperty))
            {
                Assert.False(string.IsNullOrEmpty(locationProperty.GetString()));
            }

            if (functionApp.TryGetProperty("status", out var statusProperty))
            {
                Assert.False(string.IsNullOrEmpty(statusProperty.GetString()));
            }
        }
    }

    [Fact]
    public async Task Should_handle_empty_subscription_gracefully_function_app_list()
    {
        var result = await CallToolAsync(
            "azmcp_functionapp_list",
            new()
            {
                { "subscription", "" }
            });

        Assert.False(result.HasValue);
    }

    [Fact]
    public async Task Should_handle_invalid_subscription_gracefully_function_app_list()
    {
        var result = await CallToolAsync(
            "azmcp_functionapp_list",
            new()
            {
                { "subscription", "invalid-subscription" }
            });

        Assert.True(result.HasValue);
        var errorDetails = result.Value;
        Assert.True(errorDetails.TryGetProperty("message", out _));
        Assert.True(errorDetails.TryGetProperty("type", out var typeProperty));
        Assert.Equal("Exception", typeProperty.GetString());
    }

    [Fact]
    public async Task Should_validate_required_subscription_parameter_function_app_list()
    {
        var result = await CallToolAsync(
            "azmcp_functionapp_list",
            new Dictionary<string, object?>());

        Assert.False(result.HasValue);
    }

    [Theory]
    [InlineData("consumption", null, "python", null, "linux", null)]
    [InlineData("flex", null, "dotnet-isolated", null, "linux", null)]
    [InlineData("premium", null, "powershell", "windows", "windows", null)]
    [InlineData("containerapp", null, "dotnet-isolated", null, "linux", null)]
    [InlineData("appservice", "B2", "node", "windows", "windows", "22.0.0")]
    [InlineData("appservice", "P0V3", "java", "linux", "linux", "21.0")]
    public async Task Should_create_function_app(
        string planType,
        string? planSku,
        string runtime,
        string? operatingSystem,
        string expectedOperatingSystem,
        string? runtimeVersion)
    {
        var uniqueFunctionAppName = $"mcp-test-{planType}-{DateTime.UtcNow:MMddHHmmss}";

        var result = await CallToolAsync(
            "azmcp_functionapp_create",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "function-app", uniqueFunctionAppName },
                { "location", "westus" },
                { "plan-type", planType },
                { "plan-sku", planSku },
                { "runtime", runtime },
                { "os", operatingSystem },
                { "runtime-version", runtimeVersion }
            });

        Assert.NotNull(result);
        var functionAppWrapper = result!.Value;
        Assert.True(functionAppWrapper.TryGetProperty("functionApp", out var functionApp));
        Assert.True(functionApp.TryGetProperty("name", out var nameProp));
        Assert.Equal(uniqueFunctionAppName, nameProp.GetString());
        Assert.True(functionApp.TryGetProperty("resourceGroupName", out var rgProp));
        Assert.Equal(Settings.ResourceGroupName, rgProp.GetString());
        Assert.True(functionApp.TryGetProperty("appServicePlanName", out var planProp));
        Assert.False(string.IsNullOrWhiteSpace(planProp.GetString()));

        if (functionApp.TryGetProperty("operatingSystem", out var osProp))
        {
            Assert.Equal(expectedOperatingSystem, osProp.GetString());
        }
    }

    [Theory]
    [InlineData("python", "windows")]
    [InlineData("dotnet", "invalid-os")]
    public async Task Should_fail_when_invalid_or_conflicting_os(string runtime, string os)
    {
        var uniqueFunctionAppName = $"test-functionapp-invalid-{runtime}-{DateTime.UtcNow:MMddHHmmss}";
        var result = await CallToolAsync(
            "azmcp_functionapp_create",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "function-app", uniqueFunctionAppName },
                { "location", "westus" },
                { "runtime", runtime },
                { "os", os }
            });

        if (result.HasValue)
        {
            Assert.True(result.Value.TryGetProperty("message", out _));
        }
        else
        {
            Assert.Null(result);
        }
    }

}
