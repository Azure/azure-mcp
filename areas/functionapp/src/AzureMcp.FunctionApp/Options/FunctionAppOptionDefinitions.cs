// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.FunctionApp.Options;

public static class FunctionAppOptionDefinitions
{
    public const string FunctionAppName = "function-app";
    public const string LocationName = "location";
    public const string AppServicePlanName = "app-service-plan";
    public const string PlanTypeName = "plan-type";
    public const string PlanSkuName = "plan-sku";
    public const string ContainerAppName = "container-app";
    public const string RuntimeName = "runtime";
    public const string RuntimeVersionName = "runtime-version";
    public const string OperatingSystemName = "os";

    public static readonly Option<string> FunctionApp = new(
        $"--{FunctionAppName}",
        "The Function App name.")
    {
        IsRequired = true
    };

    public static readonly Option<string> Location = new(
        $"--{LocationName}",
        "The Azure region for the Function App (e.g., eastus, westus2).")
    {
        IsRequired = true
    };

    public static readonly Option<string> AppServicePlan = new(
        $"--{AppServicePlanName}",
        "The App Service plan name to use. If not supplied, a Consumption plan will be created automatically.")
    {
        IsRequired = false
    };

    public static readonly Option<string> PlanType = new(
        $"--{PlanTypeName}",
        "The App Service plan type when creating a plan automatically. Values: consumption, flex, premium. Defaults to consumption.")
    {
        IsRequired = false
    };

    public static readonly Option<string> PlanSku = new(
        $"--{PlanSkuName}",
        "The explicit App Service plan SKU (e.g., B1, S1, P1v3). Mutually exclusive with --plan-type. If provided and --app-service-plan omitted a dedicated plan using this SKU is created.")
    {
        IsRequired = false
    };

    public static readonly Option<string> ContainerApp = new(
        $"--{ContainerAppName}",
        "Provision a Container App instead of App Service hosting (containerapp plan type). Creates a managed environment if one doesn't exist. The value is used as the Container App name. Cannot be combined with --app-service-plan, --plan-sku, or --plan-type (other than containerapp).")
    {
        IsRequired = false
    };

    public static readonly Option<string> Runtime = new(
        $"--{RuntimeName}",
        "Function runtime worker. Examples: dotnet, node, python. Defaults to dotnet.")
    {
        IsRequired = false
    };

    public static readonly Option<string> RuntimeVersion = new(
        $"--{RuntimeVersionName}",
        "Runtime version for the selected worker (e.g., node: 22, 20; python: 3.12). If omitted, a sensible default is used.")
    {
        IsRequired = false
    };

    public static readonly Option<string> OperatingSystem = new(
        $"--{OperatingSystemName}",
        "Target operating system (windows|linux). Defaults to windows except when runtime/plan requires Linux (python, flex consumption, containerapp). Python and flex consumption are Linux only.")
    {
        IsRequired = false
    };
}
