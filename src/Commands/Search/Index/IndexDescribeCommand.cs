// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Azure.Search.Documents.Indexes.Models;
using AzureMcp.Arguments.Search.Index;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Search.Index;

public sealed class IndexDescribeCommand(ILogger<IndexDescribeCommand> logger) : GlobalCommand<IndexDescribeArguments>()
{
    private const string _commandTitle = "Get Azure AI Search Index Details";
    private readonly ILogger<IndexDescribeCommand> _logger = logger;
    private readonly Option<string> _serviceOption = ArgumentDefinitions.Search.Service;
    private readonly Option<string> _indexOption = ArgumentDefinitions.Search.Index;

    public override string Name => "describe";

    public override string Description =>
        """
        Get the full definition of an Azure AI Search index. Returns the complete index configuration including
        fields, analyzers, suggesters, scoring profiles, and other settings.

        Required arguments:
        - service-name: The name of the Azure AI Search service
        - index-name: The name of the search index to retrieve
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serviceOption);
        command.AddOption(_indexOption);
    }

    protected override IndexDescribeArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Service = parseResult.GetValueForOption(_serviceOption);
        args.Index = parseResult.GetValueForOption(_indexOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindOptions(parseResult);

        try
        {
            var validationResult = Validate(parseResult.CommandResult);

            if (!validationResult.IsValid)
            {
                context.Response.Status = 400;
                context.Response.Message = validationResult.ErrorMessage!;
                return context.Response;
            }

            var searchService = context.GetService<ISearchService>();

            var indexDefinition = await searchService.DescribeIndex(
                args.Service!,
                args.Index!,
                args.RetryPolicy);

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
