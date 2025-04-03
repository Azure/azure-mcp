
using AzureMCP.Arguments;
using AzureMCP.Commands;
using AzureMCP.Models;

public abstract class BaseCommandWithSubscription<TArgs> : BaseCommand<TArgs>
    where TArgs : BaseArgumentsWithSubscription, new()
{
    protected BaseCommandWithSubscription()
    {
        // No additional options needed for base subscriptions command
    }

    // New method to register an argument chain
    protected void RegisterArgumentChain(params ArgumentChain<TArgs>[] argumentDefinitions)
    {
        var fullChain = new List<ArgumentDefinition<string>>
        {
            // Add common arguments
            CreateAuthMethodArgument(),
            CreateTenantIdArgument()
        };

        // Add command-specific arguments
        var subscriptionArg = CreateSubscriptionIdArgument();
        if (subscriptionArg != null)
        {
            fullChain.Add(subscriptionArg);
        }
        fullChain.AddRange(argumentDefinitions);

        _argumentChain = fullChain;
    }

    protected ArgumentChain<TArgs>? CreateSubscriptionIdArgument()
    {
        if (typeof(TArgs).IsSubclassOf(typeof(BaseArgumentsWithSubscription)))
        {
            return ArgumentChain<TArgs>
                .Create(ArgumentDefinitions.Common.Subscription.Name, ArgumentDefinitions.Common.Subscription.Description)
                .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.Common.Subscription))
                .WithValueAccessor(args => args.Subscription ?? string.Empty)
                .WithIsRequired(ArgumentDefinitions.Common.Subscription.Required);
        }

        return null;
    }
}