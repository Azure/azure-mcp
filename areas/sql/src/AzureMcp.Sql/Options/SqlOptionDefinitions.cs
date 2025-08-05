// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Sql.Options;

public static class SqlOptionDefinitions
{
    public const string ServerName = "server";
    public const string DatabaseName = "database";
    public const string RuleName = "name";
    public const string StartIpAddress = "start-ip";
    public const string EndIpAddress = "end-ip";

    public static readonly Option<string> Server = new(
        $"--{ServerName}",
        "The Azure SQL Server name."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Database = new(
        $"--{DatabaseName}",
        "The Azure SQL Database name."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> FirewallRuleName = new(
        $"--{RuleName}",
        "The firewall rule name."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> StartIp = new(
        $"--{StartIpAddress}",
        "The start IP address for the firewall rule (e.g., '192.168.1.1')."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> EndIp = new(
        $"--{EndIpAddress}",
        "The end IP address for the firewall rule (e.g., '192.168.1.255')."
    )
    {
        IsRequired = true
    };
}
