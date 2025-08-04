// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.ApplicationInsights.Models;

public class AppCorrelateDataSetParseResult
{
    public bool IsValid { get; set; } = true;

    public string? ErrorMessage { get; set; }

    public List<AppCorrelateDataSet> DataSets { get; set; } = new List<AppCorrelateDataSet>();
}
