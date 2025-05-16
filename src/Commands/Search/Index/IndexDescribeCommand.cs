// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using Azure.Search.Documents.Indexes.Models;
using AzureMcp.Arguments.Search.Index;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Search.Index;

public sealed class IndexDescribeCommand(ILogger<IndexDescribeCommand> logger) : GlobalCommand<IndexDescribeArguments>()
{
    private const string _commandTitle = "Get Azure AI Search Index Details";
    private readonly ILogger<IndexDescribeCommand> _logger = logger;
    private readonly Option<string> _serviceOption = ArgumentDefinitions.Search.Service.ToOption();
    private readonly Option<string> _indexOption = ArgumentDefinitions.Search.Index.ToOption();

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

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateServiceArgument());
        AddArgument(CreateIndexArgument());
    }

    protected override IndexDescribeArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Service = parseResult.GetValueForOption(_serviceOption);
        args.Index = parseResult.GetValueForOption(_indexOption);
        return args;
    }

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

    private static ArgumentBuilder<IndexDescribeArguments> CreateServiceArgument() =>
        ArgumentBuilder<IndexDescribeArguments>
            .Create(ArgumentDefinitions.Search.Service.Name, ArgumentDefinitions.Search.Service.Description)
            .WithValueAccessor(args => args.Service ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Search.Service.Required);

    private static ArgumentBuilder<IndexDescribeArguments> CreateIndexArgument() =>
        ArgumentBuilder<IndexDescribeArguments>
            .Create(ArgumentDefinitions.Search.Index.Name, ArgumentDefinitions.Search.Index.Description)
            .WithValueAccessor(args => args.Index ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Search.Index.Required);
}
