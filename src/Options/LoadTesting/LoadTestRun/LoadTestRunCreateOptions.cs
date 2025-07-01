// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.LoadTesting.LoadTestRun;

public class TestRunCreateOptions : BaseLoadTestingOptions
{
    /// <summary>
    /// The ID of the load test run resource.
    /// </summary>
    public string? TestRunId { get; set; }

    /// <summary>
    /// The ID of the load test resource.
    /// </summary>
    public string? TestId { get; set; }
}
