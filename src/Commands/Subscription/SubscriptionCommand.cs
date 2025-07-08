// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Option;
using AzureMcp.Options;

namespace AzureMcp.Commands.Subscription;

public abstract class SubscriptionCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions> : GlobalCommand<TOptions>
    where TOptions : SubscriptionOptions, new()
{

    protected readonly Option<string> _subscriptionOption = OptionDefinitions.Common.Subscription;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_subscriptionOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        // Get subscription from command line option or fallback to environment variable
        try
        {
            var subscriptionValue = parseResult.GetValueForOption(_subscriptionOption);
            options.Subscription = string.IsNullOrEmpty(subscriptionValue)
                || subscriptionValue.Contains("subscription")
                || subscriptionValue.Contains("default")
                ? Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID")
                : subscriptionValue;
        }
        catch (Exception)    // subscription id is missing
        {
            options.Subscription = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        }

        return options;
    }
}
