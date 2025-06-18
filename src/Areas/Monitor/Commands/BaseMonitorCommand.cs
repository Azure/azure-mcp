// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options;
using AzureMcp.Options.Monitor;

namespace AzureMcp.Commands.Monitor;

public abstract class BaseMonitorCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, IWorkspaceOptions, new()
{
    protected readonly Option<string> _workspaceOption = OptionDefinitions.Monitor.Workspace;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_workspaceOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Workspace = parseResult.GetValueForOption(_workspaceOption);
        return options;
    }
}
