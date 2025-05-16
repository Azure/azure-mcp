using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using AzureMcp.Arguments.Postgres;
using AzureMcp.Models.Argument;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres;

public abstract class BaseServerCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TArgs>(ILogger<BasePostgresCommand<TArgs>> logger)
    : BasePostgresCommand<TArgs>(logger) where TArgs : BasePostgresArguments, new()

{
    private readonly Option<string> _serverOption = ArgumentDefinitions.Postgres.Server.ToOption();

    public override string Name => "server";

    public override string Description =>
        "Retrieves information about a PostgreSQL server.";

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serverOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateServerArgument());
    }

    protected override TArgs BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Server = parseResult.GetValueForOption(_serverOption);
        return args;
    }

    protected ArgumentBuilder<TArgs> CreateServerArgument() =>
        ArgumentBuilder<TArgs>
            .Create(ArgumentDefinitions.Postgres.Server.Name, ArgumentDefinitions.Postgres.Server.Description)
            .WithValueAccessor(args => args.Server ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Postgres.Server.Required);
}
