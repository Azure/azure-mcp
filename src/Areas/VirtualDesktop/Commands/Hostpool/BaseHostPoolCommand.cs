// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.VirtualDesktop.Options;
using AzureMcp.Areas.VirtualDesktop.Options.Hostpool;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;

namespace AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;
public abstract class BaseHostPoolCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T>
    where T : BaseHostPoolOptions, new()
{
    protected readonly Option<string> _hostPoolOption = VirtualDesktopOptionDefinitions.HostPool;
    protected readonly Option<string> _hostPoolResourceIdOption = VirtualDesktopOptionDefinitions.HostPoolResourceIdOption;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_hostPoolOption);
        command.AddOption(_hostPoolResourceIdOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.HostPoolName = parseResult.GetValueForOption(_hostPoolOption);
        options.HostPoolResourceId = parseResult.GetValueForOption(_hostPoolResourceIdOption);
        return options;
    }
}
