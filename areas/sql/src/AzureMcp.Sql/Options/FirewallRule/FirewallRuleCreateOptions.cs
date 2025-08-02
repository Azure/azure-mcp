// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Sql.Options.FirewallRule;

/// <summary>
/// Options for the SQL Server Firewall Rule Create command.
/// </summary>
public class FirewallRuleCreateOptions : BaseSqlOptions
{
    [JsonPropertyName(SqlOptionDefinitions.RuleName)]
    public string? Name { get; set; }

    [JsonPropertyName(SqlOptionDefinitions.StartIpAddress)]
    public string? StartIpAddress { get; set; }

    [JsonPropertyName(SqlOptionDefinitions.EndIpAddress)]
    public string? EndIpAddress { get; set; }
}
