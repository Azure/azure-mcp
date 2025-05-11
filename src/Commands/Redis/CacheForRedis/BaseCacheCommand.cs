// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Redis.CacheForRedis;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Redis.CacheForRedis;

public abstract class BaseCacheCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseCacheArguments, new()
{
    protected readonly Option<string> _cacheOption = ArgumentDefinitions.Redis.Cache.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_cacheOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateCacheArgument());
        AddArgument(CreateResourceGroupArgument());
    }

    protected override T BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Cache = parseResult.GetValueForOption(_cacheOption);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? ArgumentDefinitions.Common.ResourceGroup.DefaultValue;
        return args;
    }

    protected ArgumentBuilder<T> CreateCacheArgument() =>
        ArgumentBuilder<T>
            .Create(ArgumentDefinitions.Redis.Cache.Name, ArgumentDefinitions.Redis.Cache.Description)
            .WithValueAccessor(args => args.Cache ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Redis.Cache.Required);
}
