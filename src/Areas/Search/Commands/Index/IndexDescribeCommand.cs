// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Azure.Search.Documents.Indexes.Models;
using AzureMcp.Areas.Search.Options;
using AzureMcp.Areas.Search.Options.Index;
using AzureMcp.Areas.Search.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Search.Commands.Index;

public sealed class IndexDescribeCommand(ILogger<IndexDescribeCommand> logger) : GlobalCommand<IndexDescribeOptions>()
{
    private const string CommandTitle = "Get Azure AI Search Index Details";
    private readonly ILogger<IndexDescribeCommand> _logger = logger;
    private readonly Option<string> _serviceOption = SearchOptionDefinitions.Service;
    private readonly Option<string> _indexOption = SearchOptionDefinitions.Index;

    public override string Name => "describe";

    public override string Description =>
        """
        Get the full definition of an Azure AI Search index. Returns the complete index configuration including
        fields, analyzers, suggesters, scoring profiles, and other settings.

        Required arguments:
        - service-name: The name of the Azure AI Search service
        - index-name: The name of the search index to retrieve
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serviceOption);
        command.AddOption(_indexOption);
    }

    protected override IndexDescribeOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Service = parseResult.GetValueForOption(_serviceOption);
        options.Index = parseResult.GetValueForOption(_indexOption);
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

            var searchService = context.GetService<ISearchService>();

            var indexDefinition = await searchService.DescribeIndex(
                options.Service!,
                options.Index!,
                options.RetryPolicy);

            context.Response.Results = indexDefinition != null
                ? ResponseResult.Create(new(indexDefinition), SearchJsonContext.Default.IndexDescribeCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving search index definition");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    public sealed record IndexDescribeCommandResult(SearchIndexProxy Index);

    /// <summary>
    /// This record represents the JSON-serialized form of <see cref="SearchIndex"/>
    /// </summary>
    public sealed record SearchIndexProxy()
    {
        public required string Name { get; init; }
        public required List<SearchFieldProxy> Fields { get; init; }

        public sealed record SearchFieldProxy()
        {
            public required string Name { get; init; }
            public required SearchFieldDataType Type { get; init; }
            public bool? Key { get; init; }
            public bool? Searchable { get; init; }
            public bool? Filterable { get; init; }

            [SetsRequiredMembers]
            public SearchFieldProxy(SearchField field) : this()
            {
                Name = field.Name;
                Type = field.Type;
                Key = field.IsKey;
                Searchable = field.IsSearchable;
                Filterable = field.IsFilterable;
            }
        }

        [SetsRequiredMembers]
        public SearchIndexProxy(SearchIndex index) : this()
        {
            Name = index.Name;
            Fields = index.Fields.Select(field => new SearchFieldProxy(field)).ToList();
        }
    }
}
