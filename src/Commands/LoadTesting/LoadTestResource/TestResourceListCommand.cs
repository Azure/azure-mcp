// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options.LoadTesting.LoadTestResource;
using AzureMcp.Models.LoadTesting.LoadTestResource;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.LoadTesting.LoadTestResource;
public sealed class TestResourceListCommand(ILogger<TestResourceListCommand> logger)
    : BaseLoadTestingCommand<TestResourceListOptions>
{
    private const string _commandTitle = "Test Resource List";
    private readonly ILogger<TestResourceListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        Fetches the Load Testing resources for the current selected subscription, resource group in the logged in tenant.
        Returns a list of Load Testing resources.
        
        Required arguments:
        src
        Areas
        Commands
        
        - subscription
        - resource-group

        Optional arguments:
        - test-resource-name
        """;

    public override string Title => _commandTitle;

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
            var results = await service.GetLoadTestResourcesAsync(
                options.Subscription!,
                options.ResourceGroup,
                options.TestResourceName,
                options.Tenant,
                options.RetryPolicy);

            // Set results if any were returned
            context.Response.Results = results?.Count > 0 ?
                ResponseResult.Create(new TestResourceListCommandResult(results), LoadTestJsonContext.Default.TestResourceListCommandResult) :
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
    internal record TestResourceListCommandResult(List<TestResource> LoadTests);
}
