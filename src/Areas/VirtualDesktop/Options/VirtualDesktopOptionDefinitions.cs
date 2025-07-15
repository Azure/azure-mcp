// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.VirtualDesktop.Options;

public static class VirtualDesktopOptionDefinitions
{
    public const string HostPoolName = "hostpool-name";

    public static readonly Option<string> HostPool = new(
        $"--{HostPoolName}",
        "The name of the Azure Virtual Desktop host pool. This is the unique name you chose for your hostpool."
    )
    {
        IsRequired = true
    };
}

