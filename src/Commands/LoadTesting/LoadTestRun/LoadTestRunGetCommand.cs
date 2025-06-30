// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.LoadTesting.LoadTestRun;
using AzureMcp.Models.Option;
using AzureMcp.Options.LoadTesting.LoadTestRun;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.LoadTesting.LoadTestRun;
public sealed class LoadTestRunGetCommand(ILogger<LoadTestRunGetCommand> logger)
    : BaseLoadTestingCommand<LoadTestRunGetOptions>
{
    private const string _commandTitle = "Load Test Run Get";
    private readonly ILogger<LoadTestRunGetCommand> _logger = logger;
    private readonly Option<string> _loadTestRunIdOption = OptionDefinitions.LoadTesting.LoadTestRun;

    public override string Name => "get";

    public override string Description =>
        $"""
        Get the details of the specified load test run given the load test run id in the specified subscription and tenant.
        Returns the details of the specified load test run.
        
        Required arguments:
        - subscription
        - resource-group
        - load-test-name
        - load-testrun-id
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_loadTestRunIdOption);
    }

    protected override LoadTestRunGetOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.LoadTestRunId = parseResult.GetValueForOption(_loadTestRunIdOption);
        return options;
    }

    [McpServerTool(
    Destructive = false,
    ReadOnly = true,
    Title = _commandTitle)]

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);
        try
        {
            // Required validation step using the base Validate method
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            // Get the appropriate service from DI
            var service = context.GetService<ILoadTestingService>();

            // Call service operation(s)
            var results = await service.GetLoadTestRunAsync(
                options.Subscription!,
                options.TestResourceName!,
                options.LoadTestRunId!,
                options.ResourceGroup,
                options.Tenant,
                options.RetryPolicy);

            // Set results if any were returned
            context.Response.Results = results != null ?
                ResponseResult.Create(new LoadTestRunGetCommandResult(results), LoadTestJsonContext.Default.LoadTestRunGetCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            // Log error with context information
            _logger.LogError(ex, "Error in {Operation}. Options: {Options}", Name, options);
            // Let base class handle standard error processing
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
    internal record LoadTestRunGetCommandResult(LoadTestRunResource LoadTestRun);
}
