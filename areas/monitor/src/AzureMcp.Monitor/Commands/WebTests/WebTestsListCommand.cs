// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Core.Models.Option;
using AzureMcp.Models.Monitor.WebTests;
using AzureMcp.Monitor.Options.WebTests;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.WebTests;

public sealed class WebTestsListCommand(ILogger<WebTestsListCommand> logger) : BaseMonitorWebTestsCommand<WebTestsListOptions>
{
    private const string _commandTitle = "List all web tests in a subscription or resource group";
    private const string _commandName = "list";

    private readonly Option<string> _resourceGroupOptionalOption = OptionDefinitions.Common.ResourceGroupOptional;

    public override string Name => _commandName;

    public override string Description =>
         $"""
        Lists all web tests in a specified subscription and optionally, a resource group.
        Returns a list of web tests.
        """;

    public override string Title => _commandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    private readonly ILogger<WebTestsListCommand> _logger = logger;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOptionalOption);
    }

    protected override WebTestsListOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOptionalOption);
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

            var monitorWebTestService = context.GetService<IMonitorWebTestService>();
            var webTests = options.ResourceGroup == null
                ? await monitorWebTestService.ListWebTests(options.Subscription!, options.Tenant, options.RetryPolicy)
                : await monitorWebTestService.ListWebTests(options.Subscription!, options.ResourceGroup, options.Tenant, options.RetryPolicy);

            context.Response.Results = webTests?.Count > 0 ?
                ResponseResult.Create(new WebTestsListCommandResult(webTests), MonitorJsonContext.Default.WebTestsListCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error listing web tests in subscription '{options.Subscription}'");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record WebTestsListCommandResult(List<WebTestSummaryInfo> WebTests);
}
