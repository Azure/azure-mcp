// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Option;
using AzureMcp.Options.Storage.Blob.Container;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Storage.Blob.Container;

public sealed class ContainerListCommand(ILogger<ContainerListCommand> logger) : BaseStorageCommand<ContainerListOptions>()
{
    private const string CommandTitle = "List Storage Containers";
    private readonly ILogger<ContainerListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all containers in a Storage account. This command retrieves and displays all containers available
        in the specified account. Results include container names and are returned as a JSON array.
        Requires {OptionDefinitions.Storage.AccountName}.
        """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            var storageService = context.GetService<IStorageService>();
            var containers = await storageService.ListContainers(
                options.Account!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = containers?.Count > 0
                ? ResponseResult.Create(
                    new ContainerListCommandResult(containers),
                    StorageJsonContext.Default.ContainerListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing containers. Account: {Account}.", options.Account);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record ContainerListCommandResult(List<string> Containers);
}
