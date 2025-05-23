// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments;
using AzureMcp.Arguments.Monitor;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Monitor;

public abstract class BaseMonitorCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : SubscriptionCommand<TArgs>
    where TArgs : SubscriptionArguments, IWorkspaceArguments, new()
{
    protected readonly Option<string> _workspaceOption = ArgumentDefinitions.Monitor.Workspace;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_workspaceOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Workspace = parseResult.GetValueForOption(_workspaceOption);
        return args;
    }
}
