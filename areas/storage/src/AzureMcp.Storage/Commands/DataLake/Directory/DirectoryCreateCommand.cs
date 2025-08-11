// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.DataLake.Directory;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Storage.Commands.DataLake.Directory;

public sealed class DirectoryCreateCommand(ILogger<DirectoryCreateCommand> logger) : BaseStorageCommand<DirectoryCreateOptions>
{
    private const string CommandTitle = "Create Data Lake Directory";
    private readonly ILogger<DirectoryCreateCommand> _logger = logger;

    private readonly Option<string> _directoryPathOption = StorageOptionDefinitions.DirectoryPath;

    public override string Name => "create";

    public override string Description =>
        """
        Create a directory in a Data Lake file system. This command creates a new directory at the specified path
        within the Data Lake file system. The directory path must include the file system name as the first component
        (e.g., 'myfilesystem/data/logs' or 'myfilesystem/archives/2024'). The path supports nested structures using
        forward slashes (/). If the directory already exists, the operation will succeed and return the existing
        directory information. Returns directory metadata including name, type, and creation timestamp as JSON.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = true, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_directoryPathOption);
    }

    protected override DirectoryCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.DirectoryPath = parseResult.GetValueForOption(_directoryPathOption);
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

            context.Activity?.WithSubscriptionTag(options);

            var storageService = context.GetService<IStorageService>();

            var directory = await storageService.CreateDirectory(
                options.Account!,
                options.DirectoryPath!,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new DirectoryCreateCommandResult(directory),
                StorageJsonContext.Default.DirectoryCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating directory. Account: {Account}, DirectoryPath: {DirectoryPath}.",
                options.Account, options.DirectoryPath);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record DirectoryCreateCommandResult(DataLakePathInfo Directory);
}
