// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Sql.Models;

/// <summary>
/// Represents a SQL server firewall rule.
/// </summary>
/// <param name="Name">The name of the firewall rule</param>
/// <param name="Id">The resource ID of the firewall rule</param>
/// <param name="Type">The resource type</param>
/// <param name="StartIpAddress">The start IP address of the firewall rule</param>
/// <param name="EndIpAddress">The end IP address of the firewall rule</param>
public record SqlFirewallRule(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("id")] string Id,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("startIpAddress")] string StartIpAddress,
    [property: JsonPropertyName("endIpAddress")] string EndIpAddress
);