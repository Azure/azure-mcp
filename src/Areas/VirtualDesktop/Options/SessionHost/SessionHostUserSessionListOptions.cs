// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.VirtualDesktop.Commands;
using AzureMcp.Areas.VirtualDesktop.Options.Hostpool;

namespace AzureMcp.Areas.VirtualDesktop.Options.SessionHost;

public class SessionHostUserSessionListOptions : BaseHostPoolOptions, IResourceGroupOptions
{
    [JsonPropertyName(VirtualDesktopOptionDefinitions.SessionHostName)]
    public string? SessionHostName { get; set; }
}
