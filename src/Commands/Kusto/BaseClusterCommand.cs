// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseClusterCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : SubscriptionCommand<TArgs> where TArgs : BaseClusterArguments, new()
{
    protected readonly Option<string> _clusterNameOption = ArgumentDefinitions.Kusto.Cluster.ToOption();
    protected readonly Option<string> _clusterUriOption = ArgumentDefinitions.Kusto.ClusterUri.ToOption();

    protected static bool UseClusterUri(BaseClusterArguments args) => !string.IsNullOrEmpty(args.ClusterUri);

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_clusterUriOption);
        command.AddOption(_clusterNameOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateClusterUriArgument());
        AddArgument(CreateClusterNameArgument());

        var command = GetCommand();
        command.AddValidator(result =>
        {
            var clusterUri = result.GetValueForOption(_clusterUriOption);
            var clusterName = result.GetValueForOption(_clusterNameOption);

            if (!string.IsNullOrEmpty(clusterUri))
            {
                // If clusterUri is provided, make subscription optional
                _subscriptionOption.IsRequired = false;
                var subscriptionArgument = GetArguments()?.FirstOrDefault(arg => string.Equals(arg.Name, "subscription", StringComparison.OrdinalIgnoreCase));
                if (subscriptionArgument != null)
                {
                    subscriptionArgument.Required = false;
                }
            }
            else
            {
                var subscription = result.GetValueForOption(_subscriptionOption);

                // clusterUri not provided, require both subscription and clusterName
                if (string.IsNullOrEmpty(subscription) || string.IsNullOrEmpty(clusterName))
                {
                    result.ErrorMessage = $"Either --{_clusterUriOption.Name} must be provided, or both --{_subscriptionOption.Name} and --{_clusterNameOption.Name} must be provided.";
                }
            }
        });
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.ClusterUri = parseResult.GetValueForOption(_clusterUriOption);
        args.ClusterName = parseResult.GetValueForOption(_clusterNameOption);

        return args;
    }

    protected ArgumentBuilder<TArgs> CreateClusterUriArgument() =>
        ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Kusto.ClusterUri.Name, ArgumentDefinitions.Kusto.ClusterUri.Description)
            .WithValueAccessor(args => args.ClusterUri ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Kusto.ClusterUri.Required);

    protected ArgumentBuilder<TArgs> CreateClusterNameArgument() =>
        ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Kusto.Cluster.Name, ArgumentDefinitions.Kusto.Cluster.Description)
            .WithValueAccessor(args => args.ClusterName ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Kusto.Cluster.Required);
}
