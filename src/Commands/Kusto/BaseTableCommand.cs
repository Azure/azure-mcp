// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseTableCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseDatabaseCommand<TArgs> where TArgs : BaseTableArguments, new()
{
    protected readonly Option<string> _tableOption = ArgumentDefinitions.Kusto.Table;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableOption);
    }

    private static ArgumentBuilder<BaseTableArguments> CreateTableArgument() =>
        ArgumentBuilder<BaseTableArguments>
            .Create(ArgumentDefinitions.Kusto.Table.Name, ArgumentDefinitions.Kusto.Table.Description!)
            .WithValueAccessor(args => args.Table ?? string.Empty)
            .WithIsRequired(true);


    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateTableArgument());
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Table = parseResult.GetValueForOption(_tableOption);
        return args;
    }
}
