// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.LoadTesting;

public class BaseLoadTestingOptions : SubscriptionOptions
{
    /// <summary>
    /// The name of the load testing resource.
    /// </summary>
    public string? LoadTestName { get; set; }
}
