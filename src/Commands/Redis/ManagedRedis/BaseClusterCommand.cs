// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Redis.ManagedRedis;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Redis.ManagedRedis;

public abstract class BaseClusterCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseClusterArguments, new()
{
    protected readonly Option<string> _clusterOption = ArgumentDefinitions.Redis.Cluster.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_clusterOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateClusterArgument());
        AddArgument(CreateResourceGroupArgument());
    }

    protected override T BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Cluster = parseResult.GetValueForOption(_clusterOption);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? ArgumentDefinitions.Common.ResourceGroup.DefaultValue;
        return args;
    }

    protected ArgumentBuilder<T> CreateClusterArgument() =>
        ArgumentBuilder<T>
            .Create(ArgumentDefinitions.Redis.Cluster.Name, ArgumentDefinitions.Redis.Cluster.Description)
            .WithValueAccessor(args => args.Cluster ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Redis.Cluster.Required);
}
