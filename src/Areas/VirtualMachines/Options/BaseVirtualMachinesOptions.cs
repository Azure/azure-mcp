// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.VirtualMachines.Options;

public class BaseVirtualMachinesOptions : SubscriptionOptions
{
    public string? ResourceGroup { get; set; }
}