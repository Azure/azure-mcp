// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;

namespace AzureMcp.Marketplace.Options.Product;

public class ProductListOptions : SubscriptionOptions
{
    public string? Language { get; set; }
    public string? Search { get; set; }
    public string? Filter { get; set; }
    public string? OrderBy { get; set; }
    public string? Select { get; set; }
    public string? NextCursor { get; set; }
}
