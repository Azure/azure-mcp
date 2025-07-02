// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.LoadTesting.LoadTestRun;
using AzureMcp.Models.Option;
using AzureMcp.Options.LoadTesting.LoadTestRun;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.LoadTesting.LoadTestRun;
public sealed class TestRunUpdateCommand(ILogger<TestRunUpdateCommand> logger)
    : BaseLoadTestingCommand<TestRunUpdateOptions>
{
    private const string _commandTitle = "Test Run Update";
    private readonly ILogger<TestRunUpdateCommand> _logger = logger;
    private readonly Option<string> _loadTestRunIdOption = OptionDefinitions.LoadTesting.TestRun;
    private readonly Option<string> _testIdOption = OptionDefinitions.LoadTesting.Test;
    private readonly Option<string> _displayNameOption = OptionDefinitions.LoadTesting.DisplayName;
    private readonly Option<string> _descriptionOption = OptionDefinitions.LoadTesting.Description;

    public override string Name => "update";

    public override string Description =>
        $"""
        Update the details of the specified load test run in the specified subscription and tenant.
        Returns the details of the specified load test run.
        
        Required arguments:
        - subscription
        - resource-group
        - test-resource-name
        - testrun-id
        - test-id

        Optional arguments:
        - display-name: The display name for the load test run. This is a user-friendly name to identify the test run.
        - description: The description for the load test run. This provides additional context about the test run.
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_loadTestRunIdOption);
        command.AddOption(_testIdOption);
        command.AddOption(_displayNameOption);
        command.AddOption(_descriptionOption);
    }

    protected override TestRunUpdateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TestRunId = parseResult.GetValueForOption(_loadTestRunIdOption);
        options.TestId = parseResult.GetValueForOption(_testIdOption);
        options.DisplayName = parseResult.GetValueForOption(_displayNameOption);
        options.Description = parseResult.GetValueForOption(_descriptionOption);
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
            var results = await service.CreateOrUpdateLoadTestRunAsync(
                options.Subscription!,
                options.TestResourceName!,
                options.TestId!,
                options.TestRunId!,
                oldTestRunId: null, // Old test run ID is not used in update
                options.ResourceGroup,
                options.Tenant,
                options.DisplayName,
                options.Description,
                debugMode: null, // Debug mode is not applicable for update
                options.RetryPolicy);

            // Set results if any were returned
            context.Response.Results = results != null ?
                ResponseResult.Create(new TestRunUpdateCommandResult(results), LoadTestJsonContext.Default.TestRunUpdateCommandResult) :
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
    internal record TestRunUpdateCommandResult(TestRun TestRun);
}
