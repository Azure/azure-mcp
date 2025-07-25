// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.LoadTesting.Options.LoadTestRun;

public class TestRunListOptions : BaseLoadTestingOptions
{
    /// <summary>
    /// The ID of the load test resource.
    /// </summary>
    public string? TestId { get; set; }
}
