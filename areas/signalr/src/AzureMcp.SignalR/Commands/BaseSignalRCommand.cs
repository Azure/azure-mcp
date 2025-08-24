// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using AzureMcp.SignalR.Options;

namespace AzureMcp.SignalR.Commands;

/// <summary>
/// Base command for all Azure SignalR commands.
/// </summary>
public abstract class BaseSignalRCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)]
TOptions>
    : SubscriptionCommand<TOptions> where TOptions : BaseSignalROptions, new()
{
    protected virtual bool RequiresResourceGroup => true;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        if (RequiresResourceGroup)
        {
            command.AddOption(_resourceGroupOption);
        }
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        if (RequiresResourceGroup)
        {
            options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        }

        return options;
    }
}
