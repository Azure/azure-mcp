// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Models.Monitor.WebTests;
using AzureMcp.Monitor.Options;
using AzureMcp.Monitor.Options.WebTests;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Monitor.Commands.WebTests;

public sealed class WebTestsGetCommand(ILogger<WebTestsGetCommand> logger) : BaseMonitorWebTestsCommand<WebTestsGetOptions>
{
    private const string _commandTitle = "Get details of a specific web test";
    private const string _commandName = "get";

    private readonly Option<string> _webTestResourceNameOption = MonitorOptionDefinitions.WebTest.WebTestResourceName;

    public override string Name => _commandName;

    public override string Description =>
         $"""
        Gets details for a specific web test in the provided resource group based on webtest resource name.
        Returns detailed information about a single web test.
        """;

    public override string Title => _commandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    private readonly ILogger<WebTestsGetCommand> _logger = logger;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceGroupOption);
        command.AddOption(_webTestResourceNameOption);
    }

    protected override WebTestsGetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        options.ResourceName = parseResult.GetValueForOption(_webTestResourceNameOption);
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
            var webTest = await monitorWebTestService.GetWebTest(
                options.Subscription!,
                options.ResourceGroup!,
                options.ResourceName!,
                options.Tenant,
                options.RetryPolicy);

            if (webTest != null)
            {
                context.Response.Results = ResponseResult.Create(
                    new WebTestsGetCommandResult(webTest),
                    MonitorJsonContext.Default.WebTestsGetCommandResult);
            }
            else
            {
                context.Response.Status = 404;
                context.Response.Message = $"Web test '{options.ResourceName}' not found in resource group '{options.ResourceGroup}'";
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving web test '{Name}' in resource group '{ResourceGroup}', subscription '{Subscription}'",
                options.ResourceName, options.ResourceGroup, options.Subscription);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record WebTestsGetCommandResult(WebTestDetailedInfo WebTest);
}
