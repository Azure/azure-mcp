// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Server.Options;

/// <summary>
/// Configuration options for starting the Azure MCP server service.
/// </summary>
public class ServiceStartOptions
{
    /// <summary>
    /// Gets or sets the transport mechanism for the server.
    /// Defaults to standard I/O (stdio).
    /// </summary>
    [JsonPropertyName("transport")]
    public string Transport { get; set; } = TransportTypes.StdIo;

    /// <summary>
    /// Gets or sets the port number for the server when using HTTP transport.
    /// </summary>
    [JsonPropertyName("port")]
    public int Port { get; set; }

    /// <summary>
    /// Gets or sets the service namespaces to expose through the server.
    /// When null, all available namespaces are exposed.
    /// </summary>
    [JsonPropertyName("namespace")]
    public string[]? Namespace { get; set; } = null;

    /// <summary>
    /// Gets or sets the proxy mode for the server.
    /// When null, services are exposed individually.
    /// </summary>
    [JsonPropertyName("proxy")]
    public string? Proxy { get; set; } = null;

    /// <summary>
    /// Gets or sets whether the server should operate in read-only mode.
    /// When true, only tools marked as read-only will be available.
    /// </summary>
    [JsonPropertyName("readOnly")]
    public bool? ReadOnly { get; set; } = null;
}
