// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.TerraformSchema.Options;
using AzureMcp.Areas.TerraformSchema.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.TerraformSchema.Commands;

public sealed class TerraformSchemaGetCommand(ILogger<TerraformSchemaGetCommand> logger) : BaseTerraformSchemaCommand<TerraformSchemaOptions>
{
    private const string CommandTitle = "Get Terraform Provider Schema for a resource";

    private readonly ILogger<TerraformSchemaGetCommand> _logger = logger;
    public override string Name => "get";

    public override string Description =>
   """
    Always invoke this tool immediately after user runs terraform init for the first time in the session.
    The provider-name parameter must be in the format 'registry.terraform.io/hashicorp/azurerm' etc.
    The resource-type parameter must be in the format 'azurerm_api_management_redis_cache', 'aws_s3_bucket', etc.
    The workspace-path parameter is the path to user's current working directory, which contains the .terraform directory.
    This tool retrieves the Terraform provider schema to assist in generating Terraform Infrastructure-as-Code (IaC) files.
    This ensures you obtain the most accurate and up-to-date provider schema to validate available attribute names and values, even if you already have other provider information.
    """;

    public override string Title => CommandTitle;

    [McpServerTool(Destructive = false, ReadOnly = true, Title = CommandTitle)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        TerraformSchemaOptions options = BindOptions(parseResult);
        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return Task.FromResult(context.Response);
            }
            var terraformSchemaService = context.GetService<ITerraformSchemaService>() ?? throw new InvalidOperationException("Bicep schema service is not available.");
            var response = terraformSchemaService.GetResourceSchema(
                options.ResourceType!, options.ProviderName!, options.UserDirectory!);

            context.Response.Results = response is not null ?
                ResponseResult.Create(
                    new TerraformSchemaGetCommandResult(response),
                    TerraformSchemaJsonContext.Default.TerraformSchemaGetCommandResult) :
                 null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred fetching Terraform schema.");
            HandleException(context.Response, ex);
        }
        return Task.FromResult(context.Response);

    }

    internal record TerraformSchemaGetCommandResult(string TerraformSchemaResult);
}
