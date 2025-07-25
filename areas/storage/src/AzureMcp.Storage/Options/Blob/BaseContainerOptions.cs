// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Blob;

public class BaseContainerOptions : BaseStorageOptions
{
    [JsonPropertyName(StorageOptionDefinitions.ContainerName)]
    public string? Container { get; set; }
}
