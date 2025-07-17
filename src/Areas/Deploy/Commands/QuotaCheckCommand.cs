// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Areas.Deploy.Services.Util;
using AzureMcp.Areas.Deploy.Options;
using AzureMcp.Areas.Deploy.Services;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Command;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Deploy.Commands.Quota;

public class QuotaCheckCommand(ILogger<QuotaCheckCommand> logger) : SubscriptionCommand<QuotaCheckOptions>()
{
    private const string CommandTitle = "Check Available Azure Quota for Regions";
    private readonly ILogger<QuotaCheckCommand> _logger = logger;

    private readonly Option<string> _regionOption = DeployOptionDefinitions.QuotaCheck.Region;
    private readonly Option<string> _resourceTypesOption = DeployOptionDefinitions.QuotaCheck.ResourceTypes;

    public override string Name => "quota-check";

    public override string Description =>
        """
        This tool will check the Azure quota availability for the resources that are going to be deployed.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_regionOption);
        command.AddOption(_resourceTypesOption);
    }

    protected override QuotaCheckOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Region = parseResult.GetValueForOption(_regionOption) ?? string.Empty;
        options.ResourceTypes = parseResult.GetValueForOption(_resourceTypesOption) ?? string.Empty;
        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options);
            var ResourceTypes = options.ResourceTypes.Split(',')
                .Select(rt => rt.Trim())
                .Where(rt => !string.IsNullOrWhiteSpace(rt))
                .ToList();
            var deployService = context.GetService<IDeployService>();
            Dictionary<string, List<QuotaInfo>> toolResult = await deployService.GetAzureQuotaAsync(
                ResourceTypes,
                options.Subscription!,
                options.Region);

            _logger.LogInformation("Quota check result: {ToolResult}", toolResult);

            context.Response.Results = toolResult?.Count > 0 ?
                ResponseResult.Create(
                    new QuotaCheckCommandResult(toolResult),
                    DeployJsonContext.Default.QuotaCheckCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error checking Azure quota");
            HandleException(context, ex);
        }
        return context.Response;

    }

    internal record QuotaCheckCommandResult(Dictionary<string, List<QuotaInfo>> QuotaInfo);

}
