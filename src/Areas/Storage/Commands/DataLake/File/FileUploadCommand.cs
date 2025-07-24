// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Storage.Commands;
using AzureMcp.Areas.Storage.Models;
using AzureMcp.Areas.Storage.Options;
using AzureMcp.Areas.Storage.Options.DataLake.File;
using AzureMcp.Areas.Storage.Services;
using AzureMcp.Commands.Storage;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Storage.Commands.DataLake.File;

public sealed class FileUploadCommand(ILogger<FileUploadCommand> logger) : BaseStorageCommand<FileUploadOptions>
{
    private const string CommandTitle = "Upload File to Data Lake";
    private readonly ILogger<FileUploadCommand> _logger = logger;

    private readonly Option<string> _fileSystemOption = StorageOptionDefinitions.FileSystem;
    private readonly Option<string> _filePathOption = StorageOptionDefinitions.FilePath;
    private readonly Option<string> _localFilePathOption = StorageOptionDefinitions.LocalFilePath;

    public override string Name => "upload";

    public override string Description =>
        """
        Upload a local file to Azure Data Lake Storage Gen2. This command uploads a file from the local
        file system to the specified path in a Data Lake file system. The file will be uploaded to the
        specified path within the file system (e.g., 'data/logs/app.log' or 'archives/backup.zip').
        If the file already exists at the destination, it will be overwritten. The local file path can
        be absolute or relative. Returns file metadata including name, size, and upload timestamp as JSON.
          Required options:
        - account-name: The Azure Storage account name
        - file-system-name: The Data Lake file system name
        - file-path: Destination path in the file system
        - local-file-path: Path to the local file to upload
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_fileSystemOption);
        command.AddOption(_filePathOption);
        command.AddOption(_localFilePathOption);
    }

    protected override FileUploadOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.FileSystem = parseResult.GetValueForOption(_fileSystemOption);
        options.FilePath = parseResult.GetValueForOption(_filePathOption);
        options.LocalFilePath = parseResult.GetValueForOption(_localFilePathOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
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

            var file = await storageService.UploadFile(
                options.Account!,
                options.FileSystem!,
                options.FilePath!,
                options.LocalFilePath!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new FileUploadCommandResult(file),
                StorageJsonContext.Default.FileUploadCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file. Account: {Account}, FileSystem: {FileSystem}, FilePath: {FilePath}, LocalFilePath: {LocalFilePath}.",
                options.Account, options.FileSystem, options.FilePath, options.LocalFilePath);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record FileUploadCommandResult(DataLakePathInfo File);
}
