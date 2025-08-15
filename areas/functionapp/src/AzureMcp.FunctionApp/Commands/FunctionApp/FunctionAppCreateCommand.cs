// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure;
using AzureMcp.Core.Commands;
using AzureMcp.FunctionApp.Models;
using AzureMcp.FunctionApp.Options;
using AzureMcp.FunctionApp.Services;
using Microsoft.Extensions.Logging;
using AzureMcp.FunctionApp.Options.FunctionApp;

namespace AzureMcp.FunctionApp.Commands.FunctionApp;

public sealed class FunctionAppCreateCommand(ILogger<FunctionAppCreateCommand> logger)
    : BaseFunctionAppCommand<FunctionAppCreateOptions>
{
    private const string CommandTitle = "Create Azure Function App";
    private readonly ILogger<FunctionAppCreateCommand> _logger = logger;

    private readonly Option<string> _functionAppNameOption = FunctionAppOptionDefinitions.FunctionApp;
    private readonly Option<string> _locationOption = FunctionAppOptionDefinitions.Location;
    private readonly Option<string> _appServicePlanOption = FunctionAppOptionDefinitions.AppServicePlan;
    private readonly Option<string> _planTypeOption = FunctionAppOptionDefinitions.PlanType;
    private readonly Option<string> _storageConnectionStringOption = FunctionAppOptionDefinitions.StorageConnectionString;
    private readonly Option<string> _runtimeOption = FunctionAppOptionDefinitions.Runtime;
    private readonly Option<string> _runtimeVersionOption = FunctionAppOptionDefinitions.RuntimeVersion;

    public override string Name => "create";

    public override string Description =>
    """
    Create a new Azure Function App in the specified resource group and region.
    Automatically provisions dependencies when omitted (App Service plan, Storage account) and applies sensible runtime defaults.
    Required options:
    - subscription: Target Azure subscription (can be ID or name)
    - resource-group: Resource group for the Function App (created if it does not exist)
    - functionapp: Globally unique Function App name
    - location: Azure region (e.g. eastus)
    Optional options:
    - app-service-plan: Existing App Service plan name to use; if omitted a new plan is created
    - plan-type: Plan kind when creating a plan (consumption|flex|premium) (default: consumption)
        * consumption -> Y1 (Consumption)
        * flex -> FC1 (Flex Consumption; Linux)
        * premium -> EP1 (Elastic Premium; Linux optional)
    - storage-connection-string: AzureWebJobsStorage connection string; if omitted a new Storage account is created
    - runtime: Worker runtime (dotnet|node|python|java|powershell) (default: dotnet)
    - runtime-version: Specific runtime version (auto default if omitted)
    Behavior:
    - Linux plan required for python and flex consumption; enforced automatically
    - Generates WEBSITE_NODE_DEFAULT_VERSION for Windows Node apps when a version is supplied
    - Applies FUNCTIONS_EXTENSION_VERSION ~4 and sets FUNCTIONS_WORKER_RUNTIME
    Returns: functionApp object (name, resourceGroup, location, plan, state, defaultHostName, tags)
    """;

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new() { Destructive = true, ReadOnly = false };

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        RequireResourceGroup(); // logically required per contributing guide
        command.AddOption(_functionAppNameOption);
        command.AddOption(_locationOption);
        command.AddOption(_appServicePlanOption);
        command.AddOption(_planTypeOption);
        command.AddOption(_storageConnectionStringOption);
        command.AddOption(_runtimeOption);
        command.AddOption(_runtimeVersionOption);
    }

    protected override FunctionAppCreateOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
    // Resource group binding handled centrally when RequireResourceGroup() invoked
        options.FunctionAppName = parseResult.GetValueForOption(_functionAppNameOption);
        options.Location = parseResult.GetValueForOption(_locationOption);
    options.AppServicePlan = parseResult.GetValueForOption(_appServicePlanOption);
    options.PlanType = parseResult.GetValueForOption(_planTypeOption);
        options.StorageConnectionString = parseResult.GetValueForOption(_storageConnectionStringOption);
        options.Runtime = parseResult.GetValueForOption(_runtimeOption) ?? "dotnet";
    options.RuntimeVersion = parseResult.GetValueForOption(_runtimeVersionOption);
        return options;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
                return context.Response;

            var service = context.GetService<IFunctionAppService>();
            var result = await service.CreateFunctionApp(
                options.Subscription!,
                options.ResourceGroup!,
                options.FunctionAppName!,
                options.Location!,
                options.AppServicePlan,
                options.PlanType,
                options.StorageConnectionString,
                options.Runtime ?? "dotnet",
                options.RuntimeVersion,
                options.Tenant,
                options.RetryPolicy);

            context.Response.Results = ResponseResult.Create(
                new FunctionAppCreateCommandResult(result),
                FunctionAppJsonContext.Default.FunctionAppCreateCommandResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error creating function app. Subscription: {Subscription}, ResourceGroup: {ResourceGroup}, FunctionApp: {FunctionApp}, Options: {@Options}",
                options.Subscription, options.ResourceGroup, options.FunctionAppName, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        RequestFailedException reqEx when reqEx.Status == 409 =>
            "Function App name already exists or conflict in resource group. Choose a different name or check plan settings.",
        RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed creating the Function App. Details: {reqEx.Message}",
        RequestFailedException reqEx when reqEx.Status == 404 =>
            "Resource group or plan not found. Verify the resource group and plan exist and you have access.",
        RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    internal record FunctionAppCreateCommandResult(FunctionAppInfo FunctionApp);
}
