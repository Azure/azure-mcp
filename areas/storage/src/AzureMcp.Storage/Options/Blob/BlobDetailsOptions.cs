// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Blob;

public class BlobDetailsOptions : BaseContainerOptions
{
    [JsonPropertyName(StorageOptionDefinitions.BlobName)]
    public string? Blob { get; set; }
}
