// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Option;
using AzureMcp.Options.Postgres.Server;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Postgres.Server;

public sealed class SetParamCommand(ILogger<SetParamCommand> logger) : BaseServerCommand<SetParamOptions>(logger)
{
    private const string _commandTitle = "Set PostgreSQL Server Parameter";
    private readonly Option<string> _paramOption = OptionDefinitions.Postgres.Param;
    private readonly Option<string> _valueOption = OptionDefinitions.Postgres.Value;
    public override string Name => "set-param";

    public override string Description =>
        "Sets a specific parameter of a PostgreSQL server to a certain value.";

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_paramOption);
        command.AddOption(_valueOption);
    }

    protected override SetParamOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Param = parseResult.GetValueForOption(_paramOption);
        options.Value = parseResult.GetValueForOption(_valueOption);
        return options;
    }

    [McpServerTool(Destructive = true, ReadOnly = false, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var options = BindOptions(parseResult);

            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            IPostgresService pgService = context.GetService<IPostgresService>() ?? throw new InvalidOperationException("PostgreSQL service is not available.");
            var result = await pgService.SetServerParameterAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Param!, options.Value!);
            context.Response.Results = !string.IsNullOrEmpty(result) ?
                ResponseResult.Create(
                    new SetParamCommandResult(result, options.Param!, options.Value!),
                    PostgresJsonContext.Default.SetParamCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred setting the parameter.");
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    internal record SetParamCommandResult(string Message, string Parameter, string Value);
}
