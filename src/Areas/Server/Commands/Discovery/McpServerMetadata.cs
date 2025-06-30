// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using ModelContextProtocol.Client;

namespace AzureMcp.Areas.Server.Commands.Discovery;

public sealed class McpServerMetadata
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public interface IMcpServerProvider
{
    McpServerMetadata CreateMetadata();
    Task<IMcpClient> CreateClientAsync(McpClientOptions clientOptions);
}
