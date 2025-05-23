// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseClusterCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : SubscriptionCommand<TArgs> where TArgs : BaseClusterArguments, new()
{
    protected readonly Option<string> _clusterNameOption = ArgumentDefinitions.Kusto.Cluster;
    protected readonly Option<string> _clusterUriOption = ArgumentDefinitions.Kusto.ClusterUri;

    protected static bool UseClusterUri(BaseClusterArguments args) => !string.IsNullOrEmpty(args.ClusterUri);

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_clusterUriOption);
        command.AddOption(_clusterNameOption);

        command.AddValidator(result =>
        {
            var validationResult = Validate(result);
            if (!validationResult.IsValid)
            {
                result.ErrorMessage = validationResult.ErrorMessage;
                return;
            }
        });
    }

    public override ValidationResult Validate(CommandResult parseResult)
    {
        var validationResult = new ValidationResult { IsValid = true };
        var clusterUri = parseResult.GetValueForOption(_clusterUriOption);
        var clusterName = parseResult.GetValueForOption(_clusterNameOption);
        if (!string.IsNullOrEmpty(clusterUri))
        {
            // If clusterUri is provided, subscription becomes optional
            return validationResult;
        }
        else
        {
            var subscription = parseResult.GetValueForOption(_subscriptionOption);

            // clusterUri not provided, require both subscription and clusterName
            if (string.IsNullOrEmpty(subscription) || string.IsNullOrEmpty(clusterName))
            {
                validationResult.IsValid = false;
                validationResult.ErrorMessage = $"Either --{_clusterUriOption.Name} must be provided, or both --{_subscriptionOption.Name} and --{_clusterNameOption.Name} must be provided.";
            }
        }

        if (validationResult.IsValid)
            return base.Validate(parseResult);

        return validationResult;
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.ClusterUri = parseResult.GetValueForOption(_clusterUriOption);
        args.ClusterName = parseResult.GetValueForOption(_clusterNameOption);

        return args;
    }
}
