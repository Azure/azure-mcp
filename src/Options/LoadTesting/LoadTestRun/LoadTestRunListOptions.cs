// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.LoadTesting.LoadTestRun;

public class LoadTestRunGetOptions : BaseLoadTestingOptions
{
    /// <summary>
    /// The ID of the load test run resource.
    /// </summary>
    public string? LoadTestRunId { get; set; }
}
