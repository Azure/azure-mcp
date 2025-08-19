// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureManagedLustre.Options;

public static class AzureManagedLustreOptionDefinitions
{
    public const string sku = "sku";
    public const string size = "size";
    public static readonly Option<string> SkuOption = new(
        $"--{sku}",
        "The AMLFS SKU. Allowed values: AMLFS-Durable-Premium-40, AMLFS-Durable-Premium-125, AMLFS-Durable-Premium-250, AMLFS-Durable-Premium-500."
    )
    {
        IsRequired = true
    };

    public static readonly Option<int> SizeOption = new(
        $"--{size}",
        "The AMLFS size (TiB)."
    )
    {
        IsRequired = true
    };
}
