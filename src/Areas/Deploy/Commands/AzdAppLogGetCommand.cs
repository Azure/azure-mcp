// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Deploy.Options;
using AzureMcp.Areas.Deploy.Services;
using AzureMcp.Commands.Subscription;
using AzureMcp.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Deploy.Commands;

public sealed class AzdAppLogGetCommand(ILogger<AzdAppLogGetCommand> logger) : SubscriptionCommand<AzdAppLogOptions>()
{
    private const string CommandTitle = "Get AZD deployed App Logs";
    private readonly ILogger<AzdAppLogGetCommand> _logger = logger;

    private readonly Option<string> _workspaceFolderOption = DeployOptionDefinitions.AzdAppLogOptions.WorkspaceFolder;
    private readonly Option<string> _azdEnvNameOption = DeployOptionDefinitions.AzdAppLogOptions.AzdEnvName;
    private readonly Option<int> _limitOption = DeployOptionDefinitions.AzdAppLogOptions.Limit;

    public override string Name => "azd-app-log-get";

    public override string Description =>
        """
        This tool helps fetch logs from log analytics workspace for Container Apps, App Services, function apps that were deployed through azd. Invoke this tool directly after a successful `azd up` or when user prompts to check the app's status or provide errors in the deployed apps.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_workspaceFolderOption);
        command.AddOption(_azdEnvNameOption);
        command.AddOption(_limitOption);
    }

    protected override AzdAppLogOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.WorkspaceFolder = parseResult.GetValueForOption(_workspaceFolderOption)!;
        options.AzdEnvName = parseResult.GetValueForOption(_azdEnvNameOption)!;
        options.Limit = parseResult.GetValueForOption(_limitOption);
        return options;
    }

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
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

            // Parse optional date parameters

            var deployService = context.GetService<IDeployService>();
            string result = await deployService.GetAzdResourceLogsAsync(
                options.WorkspaceFolder!,
                options.AzdEnvName!,
                options.Subscription!,
                options.Limit);

            context.Response.Message = result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred getting azd app logs.");
            HandleException(context, ex);
        }

        return context.Response;
    }

}
