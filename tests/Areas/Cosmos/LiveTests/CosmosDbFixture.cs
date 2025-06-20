// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Identity;
using AzureMcp.Tests.Client.Helpers;
using Microsoft.Azure.Cosmos;
using Xunit;

namespace AzureMcp.Tests.Areas.Cosmos.LiveTests;

public class CosmosDbFixture : IAsyncLifetime
{
    private CosmosClient? _client;

    public async ValueTask InitializeAsync()
    {
        var settingsFixture = new LiveTestSettingsFixture();
        await settingsFixture.InitializeAsync();

        _client = new CosmosClient(
            accountEndpoint: $"https://{settingsFixture.Settings.ResourceBaseName}.documents.azure.com:443/",
            tokenCredential: new DefaultAzureCredential()
        );
        Container container = _client.GetContainer("ToDoList", "Items");
        var item = new { id = Guid.NewGuid().ToString(), title = "Test Task", completed = false };
        await container.UpsertItemAsync(item, new PartitionKey(item.id));
    }

    public ValueTask DisposeAsync()
    {
        _client?.Dispose();
        return ValueTask.CompletedTask;
    }
}
