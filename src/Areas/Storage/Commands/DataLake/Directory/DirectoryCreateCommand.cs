// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Storage.Commands;
using AzureMcp.Areas.Storage.Models;
using AzureMcp.Areas.Storage.Options.DataLake.Directory;
using AzureMcp.Areas.Storage.Services;
using AzureMcp.Commands.Storage;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Storage.Commands.DataLake.Directory;

public sealed class DirectoryCreateCommand(ILogger<DirectoryCreateCommand> logger) : BaseStorageCommand<DirectoryCreateOptions>
{
    private const string CommandTitle = "Create Data Lake Directory";
    private readonly ILogger<DirectoryCreateCommand> _logger = logger;

    private readonly Option<string> _directoryPathOption = StorageOptionDefinitions.DirectoryPath;

    public override string Name => "create";

    public override string Description =>
        """
        Create a directory in a Data Lake file system. This command creates a new directory at the specified path
        within the Data Lake file system. The directory path supports nested structures using forward slashes (/).
        If the directory already exists, the operation will succeed and return the existing directory information.
        Returns directory metadata including name, type, and creation timestamp as JSON.
          Required options:
        - account-name: The storage account name
        - file-system-name: The Data Lake file system name  
        - directory-path: The path where the directory should be created
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(StorageOptionDefinitions.FileSystem);
        command.AddOption(_directoryPathOption);
    }

    protected override DirectoryCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.FileSystemName = parseResult.GetValueForOption(StorageOptionDefinitions.FileSystem);
        options.DirectoryPath = parseResult.GetValueForOption(_directoryPathOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);
        
        // Combine file system name and directory path
        var fullDirectoryPath = string.IsNullOrEmpty(options.DirectoryPath) 
            ? options.FileSystemName! 
            : $"{options.FileSystemName}/{options.DirectoryPath}";

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            AddSubscriptionInformation(context.Activity, options);

            var storageService = context.GetService<IStorageService>();
            
            var directory = await storageService.CreateDirectory(
                options.Account!,
                fullDirectoryPath,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new DirectoryCreateCommandResult(directory),
                StorageJsonContext.Default.DirectoryCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating directory. Account: {Account}, FullDirectoryPath: {FullDirectoryPath}.", 
                options.Account, fullDirectoryPath);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record DirectoryCreateCommandResult(DataLakePathInfo Directory);
}