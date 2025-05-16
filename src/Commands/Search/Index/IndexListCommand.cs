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

public sealed class IndexListCommand(ILogger<IndexListCommand> logger) : GlobalCommand<IndexListArguments>()
{
    private const string _commandTitle = "List Azure AI Search Indexes";
    private readonly ILogger<IndexListCommand> _logger = logger;
    private readonly Option<string> _serviceOption = ArgumentDefinitions.Search.Service.ToOption();
    public override string Name => "list";

    public override string Description =>
        """
        List all indexes in an Azure AI Search service.

        Required arguments:
        - service-name
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_serviceOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateIndexListArgument());
    }

    protected override IndexListArguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Service = parseResult.GetValueForOption(_serviceOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = _commandTitle)]
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

            var indexes = await searchService.ListIndexes(
                args.Service!,
                args.RetryPolicy);

            context.Response.Results = indexes?.Count > 0
                ? ResponseResult.Create(
                    new IndexListCommandResult(indexes),
                    SearchJsonContext.Default.IndexListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error listing search indexes");
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    internal record IndexListCommandResult(List<string> Indexes);

    private static ArgumentBuilder<IndexListArguments> CreateIndexListArgument() =>
        ArgumentBuilder<IndexListArguments>
            .Create(ArgumentDefinitions.Search.Service.Name, ArgumentDefinitions.Search.Service.Description)
            .WithValueAccessor(args => args.Service ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Search.Service.Required);
}
