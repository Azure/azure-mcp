// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Option;
using AzureMcp.Core.Options;

namespace AzureMcp.Authorization.Options;

public class RoleAssignmentListOptions : SubscriptionOptions
{
    [JsonPropertyName(OptionDefinitions.Authorization.ScopeName)]
    public string? Scope { get; set; }
}
