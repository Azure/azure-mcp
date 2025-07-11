// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Sql.Models;
using AzureMcp.Areas.Sql.Options.ElasticPool;
using AzureMcp.Areas.Sql.Services;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Sql.Commands.ElasticPool;

public sealed class ElasticPoolListCommand(ILogger<ElasticPoolListCommand> logger)
    : BaseElasticPoolCommand<ElasticPoolListOptions>(logger)
{
    private const string CommandTitle = "List SQL Elastic Pools";

    public override string Name => "list";

    public override string Description =>
        """
        List all elastic pools in an Azure SQL Server. This command retrieves information about all elastic pools
        configured for a specific SQL server including their configuration, state, and capacity settings.
        Equivalent to 'az sql elastic-pool list'.
        Returns a list of elastic pools with their properties including SKU, state, capacity, and database settings.
          Required options:
        - subscription: Azure subscription ID
        - resource-group: Resource group name containing the SQL server
        - server: Azure SQL Server name
        """;

    public override string Title => CommandTitle;

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            AddSubscriptionInformation(context.Activity, options);

            var sqlService = context.GetService<ISqlService>();

            var elasticPools = await sqlService.GetElasticPoolsAsync(
                options.Server!,
                options.ResourceGroup!,
                options.Subscription!,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new ElasticPoolListResult(elasticPools),
                SqlJsonContext.Default.ElasticPoolListResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing SQL elastic pools. Server: {Server}, ResourceGroup: {ResourceGroup}, Options: {@Options}",
                options.Server, options.ResourceGroup, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "SQL server not found. Verify the server name, resource group, and that you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing the SQL server. Verify you have appropriate permissions. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record ElasticPoolListResult(List<SqlElasticPool> ElasticPools);
}