// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Azure.Core.Pipeline;
using Azure.ResourceManager.Resources.Models;
using AzureMcp.Areas.Startups.Options;
using AzureMcp.Areas.Startups.Services;
using AzureMcp.Commands.Subscription;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Startups.Commands;

public sealed class StartupsDeployCommand(ILogger<StartupsDeployCommand> logger) : SubscriptionCommand<StartupsDeployOptions>()
{
    private const string CommandTitle = "Deploy Static Website for Startups";
    private readonly ILogger<StartupsDeployCommand> _logger = logger;
    private readonly Option<string> _storageAccount = StartupsOptionDefinitions.StorageAccount;
    private readonly Option<string> _resourceGroup = StartupsOptionDefinitions.ResourceGroup;
    private readonly Option<string> _sourcePath = StartupsOptionDefinitions.SourcePath;

    private readonly Option<string> _subscription = StartupsOptionDefinitions.Subscription;

    public override string Name => "deploy";
    public override string Description =>
        """
        Deploy static web resources for startups. Requires subscription {OptionDefinitions.Common.SubscriptionName},
        resource group, storage account name, and source directory path. Configures static website hosting
        and uploads content from the specified directory.
        """;
    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_subscription);
        command.AddOption(_storageAccount);
        command.AddOption(_resourceGroup);
        command.AddOption(_sourcePath);
    }

    protected override StartupsDeployOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Subscription = parseResult.GetValueForOption(_subscription);
        options.ResourceGroup = parseResult.GetValueForOption(_resourceGroup);
        options.StorageAccount = parseResult.GetValueForOption(_storageAccount);
        options.SourcePath = parseResult.GetValueForOption(_sourcePath);
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
            _logger.LogInformation("Starting deployment to storage account {StorageAccount}", options.StorageAccount);

            var service = context.GetService<IStartupsService>();

            var results = await service.DeployStaticWebAsync(options.Subscription!, options.ResourceGroup!, options.StorageAccount!, options.SourcePath!);

            _logger.LogInformation("Successfully deployed to storage account {StorageAccount}", options.StorageAccount);
            context.Response.Results = ResponseResult.Create(results, DeployJsonContext.Default.StartupsDeployResources);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deploying static website to {StorageAccount}", options.StorageAccount);
            HandleException(context, ex);
        }
        return context.Response;
    }
}

[System.Text.Json.Serialization.JsonSourceGenerationOptions(WriteIndented = false, GenerationMode = System.Text.Json.Serialization.JsonSourceGenerationMode.Default)][System.Text.Json.Serialization.JsonSerializable(typeof(StartupsDeployResources))]
internal partial class DeployJsonContext : System.Text.Json.Serialization.JsonSerializerContext;
