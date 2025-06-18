// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Models.Option;
using AzureMcp.Options.Postgres;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BasePostgresCommand<TOptions>> logger)
    : BaseServerCommand<TOptions>(logger) where TOptions : BasePostgresOptions, new()
{
    private readonly Option<string> _databaseOption = OptionDefinitions.Postgres.Database;

    public override string Name => "database";

    public override string Description =>
        "Retrieves information about a PostgreSQL database.";

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
