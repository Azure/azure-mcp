// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Storage.Options.Blob;

public class BaseContainerOptions : BaseStorageOptions
{
    [JsonPropertyName(StorageOptionDefinitions.ContainerName)]
    public string? Container { get; set; }
}
