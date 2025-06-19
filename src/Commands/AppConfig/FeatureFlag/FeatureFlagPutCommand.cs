// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Option;
using AzureMcp.Options.AppConfig.FeatureFlag;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.AppConfig.FeatureFlag;

/// <summary>
/// Command to create or update a feature flag in Azure App Configuration.
/// </summary>
public sealed class FeatureFlagPutCommand(ILogger<FeatureFlagPutCommand> logger) : BaseFeatureFlagCommand<FeatureFlagPutOptions>()
{
    private const string CommandTitle = "Set App Configuration Feature Flag";
    private readonly Option<bool> _enabledOption = OptionDefinitions.AppConfig.FeatureFlag.Enabled;
    private readonly Option<string> _descriptionOption = OptionDefinitions.AppConfig.FeatureFlag.Description;
    private readonly ILogger<FeatureFlagPutCommand> _logger = logger;

    public override string Name => "put";

    public override string Description =>
        """
        Create or update a feature flag in an App Configuration store. This command creates or updates a feature flag
        with the specified enabled state and optional description. You must specify an account name and feature flag name.
        Optionally, you can specify a label otherwise the default label will be used.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_enabledOption);
        command.AddOption(_descriptionOption);
    }

    protected override FeatureFlagPutOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Enabled = parseResult.GetValueForOption(_enabledOption);
        options.Description = parseResult.GetValueForOption(_descriptionOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = false, Title = CommandTitle)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }
            var appConfigService = context.GetService<IAppConfigService>();
            await appConfigService.SetFeatureFlag(
                options.Account!,
                options.FeatureFlagName!,
                options.Enabled ?? false,
                options.Description,
                options.Subscription!,
                options.Tenant,
                options.RetryPolicy,
                options.Label);

            context.Response.Results = ResponseResult.Create(
                new FeatureFlagPutCommandResult(options.FeatureFlagName, options.Enabled ?? false, options.Description, options.Label),
                AppConfigJsonContext.Default.FeatureFlagPutCommandResult
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred setting feature flag. Feature Flag: {FeatureFlagName}.", options.FeatureFlagName);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}

internal record FeatureFlagPutCommandResult(string? FeatureFlagName, bool Enabled, string? Description, string? Label);
