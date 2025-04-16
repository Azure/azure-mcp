// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.AppConfig.KeyValue;

public class BaseKeyValueArguments : BaseAppConfigArguments
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.KeyName)]
    public string? Key { get; set; }

    [JsonPropertyName(ArgumentDefinitions.AppConfig.LabelName)]
    public string? Label { get; set; }
}