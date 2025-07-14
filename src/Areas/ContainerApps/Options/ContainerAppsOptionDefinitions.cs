// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.ContainerApps.Options;

public static class ContainerAppsOptionDefinitions
{
    public const string ResourceGroupName = "resource-group";

    public static readonly Option<string> ResourceGroup = new(
        $"--{ResourceGroupName}",
        "The name of the Azure resource group to filter Container Apps. If not specified, lists all Container Apps in the subscription."
    )
    {
        IsRequired = false
    };
}