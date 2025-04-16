// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Cosmos;

public class BaseContainerArguments : BaseDatabaseArguments
{
    [JsonPropertyName(ArgumentDefinitions.Cosmos.ContainerName)]
    public string? Container { get; set; }
}