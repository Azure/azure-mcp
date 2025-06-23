// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Monitor.Options;

public static class MonitorOptionDefinitions
{
    public const string TableNameName = "table-name";
    public const string TableTypeName = "table-type";
    public const string QueryTextName = "query";
    public const string HoursName = "hours";
    public const string LimitName = "limit";
    public const string EntityName = "entity";
    public const string HealthModelName = "model-name";

    public static readonly Option<string> TableType = new(
        $"--{TableTypeName}",
        () => "CustomLog",
        "The type of table to query. Options: 'CustomLog', 'AzureMetrics', etc."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> TableName = new(
        $"--{TableNameName}",
        "The name of the table to query. This is the specific table within the workspace."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Query = new(
        $"--{QueryTextName}",
        "The KQL query to execute against the Log Analytics workspace. You can use predefined queries by name:\n" +
        "- 'recent': Shows most recent logs ordered by TimeGenerated\n" +
        "- 'errors': Shows error-level logs ordered by TimeGenerated\n" +
        "Otherwise, provide a custom KQL query."
    )
    {
        IsRequired = true
    };

    public static readonly Option<int> Hours = new(
        $"--{HoursName}",
        () => 24,
        "The number of hours to query back from now."
    )
    {
        IsRequired = true
    };

    public static readonly Option<int> Limit = new(
        $"--{LimitName}",
        () => 20,
        "The maximum number of results to return."
    )
    {
        IsRequired = true
    };

    public static class Health
    {
        public static readonly Option<string> Entity = new(
            $"--{EntityName}",
            "The entity to get health for."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> HealthModel = new(
            $"--{HealthModelName}",
            "The name of the health model for which to get the health."
        )
        {
            IsRequired = true
        };
    }
}
