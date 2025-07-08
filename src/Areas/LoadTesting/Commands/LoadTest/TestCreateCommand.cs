// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


using AzureMcp.Areas.LoadTesting.Models.LoadTest;
using AzureMcp.Areas.LoadTesting.Options.LoadTest;
using AzureMcp.Areas.LoadTesting.Services;
using AzureMcp.Models.Option;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.LoadTesting.Commands.LoadTest;
public sealed class TestCreateCommand(ILogger<TestCreateCommand> logger)
    : BaseLoadTestingCommand<TestCreateOptions>
{
    private const string _commandTitle = "Test Create";
    private readonly ILogger<TestCreateCommand> _logger = logger;
    private readonly Option<string> _loadTestIdOption = OptionDefinitions.LoadTesting.Test;
    private readonly Option<string> _loadTestDescriptionOption = OptionDefinitions.LoadTesting.Description;
    private readonly Option<string> _loadTestDisplayNameOption = OptionDefinitions.LoadTesting.DisplayName;
    private readonly Option<string> _loadTestEndpointOption = OptionDefinitions.LoadTesting.Endpoint;
    private readonly Option<int> _loadTestVirtualUsersOption = OptionDefinitions.LoadTesting.VirtualUsers;
    private readonly Option<int> _loadTestDurationOption = OptionDefinitions.LoadTesting.Duration;
    private readonly Option<int> _loadTestRampUpTimeOption = OptionDefinitions.LoadTesting.RampUpTime;

    public override string Name => "create";

    public override string Description =>
        $"""
        Create a new load test with the specified parameters. Currently we are supporting BASIC URL test create scneario.
        
        Required arguments:
        - subscription
        - resource-group
        - test-resource-name
        - test-id

        Options arguments:
        --description - Update the description of the test run
        --display-name - Update the display name of the test run
        --endpoint - The endpoint you want to test (GET Request)
        --virtual-users - Virtual users is a measure of load that is simulated to test the HTTP endpoint. (default - 50)
        --test-duration - This is the duration for which the load is simulated against the endpoint. Enter decimals for fractional minutes (e.g., 1.5 for 1 minute and 30 seconds). Default is 20 mins
        --ramp-up-time - The ramp-up time is the time it takes for the system to ramp-up to the total load specified. Enter decimals for fractional minutes (e.g., 1.5 for 1 minute and 30 seconds). Default is 1 min
        """;

    public override string Title => _commandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_loadTestIdOption);
        command.AddOption(_loadTestDescriptionOption);
        command.AddOption(_loadTestDisplayNameOption);
        command.AddOption(_loadTestEndpointOption);
        command.AddOption(_loadTestVirtualUsersOption);
        command.AddOption(_loadTestDurationOption);
        command.AddOption(_loadTestRampUpTimeOption);
    }

    protected override TestCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.TestId = parseResult.GetValueForOption(_loadTestIdOption);
        options.Description = parseResult.GetValueForOption(_loadTestDescriptionOption);
        options.DisplayName = parseResult.GetValueForOption(_loadTestDisplayNameOption);
        options.Endpoint = parseResult.GetValueForOption(_loadTestEndpointOption);
        options.VirtualUsers = parseResult.GetValueForOption(_loadTestVirtualUsersOption);
        options.Duration = parseResult.GetValueForOption(_loadTestDurationOption);
        options.RampUpTime = parseResult.GetValueForOption(_loadTestRampUpTimeOption);
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
            var results = await service.CreateTestAsync(
                options.Subscription!,
                options.TestResourceName!,
                options.TestId!,
                options.ResourceGroup,
                options.DisplayName,
                options.Description,
                options.Duration,
                options.VirtualUsers,
                options.RampUpTime,
                options.Endpoint,
                options.Tenant,
                options.RetryPolicy);

            // Set results if any were returned
            context.Response.Results = results != null ?
                ResponseResult.Create(new TestCreateCommandResult(results), LoadTestJsonContext.Default.TestCreateCommandResult) :
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
    internal record TestCreateCommandResult(Test Test);
}
