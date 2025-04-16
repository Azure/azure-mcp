// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.Text.Json.Serialization;

namespace AzureMcp.Arguments;

public class SubscriptionArguments : GlobalArguments
{
    [JsonPropertyName(ArgumentDefinitions.Common.SubscriptionName)]
    public string? Subscription { get; set; }
}