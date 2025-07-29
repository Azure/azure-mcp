// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.DataLake.File;

public class FileUploadOptions : BaseFileSystemOptions
{
    [JsonPropertyName(StorageOptionDefinitions.FilePathName)]
    public string? FilePath { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.SourceFilePathName)]
    public string? SourceFilePath { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.OverwriteName)]
    public bool Overwrite { get; set; }
}
