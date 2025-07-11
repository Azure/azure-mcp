// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Xunit;

namespace AzureMcp.Tests.Areas.Foundry.LiveTests;

[Trait("Area", "Foundry")]
public class FoundryCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_foundry_models()
    {
        var result = await CallToolAsync(
            "azmcp-foundry-models-list",
            new()
            {
                { "search-for-free-playground", "true" }
            });

        var modelsArray = result.AssertProperty("models");
        Assert.Equal(JsonValueKind.Array, modelsArray.ValueKind);
        Assert.NotEmpty(modelsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_list_foundry_model_deployments()
    {
        var projectName = $"{Settings.ResourceBaseName}-ai-projects";
        var accounts = Settings.ResourceBaseName;
        var result = await CallToolAsync(
            "azmcp-foundry-models-deployments-list",
            new()
            {
                { "endpoint", $"https://{accounts}.services.ai.azure.com/api/projects/{projectName}" },
                { "tenant", Settings.TenantId }
            });

        var deploymentsArray = result.AssertProperty("deployments");
        Assert.Equal(JsonValueKind.Array, deploymentsArray.ValueKind);
        Assert.NotEmpty(deploymentsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_deploy_foundry_model()
    {
        var deploymentName = $"test-deploy-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        var result = await CallToolAsync(
            "azmcp-foundry-models-deploy",
            new()
            {
                { "deployment-name", deploymentName },
                { "model-name", "gpt-4o" },
                { "model-format", "OpenAI"},
                { "azure-ai-services-name", Settings.ResourceBaseName },
                { "resource-group", Settings.ResourceGroupName },
                { "subscription", Settings.SubscriptionId },
            });

        var deploymentResource = result.AssertProperty("deploymentData");
        Assert.Equal(JsonValueKind.Object, deploymentResource.ValueKind);
        Assert.NotEmpty(deploymentResource.EnumerateObject());
    }
}
