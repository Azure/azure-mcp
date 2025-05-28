// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Cosmos;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Cosmos;

public sealed class ContainerListCommand(ILogger<ContainerListCommand> logger) : BaseDatabaseCommand<ContainerListArguments>()
{
    private const string _commandTitle = "List Cosmos DB Containers";
    private readonly ILogger<ContainerListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all containers in a Cosmos DB database. This command retrieves and displays all containers within
        the specified database and Cosmos DB account. Results include container names and are returned as a
        JSON array. You must specify both an account name and a database name.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
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
