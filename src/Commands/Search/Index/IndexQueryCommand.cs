// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments.Search.Index;
using AzureMcp.Models.Argument;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.Search.Index;

public sealed class IndexQueryCommand(ILogger<IndexQueryCommand> logger) : GlobalCommand<IndexQueryArguments>()
{
    private const string _commandTitle = "Query Azure AI Search Index";
    private readonly ILogger<IndexQueryCommand> _logger = logger;
    private readonly Option<string> _serviceOption = ArgumentDefinitions.Search.Service;
    private readonly Option<string> _indexOption = ArgumentDefinitions.Search.Index;
    private readonly Option<string> _queryOption = ArgumentDefinitions.Search.Query;

    public override string Name => "query";

    public override string Description =>
        """
        Query an Azure AI Search index. Returns search results matching the specified query.

        Required arguments:
        - service-name: The name of the Azure AI Search service
        - index-name: The name of the search index to query
        - query: The search text to query with
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serviceOption);
        command.AddOption(_indexOption);
        command.AddOption(_queryOption);
    }

    protected override IndexQueryArguments BindOptions(ParseResult parseResult)
    {
        var args = base.BindOptions(parseResult);
        args.Service = parseResult.GetValueForOption(_serviceOption);
        args.Index = parseResult.GetValueForOption(_indexOption);
        args.Query = parseResult.GetValueForOption(_queryOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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
}
