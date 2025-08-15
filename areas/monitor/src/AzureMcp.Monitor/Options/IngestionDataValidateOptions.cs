// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Monitor.Options;

public class IngestionDataValidateOptions : SubscriptionOptions
{
    [JsonPropertyName(MonitorOptionDefinitions.DataCollectionRuleName)]
    public string? DataCollectionRule { get; set; }

    [JsonPropertyName(MonitorOptionDefinitions.LogDataName)]
    public string? LogData { get; set; }
}
