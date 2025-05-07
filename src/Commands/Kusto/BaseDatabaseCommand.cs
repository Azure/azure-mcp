// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseClusterCommand<TArgs> where TArgs : BaseDatabaseArguments, new()
{
    protected readonly Option<string> _databaseOption = ArgumentDefinitions.Kusto.Database.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Database = parseResult.GetValueForOption(_databaseOption);
        return args;
    }
}
