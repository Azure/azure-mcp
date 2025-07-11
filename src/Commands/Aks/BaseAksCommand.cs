using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Aks;

namespace AzureMcp.Commands.Aks;

public abstract class BaseAksCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : BaseAksOptions, new()
{
    protected readonly Option<string> _clusterOption = OptionDefinitions.Aks.Cluster;

    // Allow derived classes to specify if resource group is required
    protected virtual bool RequiresResourceGroup => true;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);

        // Add resource group option and set if it's required
        _resourceGroupOption.IsRequired = RequiresResourceGroup;
        command.AddOption(_resourceGroupOption);

        // Only add cluster option for commands that need it (BaseClusterOptions or derived)
        if (typeof(TOptions).IsAssignableTo(typeof(Options.Aks.Cluster.BaseClusterOptions)))
        {
            command.AddOption(_clusterOption);
        }
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);

        // Bind resource group option
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);

        // Only bind cluster-specific options for commands that need them
        if (options is Options.Aks.Cluster.BaseClusterOptions clusterOptions)
        {
            clusterOptions.Cluster = parseResult.GetValueForOption(_clusterOption);
        }

        return options;
    }
}
