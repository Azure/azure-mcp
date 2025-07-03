// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Monitor.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Options;

namespace AzureMcp.Areas.Monitor.Commands;

public abstract class BaseMonitorCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, IWorkspaceOptions, new()
{
    private readonly Option<string> _workspaceOption = WorkspaceOptionDefinitions.Workspace;

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
