// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Storage.Options;

namespace AzureMcp.Areas.Storage.Options.DataLake.Directory;

public class DirectoryCreateOptions : BaseStorageOptions
{
    [JsonPropertyName(StorageOptionDefinitions.FileSystemName)]
    public string? FileSystemName { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.DirectoryPathName)]
    public string? DirectoryPath { get; set; }
}