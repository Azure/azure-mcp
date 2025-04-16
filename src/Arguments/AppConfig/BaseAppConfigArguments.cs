// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.AppConfig;

public class BaseAppConfigArguments : SubscriptionArguments
{
    [JsonPropertyName(ArgumentDefinitions.AppConfig.AccountName)]
    public string? Account { get; set; }
}