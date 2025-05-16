using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Postgres;
using AzureMcp.Models.Argument;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres;

public abstract class BaseDatabaseCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>(ILogger<BasePostgresCommand<TArgs>> logger)
    : BaseServerCommand<TArgs>(logger) where TArgs : BasePostgresArguments, new()
{
    private readonly Option<string> _databaseOption = ArgumentDefinitions.Postgres.Database.ToOption();

    public override string Name => "database";

    public override string Description =>
        "Retrieves information about a PostgreSQL database.";

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_databaseOption);
    }

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

    protected ArgumentBuilder<TArgs> CreateDatabaseArgument() =>
        ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Postgres.Database.Name, ArgumentDefinitions.Postgres.Database.Description)
            .WithValueAccessor(args => args.Database ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Postgres.Database.Required);
}
