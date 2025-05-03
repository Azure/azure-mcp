// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.AppConfig.KeyValue;

public class KeyValueListArguments : BaseAppConfigArguments
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.KeyName)]
    public string? Key { get; set; }

    [JsonPropertyName(ArgumentDefinitions.AppConfig.LabelName)]
    public string? Label { get; set; }
}
