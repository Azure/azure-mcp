// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Areas.VirtualDesktop.Commands;

/// <summary>
/// Base command for all Virtual Desktop commands
/// </summary>
public abstract class BaseVirtualDesktopCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{
    protected new readonly Option<string> _resourceGroupOption = OptionDefinitions.Common.ResourceGroup;
    protected virtual bool RequiresResourceGroup => false;

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

        if (RequiresResourceGroup && options is IResourceGroupOptions rgOptions)
        {
            rgOptions.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        }

        return options;
    }
}

/// <summary>
/// Interface for options that include resource group
/// </summary>
public interface IResourceGroupOptions
{
    string? ResourceGroup { get; set; }
}
