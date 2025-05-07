// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.Postgres.Server;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Postgres.Server;

public sealed class GetParamCommand(ILogger<GetParamCommand> logger) : BaseServerCommand<GetParamArguments>(logger)
{
    private readonly Option<string> _paramOption = ArgumentDefinitions.Postgres.Param.ToOption();
    protected override string GetCommandName() => "param";

    protected override string GetCommandDescription() =>
        "Retrieves a specific parameter of a PostgreSQL server.";

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_paramOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateParamArgument());
    }

    protected override GetParamArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Param = parseResult.GetValueForOption(_paramOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var args = BindArguments(parseResult);

            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            var parameterValue = await pgService.GetServerParameterAsync(args.Subscription!, args.ResourceGroup!, args.User!, args.Server!, args.Param!);
            context.Response.Results = parameterValue?.Length > 0 ?
                ResponseResult.Create(
                    new GetParamCommandResult(parameterValue),
                    PostgresJsonContext.Default.GetParamCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred retrieving the parameter.");
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    private static ArgumentBuilder<GetParamArguments> CreateParamArgument() =>
        ArgumentBuilder<GetParamArguments>
            .Create(ArgumentDefinitions.Postgres.Param.Name, ArgumentDefinitions.Postgres.Param.Description)
            .WithValueAccessor(args => args.Param ?? string.Empty)
            .WithIsRequired(true);

    internal record GetParamCommandResult(string ParameterValue);
}
