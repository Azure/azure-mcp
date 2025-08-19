// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Client;

namespace AzureMcp.Tests.Client.Helpers;

public class LiveTestFixture : LiveTestSettingsFixture
{
    public IMcpClient Client { get; private set; } = default!;

    private string[]? _customArguments;
    private ILoggerFactory? _loggerFactory;

    /// <summary>
    /// Sets custom arguments for the MCP server. Call this before InitializeAsync().
    /// </summary>
    /// <param name="arguments">Custom arguments to pass to the server (e.g., ["server", "start", "--mode", "single"])</param>
    public void SetArguments(params string[] arguments)
    {
        _customArguments = arguments;
    }

    /// <summary>
    /// Sets the logger factory for capturing MCP server logs. Call this before InitializeAsync().
    /// </summary>
    /// <param name="loggerFactory">Logger factory that will capture server stderr and route to test output</param>
    public void SetLoggerFactory(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();

        string testAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string executablePath = OperatingSystem.IsWindows() ? Path.Combine(testAssemblyPath, "azmcp.exe") : Path.Combine(testAssemblyPath, "azmcp");

        // Use custom arguments if provided, otherwise default to ["server", "start"]
        var arguments = _customArguments ?? ["server", "start", "--mode", "all"];

        StdioClientTransportOptions transportOptions = new()
        {
            Name = "Test Server",
            Command = executablePath,
            Arguments = arguments
        };

        if (!string.IsNullOrEmpty(Settings.TestPackage))
        {
            Environment.CurrentDirectory = Settings.SettingsDirectory;
            transportOptions.Command = "npx";
            transportOptions.Arguments = ["-y", Settings.TestPackage, .. arguments];
        }

        var clientTransport = new StdioClientTransport(transportOptions);

        Client = await McpClientFactory.CreateAsync(clientTransport);
    }
}
