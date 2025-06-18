// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Cosmos.Options;

public class BaseContainerOptions : BaseDatabaseOptions
{
    [JsonPropertyName(OptionDefinitions.Cosmos.ContainerName)]
    public string? Container { get; set; }
}
