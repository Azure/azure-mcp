// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Redis.Options.ManagedRedis;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;

namespace AzureMcp.Redis.Commands.ManagedRedis;

public abstract class BaseClusterCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseClusterOptions, new()
{
    protected readonly Option<string> _clusterOption = RedisOptionDefinitions.Cluster;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_clusterOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Cluster = parseResult.GetValueForOption(_clusterOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? "";
        return options;
    }
}
