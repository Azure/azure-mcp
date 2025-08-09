// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Blob.Batch;

public class BatchDeleteOptions : BaseContainerOptions
{
    [JsonPropertyName(StorageOptionDefinitions.BlobNamesParam)]
    public string[]? BlobNames { get; set; }
}
