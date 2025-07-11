// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;

namespace AzureMcp.Areas.VirtualMachines.Options;

public static class VirtualMachinesOptionDefinitions
{
    public static readonly Option<string> ResourceGroup = new("--resource-group")
    {
        Description = "Name of the Azure resource group",
        IsRequired = false
    };
}