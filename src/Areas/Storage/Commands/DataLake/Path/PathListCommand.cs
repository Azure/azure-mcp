// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.Storage.Models;
using AzureMcp.Areas.Storage.Options.DataLake.Path;
using AzureMcp.Areas.Storage.Services;
using AzureMcp.Commands.Storage;
using AzureMcp.Models.Command;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;
using System.CommandLine;

namespace AzureMcp.Areas.Storage.Commands.DataLake.Path;

public sealed class PathListCommand(ILogger<PathListCommand> logger) : BasePathCommand<PathListOptions>
{
    private const string CommandTitle = "List Data Lake Paths";
    private readonly ILogger<PathListCommand> _logger = logger;

    private readonly Option<string> _pathPrefixOption = StorageOptionDefinitions.PathPrefix;

    public override string Name => "list";

    public override string Description =>
        """
        List all paths, directories, and files in a Data Lake Storage filesystem. This tool recursively lists 
        all items within a specified path, including subdirectories and files with their metadata. Returns path 
        information as JSON. Requires account-name, file-system-name, and optional path prefix.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_pathPrefixOption);
    }

    protected override PathListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.PathPrefix = parseResult.GetValueForOption(_pathPrefixOption);
        return options;
    }

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

            context.Activity?.WithSubscriptionTag(options);

            var storageService = context.GetService<IStorageService>();
            var paths = await storageService.ListDataLakePathsWithPrefix(
                options.Account!,
                options.FileSystem!,
                options.PathPrefix,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new PathListCommandResult(paths ?? []),
                StorageJsonContext.Default.PathListCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing Data Lake paths. Account: {Account}, FileSystem: {FileSystem}, PathPrefix: {PathPrefix}.", 
                options.Account, options.FileSystem, options.PathPrefix);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record PathListCommandResult(List<DataLakePathInfo> Paths);
}