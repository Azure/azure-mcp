// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Cosmos;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Cosmos;

public sealed class DatabaseListCommand(ILogger<DatabaseListCommand> logger) : BaseCosmosCommand<DatabaseListArguments>()
{
    private const string _commandTitle = "List Cosmos DB Databases";
    private readonly ILogger<DatabaseListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List all databases in a Cosmos DB account. This command retrieves and displays all databases available
        in the specified Cosmos DB account. Results include database names and are returned as a JSON array.
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
            var databases = await cosmosService.ListDatabases(
                args.Account!,
                args.Subscription!,
                args.AuthMethod ?? AuthMethod.Credential,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = databases?.Count > 0 ?
                ResponseResult.Create(
                    new DatabaseListCommandResult(databases),
                    CosmosJsonContext.Default.DatabaseListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing databases. Account: {Account}.", args.Account);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record DatabaseListCommandResult(List<string> Databases);
}
