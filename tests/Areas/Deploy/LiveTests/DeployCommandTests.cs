// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using System.Text.Json.Nodes;
using AzureMcp.Areas.Deploy.Models;
using AzureMcp.Areas.Deploy.Services;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using ModelContextProtocol.Client;
using Xunit;

namespace AzureMcp.Tests.Areas.Deploy.LiveTests;

[Trait("Area", "Deploy")]
public class DeployCommandTests : CommandTestsBase,
    IClassFixture<LiveTestFixture>
{
    private readonly string _subscriptionId;

    public DeployCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output) : base(liveTestFixture, output)
    {
        _subscriptionId = Settings.SubscriptionId;
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_plan()
    {
        // act
        var result = await CallToolMessageAsync(
            "azmcp-deploy-plan-get",
            new()
            {
                { "workspace-folder", "C:/" },
                { "project-name", "django" }
            });
        // assert
        Assert.StartsWith(result, "Title:");
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_check_azure_quota()
    {
        JsonElement? result = await CallToolAsync(
            "azmcp-deploy-quota-check",
            new() {
                { "subscription", _subscriptionId },
                { "region", "eastus" },
                { "resource-types", "Microsoft.App, Microsoft.Storage/storageAccounts" }
            });
        // assert
        var quotas = result.AssertProperty("quotaInfo");
        Assert.Equal(JsonValueKind.Object, quotas.ValueKind);
        var appQuotas = quotas.AssertProperty("Microsoft.App");
        Assert.Equal(JsonValueKind.Array, appQuotas.ValueKind);
        Assert.NotEmpty(appQuotas.EnumerateArray());
        var storageQuotas = quotas.AssertProperty("Microsoft.Storage/storageAccounts");
        Assert.Equal(JsonValueKind.Array, storageQuotas.ValueKind);
        Assert.NotEmpty(storageQuotas.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_infrastructure_code_rules()
    {
        // arrange
        var parameters = new
        {
            deploymentTool = "azd",
            iacType = "bicep",
            resourceTypes = new[] { "appservice", "azurestorage" }
        };

        // act
        var result = await CallToolMessageAsync(
            "azmcp-deploy-infra-code-rules-get",
            new()
            {
                { "deployment-tool", "azd" },
                { "iac-type", "bicep" },
                { "resource-types", "appservice, azurestorage" }
            });

        Assert.Contains("Deployment Tool: azd", result ?? String.Empty, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_infrastructure_rules_for_terraform()
    {
        // act
        var result = await CallToolMessageAsync(
            "azmcp-deploy-infra-code-rules-get",
            new()
            {
                { "deployment-tool", "azd" },
                { "iac-type", "terraform" },
                { "resource-types", "containerapp, azurecosmosdb" }
            });

        // assert
        Assert.Contains("IaC Type: terraform. IaC Type rules:", result ?? String.Empty, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_generate_pipeline()
    {
        // act
        var result = await CallToolMessageAsync(
            "azmcp-deploy-pipeline-generate",
            new()
            {
                { "subscription", _subscriptionId },
                { "use-azd-pipeline-config", true }
            });

        // assert
        Assert.Contains("Run \"azd pipeline config\" to help the user create a deployment pipeline.", result);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_generate_pipeline_with_github_details()
    {
        // act
        var result = await CallToolMessageAsync(
            "azmcp-deploy-pipeline-generate",
            new()
            {
                { "subscription", _subscriptionId },
                { "use-azd-pipeline-config", false },
                { "organization-name", "test-org" },
                { "repository-name", "test-repo" },
                { "github-environment-name", "production" }
            });

        // assert
        Assert.StartsWith("Help the user to set up a CI/CD pipeline", result ?? String.Empty);
    }


    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_get_azd_app_logs()
    {
        // act
        var result = await CallToolMessageAsync(
            "azmcp-deploy-azd-app-log-get",
            new()
            {
                { "subscription", _subscriptionId },
                { "workspace-folder", "C:/Users/" },
                { "azd-env-name", "dotnet-demo" },
                { "limit", 10 }
            });

        // assert
        Assert.StartsWith("App logs retrieved:", result);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_check_azure_regions()
    {
        // act
        var result = await CallToolAsync(
            "azmcp-deploy-region-check",
            new()
            {
                { "subscription", _subscriptionId },
                { "resource-types", "Microsoft.Web/sites, Microsoft.Storage/storageAccounts" },
            });

        // assert
        var availableRegions = result.AssertProperty("availableRegions");
        Assert.Equal(JsonValueKind.Array, availableRegions.ValueKind);
        Assert.NotEmpty(availableRegions.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_check_regions_with_cognitive_services()
    {
        // act
        var result = await CallToolAsync(
            "azmcp-deploy-region-check",
            new()
            {
                { "subscription", _subscriptionId },
                { "resource-types", "Microsoft.CognitiveServices/accounts" },
                { "cognitive-service-model-name", "gpt-4o" },
                { "cognitive-service-deployment-sku-name", "Standard" }
            });

        // assert
        var availableRegions = result.AssertProperty("availableRegions");
        Assert.Equal(JsonValueKind.Array, availableRegions.ValueKind);
        Assert.NotEmpty(availableRegions.EnumerateArray());
    }

    private async Task<string?> CallToolMessageAsync(string command, Dictionary<string, object?> parameters)
    {
        // Output will be streamed, so if we're not in debug mode, hold the debug output for logging in the failure case
        Action<string> writeOutput = Settings.DebugOutput
            ? s => Output.WriteLine(s)
            : s => FailureOutput.AppendLine(s);

        writeOutput($"request: {JsonSerializer.Serialize(new { command, parameters })}");

        var result = await Client.CallToolAsync(command, parameters);

        var content = McpTestUtilities.GetFirstText(result.Content);
        if (string.IsNullOrWhiteSpace(content))
        {
            Output.WriteLine($"response: {JsonSerializer.Serialize(result)}");
            throw new Exception("No JSON content found in the response.");
        }

        var root = JsonSerializer.Deserialize<JsonElement>(content!);
        if (root.ValueKind != JsonValueKind.Object)
        {
            Output.WriteLine($"response: {JsonSerializer.Serialize(result)}");
            throw new Exception("Invalid JSON response.");
        }

        // Remove the `args` property and log the content
        var trimmed = root.Deserialize<JsonObject>()!;
        trimmed.Remove("args");
        writeOutput($"response content: {trimmed.ToJsonString(new JsonSerializerOptions { WriteIndented = true })}");

        return root.TryGetProperty("message", out var property) ? property.GetString() : null;
    }

}
