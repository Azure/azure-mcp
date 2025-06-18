// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.Redis.CacheForRedis;

namespace AzureMcp.Commands.Redis.CacheForRedis;

public abstract class BaseCacheCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseCacheOptions, new()
{
    protected readonly Option<string> _cacheOption = OptionDefinitions.Redis.Cache;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_cacheOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Cache = parseResult.GetValueForOption(_cacheOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? "";
        return options;
    }
}
