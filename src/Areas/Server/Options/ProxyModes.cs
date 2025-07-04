// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Options;

/// <summary>
/// Defines the supported proxy modes for the Azure MCP server.
/// </summary>
internal static class ProxyModes
{
    /// <summary>
    /// Single proxy mode - exposes a single "azure" tool that handles internal routing across all Azure MCP tools.
    /// </summary>
    public const string Single = "single";

    /// <summary>
    /// Namespace proxy mode - collapses all tools within each namespace into a single tool
    /// (e.g., all storage operations become one "storage" tool with internal routing).
    /// </summary>
    public const string Namespace = "namespace";
}
