// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Options.Queue.Message;

public class QueueMessageSendOptions : BaseQueueOptions
{
    [JsonPropertyName(StorageOptionDefinitions.MessageName)]
    public string? Message { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.TimeToLiveInSecondsName)]
    public int? TimeToLiveInSeconds { get; set; }

    [JsonPropertyName(StorageOptionDefinitions.VisibilityTimeoutInSecondsName)]
    public int? VisibilityTimeoutInSeconds { get; set; }
}
