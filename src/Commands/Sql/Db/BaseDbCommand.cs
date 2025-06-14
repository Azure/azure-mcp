// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Sql.Server;
using AzureMcp.Models.Option;
using AzureMcp.Options.Sql.Database;

namespace AzureMcp.Commands.Sql.Db;

public abstract class BaseDbCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseServerCommand<TOptions> where TOptions : BaseDatabaseOptions, new()
{
    private readonly Option<string> _databaseOption = OptionDefinitions.Sql.Database;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
        command.AddOption(_resourceGroupOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Database = parseResult.GetValueForOption(_databaseOption);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        return options;
    }
}
