// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Server.Models;
using ModelContextProtocol.Client;

namespace AzureMcp.Areas.Server.Commands.Tools;

public sealed class RegistryMcpClientProvider : IMcpClientProvider
{
    private readonly string _id;
    private readonly RegistryServerInfo _serverInfo;

    public RegistryMcpClientProvider(string id, RegistryServerInfo serverInfo)
    {
        _id = id;
        _serverInfo = serverInfo;
    }

    public McpServerMetadata CreateMetadata()
    {
        return new McpServerMetadata
        {
            Id = _id,
            Name = _id,
            Description = _serverInfo.Description ?? string.Empty
        };
    }

    public async Task<IMcpClient> CreateClientAsync(McpClientOptions clientOptions)
    {
        if (!string.IsNullOrWhiteSpace(_serverInfo.Url))
        {
            return await CreateSseClientAsync(clientOptions);
        }
        else if (!string.IsNullOrWhiteSpace(_serverInfo.Type) && _serverInfo.Type.Equals("stdio", StringComparison.OrdinalIgnoreCase))
        {
            return await CreateStdioClientAsync(clientOptions);
        }
        else
        {
            throw new InvalidOperationException($"Registry server '{_id}' does not have a valid url or type for transport.");
        }
    }

    private async Task<IMcpClient> CreateSseClientAsync(McpClientOptions clientOptions)
    {
        var transportOptions = new SseClientTransportOptions
        {
            Name = _id,
            Endpoint = new Uri(_serverInfo.Url!),
            TransportMode = HttpTransportMode.AutoDetect,
        };
        var clientTransport = new SseClientTransport(transportOptions);
        return await McpClientFactory.CreateAsync(clientTransport, clientOptions);
    }

    private async Task<IMcpClient> CreateStdioClientAsync(McpClientOptions clientOptions)
    {
        if (string.IsNullOrWhiteSpace(_serverInfo.Command))
        {
            throw new InvalidOperationException($"Registry server '{_id}' does not have a valid command for stdio transport.");
        }

        // Merge current system environment variables with serverInfo.Env (serverInfo.Env overrides system)
        var env = Environment.GetEnvironmentVariables()
            .Cast<System.Collections.DictionaryEntry>()
            .ToDictionary(e => (string)e.Key, e => (string?)e.Value);

        if (_serverInfo.Env != null)
        {
            foreach (var kvp in _serverInfo.Env)
            {
                env[kvp.Key] = kvp.Value;
            }
        }

        var transportOptions = new StdioClientTransportOptions
        {
            Name = _id,
            Command = _serverInfo.Command,
            Arguments = _serverInfo.Args,
            EnvironmentVariables = env
        };

        var clientTransport = new StdioClientTransport(transportOptions);
        return await McpClientFactory.CreateAsync(clientTransport, clientOptions);
    }
}
