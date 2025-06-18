// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands;
using AzureMcp.Commands.Server.Tools;
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
            var services = new ServiceCollection().AddLogging().BuildServiceProvider();
            var commandFactoryLogger = services.GetRequiredService<ILogger<CommandFactory>>();
            _commandFactory = new CommandFactory(services, commandFactoryLogger);
            _mcpClientServiceLogger = services.GetRequiredService<ILogger<McpClientService>>();

            var testBinDir = AppContext.BaseDirectory;
            var exeName = OperatingSystem.IsWindows() ? "azmcp.exe" : "azmcp";
            _entryPoint = Path.Combine(testBinDir, exeName);
        }

        [Fact]
        public void Constructor_PopulatesProviders()
        {
            var service = new McpClientService(_commandFactory, _mcpClientServiceLogger);
            var metadata = service.ListProviderMetadata();
            Assert.NotEmpty(metadata);
            // Should match the number of subgroups in the root group
            Assert.Equal(_commandFactory.RootGroup.SubGroup.Count, metadata.Count);
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
    }
}
