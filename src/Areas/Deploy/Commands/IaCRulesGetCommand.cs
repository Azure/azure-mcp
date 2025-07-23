// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Deploy.Models;
using AzureMcp.Areas.Deploy.Options;
using AzureMcp.Areas.Deploy.Services.Util;
using AzureMcp.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Deploy.Commands.InfraCodeRules;

public sealed class IaCRulesGetCommand(ILogger<IaCRulesGetCommand> logger)
    : BaseCommand()
{
    private const string CommandTitle = "Get Iac(Infrastructure as Code) Rules";
    private readonly ILogger<IaCRulesGetCommand> _logger = logger;

    private readonly Option<string> _deploymentToolOption = DeployOptionDefinitions.IaCRules.DeploymentTool;
    private readonly Option<string> _iacTypeOption = DeployOptionDefinitions.IaCRules.IacType;
    private readonly Option<string> _resourceTypesOption = DeployOptionDefinitions.IaCRules.ResourceTypes;

    public override string Name => "iac-rules-get";

    public override string Description =>
        """
        This tool offers guidelines for creating Bicep/Terraform files to deploy applications on Azure. The guidelines outline rules to improve the quality of Infrastructure as Code files, ensuring they are compatible with the azd tool and adhere to best practices.
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_deploymentToolOption);
        command.AddOption(_iacTypeOption);
        command.AddOption(_resourceTypesOption);
    }

    private InfraCodeRulesOptions BindOptions(ParseResult parseResult)
    {
        var options = new InfraCodeRulesOptions();
        options.DeploymentTool = parseResult.GetValueForOption(_deploymentToolOption) ?? string.Empty;
        options.IacType = parseResult.GetValueForOption(_iacTypeOption) ?? string.Empty;
        options.ResourceTypes = parseResult.GetValueForOption(_resourceTypesOption) ?? string.Empty;

        return options;
    }

    [McpServerTool(
        Destructive = false,
        ReadOnly = true,
        Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);
        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return Task.FromResult(context.Response);
            }

            var resourceTypes = options.ResourceTypes.Split(',')
                .Select(rt => rt.Trim())
                .Where(rt => !string.IsNullOrWhiteSpace(rt))
                .ToArray();

            List<string> result = InfraCodeRuleRetriever.GetIaCRules(
                options.DeploymentTool,
                options.IacType,
                resourceTypes);

            context.Response.Message = string.Join(Environment.NewLine, result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred listing accounts.");
            HandleException(context, ex);
        }
        return Task.FromResult(context.Response);
    }
}
