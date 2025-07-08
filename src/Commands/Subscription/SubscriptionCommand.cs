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

        command.AddValidator(result =>
        {
            var subscriptionValue = result.GetValueForOption(_subscriptionOption);
            var envSubscription = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");

            // Check if both subscription option and environment variable are missing or invalid
            var hasValidSubscription = !string.IsNullOrEmpty(subscriptionValue) &&
                                     !subscriptionValue.Contains("subscription") &&
                                     !subscriptionValue.Contains("default");

            var hasValidEnvVar = !string.IsNullOrEmpty(envSubscription);

            if (!hasValidSubscription && !hasValidEnvVar)
            {
                result.ErrorMessage = "Subscription is required. Provide --subscription option or set AZURE_SUBSCRIPTION_ID environment variable.";
            }
        });
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        // Get subscription from command line option or fallback to environment variable
        var subscriptionValue = parseResult.GetValueForOption(_subscriptionOption);
        options.Subscription = string.IsNullOrEmpty(subscriptionValue)
            || subscriptionValue.Contains("subscription")
            || subscriptionValue.Contains("default")
            ? Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID")
            : subscriptionValue;

        return options;
    }
}
