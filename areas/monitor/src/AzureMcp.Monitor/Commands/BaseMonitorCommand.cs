// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.Core.Options;
using AzureMcp.Monitor.Options;

namespace AzureMcp.Monitor.Commands;

public abstract class BaseMonitorCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{
    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        var optionsType = typeof(TOptions);

        // Check if TOptions implements IWorkspaceOptions to decide which option to use
        if (typeof(IWorkspaceOptions).IsAssignableFrom(optionsType))
        {
            RequireResourceGroup();
            command.AddOption(WorkspaceOptionDefinitions.Workspace);
        }
        else if (optionsType == typeof(IngestionUploadOptions))
        {
            command.AddOption(MonitorOptionDefinitions.Ingestion.IngestionEndpoint);
        }
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        var optionsType = typeof(TOptions);

        if (typeof(IWorkspaceOptions).IsAssignableFrom(optionsType) && options is IWorkspaceOptions workspaceOptions)
        {
            workspaceOptions.Workspace = parseResult.GetValueForOption(WorkspaceOptionDefinitions.Workspace);
        }
        else if (optionsType == typeof(IngestionUploadOptions) && options is IngestionUploadOptions ingestionOptions)
        {
            ingestionOptions.IngestionEndpoint = parseResult.GetValueForOption(MonitorOptionDefinitions.Ingestion.IngestionEndpoint);
        }

        return options;
    }
}
