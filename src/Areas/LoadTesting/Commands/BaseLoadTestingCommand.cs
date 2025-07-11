// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.LoadTesting.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.LoadTesting.Commands;
public abstract class BaseLoadTestingCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : BaseLoadTestingOptions, new()
{
    protected readonly Option<string> _loadTestOption = OptionDefinitions.LoadTesting.TestResource;
    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_loadTestOption);
        command.AddOption(_resourceGroupOption);
    }
    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TestResourceName = parseResult.GetValueForOption(_loadTestOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
    }
}
