// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Cosmos.Options;

public class ItemQueryOptions : BaseContainerOptions
{
    [JsonPropertyName(OptionDefinitions.Cosmos.QueryText)]
    public string? Query { get; set; }
}
