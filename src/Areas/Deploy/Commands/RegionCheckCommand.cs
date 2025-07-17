// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Areas.Deploy.Services.Util;
using AzureMcp.Areas.Deploy.Models;
using AzureMcp.Areas.Deploy.Options;
using AzureMcp.Areas.Deploy.Services;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Command;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Deploy.Commands.Region;

public sealed class RegionCheckCommand(ILogger<RegionCheckCommand> logger) : SubscriptionCommand<RegionCheckOptions>()
{
    private const string CommandTitle = "Get Available Azure Regions";
    private readonly ILogger<RegionCheckCommand> _logger = logger;

    private readonly Option<string> _resourceTypesOption = DeployOptionDefinitions.RegionCheck.ResourceTypes;
    private readonly Option<string> _cognitiveServiceModelNameOption = DeployOptionDefinitions.RegionCheck.CognitiveServiceModelName;
    private readonly Option<string> _cognitiveServiceModelVersionOption = DeployOptionDefinitions.RegionCheck.CognitiveServiceModelVersion;
    private readonly Option<string> _cognitiveServiceDeploymentSkuNameOption = DeployOptionDefinitions.RegionCheck.CognitiveServiceDeploymentSkuName;

    public override string Name => "available-region-get";

    public override string Description =>
        """
        Given a list of Azure resource types, this tool will return a list of regions where the resource types are available. Always get the user's subscription ID before calling this tool.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceTypesOption);
        command.AddOption(_cognitiveServiceModelNameOption);
        command.AddOption(_cognitiveServiceModelVersionOption);
        command.AddOption(_cognitiveServiceDeploymentSkuNameOption);
    }

    protected override RegionCheckOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.ResourceTypes = parseResult.GetValueForOption(_resourceTypesOption) ?? string.Empty;
        options.CognitiveServiceModelName = parseResult.GetValueForOption(_cognitiveServiceModelNameOption);
        options.CognitiveServiceModelVersion = parseResult.GetValueForOption(_cognitiveServiceModelVersionOption);
        options.CognitiveServiceDeploymentSkuName = parseResult.GetValueForOption(_cognitiveServiceDeploymentSkuNameOption);
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

            var resourceTypes = options.ResourceTypes.Split(',')
                .Select(rt => rt.Trim())
                .Where(rt => !string.IsNullOrWhiteSpace(rt))
                .ToArray();

            if (resourceTypes.Length == 0)
            {
                throw new ArgumentException("Resource types cannot be empty.", nameof(options.ResourceTypes));
            }

            var deployService = context.GetService<IDeployService>();
            List<string> toolResult = await deployService.GetAvailableRegionsForResourceTypesAsync(
                resourceTypes,
                options.Subscription!,
                options.CognitiveServiceModelName,
                options.CognitiveServiceModelVersion,
                options.CognitiveServiceDeploymentSkuName);

            _logger.LogInformation("Region check result: {ToolResult}", toolResult);

            context.Response.Results = toolResult?.Count > 0 ?
                ResponseResult.Create(
                    new RegionCheckCommandResult(toolResult),
                    DeployJsonContext.Default.RegionCheckCommandResult) :
                null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred checking available Azure regions.");
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record RegionCheckCommandResult(List<string> AvailableRegions);
}
