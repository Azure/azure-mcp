// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Options;
using AzureMcp.Commands;
using ModelContextProtocol.Client;

namespace AzureMcp.Areas.Server.Commands.Discovery;

/// <summary>
/// Represents a command group that provides metadata and MCP client creation.
/// </summary>
public sealed class CommandGroupServerProvider(CommandGroup commandGroup) : IMcpServerProvider
{
    private readonly CommandGroup _commandGroup = commandGroup;
    private string? _entryPoint = System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName;

    /// <summary>
    /// Gets or sets the entry point executable path for the MCP server.
    /// If set to null or empty, defaults to the current process executable.
    /// </summary>
    public string? EntryPoint
    {
        get => _entryPoint;
        set => _entryPoint = string.IsNullOrWhiteSpace(value)
            ? System.Diagnostics.Process.GetCurrentProcess().MainModule?.FileName
            : value;
    }

    /// <summary>
    /// Gets or sets whether the MCP server should run in read-only mode.
    /// </summary>
    public bool ReadOnly { get; set; } = false;

    /// <summary>
    /// Creates an MCP client from a command group.
    /// </summary>
    public async Task<IMcpClient> CreateClientAsync(McpClientOptions clientOptions)
    {
        if (string.IsNullOrWhiteSpace(EntryPoint))
        {
            throw new InvalidOperationException("EntryPoint must be set before creating the MCP client.");
        }

        var arguments = new List<string> { "server", "start", "--namespace", _commandGroup.Name };

        if (ReadOnly)
        {
            arguments.Add($"--${ServiceOptionDefinitions.ReadOnlyName}");
        }

        var transportOptions = new StdioClientTransportOptions
        {
            Name = _commandGroup.Name,
            Command = EntryPoint,
            Arguments = arguments.ToArray(),
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
