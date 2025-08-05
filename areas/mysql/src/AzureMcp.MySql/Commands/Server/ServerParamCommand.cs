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

public sealed class ServerParamCommand(ILogger<ServerParamCommand> logger) : BaseServerCommand<ServerParamOptions>(logger)
{
    private const string CommandTitle = "Get MySQL Server Parameter";
    private readonly Option<string> _paramOption = MySqlOptionDefinitions.Param;

    public override string Name => "param";

    public override string Description => "Gets a specific parameter of a MySQL server.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_paramOption);
    }

    protected override ServerParamOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Param = parseResult.GetValueForOption(_paramOption);
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

            IMySqlService mysqlService = context.GetService<IMySqlService>() ?? throw new InvalidOperationException("MySQL service is not available.");
            string paramValue = await mysqlService.GetServerParameterAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!, options.Param!);
            context.Response.Results = !string.IsNullOrEmpty(paramValue) ?
                ResponseResult.Create(
                    new ServerParamCommandResult(options.Param!, paramValue),
                    MySqlJsonContext.Default.ServerParamCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred getting server parameter.");
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record ServerParamCommandResult(string Parameter, string Value);
}
