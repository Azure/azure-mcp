// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments.AppConfig;

public class BaseAppConfigArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.AccountName)]
    public string? Account { get; set; }
}
