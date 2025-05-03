// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseContainerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseDatabaseCommand<TArgs> where TArgs : Arguments.Cosmos.BaseContainerArguments, new()
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
            .WithIsRequired(ArgumentDefinitions.Cosmos.Container.Required);
}
