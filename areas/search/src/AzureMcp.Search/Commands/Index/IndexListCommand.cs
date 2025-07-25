// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Search.Models;
using AzureMcp.Search.Options;
using AzureMcp.Search.Options.Index;
using AzureMcp.Search.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Search.Commands.Index;

public sealed class IndexListCommand(ILogger<IndexListCommand> logger) : GlobalCommand<IndexListOptions>()
{
    private const string CommandTitle = "List Azure AI Search Indexes";
    private readonly ILogger<IndexListCommand> _logger = logger;
    private readonly Option<string> _serviceOption = SearchOptionDefinitions.Service;
    public override string Name => "list";

    public override string Description =>
        """
        List all indexes in an Azure AI Search service.

        Required arguments:
        - service-name
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serviceOption);
    }

    protected override IndexListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Service = parseResult.GetValueForOption(_serviceOption);
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

            var indexes = await searchService.ListIndexes(
                options.Service!,
                options.RetryPolicy);

            context.Response.Results = indexes?.Count > 0
                ? ResponseResult.Create(
                    new IndexListCommandResult(indexes),
                    SearchJsonContext.Default.IndexListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing search indexes");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record IndexListCommandResult(List<IndexInfo> Indexes);
}
