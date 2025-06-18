// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.AppConfig;

public class BaseAppConfigOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.AppConfig.AccountName)]
    public string? Account { get; set; }
}
