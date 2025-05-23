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
    protected readonly Option<string> _cacheOption = ArgumentDefinitions.Redis.Cache;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_cacheOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override T BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Cache = parseResult.GetValueForOption(_cacheOption);
        args.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption) ?? "";
        return args;
    }
}
