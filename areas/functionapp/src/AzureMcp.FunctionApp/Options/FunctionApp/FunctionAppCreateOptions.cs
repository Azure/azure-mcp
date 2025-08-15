// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.FunctionApp.Options.FunctionApp;

public class FunctionAppCreateOptions : BaseFunctionAppOptions
{
    [JsonPropertyName(FunctionAppOptionDefinitions.LocationName)]
    public string? Location { get; set; }

    [JsonPropertyName(FunctionAppOptionDefinitions.AppServicePlanName)]
    public string? AppServicePlan { get; set; }

    [JsonPropertyName(FunctionAppOptionDefinitions.PlanTypeName)]
    public string? PlanType { get; set; }

    [JsonPropertyName(FunctionAppOptionDefinitions.PlanSkuName)]
    public string? PlanSku { get; set; }

    [JsonPropertyName(FunctionAppOptionDefinitions.ContainerAppName)]
    public string? ContainerAppName { get; set; }

    [JsonPropertyName(FunctionAppOptionDefinitions.RuntimeName)]
    public string? Runtime { get; set; }

    [JsonPropertyName(FunctionAppOptionDefinitions.RuntimeVersionName)]
    public string? RuntimeVersion { get; set; }
}
