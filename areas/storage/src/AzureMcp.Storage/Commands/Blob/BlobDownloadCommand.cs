// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;
using BlobDownloadOptions = AzureMcp.Storage.Options.Blob.BlobDownloadOptions;

namespace AzureMcp.Storage.Commands.Blob;

public sealed class BlobDownloadCommand(ILogger<BlobDownloadCommand> logger) : BaseBlobCommand<BlobDownloadOptions>()
{
    private const string CommandTitle = "Download Storage Blob to local file";
    private readonly ILogger<BlobDownloadCommand> _logger = logger;

    private readonly Option<string> _localFilePathOption = StorageOptionDefinitions.LocalFilePath;
    private readonly Option<bool> _overwriteOption = StorageOptionDefinitions.Overwrite;

    public override string Name => "download";

    public override string Description =>
        $"""
        Downloads a blob to a file with the option to overwrite if the file already exists. Returns the blob name, 
        blob container name, download location, blob size, last modified time, ETag, MD5 of the downloaded blob, 
        and whether the download location was overwritten as JSON.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = true, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_localFilePathOption);
        command.AddOption(_overwriteOption);
    }

    protected override BlobDownloadOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.LocalFilePath = parseResult.GetValueForOption(_localFilePathOption);
        options.Overwrite = parseResult.GetValueForOption(_overwriteOption);
        return options;
    }

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
            var result = await storageService.DownloadBlob(
                options.Account!,
                options.Container!,
                options.Blob!,
                options.LocalFilePath!,
                options.Overwrite,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy
            );

            var commandResult = new BlobDownloadCommandResult(result);
            context.Response.Results = ResponseResult.Create(commandResult, StorageJsonContext.Default.BlobDownloadCommandResult);
            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error downloading blob. Account: {Account}, Container: {Container}, Blob: {Blob}, LocalPath: {LocalPath}, Overwrite: {Overwrite}.",
                options.Account, options.Container, options.Blob, options.LocalFilePath, options.Overwrite);
            HandleException(context, ex);
            return context.Response;
        }
    }

    internal record BlobDownloadCommandResult(BlobDownloadInfo DownloadInfo);
}
