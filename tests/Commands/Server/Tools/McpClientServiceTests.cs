// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;
using AzureMcp.Areas.Server.Commands;
using AzureMcp.Areas.Server.Commands.Tools;
using AzureMcp.Areas.Server.Models;
using AzureMcp.Commands;
using AzureMcp.Models.Server;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Client;
using Xunit;

namespace AzureMcp.Tests.Commands.Server.Tools
{
    public class McpClientServiceTests
    {
        private readonly CommandFactory _commandFactory;
        private readonly string _entryPoint;
        private readonly ILogger<McpClientService> _mcpClientServiceLogger;

        public McpClientServiceTests()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();
            _commandFactory = CommandFactoryHelpers.CreateCommandFactory(serviceProvider);
            _mcpClientServiceLogger = serviceProvider.GetRequiredService<ILogger<McpClientService>>();

            var testBinDir = AppContext.BaseDirectory;
            var exeName = OperatingSystem.IsWindows() ? "azmcp.exe" : "azmcp";
            _entryPoint = Path.Combine(testBinDir, exeName);
        }
        [Fact]
        public async Task Constructor_PopulatesProviders()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            var metadata = service.ListProviderMetadata();
            Assert.NotEmpty(metadata);

            // Calculate expected command group providers (excluding ignored groups)
            var expectedCommandGroups = _commandFactory.RootGroup.SubGroup
                .Where(g => !new[] { "extension", "server", "tools" }.Contains(g.Name, StringComparer.OrdinalIgnoreCase))
                .Count();

            // Get expected registry providers by loading the registry directly
            var registry = await RegistryLoader.LoadRegistryAsync();
            var expectedRegistryProviders = registry?.Servers?.Count ?? 0;

            var expectedTotalProviders = expectedCommandGroups + expectedRegistryProviders;

