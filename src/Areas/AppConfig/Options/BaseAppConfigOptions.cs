// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.AppConfig.Options;

public class BaseAppConfigOptions : SubscriptionOptions
{
    [JsonPropertyName(AppConfigOptionDefinitions.AccountName)]
    public string? Account { get; set; }
}
