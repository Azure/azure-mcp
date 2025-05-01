// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Command;

namespace AzureMcp.Services.Interfaces.MCP;

/// <summary>
/// Implementation of IMcpServerExtensions that provides support for sending real-time results
/// </summary>
public class AzureMcpServerExtensions : IMcpServerExtensions
{
    /// <summary>
    /// Sends real-time results to the client during long-running operations
    /// </summary>
    /// <param name="response">The response containing real-time results to send</param>
    public void SendRealtimeResults(CommandResponse response)
    {
        // Implementation of sending real-time results
        // This depends on how your MCP server handles streaming results
        
        // For now, we'll just log that this method was called
        Console.WriteLine($"Sending real-time results: {response.Message}");
        
        // TODO: Implement actual streaming functionality based on your specific requirements
    }
}