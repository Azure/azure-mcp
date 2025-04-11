// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Storage.Blob;
using AzureMcp.Commands.Storage.Blob.Container;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;
using System.CommandLine.Parsing;

namespace AzureMcp.Commands.Storage.Blob;

public sealed class BlobListCommand : BaseContainerCommand<BlobListArguments>
{
    protected override string GetCommandName() => "list";

    protected override string GetCommandDescription() =>
        $"""
        List all blobs in a Storage container. This command retrieves and displays all blobs available
        in the specified container and Storage account. Results include blob names, sizes, and content types,
        returned as a JSON array. Requires {Models.Argument.ArgumentDefinitions.Storage.AccountName} and 
        {Models.Argument.ArgumentDefinitions.Storage.ContainerName}.
        """;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions)
    {
        var args = BindArguments(commandOptions);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var storageService = context.GetService<IStorageService>();
            var blobs = await storageService.ListBlobs(
                args.Account!,
                args.Container!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = blobs?.Count > 0 ? new { blobs } : null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}