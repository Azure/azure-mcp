// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.LoadTesting.Options;
public class BaseLoadTestingOptions : SubscriptionOptions
{
    /// <summary>
    /// The name of the test resource.
    /// </summary>
    public string? TestResourceName { get; set; }
}
