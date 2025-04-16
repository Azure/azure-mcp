// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseContainerCommand<TArgs> : BaseDatabaseCommand<TArgs> where TArgs : Arguments.Cosmos.BaseContainerArguments, new()
{
    private readonly Option<string> _containerOption = ArgumentDefinitions.Cosmos.Container.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_containerOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();

        AddArgument(CreateContainerArgument());
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Container = parseResult.GetValueForOption(_containerOption);
        return args;
    }

    private ArgumentBuilder<Arguments.Cosmos.BaseContainerArguments> CreateContainerArgument() =>
        ArgumentBuilder<Arguments.Cosmos.BaseContainerArguments>
            .Create(ArgumentDefinitions.Cosmos.Container.Name, ArgumentDefinitions.Cosmos.Container.Description)
            .WithValueAccessor(args => args.Container ?? string.Empty)
            .WithSuggestedValuesLoader(async (context, args) =>
                await GetContainerOptions(
                    context,
                    args.Account ?? string.Empty,
                    args.Database ?? string.Empty,
                    args.Subscription ?? string.Empty))
            .WithIsRequired(ArgumentDefinitions.Cosmos.Container.Required);
}