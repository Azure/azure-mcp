// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.LoadTesting.Models.LoadTestRun;
using AzureMcp.Areas.LoadTesting.Options.LoadTestRun;
using AzureMcp.Areas.LoadTesting.Services;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.LoadTesting.Commands.LoadTestRun;
public sealed class TestRunCreateCommand(ILogger<TestRunCreateCommand> logger)
    : BaseLoadTestingCommand<TestRunCreateOptions>
{
    private const string _commandTitle = "Test Run Create";
    private readonly ILogger<TestRunCreateCommand> _logger = logger;
    private readonly Option<string> _loadTestRunIdOption = OptionDefinitions.LoadTesting.TestRun;
    private readonly Option<string> _testIdOption = OptionDefinitions.LoadTesting.Test;
    private readonly Option<string> _displayNameOption = OptionDefinitions.LoadTesting.DisplayName;
    private readonly Option<string> _descriptionOption = OptionDefinitions.LoadTesting.Description;
    private readonly Option<string> _oldTestRunIdOption = OptionDefinitions.LoadTesting.OldTestRunId;

    public override string Name => "create";

    public override string Description =>
        $"""
        Create and give the details of the specified load test run in the specified subscription and tenant.
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
        - old-testrun-id: The ID of an existing test run to update. If provided, the command will trigger a rerun of the given test run id.
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_loadTestRunIdOption);
        command.AddOption(_testIdOption);
        command.AddOption(_displayNameOption);
        command.AddOption(_descriptionOption);
        command.AddOption(_oldTestRunIdOption);
    }

    protected override TestRunCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TestRunId = parseResult.GetValueForOption(_loadTestRunIdOption);
        options.TestId = parseResult.GetValueForOption(_testIdOption);
        options.DisplayName = parseResult.GetValueForOption(_displayNameOption);
        options.Description = parseResult.GetValueForOption(_descriptionOption);
        options.OldTestRunId = parseResult.GetValueForOption(_oldTestRunIdOption);
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
                options.OldTestRunId,
                options.ResourceGroup,
                options.Tenant,
                options.DisplayName,
                options.Description,
                false, // DebugMode is not used in create
                options.RetryPolicy);

            // Set results if any were returned
            context.Response.Results = results != null ?
                ResponseResult.Create(new TestRunCreateCommandResult(results), LoadTestJsonContext.Default.TestRunCreateCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            // Log error with context information
            _logger.LogError(ex, "Error in {Operation}. Options: {Options}", Name, options);
            // Let base class handle standard error processing
            HandleException(context, ex);
        }

        return context.Response;
    }
    internal record TestRunCreateCommandResult(TestRun TestRun);
}
