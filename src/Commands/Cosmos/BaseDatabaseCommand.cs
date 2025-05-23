// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseCosmosCommand<TArgs> where TArgs : BaseDatabaseArguments, new()
{
    protected readonly Option<string> _databaseOption = ArgumentDefinitions.Cosmos.Database;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Database = parseResult.GetValueForOption(_databaseOption);
        return args;
    }
}
