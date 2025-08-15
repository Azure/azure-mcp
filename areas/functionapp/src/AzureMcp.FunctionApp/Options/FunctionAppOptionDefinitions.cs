// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.FunctionApp.Options;

public static class FunctionAppOptionDefinitions
{
    public const string FunctionAppName = "functionapp";
    public const string LocationName = "location";
    public const string AppServicePlanName = "app-service-plan";
    public const string PlanTypeName = "plan-type";
    public const string StorageConnectionStringName = "storage-connection-string";
    public const string RuntimeName = "runtime";
    public const string RuntimeVersionName = "runtime-version";

    public static readonly Option<string> FunctionApp = new(
        $"--{FunctionAppName}",
        "Function App name.")
    {
        IsRequired = true
    };

    public static readonly Option<string> Location = new(
        $"--{LocationName}",
        "Azure region for the Function App (e.g., eastus, westus2).")
    {
        IsRequired = true
    };

    public static readonly Option<string> AppServicePlan = new(
        $"--{AppServicePlanName}",
        "App Service plan name to use. If not supplied, a Consumption plan will be created automatically.")
    {
        IsRequired = false
    };

    public static readonly Option<string> PlanType = new(
        $"--{PlanTypeName}",
        "App Service plan type when creating a plan automatically. Values: consumption, flex, premium. Defaults to consumption.")
    {
        IsRequired = false
    };

    public static readonly Option<string> StorageConnectionString = new(
        $"--{StorageConnectionStringName}",
        "Storage account connection string used for AzureWebJobsStorage. Required for Windows/Consumption apps.")
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
}
