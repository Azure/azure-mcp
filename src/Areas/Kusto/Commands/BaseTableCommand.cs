// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Kusto.Options;
using AzureMcp.Commands;
using AzureMcp.Models.Option;

namespace AzureMcp.Areas.Kusto.Commands;

public abstract class BaseTableCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseDatabaseCommand<TOptions> where TOptions : BaseTableOptions, new()
{
    protected readonly Option<string> _tableOption = OptionDefinitions.Kusto.Table;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tableOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Table = parseResult.GetValueForOption(_tableOption);
        return options;
    }
}
