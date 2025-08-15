// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Monitor.Options;

public class IngestionStatusCheckOptions : WorkspaceOptions
{
    [JsonPropertyName(MonitorOptionDefinitions.DataCollectionRuleName)]
    public string? DataCollectionRule { get; set; }

    [JsonPropertyName(MonitorOptionDefinitions.OperationIdName)]
    public string? OperationId { get; set; }
}
