// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Options;
using AzureMcp.Areas.VirtualDesktop.Options.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Options.SessionHost;
using AzureMcp.Commands;

namespace AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;

public abstract class BaseSessionHostCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : BaseHostPoolCommand<T>
    where T : BaseHostPoolOptions, new()
{
    protected readonly Option<string> _sessionHostOption = VirtualDesktopOptionDefinitions.SessionHost;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_sessionHostOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        if (options is SessionHostUserSessionListOptions sessionHostOptions)
        {
            sessionHostOptions.SessionHostName = parseResult.GetValueForOption(_sessionHostOption);
        }

        return options;
    }
}
