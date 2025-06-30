// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.LoadTesting;

namespace AzureMcp.Commands.LoadTesting;
public abstract class BaseLoadTestingCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : BaseLoadTestingOptions, new()
{
    public override string Name => "loadtest";

    protected readonly Option<string> _loadTestOption = OptionDefinitions.LoadTesting.TestResource;

    public override string Description =>
        "Retrieves information about a load test.";

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
