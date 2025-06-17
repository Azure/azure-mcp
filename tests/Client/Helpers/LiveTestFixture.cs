// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using ModelContextProtocol.Client;

namespace AzureMcp.Tests.Client.Helpers;

public class LiveTestFixture : LiveTestSettingsFixture
{
    public IMcpClient Client { get; private set; } = default!;

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();

        string testAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string executablePath = OperatingSystem.IsWindows() ? Path.Combine(testAssemblyPath, "azmcp.exe") : Path.Combine(testAssemblyPath, "azmcp");

        StdioClientTransportOptions transportOptions = new()
        {
            Name = "Test Server",
            Command = executablePath,
            Arguments = ["server", "start"]
        };

        if (!string.IsNullOrEmpty(Settings.TestPackage))
        {
            Environment.CurrentDirectory = Settings.SettingsDirectory;
            transportOptions.Command = "npx";
#if DEBUG
            transportOptions.Arguments = ["-y", Settings.TestPackage, "server", "start", "--debug"];
#else
            transportOptions.Arguments = ["-y", Settings.TestPackage, "server", "start"];
#endif
        }

        var clientTransport = new StdioClientTransport(transportOptions);
#if DEBUG
        var clientOptions = new McpClientOptions
        {
            InitializationTimeout = TimeSpan.FromMinutes(2)
        };
        Client = await McpClientFactory.CreateAsync(clientTransport, clientOptions);
#else
        Client = await McpClientFactory.CreateAsync(clientTransport);
#endif
    }
}
