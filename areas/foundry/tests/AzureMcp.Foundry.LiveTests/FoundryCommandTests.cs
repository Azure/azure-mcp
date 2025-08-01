// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Tests;
using AzureMcp.Tests.Client;
using AzureMcp.Tests.Client.Helpers;
using Azure.AI.Agents.Persistent;
using Xunit;
using AzureMcp.Core.Services.Azure.Authentication;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure;

namespace AzureMcp.Foundry.LiveTests;

public class FoundryCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>
{
    // [Fact]
    // public async Task Should_list_foundry_models()
    // {
    //     var result = await CallToolAsync(
    //         "azmcp_foundry_models_list",
    //         new()
    //         {
    //             { "search-for-free-playground", "true" }
    //         });

    //     var modelsArray = result.AssertProperty("models");
    //     Assert.Equal(JsonValueKind.Array, modelsArray.ValueKind);
    //     Assert.NotEmpty(modelsArray.EnumerateArray());
    // }

    // [Fact]
    // public async Task Should_list_foundry_model_deployments()
    // {
    //     var projectName = $"{Settings.ResourceBaseName}-ai-projects";
    //     var accounts = Settings.ResourceBaseName;
    //     var result = await CallToolAsync(
    //         "azmcp_foundry_models_deployments_list",
    //         new()
    //         {
    //             { "endpoint", $"https://{accounts}.services.ai.azure.com/api/projects/{projectName}" },
    //             { "tenant", Settings.TenantId }
    //         });

    //     var deploymentsArray = result.AssertProperty("deployments");
    //     Assert.Equal(JsonValueKind.Array, deploymentsArray.ValueKind);
    //     Assert.NotEmpty(deploymentsArray.EnumerateArray());
    // }

    // [Fact]
    // public async Task Should_deploy_foundry_model()
    // {
    //     var deploymentName = $"test-deploy-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
    //     var result = await CallToolAsync(
    //         "azmcp_foundry_models_deploy",
    //         new()
    //         {
    //             { "deployment-name", deploymentName },
    //             { "model-name", "gpt-4o" },
    //             { "model-format", "OpenAI"},
    //             { "azure-ai-services-name", Settings.ResourceBaseName },
    //             { "resource-group", Settings.ResourceGroupName },
    //             { "subscription", Settings.SubscriptionId },
    //         });

    //     var deploymentResource = result.AssertProperty("deploymentData");
    //     Assert.Equal(JsonValueKind.Object, deploymentResource.ValueKind);
    //     Assert.NotEmpty(deploymentResource.EnumerateObject());
    // }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_connect_agent()
    {
        var projectName = $"{Settings.ResourceBaseName}-ai-projects";
        var accounts = Settings.ResourceBaseName;
        var agentName = $"test-agent-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
        var query = "What is the weather today?";
        var endpoint = $"https://{accounts}.services.ai.azure.com/api/projects/{projectName}";

        var agentId = await CreateAgent(agentName, endpoint, "gpt-4o");

        var result = await CallToolAsync(
            "azmcp_foundry_agents_connect",
            new()
            {
                { "agent-id", agentId },
                { "query", query },
                { "endpoint", endpoint }
            });
        result.AssertProperty("query");
        result.AssertProperty("text_query");
        result.AssertProperty("text_response");
        result.AssertProperty("tool_definitions");
        result.AssertProperty("thread_id");
        result.AssertProperty("run_id");
    }

    // [Fact]
    // [Trait("Category", "Live")]
    // public async Task Should_query_and_evaluate_agent()
    // {
    //     // to be filled in

    //     var deploymentName = $"test-deploy-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
    //     var result = await CallToolAsync(
    //         "azmcp_foundry_agents_query_and_evaluate",
    //         new()
    //         {
    //             { "deployment-name", deploymentName },
    //             { "model-name", "gpt-4o" },
    //             { "model-format", "OpenAI"},
    //             { "azure-ai-services-name", Settings.ResourceBaseName },
    //             { "resource-group", Settings.ResourceGroupName },
    //             { "subscription", Settings.SubscriptionId },
    //         });

    //     var deploymentResource = result.AssertProperty("deploymentData");
    //     Assert.Equal(JsonValueKind.Object, deploymentResource.ValueKind);
    //     Assert.NotEmpty(deploymentResource.EnumerateObject());
    // }

    // [Fact]
    // [Trait("Category", "Live")]
    // public async Task Should_evaluate_agent()
    // {
    //     // to be filled in
    //     var deploymentName = $"test-deploy-{DateTimeOffset.UtcNow.ToUnixTimeSeconds()}";
    //     var result = await CallToolAsync(
    //         "azmcp_foundry_agents_evaluate",
    //         new()
    //         {
    //             { "deployment-name", deploymentName },
    //             { "model-name", "gpt-4o" },
    //             { "model-format", "OpenAI"},
    //             { "azure-ai-services-name", Settings.ResourceBaseName },
    //             { "resource-group", Settings.ResourceGroupName },
    //             { "subscription", Settings.SubscriptionId },
    //         });

    //     var deploymentResource = result.AssertProperty("deploymentData");
    //     Assert.Equal(JsonValueKind.Object, deploymentResource.ValueKind);
    //     Assert.NotEmpty(deploymentResource.EnumerateObject());
    // }

    private async Task<string> CreateAgent(string agentName, string projectEndpoint, string deploymentName)
    {
        var client = new PersistentAgentsClient(
            projectEndpoint,
            new CustomChainedCredential());

        var bingConnectionId = $"/subscriptions/{Settings.SubscriptionId}/resourceGroups/{Settings.ResourceGroupName}/providers/Microsoft.CognitiveServices/accounts/{Settings.ResourceBaseName}/projects/{Settings.ResourceBaseName}-ai-projects/connections/{Settings.ResourceBaseName}-bing-connection";

        var bingGroundingToolParameters = new BingGroundingSearchToolParameters(
            [new BingGroundingSearchConfiguration(bingConnectionId)]
        );

        PersistentAgent agent = await client.Administration.CreateAgentAsync(
            model: deploymentName,
            name: agentName,
            instructions: "You politely help with general knowledge questions. Use the bing search tool to help ground your responses.",
            tools: [new BingGroundingToolDefinition(bingGroundingToolParameters)]);
        return agent.Id;
    }
}
