// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Client;
using Xunit;

namespace AzureMcp.Tests.Commands.Server.Tools
{
    public class McpCommandGroupTests
    {
        private readonly CommandFactory _commandFactory;
        public McpCommandGroupTests()
        {
            var services = new ServiceCollection().AddLogging().BuildServiceProvider();
            var logger = services.GetRequiredService<ILogger<CommandFactory>>();
            _commandFactory = new CommandFactory(services, logger);
        }

        [Fact]
        public void CreateMetadata_ReturnsExpectedMetadata()
        {
            // Arrange
            // For testGroup, CommandFactory does not have it by default, so fallback to direct instantiation
            var commandGroup = new CommandGroup("testGroup", "Test Description");
            var mcpCommandGroup = new McpCommandGroup(commandGroup);

            // Act
            var metadata = mcpCommandGroup.CreateMetadata();

            // Assert
            Assert.Equal("testGroup", metadata.Id);
            Assert.Equal("testGroup", metadata.Name);
            Assert.Equal("Test Description", metadata.Description);
        }

        [Fact]
        public async Task CreateClientAsync_ReturnsClientInstance()
        {
            // Arrange
            // Use CommandFactory to get the storage command group
            var storageGroup = _commandFactory.RootGroup.SubGroup.FirstOrDefault(g => g.Name == "storage");
            Assert.NotNull(storageGroup);

            // Use the built azmcp.exe as the entry point for testing (should be in the same directory as the test exe)
            var testBinDir = AppContext.BaseDirectory;
            var exeName = OperatingSystem.IsWindows() ? "azmcp.exe" : "azmcp";
            var entryPoint = Path.Combine(testBinDir, exeName);
            Assert.True(File.Exists(entryPoint), $"{exeName} not found at {entryPoint}");

            var mcpCommandGroup = new McpCommandGroup(storageGroup, entryPoint);
            var options = new McpClientOptions();

            // Act
            var client = await mcpCommandGroup.CreateClientAsync(options);

            // Assert
            Assert.NotNull(client);
        }
    }
}
