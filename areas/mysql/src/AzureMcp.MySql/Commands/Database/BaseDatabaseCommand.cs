// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.MySql.Commands.Server;
using AzureMcp.MySql.Options;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands.Database;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseMySqlCommand<TOptions>> logger)
    : BaseServerCommand<TOptions>(logger) where TOptions : MySqlDatabaseOptions, new()
{
    private readonly Option<string> _databaseOption = MySqlOptionDefinitions.Database;

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
