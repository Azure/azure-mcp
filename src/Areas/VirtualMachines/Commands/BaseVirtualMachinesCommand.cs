// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.VirtualMachines.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.VirtualMachines.Commands;

public abstract class BaseVirtualMachinesCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions>
    where TOptions : BaseVirtualMachinesOptions, new()
{
    protected readonly Option<string> _resourceGroupOption = OptionDefinitions.Common.ResourceGroup;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
    }
}