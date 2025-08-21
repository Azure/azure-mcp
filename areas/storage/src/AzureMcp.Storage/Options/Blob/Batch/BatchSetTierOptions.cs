// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Blob.Batch;

public class BatchSetTierOptions : BaseContainerOptions
{
    [JsonPropertyName(StorageOptionDefinitions.TierName)]
    public string? Tier { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.BlobsName)]
    public string[]? BlobNames { get; set; }
}
