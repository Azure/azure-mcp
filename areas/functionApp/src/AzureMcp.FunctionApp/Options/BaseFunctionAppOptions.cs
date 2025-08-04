// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.FunctionApp.Options;

public class BaseFunctionAppOptions : SubscriptionOptions
{
    [JsonPropertyName(FunctionAppOptionDefinitions.FunctionAppName)]
    public string? FunctionAppName { get; set; }
}
