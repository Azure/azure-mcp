// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.MySql.Commands;
using AzureMcp.MySql.Json;
using AzureMcp.MySql.Options.Server;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql.Commands.Server;

public sealed class ServerConfigGetCommand(ILogger<ServerConfigGetCommand> logger) : BaseServerCommand<ServerConfigGetOptions>(logger)
{
    private const string CommandTitle = "Get MySQL Server Configuration";

    public override string Name => "config";

    public override string Description => "Retrieves comprehensive configuration details for the specified Azure Database for MySQL Flexible Server instance. This command provides insights into server settings, performance parameters, security configurations, and operational characteristics essential for database administration and optimization. Returns configuration data in JSON format including ServerName, Location, Version, SKU, StorageSizeGB, BackupRetentionDays, and GeoRedundantBackup properties.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

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
            string config = await mysqlService.GetServerConfigAsync(options.Subscription!, options.ResourceGroup!, options.User!, options.Server!);
            context.Response.Results = !string.IsNullOrEmpty(config) ?
                ResponseResult.Create(
                    new ServerConfigGetCommandResult(config),
                    MySqlJsonContext.Default.ServerConfigGetCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred getting server configuration.");
            HandleException(context, ex);
        }
        return context.Response;
    }

    internal record ServerConfigGetCommandResult(string Configuration);
}
