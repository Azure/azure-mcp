// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Commands;
using AzureMcp.Extension.Options;
using AzureMcp.Extension.Services;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Extension.Commands;

public sealed class AzCommand(ILogger<AzCommand> logger) : GlobalCommand<AzOptions>()
{
    private const string CommandTitle = "Azure CLI Command";
    private readonly ILogger<AzCommand> _logger = logger;
    private readonly Option<string> _intentOption = ExtensionOptionDefinitions.Az.Intent;

    public override string Name => "az";

    public override string Description =>
        """
Your job is to generate one ore more Azure CLI commands based on a provided intent description. The intent describes the goal to accomplish using Azure CLI. For example, 'List all resources group in my subscription'.
""";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = true, ReadOnly = false };

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
            var intent = options.Intent;

            // Always resolve the real API client from DI at execution time
            var copilotService = context.GetService<IAzCommandCopilotService>();
            using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(2));
            var apiResponse = await copilotService.GenerateAzCliCommandAsync(intent, cancellationTokenSource.Token);

            // Use source-generated context for AOT safety

            var result = new AzResult()
            {
                Output = apiResponse,
            };
            context.Response.Results = ResponseResult.Create(result, ExtensionJsonContext.Default.AzResult);
            context.Response.Status = 200;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred executing command.");
            HandleException(context, ex);
        }

        return context.Response;
    }
}
