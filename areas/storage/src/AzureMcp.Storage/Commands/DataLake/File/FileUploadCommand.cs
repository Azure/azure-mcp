// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Storage.Commands.DataLake.FileSystem;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.DataLake.File;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Storage.Commands.DataLake.File;

public sealed class FileUploadCommand(ILogger<FileUploadCommand> logger) : BaseFileSystemCommand<FileUploadOptions>
{
    private const string CommandTitle = "Upload File to Data Lake";
    private readonly ILogger<FileUploadCommand> _logger = logger;

    private readonly Option<string> _filePathOption = StorageOptionDefinitions.FilePath;
    private readonly Option<string> _localFilePathOption = StorageOptionDefinitions.LocalFilePath;

    public override string Name => "upload";

    public override string Description =>
        """
        Upload a local file to Azure Data Lake Storage Gen2. This command uploads a file from your local machine 
        to the specified path in a Data Lake file system. The command will overwrite existing files if they exist.
        Returns file metadata including name, size, last modified date, and ETag upon successful upload.
          Required options:
        - account-name: The Azure Storage account name
        - file-system-name: The Data Lake file system name  
        - file-path: Full path where the file will be stored (e.g., 'myfilesystem/data/myfile.txt')
        - local-file-path: Path to the local file to upload
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_filePathOption);
        command.AddOption(_localFilePathOption);
    }

    protected override FileUploadOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.FilePath = parseResult.GetValueForOption(_filePathOption);
        options.LocalFilePath = parseResult.GetValueForOption(_localFilePathOption);
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = false,
        Title = CommandTitle)]
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
            var uploadedFile = await storageService.UploadFile(
                options.Account!,
                options.FilePath!,
                options.LocalFilePath!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new FileUploadCommandResult(uploadedFile),
                StorageJsonContext.Default.FileUploadCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error uploading file. Account: {Account}, FilePath: {FilePath}, LocalFilePath: {LocalFilePath}, Options: {@Options}",
                options.Account, options.FilePath, options.LocalFilePath, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        FileNotFoundException => $"Local file not found. Verify the file path exists and you have read access to it. Details: {ex.Message}",
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "Data Lake file system not found. Verify the file system exists and you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing Data Lake. Verify you have Storage Blob Data Contributor role. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        FileNotFoundException => 400,
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record FileUploadCommandResult(DataLakePathInfo File);
}
