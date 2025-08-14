// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Blob;

public class BlobUploadOptions : BaseBlobOptions
{
    [JsonPropertyName(StorageOptionDefinitions.LocalFilePathName)]
    public string? LocalFilePath { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.OverwriteName)]
    public bool? Overwrite { get; set; }
}
