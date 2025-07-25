// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Storage.Commands;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Options.DataLake.FileSystem;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Storage.Commands.DataLake.FileSystem;

public sealed class FileSystemListPathsCommand(ILogger<FileSystemListPathsCommand> logger) : BaseFileSystemCommand<ListPathsOptions>
{
    private const string CommandTitle = "List Data Lake File System Paths";
    private readonly ILogger<FileSystemListPathsCommand> _logger = logger;

    public override string Name => "list-paths";

    public override string Description =>
        """
        List all paths in a Data Lake file system. This command retrieves and displays all paths (files and directories)
        available in the specified Data Lake file system within the storage account. Results include path names, 
        types (file or directory), and metadata, returned as a JSON array. Requires account-name and file-system-name.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

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
            var paths = await storageService.ListDataLakePaths(
                options.Account!,
                options.FileSystem!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new FileSystemListPathsCommandResult(paths ?? []),
                StorageJsonContext.Default.FileSystemListPathsCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing Data Lake file system paths. Account: {Account}, FileSystem: {FileSystem}.", options.Account, options.FileSystem);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record FileSystemListPathsCommandResult(List<DataLakePathInfo> Paths);
}
