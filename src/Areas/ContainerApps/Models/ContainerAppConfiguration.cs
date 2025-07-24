// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.ContainerApps.Models;

public class ContainerAppConfiguration
{
    [JsonPropertyName("ingress")]
    public ContainerAppIngress? Ingress { get; set; }

    [JsonPropertyName("secrets")]
    public List<ContainerAppSecret>? Secrets { get; set; }

    [JsonPropertyName("registries")]
    public List<ContainerAppRegistry>? Registries { get; set; }
}
