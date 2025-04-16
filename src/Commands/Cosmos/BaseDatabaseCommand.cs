// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models.Argument;
using System.CommandLine;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Cosmos;

public abstract class BaseDatabaseCommand<TArgs> : BaseCosmosCommand<TArgs> where TArgs : BaseDatabaseArguments, new()
{
    protected readonly Option<string> _databaseOption = ArgumentDefinitions.Cosmos.Database.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateDatabaseArgument());

    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Database = parseResult.GetValueForOption(_databaseOption);
        return args;
    }

    protected ArgumentBuilder<TArgs> CreateDatabaseArgument() =>
        ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Cosmos.Database.Name, ArgumentDefinitions.Cosmos.Database.Description)
            .WithValueAccessor(args => args.Database ?? string.Empty)
            .WithSuggestedValuesLoader(async (context, args) =>
                await GetDatabaseOptions(context, args.Account ?? string.Empty, args.Subscription ?? string.Empty))
            .WithIsRequired(ArgumentDefinitions.Cosmos.Database.Required);
}