
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Areas.VirtualDesktop.Options.Hostpool;

public class BaseHostPoolOptions : SubscriptionOptions
{
    [JsonPropertyName(VirtualDesktopOptionDefinitions.HostPoolName)]
    public string? HostPoolName { get; set; }

    [JsonPropertyName(VirtualDesktopOptionDefinitions.HostPoolResourceId)]
    public string? HostPoolResourceId { get; set; }
}
