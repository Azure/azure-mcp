// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;

namespace AzureMcp.LoadTesting.Options;
public class BaseLoadTestingOptions : SubscriptionOptions
{
    /// <summary>
    /// The name of the test resource.
    /// </summary>
    public string? TestResourceName { get; set; }
}
