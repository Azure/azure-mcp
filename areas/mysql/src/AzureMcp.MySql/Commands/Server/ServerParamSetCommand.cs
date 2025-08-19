// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.MySql.Commands;
using AzureMcp.MySql.Json;
using AzureMcp.MySql.Options;
using AzureMcp.MySql.Options.Server;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands.Server;

public sealed class ServerParamSetCommand(ILogger<ServerParamSetCommand> logger) : BaseServerCommand<ServerParamSetOptions>(logger)
{
    private const string CommandTitle = "Set MySQL Server Parameter";
    private readonly Option<string> _paramOption = MySqlOptionDefinitions.Param;
    private readonly Option<string> _valueOption = MySqlOptionDefinitions.Value;

    public override string Name => "set";

    public override string Description => "Sets/updates a MySQL server configuration parameter to a new value to optimize performance, security, or operational behavior. This command enables fine-tuned configuration management with validation to ensure parameter changes are compatible with the server's current state and constraints.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = true, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_paramOption);
        command.AddOption(_valueOption);
    }

    protected override ServerParamSetOptions BindOptions(ParseResult parseResult)
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

            IMySqlService mysqlService = context.GetService<IMySqlService>() ?? throw new InvalidOperationException("MySQL service is not available.");
            string result = await mysqlService.SetServerParameterAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Param!, options.Value!);
            context.Response.Results = !string.IsNullOrEmpty(result) ?
                ResponseResult.Create(
                    new ServerParamSetCommandResult(options.Param!, result),
                    MySqlJsonContext.Default.ServerParamSetCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred setting server parameter.");
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record ServerParamSetCommandResult(string Parameter, string Value);
}