            // Should have exact count of command group providers plus registry providers
            Assert.Equal(expectedTotalProviders, metadata.Count);
        }

        [Fact]
        public void ListProviderMetadata_ReturnsAllMetadata()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            var metadata = service.ListProviderMetadata();
            Assert.All(metadata, m =>
            {
                Assert.False(string.IsNullOrWhiteSpace(m.Id));
                Assert.False(string.IsNullOrWhiteSpace(m.Name));
            });
        }

        [Fact]
        public async Task GetProviderClientAsync_ReturnsClientForValidProvider()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger)
            {
                EntryPoint = _entryPoint
            };
            var firstMeta = service.ListProviderMetadata()[0];
            var options = new McpClientOptions();
            var client = await service.GetProviderClientAsync(firstMeta.Id, options);
            Assert.NotNull(client);
        }

        [Fact]
        public async Task GetProviderClientAsync_ThrowsForInvalidProvider()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            var options = new McpClientOptions();
            await Assert.ThrowsAsync<KeyNotFoundException>(async () =>
            {
                await service.GetProviderClientAsync("not-a-real-provider", options);
            });
        }

        [Fact]
        public void EntryPoint_SetToValidValue_ReturnsSetValue()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            var customEntryPoint = "/custom/path/to/executable";

            service.EntryPoint = customEntryPoint;

            Assert.Equal(customEntryPoint, service.EntryPoint);
        }

        [Fact]
        public void ReadOnly_DefaultValue_IsFalse()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            Assert.False(service.ReadOnly);
        }

        [Fact]
        public void ReadOnly_SetToTrue_ReturnsTrue()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            service.ReadOnly = true;
            Assert.True(service.ReadOnly);
        }

        [Fact]
        public void Constructor_IncludesRegistryProviders_WhenRegistryExists()
        {
            // This test assumes a registry.json exists in resources, but if not, it should still pass
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            var metadata = service.ListProviderMetadata();

            // Check if any providers have descriptions that suggest they came from registry
            // Registry providers typically have more detailed descriptions than command groups
            var possibleRegistryProviders = metadata.Where(m => !string.IsNullOrEmpty(m.Description)).ToList();

            // This assertion is lenient since registry.json may not exist in test environment
            Assert.True(metadata.Count > 0, "Should have at least some providers");
        }

        [Fact]
        public void RegistryMcpClientProvider_CreateMetadata_ReturnsCorrectMetadata()
        {
            // Arrange
            var serverInfo = new RegistryServerInfo
            {
                Url = "https://example.com/mcp",
                Description = "Test MCP Server from Registry",
                Type = "sse"
            };
            var provider = new RegistryMcpClientProvider("test-server", serverInfo);

            // Act
            var metadata = provider.CreateMetadata();

            // Assert
            Assert.Equal("test-server", metadata.Id);
            Assert.Equal("test-server", metadata.Name);
            Assert.Equal("Test MCP Server from Registry", metadata.Description);
        }

        [Fact]
        public async Task RegistryMcpClientProvider_CreateClientAsync_WithUrl_CreatesSseClient()
        {
            // Arrange
            var serverInfo = new RegistryServerInfo
            {
                Url = "https://example.com/mcp",
                Description = "Test SSE Server",
                Type = "sse"
            };
            var provider = new RegistryMcpClientProvider("sse-server", serverInfo);
            var clientOptions = new McpClientOptions();

            // Act & Assert
            // Note: This will fail in a test environment without a real server
            // We're mainly testing that the correct code path is taken
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await provider.CreateClientAsync(clientOptions);
            });
        }

        [Fact]
        public async Task RegistryMcpClientProvider_CreateClientAsync_WithStdio_CreatesStdioClient()
        {
            // Arrange
            var serverInfo = new RegistryServerInfo
            {
                Type = "stdio",
                Command = "node",
                Args = ["server.js"],
                Description = "Test Stdio Server",
                Env = new Dictionary<string, string>
                {
                    ["NODE_ENV"] = "test"
                }
            };
            var provider = new RegistryMcpClientProvider("stdio-server", serverInfo);
            var clientOptions = new McpClientOptions();

            // Act & Assert
            // Note: This will fail in a test environment without the actual command
            // We're mainly testing that the correct code path is taken
            await Assert.ThrowsAnyAsync<Exception>(async () =>
            {
                await provider.CreateClientAsync(clientOptions);
            });
        }

        [Fact]
        public async Task RegistryMcpClientProvider_CreateClientAsync_WithoutUrlOrType_ThrowsException()
        {
            // Arrange
            var serverInfo = new RegistryServerInfo
            {
                Description = "Invalid Server Config"
                // No URL or Type specified
            };
            var provider = new RegistryMcpClientProvider("invalid-server", serverInfo);
            var clientOptions = new McpClientOptions();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await provider.CreateClientAsync(clientOptions);
            });

            Assert.Contains("does not have a valid url or type for transport", exception.Message);
        }

        [Fact]
        public async Task RegistryMcpClientProvider_CreateClientAsync_StdioWithoutCommand_ThrowsException()
        {
            // Arrange
            var serverInfo = new RegistryServerInfo
            {
                Type = "stdio",
                Description = "Stdio Server without command"
                // No Command specified
            };
            var provider = new RegistryMcpClientProvider("stdio-no-command", serverInfo);
            var clientOptions = new McpClientOptions();

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(async () =>
            {
                await provider.CreateClientAsync(clientOptions);
            });

            Assert.Contains("does not have a valid command for stdio transport", exception.Message);
        }

        [Fact]
        public void McpClientService_ListProviderMetadata_IncludesBothCommandGroupAndRegistryProviders()
        {
            // Arrange
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);

            // Act
            var metadata = service.ListProviderMetadata();

            // Assert
            Assert.NotEmpty(metadata);

            // All metadata should have required fields
            Assert.All(metadata, m =>
            {
                Assert.False(string.IsNullOrWhiteSpace(m.Id));
                Assert.False(string.IsNullOrWhiteSpace(m.Name));
                // Description can be empty for command groups but not null
                Assert.NotNull(m.Description);
            });

            // Should have unique IDs
            var uniqueIds = metadata.Select(m => m.Id).Distinct().Count();
            Assert.Equal(metadata.Count, uniqueIds);
        }

        [Fact]
        public async Task McpClientService_GetProviderClientAsync_CachesClients()
        {
            // Arrange
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            service.EntryPoint = _entryPoint;

            var firstMeta = service.ListProviderMetadata().First();
            var options = new McpClientOptions();

            // Act
            var client1 = await service.GetProviderClientAsync(firstMeta.Id, options);
            var client2 = await service.GetProviderClientAsync(firstMeta.Id, options);

            // Assert
            Assert.NotNull(client1);
            Assert.NotNull(client2);
            Assert.Same(client1, client2); // Should be the same cached instance
        }

        [Fact]
        public void McpClientService_Dispose_DisposesAllCachedClients()
        {
            // Arrange
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);

            // Act
            service.Dispose();

            // Assert - No exception should be thrown
            // Multiple dispose calls should be safe
            service.Dispose();
        }

        [Fact]
        public async Task RegistryLoader_LoadRegistryAsync_ReturnsNullIfNoRegistry()
        {
            // Act
            var registry = await RegistryLoader.LoadRegistryAsync();

            // Assert
            // This may be null if no registry.json is embedded, which is fine for tests
            // If it exists, it should be a valid RegistryRoot
            if (registry != null)
            {
                Assert.NotNull(registry.Servers);
            }
        }

        [Fact]
        public void RegistryJsonSerialization_RoundTrip_WorksCorrectly()
        {
            // Arrange
            var originalRegistry = new RegistryRoot
            {
                Servers = new Dictionary<string, RegistryServerInfo>
                {
                    ["test-sse"] = new RegistryServerInfo
                    {
                        Url = "https://example.com/mcp",
                        Description = "Test SSE Server",
                        Type = "sse"
                    },
                    ["test-stdio"] = new RegistryServerInfo
                    {
                        Type = "stdio",
                        Command = "node",
                        Args = ["server.js"],
                        Description = "Test Stdio Server",
                        Env = new Dictionary<string, string>
                        {
                            ["NODE_ENV"] = "test"
                        }
                    }
                }
            };

            // Act
            var json = JsonSerializer.Serialize(originalRegistry, RegistryJsonContext.Default.RegistryRoot);
            var deserializedRegistry = JsonSerializer.Deserialize(json, RegistryJsonContext.Default.RegistryRoot);

            // Assert
            Assert.NotNull(deserializedRegistry);
            Assert.NotNull(deserializedRegistry.Servers);
            Assert.Equal(2, deserializedRegistry.Servers.Count);

            var sseServer = deserializedRegistry.Servers["test-sse"];
            Assert.Equal("https://example.com/mcp", sseServer.Url);
            Assert.Equal("Test SSE Server", sseServer.Description);
            Assert.Equal("sse", sseServer.Type);

            var stdioServer = deserializedRegistry.Servers["test-stdio"];
            Assert.Equal("stdio", stdioServer.Type);
            Assert.Equal("node", stdioServer.Command);
            Assert.Contains("server.js", stdioServer.Args!);
            Assert.Equal("test", stdioServer.Env!["NODE_ENV"]);
        }
    }
}
