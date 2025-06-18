// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;

namespace AzureMcp.Areas.Server.Commands.Tools;

/// <summary>
/// Interface for providing MCP client instances for various server providers.
/// </summary>
public interface IMcpClientService : IDisposable
{
    List<McpServerMetadata> ListProviderMetadata();
    Task<IMcpClient?> GetProviderClientAsync(string name, McpClientOptions clientOptions);
}
