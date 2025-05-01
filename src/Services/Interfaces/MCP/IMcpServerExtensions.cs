// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Command;
using ModelContextProtocol.Server;

namespace AzureMcp.Services.Interfaces.MCP;

/// <summary>
/// Extensions interface for IMcpServer that adds support for sending real-time results
/// </summary>
public interface IMcpServerExtensions
{
    /// <summary>
    /// Sends real-time results to the client during long-running operations
    /// </summary>
    /// <param name="response">The response containing real-time results to send</param>
    void SendRealtimeResults(CommandResponse response);
}