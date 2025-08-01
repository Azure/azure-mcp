// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Share;

public class BaseShareOptions : BaseStorageOptions
{
    [JsonPropertyName(StorageOptionDefinitions.ShareName)]
    public string? Share { get; set; }
}
