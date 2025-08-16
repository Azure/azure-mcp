// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Acr.Options;
using AzureMcp.Acr.Options.Registry;
using AzureMcp.Acr.Services;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Services.Telemetry;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Acr.Commands.Registry;

public sealed class RegistryRepositoryListCommand(ILogger<RegistryRepositoryListCommand> logger)
    : BaseAcrCommand<RegistryRepositoryListOptions>
{
    private const string CommandTitle = "List Container Registry Repositories";
    private readonly ILogger<RegistryRepositoryListCommand> _logger = logger;

    public override string Name => "list";

    public override string Description =>
        """
        List repositories in Azure Container Registries. By default, lists repositories for all registries in the subscription.
        You can narrow the scope using --resource-group and/or --registry to list repositories for a specific registry only.
        """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = false, ReadOnly = true };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        UseResourceGroup();
        command.AddOption(AcrOptionDefinitions.Registry);
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

            var service = context.GetService<IAcrService>();
            var map = await service.ListRegistryRepositories(
                options.Subscription!,
                options.ResourceGroup,
                options.Registry,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = map.Count > 0
                ? ResponseResult.Create(
                    new RegistryRepositoryListCommandResult(map),
                    AcrJsonContext.Default.RegistryRepositoryListCommandResult)
                : null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error listing ACR repositories. Subscription: {Subscription}, ResourceGroup: {ResourceGroup}, Registry: {Registry}",
                options.Subscription, options.ResourceGroup, options.Registry);
            HandleException(context, ex);
        }

        return context.Response;
    }

    internal record RegistryRepositoryListCommandResult(Dictionary<string, List<string>> RepositoriesByRegistry);
}
