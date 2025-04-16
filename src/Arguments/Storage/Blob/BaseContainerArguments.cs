// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Storage.Blob;

public class BaseContainerArguments : BaseStorageArguments
{
    [JsonPropertyName(ArgumentDefinitions.Storage.ContainerName)]
    public string? Container { get; set; }
}