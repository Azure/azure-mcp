// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Tests.Client.Helpers;
using ModelContextProtocol;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Types;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Client;

public class ClientToolTests : IClassFixture<McpClientFixture>
{
    private readonly IMcpClient _client;

    public ClientToolTests(McpClientFixture fixture)
    {
        _client = fixture.Client;
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_List_Tools()
    {
        var tools = await _client.ListToolsAsync();
        Assert.NotEmpty(tools);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Client_Should_Invoke_Tool_Successfully()
    {
        var result = await _client.CallToolAsync(
            "azmcp-subscription-list",
            new Dictionary<string, object?>
            { });

        var content = result.Content.FirstOrDefault(c => c.MimeType == "application/json")?.Text;

        Assert.False(string.IsNullOrWhiteSpace(content));

        var root = JsonSerializer.Deserialize<JsonElement>(content!);
        Assert.Equal(JsonValueKind.Object, root.ValueKind);

        Assert.True(root.TryGetProperty("subscriptions", out var subscriptionsArray));
        Assert.Equal(JsonValueKind.Array, subscriptionsArray.ValueKind);

        Assert.NotEmpty(subscriptionsArray.EnumerateArray());
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Client_Should_Handle_Invalid_Tools()
    {
        var result = await _client.CallToolAsync(
                "non_existent_tool",
                new Dictionary<string, object?>());

        var content = result.Content.FirstOrDefault(c => c.MimeType == "application/json")?.Text;
        Assert.True(string.IsNullOrWhiteSpace(content));
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Client_Should_Ping_Server_Successfully()
    {
        await _client.PingAsync();
        // If no exception is thrown, the ping was successful.
    }


    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Error_When_Resources_List_Not_Supported()
    {
        var ex = await Assert.ThrowsAsync<McpException>(() => _client.ListResourcesAsync());
        Assert.Contains("Request failed", ex.Message);
        Assert.Equal(-32601, ex.ErrorCode);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Error_When_Resources_Read_Not_Supported()
    {
        var ex = await Assert.ThrowsAsync<McpException>(() => _client.ReadResourceAsync("test://resource"));
        Assert.Contains("Request failed", ex.Message);
        Assert.Equal(-32601, ex.ErrorCode);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Error_When_Resources_Templates_List_Not_Supported()
    {
        var ex = await Assert.ThrowsAsync<McpException>(() => _client.ListResourceTemplatesAsync());
        Assert.Contains("Request failed", ex.Message);
        Assert.Equal(-32601, ex.ErrorCode);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Error_When_Resources_Subscribe_Not_Supported()
    {
        var ex = await Assert.ThrowsAsync<McpException>(() => _client.SubscribeToResourceAsync("test://resource"));
        Assert.Contains("Request failed", ex.Message);
        Assert.Equal(-32601, ex.ErrorCode);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Error_When_Resources_Unsubscribe_Not_Supported()
    {
        var ex = await Assert.ThrowsAsync<McpException>(() => _client.UnsubscribeFromResourceAsync("test://resource"));
        Assert.Contains("Request failed", ex.Message);
        Assert.Equal(-32601, ex.ErrorCode);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Not_Hang_On_Logging_SetLevel_Not_Supported()
    {
        await _client.SetLoggingLevel(LoggingLevel.Info);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Error_When_Prompts_List_Not_Supported()
    {
        var ex = await Assert.ThrowsAsync<McpException>(() => _client.ListPromptsAsync());
        Assert.Contains("Request failed", ex.Message);
        Assert.Equal(-32601, ex.ErrorCode);
    }

    [Fact]
    [Trait("Category", "Live")]
    public async Task Should_Error_When_Prompts_Get_Not_Supported()
    {
        var ex = await Assert.ThrowsAsync<McpException>(() => _client.GetPromptAsync("unsupported_prompt"));
        Assert.Contains("Request failed", ex.Message);
        Assert.Equal(-32601, ex.ErrorCode);
    }
}