// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Postgres.Commands;
using AzureMcp.Postgres.Options;
using AzureMcp.Postgres.Options.Server;
using AzureMcp.Postgres.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Postgres.Commands.Server;

public sealed class SetParamCommand(ILogger<SetParamCommand> logger) : BaseServerCommand<SetParamOptions>(logger)
{
    private const string CommandTitle = "Set PostgreSQL Server Parameter";
    private readonly Option<string> _paramOption = PostgresOptionDefinitions.Param;
    private readonly Option<string> _valueOption = PostgresOptionDefinitions.Value;
    public override string Name => "setparam";

    public override string Description =>
        "Sets a specific parameter of a PostgreSQL server to a certain value.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = true, ReadOnly = false };

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

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var options = BindOptions(parseResult);

            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

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
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record SetParamCommandResult(string Message, string Parameter, string Value);
}
