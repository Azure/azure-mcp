// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Extension.Options;
using AzureMcp.Areas.Extension.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Extension.Commands;

public sealed class AzCommand(ILogger<AzCommand> logger) : GlobalCommand<AzOptions>()
{
    private const string CommandTitle = "Generate Azure CLI Command";
    private readonly ILogger<AzCommand> _logger = logger;
    private readonly Option<string> _intentOption = ExtensionOptionDefinitions.Az.Intent;

    /// <summary>
    /// Clears the cached Azure CLI path. Used for testing purposes.
    /// </summary>
    internal static void ClearCachedAzPath()
    {
        _cachedAzPath = null;
    }

    public override string Name => "az";

    public override string Description =>
        """
        Generates an Azure CLI command based on a provided intent description. The intent should describe the goal to accomplish using Azure CLI. Returns the recommended Azure CLI commands. For example: 'List all resource groups in the subscription.'
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_intentOption);
    }

    protected override AzOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Intent = parseResult.GetValueForOption(_intentOption);
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
            ArgumentNullException.ThrowIfNull(options.Intent);
            var service = context.GetService<IExtensionService>();
            var cliCommandResponse = await service.GenerateAzCommandAsync(options.Intent!);
            context.Response.Results = ResponseResult.Create(cliCommandResponse, JsonSourceGenerationContext.Default.String);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating Azure CLI command.");
            HandleException(context, ex);
        }
        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        _ => base.GetStatusCode(ex)
    };
}
