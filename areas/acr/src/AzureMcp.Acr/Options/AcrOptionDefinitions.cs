// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Models.Option;

namespace AzureMcp.Acr.Options;

public static class AcrOptionDefinitions
{
    public const string RegistryName = "registry";

    public static readonly Option<string> Registry = new(
        $"--{RegistryName}",
        "The name of the Azure Container Registry. This is the unique name you chose for your container registry."
    );

    public static readonly Option<string> OptionalResourceGroup = new(
        $"--{OptionDefinitions.Common.ResourceGroupName}",
        "The name of the Azure resource group. This is a logical container for Azure resources."
    )
    {
        IsRequired = false
    };
}
