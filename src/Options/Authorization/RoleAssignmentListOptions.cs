// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Authorization;

public class RoleAssignmentListOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Authorization.ScopeName)]
    public string? Scope { get; set; }
}
