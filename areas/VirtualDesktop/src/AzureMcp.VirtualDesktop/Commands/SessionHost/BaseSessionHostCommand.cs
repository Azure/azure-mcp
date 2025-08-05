// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.VirtualDesktop.Commands.Hostpool;
using AzureMcp.Areas.VirtualDesktop.Options;
using AzureMcp.Areas.VirtualDesktop.Options.SessionHost;

namespace AzureMcp.Areas.VirtualDesktop.Commands.SessionHost;

public abstract class BaseSessionHostCommand
    : BaseHostPoolCommand<SessionHostUserSessionListOptions>
{
    protected readonly Option<string> _sessionHostOption = VirtualDesktopOptionDefinitions.SessionHost;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_sessionHostOption);
    }

    protected override SessionHostUserSessionListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.SessionHostName = parseResult.GetValueForOption(_sessionHostOption);
        return options;
    }
}
