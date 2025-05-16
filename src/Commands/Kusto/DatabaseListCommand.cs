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
    private const string _commandTitle = "List Kusto Databases";
    private readonly ILogger<DatabaseListCommand> _logger;

    public DatabaseListCommand(ILogger<DatabaseListCommand> logger) : base()
    {
        _logger = logger;
    }

    public override string Name => "list";

    public override string Description =>
        """
        List all databases in a Kusto cluster. Requires `cluster-uri` ( or `subscription` and `cluster-name`). Result is a list of database names, returned as a JSON array.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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
