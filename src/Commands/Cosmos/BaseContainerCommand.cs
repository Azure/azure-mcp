// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseContainerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseDatabaseCommand<TArgs> where TArgs : Arguments.Cosmos.BaseContainerArguments, new()
{
    private readonly Option<string> _containerOption = ArgumentDefinitions.Cosmos.Container;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_containerOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Container = parseResult.GetValueForOption(_containerOption);
        return args;
    }
}
