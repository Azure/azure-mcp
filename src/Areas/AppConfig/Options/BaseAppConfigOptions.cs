// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.AppConfig.Options;

public class BaseAppConfigOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.AppConfig.AccountName)]
    public string? Account { get; set; }
}
