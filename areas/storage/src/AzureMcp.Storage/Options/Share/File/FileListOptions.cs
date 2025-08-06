// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Share.File;

public class FileListOptions : BaseFileOptions
{
    [JsonPropertyName(StorageOptionDefinitions.PrefixName)]
    public string? Prefix { get; set; }
}
