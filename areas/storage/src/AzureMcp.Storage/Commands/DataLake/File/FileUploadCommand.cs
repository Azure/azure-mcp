// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Storage.Commands;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.DataLake.File;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Storage.Commands.DataLake.File;

public sealed class FileUploadCommand(ILogger<FileUploadCommand> logger) : BaseFileCommand<FileUploadOptions>
{
    private const string CommandTitle = "Upload File to Data Lake";
    private readonly ILogger<FileUploadCommand> _logger = logger;

    private readonly Option<string> _filePathOption = StorageOptionDefinitions.FilePath;
    private readonly Option<string> _sourceFilePathOption = StorageOptionDefinitions.SourceFilePath;
    private readonly Option<bool> _overwriteOption = StorageOptionDefinitions.Overwrite;

    public override string Name => "upload";

    public override string Description =>
        """
        Upload a file to Azure Data Lake Storage Gen2. This command uploads a local file to the specified
        path within a Data Lake file system. The command supports overwriting existing files with the 
        --overwrite option. Returns upload confirmation with file metadata including size and modification time.
          Required options:
        - account-name: The storage account name
        - file-system-name: The Data Lake file system name
        - file-path: The destination path in the Data Lake (e.g., 'data/myfile.txt')
        - source-file-path: The local file to upload
          Optional options:
        - overwrite: Whether to overwrite existing files (default: false)
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new()
    {
        Destructive = true,     // File upload can overwrite existing content
        ReadOnly = false        // This modifies storage by uploading content
    };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_filePathOption);
        command.AddOption(_sourceFilePathOption);
        command.AddOption(_overwriteOption);
    }

    protected override FileUploadOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.FilePath = parseResult.GetValueForOption(_filePathOption);
        options.SourceFilePath = parseResult.GetValueForOption(_sourceFilePathOption);
        options.Overwrite = parseResult.GetValueForOption(_overwriteOption);
        return options;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            // Required validation step
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);

            // Get the storage service from DI
            var storageService = context.GetService<IStorageService>();

            // Call service to upload file
            var result = await storageService.UploadFile(
                options.Account!,
                options.FileSystem!,
                options.FilePath!,
                options.SourceFilePath!,
                options.Overwrite,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            // Set results if upload was successful
            context.Response.Results = ResponseResult.Create(
                new FileUploadCommandResult(result),
                StorageJsonContext.Default.FileUploadCommandResult);
        }
        catch (Exception ex)
        {
            // Log error with all relevant context
            _logger.LogError(ex,
                "Error uploading file to Data Lake. Account: {Account}, FileSystem: {FileSystem}, FilePath: {FilePath}, SourceFilePath: {SourceFilePath}, Overwrite: {Overwrite}, Options: {@Options}",
                options.Account, options.FileSystem, options.FilePath, options.SourceFilePath, options.Overwrite, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    // Implementation-specific error handling
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "File system or storage account not found. Verify the account and file system exist and you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing the storage account. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx when reqEx.Status == 409 =>
            "File already exists. Use --overwrite option to replace the existing file.",
        FileNotFoundException =>
            "Source file not found. Verify the local file path exists and is accessible.",
        DirectoryNotFoundException =>
            "Source file directory not found. Verify the local file path is correct.",
        UnauthorizedAccessException =>
            "Access denied to the source file. Verify you have read permissions for the source file.",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        FileNotFoundException => 404,
        DirectoryNotFoundException => 404,
        UnauthorizedAccessException => 403,
        _ => base.GetStatusCode(ex)
    };

    // Strongly-typed result records
    internal record FileUploadCommandResult(FileUploadResult Result);
}
