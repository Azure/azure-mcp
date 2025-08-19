// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Monitor.Options;

public static class MonitorOptionDefinitions
{
    public const string TableNameName = "table";
    public const string TableTypeName = "table-type";
    public const string QueryTextName = "query";
    public const string HoursName = "hours";
    public const string LimitName = "limit";
    public const string EntityName = "entity";
    public const string HealthModelName = "health-model";
    public const string DataCollectionRuleName = "data-collection-rule";
    public const string LogDataName = "log-data";
    public const string StreamNameName = "stream-name";
    public const string OperationIdName = "operation-id";
    public const string IngestionEndpointName = "ingestion-endpoint";

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

    public static class Metrics
    {
        // Metrics related options
        public const string ResourceIdName = "resource-id";
        public const string ResourceTypeName = "resource-type";
        public const string ResourceNameName = "resource";
        public const string MetricNamespaceName = "metric-namespace";
        public const string MetricNamesName = "metric-names";
        public const string StartTimeName = "start-time";
        public const string EndTimeName = "end-time";
        public const string IntervalName = "interval";
        public const string AggregationName = "aggregation";
        public const string FilterName = "filter";
        public const string SearchStringName = "search-string";

        public const string EntityName = "entity";
        public const string HealthModelName = "health-model";
        public const string MaxBucketsName = "max-buckets";

        // Metrics options
        public static readonly Option<string> MetricNamespaceOptional = new(
            $"--{MetricNamespaceName}",
            "The metric namespace to query. Obtain this value from the azmcp-monitor-metrics-definitions command."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> MetricNamespace = new(
            $"--{MetricNamespaceName}",
            "The metric namespace to query. Obtain this value from the azmcp-monitor-metrics-definitions command."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> MetricNames = new(
            $"--{MetricNamesName}",
            "The names of metrics to query (comma-separated)."
        )
        {
            IsRequired = true,
            AllowMultipleArgumentsPerToken = true
        };

        public static readonly Option<string> StartTime = new(
            $"--{StartTimeName}",
            () => DateTime.UtcNow.AddHours(-24).ToString("o"),
            "The start time for the query in ISO format (e.g., 2023-01-01T00:00:00Z). Defaults to 24 hours ago."
        );

        public static readonly Option<string> EndTime = new(
            $"--{EndTimeName}",
            () => DateTime.UtcNow.ToString("o"),
            "The end time for the query in ISO format (e.g., 2023-01-01T00:00:00Z). Defaults to now."
        );

        public static readonly Option<string> Interval = new(
            $"--{IntervalName}",
            "The time interval for data points (e.g., PT1H for 1 hour, PT5M for 5 minutes)."
        );

        public static readonly Option<string> Aggregation = new(
            $"--{AggregationName}",
            "The aggregation type to use (Average, Maximum, Minimum, Total, Count)."
        );

        public static readonly Option<string> Filter = new(
            $"--{FilterName}",
            "OData filter to apply to the metrics query."
        );

        public static readonly Option<string> SearchString = new(
            $"--{SearchStringName}",
            "A string to filter the metric definitions by. Helpful for reducing the number of records returned. Performs case-insensitive matching on metric name and description fields."
        )
        {
            IsRequired = false
        };

        public static readonly Option<int> DefinitionsLimit = new(
            $"--limit",
            () => 10,
            "The maximum number of metric definitions to return. Defaults to 10."
        )
        {
            IsRequired = false
        };

        public static readonly Option<int> NamespacesLimit = new(
            $"--limit",
            () => 10,
            "The maximum number of metric namespaces to return. Defaults to 10."
        )
        {
            IsRequired = false
        };

        public static readonly Option<int> MaxBuckets = new(
            $"--{MaxBucketsName}",
            () => 50,
            "The maximum number of time buckets to return. Defaults to 50."
        )
        {
            IsRequired = false
        };


        public static readonly Option<string> ResourceType = new(
            $"--{ResourceTypeName}",
            "The Azure resource type (e.g., 'Microsoft.Storage/storageAccounts', 'Microsoft.Compute/virtualMachines'). If not specified, will attempt to infer from resource name."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> ResourceName = new(
            $"--{ResourceNameName}",
            "The name of the Azure resource to query metrics for."
        )
        {
            IsRequired = true
        };
    }

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

    public static class Ingestion
    {
        public static readonly Option<string> DataCollectionRule = new(
            $"--{DataCollectionRuleName}",
            "The ID of the data collection rule (DCR) to use for log ingestion. This defines the schema and transformation rules for the uploaded data."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> LogData = new(
            $"--{LogDataName}",
            "The log data to upload as a JSON string or JSON array. The data must conform to the schema defined in the data collection rule."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> StreamName = new(
            $"--{StreamNameName}",
            "The name of the stream within the data collection rule to target for the log data upload."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> OperationId = new(
            $"--{OperationIdName}",
            "The operation ID to check status for. If not provided, the most recent ingestion operation will be checked."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> IngestionEndpoint = new(
            $"--{IngestionEndpointName}",
            "The Azure Monitor ingestion endpoint URL (e.g., https://myendpoint-abcd.eastus-1.ingest.monitor.azure.com). This is the data collection endpoint associated with your data collection rule."
        )
        {
            IsRequired = true
        };
    }
}
