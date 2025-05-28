// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseClusterCommand<TArgs> where TArgs : BaseDatabaseArguments, new()
{
    protected readonly Option<string> _databaseOption = ArgumentDefinitions.Kusto.Database;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
    }

    private static ArgumentBuilder<BaseDatabaseArguments> CreateDatabaseArgument() =>
        ArgumentBuilder<BaseDatabaseArguments>
            .Create(ArgumentDefinitions.Kusto.Database.Name, ArgumentDefinitions.Kusto.Database.Description!)
            .WithValueAccessor(args => args.Database ?? string.Empty)
            .WithIsRequired(true);

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
}
