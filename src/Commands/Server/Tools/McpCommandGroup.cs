// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Commands;
using AzureMcp.Commands.Server.Tools;
using ModelContextProtocol.Client;

/// <summary>
/// Represents a command group that provides metadata and MCP client creation.
/// </summary>
public sealed class McpCommandGroup(CommandGroup commandGroup, string? entryPoint = null) : IMcpClientProvider
{
    private readonly CommandGroup _commandGroup = commandGroup;
    private readonly string _entryPoint = string.IsNullOrWhiteSpace(entryPoint)
        ? System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName
        ?? throw new InvalidOperationException("Could not determine the entry point executable for the current process.")
        : entryPoint;

    /// <summary>
    /// Creates an MCP client from a command group.
    /// </summary>
    public async Task<IMcpClient> CreateClientAsync(McpClientOptions clientOptions)
    {
        var arguments = new[] { "server", "start", "--service", _commandGroup.Name };
        var transportOptions = new StdioClientTransportOptions
        {
            Name = _commandGroup.Name,
            Command = _entryPoint,
            Arguments = arguments,
        };

        var clientTransport = new StdioClientTransport(transportOptions);
        return await McpClientFactory.CreateAsync(clientTransport, clientOptions);
    }

    /// <summary>
    /// Creates metadata for the MCP server provider based on the command group.
    /// </summary>
    public McpServerMetadata CreateMetadata()
    {
        return new McpServerMetadata
        {
            Id = _commandGroup.Name,
            Name = _commandGroup.Name,
            Description = _commandGroup.Description
        };
    }
}
