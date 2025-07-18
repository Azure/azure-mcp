// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.VirtualDesktop.Options;

public static class VirtualDesktopOptionDefinitions
{
    public const string HostPoolName = "hostpool-name";
    public const string SessionHostName = "sessionhost-name";

    public static readonly Option<string> HostPool = new(
        $"--{HostPoolName}",
        "The name of the Azure Virtual Desktop host pool. This is the unique name you chose for your hostpool."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> SessionHost = new(
        $"--{SessionHostName}",
        "The name of the session host. This is the computer name of the virtual machine in the host pool."
    )
    {
        IsRequired = true
    };
}

