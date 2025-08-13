// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Queue;

public class BaseQueueOptions : BaseStorageOptions
{
    [JsonPropertyName(StorageOptionDefinitions.QueueName)]
    public string? Queue { get; set; }
}
