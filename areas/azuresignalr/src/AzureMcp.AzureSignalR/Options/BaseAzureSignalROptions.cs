// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;

namespace AzureMcp.AzureSignalR.Options;

/// <summary>
/// Base options for Azure SignalR commands.
/// </summary>
public class BaseAzureSignalROptions : SubscriptionOptions
{
    public string? SignalRName { get; set; }
}
