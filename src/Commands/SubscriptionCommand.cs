// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands;

public abstract class SubscriptionCommand<TArgs> : GlobalCommand<TArgs>
    where TArgs : SubscriptionArguments, new()
{
    protected readonly Option<string> _subscriptionOption = ArgumentDefinitions.Common.Subscription.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_subscriptionOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateSubscriptionArgument());
    }

    protected ArgumentBuilder<TArgs> CreateSubscriptionArgument()
    {
        return ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Common.Subscription.Name, ArgumentDefinitions.Common.Subscription.Description)
            .WithValueAccessor(args => args.Subscription ?? string.Empty)
            .WithSuggestedValuesLoader(async (context, args) =>
            {

                var subArgs = args as SubscriptionArguments;
                if (string.IsNullOrEmpty(subArgs?.Subscription))
                {
                    return await GetSubscriptionOptions(context);
                }
                return [];

            })
            .WithIsRequired(ArgumentDefinitions.Common.Subscription.Required);
    }

    // Helper method to get subscription options
    protected virtual async Task<List<ArgumentOption>> GetSubscriptionOptions(CommandContext context)
    {
        try
        {
            var subscriptionService = context.GetService<ISubscriptionService>();
            var subscriptions = await subscriptionService.GetSubscriptions();
            return subscriptions ?? [];
        }
        catch
        {
            // Silently handle subscription fetch failures
            return [];
        }
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Subscription = parseResult.GetValueForOption(_subscriptionOption);
        return args;
    }
}