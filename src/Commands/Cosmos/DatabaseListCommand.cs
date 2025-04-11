// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Cosmos;

public sealed class DatabaseListCommand : BaseCosmosCommand<DatabaseListArguments>
{
    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List all databases in a Cosmos DB account. This command retrieves and displays all databases available 
        in the specified Cosmos DB account. Results include database names and are returned as a JSON array.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var cosmosService = context.GetService<ICosmosService>();
            var databases = await cosmosService.ListDatabases(
                args.Account!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = databases?.Count > 0 ?
                new { databases } :
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}