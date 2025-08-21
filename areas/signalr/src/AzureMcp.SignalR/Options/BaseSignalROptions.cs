// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;

namespace AzureMcp.SignalR.Options;

/// <summary>
/// Base options for Azure SignalR commands.
/// </summary>
public class BaseSignalROptions : SubscriptionOptions
{
    public string? SignalRName { get; set; }
}
