// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments.Search.Index;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.Search.Index;

public sealed class IndexQueryCommand(ILogger<IndexQueryCommand> logger) : GlobalCommand<IndexQueryArguments>()
{
    private readonly ILogger<IndexQueryCommand> _logger = logger;
    private readonly Option<string> _serviceOption = ArgumentDefinitions.Search.Service.ToOption();
    private readonly Option<string> _indexOption = ArgumentDefinitions.Search.Index.ToOption();
    private readonly Option<string> _queryOption = ArgumentDefinitions.Search.Query.ToOption();

    protected override string GetCommandName() => "query";

    protected override string GetCommandDescription() =>
        """
        Query an Azure AI Search index. Returns search results matching the specified query.

        Required arguments:
        - service-name: The name of the Azure AI Search service
        - index-name: The name of the search index to query
        - query: The search text to query with
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serviceOption);
        command.AddOption(_indexOption);
        command.AddOption(_queryOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateServiceArgument());
        AddArgument(CreateIndexArgument());
        AddArgument(CreateQueryArgument());
    }

    protected override IndexQueryArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Service = parseResult.GetValueForOption(_serviceOption);
        args.Index = parseResult.GetValueForOption(_indexOption);
        args.Query = parseResult.GetValueForOption(_queryOption);
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

            var results = await searchService.QueryIndex(
                args.Service!,
                args.Index!,
                args.Query!,
                args.RetryPolicy);

            context.Response.Results = ResponseResult.Create(results, SearchJsonContext.Default.ListJsonElement);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing search query");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    private static ArgumentBuilder<IndexQueryArguments> CreateServiceArgument() =>
        ArgumentBuilder<IndexQueryArguments>
            .Create(ArgumentDefinitions.Search.Service.Name, ArgumentDefinitions.Search.Service.Description)
            .WithValueAccessor(args => args.Service ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Search.Service.Required);

    private static ArgumentBuilder<IndexQueryArguments> CreateIndexArgument() =>
        ArgumentBuilder<IndexQueryArguments>
            .Create(ArgumentDefinitions.Search.Index.Name, ArgumentDefinitions.Search.Index.Description)
            .WithValueAccessor(args => args.Index ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Search.Index.Required);

    private static ArgumentBuilder<IndexQueryArguments> CreateQueryArgument() =>
        ArgumentBuilder<IndexQueryArguments>
            .Create(ArgumentDefinitions.Search.Query.Name, ArgumentDefinitions.Search.Query.Description)
            .WithValueAccessor(args => args.Query ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Search.Query.Required);
}
