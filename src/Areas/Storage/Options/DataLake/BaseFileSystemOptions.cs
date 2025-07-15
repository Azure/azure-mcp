// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Storage.Options.DataLake;

public class BaseFileSystemOptions : BaseStorageOptions
{
    [JsonPropertyName(StorageOptionDefinitions.FileSystemName)]
    public string? FileSystem { get; set; }
}
