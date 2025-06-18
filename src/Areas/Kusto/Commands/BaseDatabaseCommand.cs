// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Option;
using AzureMcp.Options.Kusto;

namespace AzureMcp.Commands.Kusto;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseClusterCommand<TOptions> where TOptions : BaseDatabaseOptions, new()
{
    protected readonly Option<string> _databaseOption = OptionDefinitions.Kusto.Database;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Database = parseResult.GetValueForOption(_databaseOption);
        return options;
    }
}
