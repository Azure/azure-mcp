
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.VirtualDesktop.Options.Hostpool;
public class BaseHostPoolOptions : SubscriptionOptions
{
    [JsonPropertyName(VirtualDesktopOptionDefinitions.HostPoolName)]
    public string? HostPoolName { get; set; }
}
