// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Storage.Blobs.Models;
using AzureMcp.Arguments.Storage.Blob.Container;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Storage.Blob.Container;

public sealed class ContainerDetailsCommand(ILogger<ContainerDetailsCommand> logger) : BaseContainerCommand<ContainerDetailsArguments>()
{
    private const string _commandTitle = "Get Storage Container Details";
    private readonly ILogger<ContainerDetailsCommand> _logger = logger;

    public override string Name => "details";

    public override string Description =>
        $"""
        Get detailed properties of a storage container including metadata, lease status, and access level.
        Requires {ArgumentDefinitions.Storage.AccountName} and {ArgumentDefinitions.Storage.ContainerName}.
        """;

    public override string Title => _commandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var storageService = context.GetService<IStorageService>();
            var details = await storageService.GetContainerDetails(
                args.Account!,
                args.Container!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy
            );

            var result = new ContainerDetailsCommandResult(new JsonBlobContainerProperties(details));
            context.Response.Results = ResponseResult.Create(result, StorageJsonContext.Default.ContainerDetailsCommandResult);
            return context.Response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting container details. Account: {Account}, Container: {Container}.", args.Account, args.Container);
            HandleException(context.Response, ex);
            return context.Response;
        }
    }

    internal record ContainerDetailsCommandResult(JsonBlobContainerProperties Details);

    internal class JsonBlobContainerProperties
    {
        private readonly BlobContainerProperties props;

        internal JsonBlobContainerProperties(BlobContainerProperties props)
        {
            this.props = props;
        }

        public DateTimeOffset LastModified => props.LastModified;
        public LeaseStatus? LeaseStatus => props.LeaseStatus;
        public LeaseState? LeaseState => props.LeaseState;
        public LeaseDurationType? LeaseDuration => props.LeaseDuration;
        public PublicAccessType? PublicAccess => props.PublicAccess;
        public bool? HasImmutabilityPolicy => props.HasImmutabilityPolicy;
        public bool? HasLegalHold => props.HasLegalHold;
        public string DefaultEncryptionScope => props.DefaultEncryptionScope;
        public bool? PreventEncryptionScopeOverride => props.PreventEncryptionScopeOverride;
        public DateTimeOffset? DeletedOn => props.DeletedOn;
        public JsonETag ETag => new(props.ETag);
        public int? RemainingRetentionDays => props.RemainingRetentionDays;
        public IDictionary<string, string> Metadata => props.Metadata;
        public bool HasImmutableStorageWithVersioning => props.HasImmutableStorageWithVersioning;
    }

    [JsonConverter(typeof(ETagConverter))]
    internal struct JsonETag(Azure.ETag tag)
    {
        public void Write(Utf8JsonWriter writer)
        {
            var value = tag;
            if (value == default(Azure.ETag))
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(value.ToString("H"));
            }
        }
    }

    internal class ETagConverter : JsonConverter<JsonETag>
    {
        public override JsonETag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? s = reader.GetString();
            return new(s == null ? default(Azure.ETag) : new Azure.ETag(s));
        }

        public override void Write(Utf8JsonWriter writer, JsonETag value, JsonSerializerOptions options)
        {
            value.Write(writer);
        }
    }
}
