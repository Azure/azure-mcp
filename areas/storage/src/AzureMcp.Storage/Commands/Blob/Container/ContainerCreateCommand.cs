// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Storage.Blobs.Models;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Services.Telemetry;
using AzureMcp.Storage.Options;
using AzureMcp.Storage.Options.Blob.Container;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Storage.Commands.Blob.Container;

public sealed class ContainerCreateCommand(ILogger<ContainerCreateCommand> logger) : BaseContainerCommand<ContainerCreateOptions>()
{
    private const string CommandTitle = "Create Storage Blob Container";
    private readonly ILogger<ContainerCreateCommand> _logger = logger;

    private readonly Option<string> _blobContainerPublicAccessOption = StorageOptionDefinitions.BlobContainerPublicAccess;

    public override string Name => "create";

    public override string Description =>
        """
        Creates a blob container with optional blob public access. Returns the last modified time and the ETag of the blob container as JSON.
        Requires account and container.
          Required options:
        - account: Storage account name
        - container: Container name
          Optional options:
        - blob-container-public-access: Public access level (blob or container)
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_blobContainerPublicAccessOption);
    }

    protected override ContainerCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.BlobContainerPublicAccess = parseResult.GetValueForOption(_blobContainerPublicAccessOption);
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
            var containerProperties = await storageService.CreateContainer(
                options.Account!,
                options.Container!,
                options.Subscription!,
                options.BlobContainerPublicAccess,
                options.Tenant,
                options.RetryPolicy);

            var result = new ContainerCreateResult(
                containerProperties.LastModified,
                containerProperties.ETag.ToString(),
                containerProperties.PublicAccess);

            context.Response.Results = ResponseResult.Create(
                new ContainerCreateCommandResult(result),
                StorageJsonContext.Default.ContainerCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error creating container. Account: {Account}, Container: {Container}, PublicAccess: {PublicAccess}, Options: {@Options}",
                options.Account, options.Container, options.BlobContainerPublicAccess, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 409 =>
            "Container already exists. Use a different container name or check if you have access to the existing container.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing the storage account. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "Storage account not found. Verify the account exists and you have access.",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record ContainerCreateResult(
        DateTimeOffset LastModified,
        string ETag,
        PublicAccessType? PublicAccess);

    internal record ContainerCreateCommandResult(ContainerCreateResult Container);
}
