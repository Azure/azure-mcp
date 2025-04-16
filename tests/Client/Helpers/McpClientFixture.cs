// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;
using System.Reflection;
using Xunit;

namespace AzureMcp.Tests.Client.Helpers;

public class McpClientFixture : IAsyncLifetime
{
    public IMcpClient Client { get; private set; } = default!;

    public async Task InitializeAsync()
    {
        var testAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var executablePath = OperatingSystem.IsWindows() ? Path.Combine(testAssemblyPath!, "azmcp.exe") : Path.Combine(testAssemblyPath!, "azmcp");

        if (!string.IsNullOrWhiteSpace(executablePath))
        {
            var clientTransport = new StdioClientTransport(new StdioClientTransportOptions
            {
                Name = "Test Server",
                Command = executablePath,
                Arguments = new[] { "server", "start" },
            });

            Client = await McpClientFactory.CreateAsync(clientTransport);
        }
    }

    public async Task DisposeAsync()
    {
        if (Client is IAsyncDisposable disposable)
            await disposable.DisposeAsync();
    }
}