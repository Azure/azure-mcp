// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.Cosmos;

public class BaseContainerArguments : BaseDatabaseArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.ContainerName)]
    public string? Container { get; set; }
}
