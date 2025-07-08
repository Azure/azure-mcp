// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Server.Options;

/// <summary>
/// Defines the supported transport mechanisms for the Azure MCP server.
/// </summary>
internal static class TransportTypes
{
    /// <summary>
    /// Standard Input/Output transport mechanism.
    /// </summary>
    public const string StdIo = "stdio";

    /// <summary>
    /// Server-Sent Events transport mechanism.
    /// </summary>
    public const string Sse = "sse";
}
