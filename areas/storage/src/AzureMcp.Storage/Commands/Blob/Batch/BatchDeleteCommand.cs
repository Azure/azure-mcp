// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Storage.Commands.Blob.Container;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.Blob.Batch;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Storage.Commands.Blob.Batch;

public sealed class BatchDeleteCommand(ILogger<BatchDeleteCommand> logger) : BaseContainerCommand<BatchDeleteOptions>()
{
    private const string CommandTitle = "Delete Multiple Blobs";
    private readonly ILogger<BatchDeleteCommand> _logger = logger;

    private readonly Option<string[]> _blobNamesOption = StorageOptionDefinitions.BlobNames;

    public override string Name => "delete";

    public override string Description =>
        $"""
        Delete multiple blobs in a single batch operation. This tool efficiently deletes multiple 
        blobs simultaneously in a single request, reducing the number of API calls and improving 
        performance when removing multiple files. Requires {StorageOptionDefinitions.AccountName}, 
        {StorageOptionDefinitions.ContainerName}, and {StorageOptionDefinitions.BlobNames}.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = true, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_blobNamesOption);
    }

    protected override BatchDeleteOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.BlobNames = parseResult.GetValueForOption(_blobNamesOption);
        return options;
    }

    [McpServerTool(Destructive = true, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            var storageService = context.GetService<IStorageService>();
            var result = await storageService.DeleteBlobsBatch(
                options.Account!,
                options.Container!,
                options.BlobNames!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new BatchDeleteCommandResult(result.SuccessfulBlobs, result.FailedBlobs),
                null); // TODO: Add to StorageJsonContext when build completes
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error deleting blobs batch. Account: {Account}, Container: {Container}, BlobCount: {BlobCount}.",
                options.Account, options.Container, options.BlobNames?.Length ?? 0);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record BatchDeleteCommandResult(List<string> SuccessfulBlobs, List<string> FailedBlobs);
}
