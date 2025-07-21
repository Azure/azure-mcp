// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.ContainerApps.Options;

namespace AzureMcp.Areas.ContainerApps.Options.ContainerRegistry;

public class ContainerRegistryCreateOptions : BaseContainerAppsOptions
{
    [JsonPropertyName(ContainerAppsOptionDefinitions.ContainerRegistryName)]
    public string? RegistryName { get; set; }

    [JsonPropertyName(ContainerAppsOptionDefinitions.LocationName)]
    public string? Location { get; set; }

    [JsonPropertyName(ContainerAppsOptionDefinitions.SkuName)]
    public string? SkuName { get; set; }
}
