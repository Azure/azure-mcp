// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Postgres.Server;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres.Server;

public sealed class GetParamCommand(ILogger<GetParamCommand> logger) : BaseServerCommand<GetParamArguments>(logger)
{
    private const string _commandTitle = "Get PostgreSQL Server Parameter";
    private readonly Option<string> _paramOption = ArgumentDefinitions.Postgres.Param;
    public override string Name => "param";

    public override string Description =>
        "Retrieves a specific parameter of a PostgreSQL server.";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_paramOption);
    }

    protected override GetParamArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Param = parseResult.GetValueForOption(_paramOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var args = BindOptions(parseResult);

            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
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

    internal record GetParamCommandResult(string ParameterValue);
}
