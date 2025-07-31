// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.ApplicationInsights.Models;

namespace AzureMcp.ApplicationInsights.Options
{
    public static class ApplicationInsightsOptionDefinitions
    {
        public const string ResourceNameName = "resource-name";
        public const string ResourceGroupName = "resource-group";
        public const string ResourceIdName = "resource-id";
        public const string SymptomName = "symptom";
        public const string StartTimeName = "start-time";
        public const string EndTimeName = "end-time";
        public const string CorrelationDataSetName = "data-sets";
        public const string SpanIdName = "span-id";
        public const string TraceIdName = "trace-id";
        public const string TableName = "table";
        public const string FiltersName = "filters";
        public const string ItemIdName = "item-id";
        public const string ItemTypeName = "item-type";

        public static readonly Option<string> ResourceName = new(
            $"--{ResourceNameName}",
            "The name of the Application Insights resource."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> ResourceGroup = new(
            $"--{ResourceGroupName}",
            "The name of the Azure resource group containing the Application Insights resource."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> ResourceId = new(
            $"--{ResourceIdName}",
            "The resource ID of the Application Insights resource."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> Symptom = new(
            $"--{SymptomName}",
            "The user-reported description of the problem occurring. Include as much detail as possible, including relevant details provided by the user such as result codes, operations, and time information."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> StartTime = new(
            $"--{StartTimeName}",
            () => DateTime.UtcNow.AddHours(-24).ToString("o"),
            "The start time of the investigation in ISO format (e.g., 2023-01-01T00:00:00Z). Defaults to 24 hours ago."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> EndTime = new(
            $"--{EndTimeName}",
            () => DateTime.UtcNow.ToString("o"),
            "The end time of the investigation in ISO format (e.g., 2023-01-01T00:00:00Z). Defaults to now."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> SpanId = new Option<string>(
            $"--{SpanIdName}",
            "The specific span ID in the trace to analyze.")
        {
            IsRequired = false
        };

        public static readonly Option<string> ItemId = new Option<string>(
            $"--{ItemIdName}",
            "The specific ItemId in the distributed trace to retrieve details of.")
        {
            IsRequired = true
        };

        public static readonly Option<string> ItemType = new Option<string>(
            $"--{ItemTypeName}",
            "The specific ItemType of the item in the distributed trace to retrieve details of. (e.g. 'exception')")
        {
            IsRequired = true
        };

        public static readonly Option<string> TraceId = new Option<string>(
            $"--{TraceIdName}",
            "The specific trace ID to analyze.")
        {
            IsRequired = true
        };

        public static readonly Option<string> Table = new Option<string>(
            $"--{TableName}",
            "The table to list traces for. Valid values are 'requests', 'dependencies', 'availabilityResults', 'exceptions'.")
        {
            IsRequired = true
        };

        public static readonly Option<string[]> Filters = new Option<string[]>(
            $"--{FiltersName}",
            "The filters to apply to the trace results. JSON array of \"dimension=\\\"value\\\"\". Dimension names should be valid Application Insights column names. (e.g. [ \"success=\\\"false\\\"\", \"resultCode=\\\"500\\\"\" ])")
        {
            IsRequired = false,
            Arity = ArgumentArity.ZeroOrMore,
            AllowMultipleArgumentsPerToken = true
        };

        public static readonly Option<AppCorrelateDataSetParseResult> DataSets = new(
            $"--{CorrelationDataSetName}",
            (ArgumentResult result) =>
            {
                if (result.Tokens.Count == 0)
                {
                    return new AppCorrelateDataSetParseResult()
                    {
                        IsValid = false,
                        ErrorMessage = $"The {CorrelationDataSetName} option is required and must contain at least one data set."
                    };
                }

                try
                {
                    var dataSets = JsonSerializer.Deserialize(result.Tokens[0].Value.Trim('\''), ApplicationInsightsJsonContext.Default.ListAppCorrelateDataSet);

                    foreach (var dataSet in dataSets ?? Enumerable.Empty<AppCorrelateDataSet>())
                    {
                        if (string.IsNullOrWhiteSpace(dataSet.Table))
                        {
                            return new AppCorrelateDataSetParseResult()
                            {
                                IsValid = false,
                                ErrorMessage = $"Each data set must have a valid 'table' property."
                            };
                        }
                        if (string.IsNullOrWhiteSpace(dataSet.Aggregation))
                        {
                            dataSet.Aggregation = "Count"; // Default aggregation
                        }
                    }

                    return new AppCorrelateDataSetParseResult()
                    {
                        IsValid = true,
                        DataSets = dataSets ?? new List<AppCorrelateDataSet>()
                    };
                }
                catch (JsonException ex)
                {
                    return new AppCorrelateDataSetParseResult()
                    {
                        IsValid = false,
                        ErrorMessage = $"Invalid JSON format for the {CorrelationDataSetName} option. Error: {ex.Message}"
                    };
                }
            },
            isDefault: false,
            "The data sets to include in the correlation analysis. This is a JSON array with one or more data sets to compare, formatted as follows:" +
            "[{\"table\":\"The name of the table to perform correlation analysis on. Should be a valid Application Insights table name\",\"filters\":[ \"JSON array with one or more filters represented as \"dimension=\\\"value\\\". Dimension names should be valid Application Insights column names\" ],\"splitBy\":\"A single dimension to split by, or null (if data set should not be split). This should be a valid Application Insights column name.\",\"aggregation\":\"The aggregation method to use. Default is 'Count'. Valid values are 'Count', 'Average' and '95thPercentile'.\"}]"
        )
        {
            IsRequired = true
        };
    }
}
