using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Postgres;
using AzureMcp.Models.Argument;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres;

public abstract class BaseServerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>(ILogger<BasePostgresCommand<TArgs>> logger)
    : BasePostgresCommand<TArgs>(logger) where TArgs : BasePostgresArguments, new()

{
    private readonly Option<string> _serverOption = ArgumentDefinitions.Postgres.Server;

    public override string Name => "server";

    public override string Description =>
        "Retrieves information about a PostgreSQL server.";

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serverOption);
    }

    protected override TArgs BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Server = parseResult.GetValueForOption(_serverOption);
        return args;
    }
}
