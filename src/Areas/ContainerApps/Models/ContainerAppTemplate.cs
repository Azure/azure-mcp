// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.ContainerApps.Models;

public class ContainerAppTemplate
{
    [JsonPropertyName("containers")]
    public List<ContainerAppContainer>? Containers { get; set; }

    [JsonPropertyName("scale")]
    public ContainerAppScale? Scale { get; set; }
}
