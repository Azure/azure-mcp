// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Argument;

namespace AzureMcp.Arguments;

public class SubscriptionArguments : GlobalArguments
{
    [JsonPropertyName(ArgumentDefinitions.Common.SubscriptionName)]
    public string? Subscription { get; set; }
}
