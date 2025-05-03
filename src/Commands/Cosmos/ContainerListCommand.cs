// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Cosmos;
using AzureMcp.Models;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Cosmos;

public sealed class ContainerListCommand(ILogger<ContainerListCommand> logger) : BaseDatabaseCommand<ContainerListArguments>()
{
    private readonly ILogger<ContainerListCommand> _logger = logger;

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        """
        List all containers in a Cosmos DB database. This command retrieves and displays all containers within
        the specified database and Cosmos DB account. Results include container names and are returned as a
        JSON array. You must specify both an account name and a database name.
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
            var containers = await cosmosService.ListContainers(
                args.Account!,
                args.Database!,
                args.Subscription!,
                args.AuthMethod ?? AuthMethod.Credential,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = containers?.Count > 0 ?
                ResponseResult.Create(
                    new ContainerListCommandResult(containers),
                    CosmosJsonContext.Default.ContainerListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing containers for Cosmos DB database.");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ContainerListCommandResult(IReadOnlyList<string> Containers);
}
