// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.LoadTesting;

public class BaseLoadTestingOptions : SubscriptionOptions
{
    /// <summary>
    /// The name of the test resource.
    /// </summary>
    public string? TestResourceName { get; set; }
}
