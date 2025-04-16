// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Storage.Blob.Container;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Storage.Blob.Container;

public sealed class ContainerListCommand : BaseStorageCommand<ContainerListArguments>
{
    private readonly ILogger<ContainerListCommand> _logger;

    public ContainerListCommand(ILogger<ContainerListCommand> logger) : base()
    {
        _logger = logger;
    }

    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        $"""
        List all containers in a Storage account. This command retrieves and displays all containers available
        in the specified account. Results include container names and are returned as a JSON array.
        Requires {Models.Argument.ArgumentDefinitions.Storage.AccountName}.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var args = BindArguments(commandOptions);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var storageService = context.GetService<IStorageService>();
            var containers = await storageService.ListContainers(
                args.Account!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = containers?.Count > 0 ? new { containers } : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing containers. Account: {Account}.", args.Account);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}