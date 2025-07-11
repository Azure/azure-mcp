// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Options;

/// <summary>
/// Defines the supported proxy modes for the Azure MCP server.
/// </summary>
internal static class ModeTypes
{
    /// <summary>
    /// Single tool proxy mode - exposes a single "azure" tool that handles internal routing across all Azure MCP tools.
    /// </summary>
    public const string SingleToolProxy = "single";

    /// <summary>
    /// Namespace tool proxy mode - collapses all tools within each namespace into a single tool
    /// (e.g., all storage operations become one "storage" tool with internal routing).
    /// </summary>
    public const string NamespaceProxy = "namespace";

    // More descriptive aliases for improved usability
    /// <summary>
    /// Alias for SingleToolProxy - exposes a single unified Azure tool.
    /// </summary>
    public const string UnifiedTool = "unified-tool";

    /// <summary>
    /// Alias for NamespaceProxy - exposes one tool per Azure service area.
    /// </summary>
    public const string PerServiceTool = "per-service";

    /// <summary>
    /// Gets all valid mode values including aliases.
    /// </summary>
    public static readonly string[] AllModeValues = 
    {
        SingleToolProxy,
        NamespaceProxy,
        UnifiedTool,
        PerServiceTool
    };

    /// <summary>
    /// Maps descriptive mode values to their canonical equivalents.
    /// </summary>
    public static string GetCanonicalMode(string mode)
    {
        return mode switch
        {
            UnifiedTool => SingleToolProxy,
            PerServiceTool => NamespaceProxy,
            _ => mode
        };
    }
}
