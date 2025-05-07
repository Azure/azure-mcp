// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Kusto;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Kusto;

public sealed class DatabaseListCommand : BaseClusterCommand<DatabaseListArguments>
{
    private readonly ILogger<DatabaseListCommand> _logger;

    public DatabaseListCommand(ILogger<DatabaseListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List all databases in a Kusto cluster. This command retrieves all databases available
         in the specified cluster and subscription. Result is a list of database names, returned as a JSON array.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);
        try
        {
            if (!await ProcessArguments(context, args))
                return context.Response;

            var kusto = context.GetService<IKustoService>();

            List<string> databasesNames = [];

            if (UseClusterUri(args))
            {
                databasesNames = await kusto.ListDatabases(
                    args.ClusterUri!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }
            else
            {
                databasesNames = await kusto.ListDatabases(
                    args.Subscription!,
                    args.ClusterName!,
                    args.Tenant,
                    args.AuthMethod,
                    args.RetryPolicy);
            }

            context.Response.Results = databasesNames?.Count > 0 ?
                ResponseResult.Create(new DatabaseListCommandResult(databasesNames), KustoJsonContext.Default.DatabaseListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing databases. Cluster: {Cluster}.", args.ClusterUri ?? args.ClusterName);
            HandleException(context.Response, ex);
        }
        return context.Response;
    }

    public record DatabaseListCommandResult(List<string> Databases);
}
