// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Arguments.Storage.Blob;
using AzureMcp.Commands.Storage.Blob.Container;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Storage.Blob;

public sealed class BlobListCommand(ILogger<BlobListCommand> logger) : BaseContainerCommand<BlobListArguments>()
{
    private const string _commandTitle = "List Storage Blobs";
    private readonly ILogger<BlobListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all blobs in a Storage container. This command retrieves and displays all blobs available
        in the specified container and Storage account. Results include blob names, sizes, and content types,
        returned as a JSON array. Requires {Models.Argument.ArgumentDefinitions.Storage.AccountName} and
        {Models.Argument.ArgumentDefinitions.Storage.ContainerName}.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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
            var blobs = await storageService.ListBlobs(
                args.Account!,
                args.Container!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = blobs?.Count > 0
                ? ResponseResult.Create(new BlobListCommandResult(blobs), StorageJsonContext.Default.BlobListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing storage blobs.  Account: {Account}, Container: {Container}.", args.Account, args.Container);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record BlobListCommandResult(List<string> Blobs);
}
