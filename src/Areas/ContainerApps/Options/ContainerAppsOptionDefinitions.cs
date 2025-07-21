// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;

namespace AzureMcp.Areas.ContainerApps.Options;

public static class ContainerAppsOptionDefinitions
{
    public const string ContainerRegistryName = "registry-name";
    public const string ResourceGroupName = "resource-group";
    public const string LocationName = "location";
    public const string SkuName = "sku-name";

    public static readonly Option<string> ContainerRegistry = new(
        $"--{ContainerRegistryName}",
        "The name of the Azure Container Registry to create or access. Must be globally unique across Azure (e.g., 'myregistryname')."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> ResourceGroup = new(
        $"--{ResourceGroupName}",
        "The name of the Azure resource group. This is a logical container for Azure resources."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Location = new(
        $"--{LocationName}",
        "The Azure region where the container registry will be created (e.g., 'eastus', 'westus2', 'centralus')."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> SkuOption = new(
        $"--{SkuName}",
        "The SKU (pricing tier) for the container registry. Valid values: 'Basic', 'Standard', 'Premium'. Default is 'Basic'."
    )
    {
        IsRequired = false
    };
}
