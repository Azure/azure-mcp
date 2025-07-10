// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureMcp.Areas.Server.Commands.Discovery;
using AzureMcp.Areas.Server.Models;
using ModelContextProtocol.Client;
using Xunit;

namespace AzureMcp.Tests.Areas.Server.UnitTests.Commands.Discovery;

[Trait("Area", "Server")]
public class RegistryServerProviderTests
{
    [Fact]
    public void Constructor_InitializesCorrectly()
    {
        // Arrange
        string testId = "testProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = "Test Description"
        };

        // Act
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Assert
        Assert.NotNull(provider);
        Assert.IsType<RegistryServerProvider>(provider);
    }

    [Fact]
    public void CreateMetadata_ReturnsExpectedMetadata()
    {
        // Arrange
        string testId = "testProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = "Test Description"
        };
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Act
        var metadata = provider.CreateMetadata();

        // Assert
        Assert.NotNull(metadata);
        Assert.Equal(testId, metadata.Id);
        Assert.Equal(testId, metadata.Name);
        Assert.Equal(serverInfo.Description, metadata.Description);
    }

    [Fact]
    public void CreateMetadata_EmptyDescription_ReturnsEmptyString()
    {
        // Arrange
        string testId = "testProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = null
        };
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Act
        var metadata = provider.CreateMetadata();

        // Assert
        Assert.NotNull(metadata);
        Assert.Equal(testId, metadata.Id);
        Assert.Equal(testId, metadata.Name);
        Assert.Equal(string.Empty, metadata.Description);
    }

    [Fact]
    public async Task CreateClientAsync_WithUrl_CreatesSseClient()
    {
        // Arrange
        string testId = "sseProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = "Test SSE Provider",
            Url = "https://example.com/mcp"
        };
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Act & Assert - Should not throw, but we can't easily mock the actual client creation
        // due to SseClientTransport using sealed classes
        var exception = await Record.ExceptionAsync(() => provider.CreateClientAsync(new McpClientOptions()));

        // The specific exception may vary based on the implementation (HttpRequestException for 404, etc.)
        // but we should get an exception of some kind
        Assert.NotNull(exception);
        // Not an InvalidOperationException about missing URL or invalid transport
        Assert.IsNotType<InvalidOperationException>(exception);
    }

    [Fact]
    public async Task CreateClientAsync_WithStdioType_CreatesStdioClient()
    {
        // Arrange
        string testId = "stdioProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = "Test Stdio Provider",
            Type = "stdio",
            Command = "echo",
            Args = ["hello world"]
        };
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Act & Assert - Should not throw, but the subprocess won't actually start correctly in test
        // Without mocking, we can't easily verify the full client creation
        // This test is just to verify the code path for stdio client creation
        var exception = await Record.ExceptionAsync(() => provider.CreateClientAsync(new McpClientOptions()));

        // We expect some kind of exception during the subprocess startup, but not an InvalidOperationException
        // about missing command or invalid transport
        Assert.NotNull(exception);
        Assert.IsNotType<InvalidOperationException>(exception);
    }

    [Fact]
    public async Task CreateClientAsync_WithEnvVariables_MergesWithSystemEnvironment()
    {
        // Arrange
        string testId = "envProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = "Test Env Provider",
            Type = "stdio",
            Command = "echo",
            Args = ["hello world"],
            Env = new Dictionary<string, string>
                {
                    { "TEST_VAR", "test value" }
                }
        };
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Act & Assert - Should not throw, but the subprocess won't actually start correctly in test
        var exception = await Record.ExceptionAsync(() => provider.CreateClientAsync(new McpClientOptions()));

        // We expect some kind of exception during the subprocess startup, but not an InvalidOperationException
        // about missing command or invalid transport
        Assert.NotNull(exception);
        Assert.IsNotType<InvalidOperationException>(exception);
    }

    [Fact]
    public async Task CreateClientAsync_NoValidTransport_ThrowsInvalidOperationException()
    {
        // Arrange
        string testId = "invalidProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = "Invalid Provider - No Transport"
            // No Url or Type specified
        };
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => provider.CreateClientAsync(new McpClientOptions()));

        Assert.Contains($"Registry server '{testId}' does not have a valid url or type for transport.",
            exception.Message);
    }

    [Fact]
    public async Task CreateClientAsync_StdioWithoutCommand_ThrowsInvalidOperationException()
    {
        // Arrange
        string testId = "invalidStdioProvider";
        var serverInfo = new RegistryServerInfo
        {
            Description = "Invalid Stdio Provider - No Command",
            Type = "stdio"
            // No Command specified
        };
        var provider = new RegistryServerProvider(testId, serverInfo);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => provider.CreateClientAsync(new McpClientOptions()));

        Assert.Contains($"Registry server '{testId}' does not have a valid command for stdio transport.",
            exception.Message);
    }
}
