// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Argument;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseQueryCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>
    : BaseDatabaseCommand<TArgs> where TArgs : QueryArguments, new()
{
    protected readonly Option<string> _queryOption = ArgumentDefinitions.Kusto.Query.ToOption();


    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_queryOption);
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Query = parseResult.GetValueForOption(_queryOption);
        return args;
    }
}
