// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.FunctionApp.Options;

public class BaseFunctionAppOptions : SubscriptionOptions
{
    [JsonPropertyName(FunctionAppOptionDefinitions.FunctionAppName)]
    public string? FunctionAppName { get; set; }
}
