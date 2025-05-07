// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseTableCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseDatabaseCommand<TArgs> where TArgs : BaseTableArguments, new()
{
    protected readonly Option<string> _tableOption = ArgumentDefinitions.Kusto.Table.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableOption);
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Table = parseResult.GetValueForOption(_tableOption);
        return args;
    }
}
