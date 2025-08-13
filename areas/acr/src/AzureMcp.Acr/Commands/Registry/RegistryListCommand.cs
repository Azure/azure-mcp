// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Acr.Options.Registry;
using AzureMcp.Acr.Services;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Acr.Commands.Registry;

public sealed class RegistryListCommand(ILogger<RegistryListCommand> logger) : BaseAcrCommand<RegistryListOptions>
{
    private const string CommandTitle = "List Container Registries";
    private readonly ILogger<RegistryListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        $"""
        List Azure Container Registries in a subscription. Optionally filter by resource group. Each registry result
        includes: name, location, loginServer, skuName, skuTier. If no registries are found the tool returns null results
        (consistent with other list commands).
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        UseResourceGroup();
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

            var acrService = context.GetService<IAcrService>();
            var registries = await acrService.ListRegistries(
                options.Subscription!,
                options.ResourceGroup,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = registries?.Count > 0
                ? ResponseResult.Create(new RegistryListCommandResult(registries), AcrJsonContext.Default.RegistryListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing container registries. Subscription: {Subscription}, ResourceGroup: {ResourceGroup}, Options: {@Options}",
                options.Subscription, options.ResourceGroup, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record RegistryListCommandResult(List<Models.AcrRegistryInfo> Registries);
}
