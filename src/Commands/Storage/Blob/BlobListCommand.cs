// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Storage.Blob.Container;
using AzureMcp.Models.Option;
using AzureMcp.Options.Storage.Blob;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Storage.Blob;

public sealed class BlobListCommand(ILogger<BlobListCommand> logger) : BaseContainerCommand<BlobListOptions>()
{
    private const string CommandTitle = "List Storage Blobs";
    private readonly ILogger<BlobListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List all blobs in a Storage container. This command retrieves and displays all blobs available
        in the specified container and Storage account. Results include blob names, sizes, and content types,
        returned as a JSON array. Requires {OptionDefinitions.Storage.AccountName} and
        {OptionDefinitions.Storage.ContainerName}.
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
            var blobs = await storageService.ListBlobs(
                options.Account!,
                options.Container!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = blobs?.Count > 0
                ? ResponseResult.Create(new BlobListCommandResult(blobs), StorageJsonContext.Default.BlobListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing storage blobs.  Account: {Account}, Container: {Container}.", options.Account, options.Container);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record BlobListCommandResult(List<string> Blobs);
}
