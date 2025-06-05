// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.LoadTesting;

public class BaseLoadTestingOptions : SubscriptionOptions
{
    /// <summary>
    /// The ID of the load testing resource.
    /// </summary>
    public string? LoadTestId { get; set; }
}