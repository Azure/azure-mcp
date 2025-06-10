using AzureMcp.Commands.LoadTesting;
using AzureMcp.Models.LoadTesting;
using AzureMcp.Options.LoadTesting.LoadTest;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.LoadTesting.LoadTest;
public sealed class LoadTestListCommand(ILogger<LoadTestListCommand> logger)
    : BaseLoadTestingCommand<LoadTestListOptions>
{
    private const string _commandTitle = "Load Test List";
    private readonly ILogger<LoadTestListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        Fetches the Load Testing resources for the current selected subscription and tenant.
        Returns a list of Load Testing resources.
        
        Required arguments:
        - subscription
        - tenant

        Optional arguments:
        - resource-group
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
            var results = await service.GetLoadTestsForSubscriptionAsync(
                options.Subscription!,
                options.ResourceGroup,
                options.Tenant,
                options.RetryPolicy);

            // Set results if any were returned
            context.Response.Results = results?.Count > 0 ?
                ResponseResult.Create(new LoadTestListCommandResult(results), LoadTestJsonContext.Default.LoadTestListCommandResult) :
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
    internal record LoadTestListCommandResult(List<LoadTestResource> LoadTests);
}
