// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.Foundry.Options.Models;

public class ModelDeploymentOptions : SubscriptionOptions
{
    public string DeploymentName { get; set; }
    public string ModelName { get; set; }
    public string ModelFormat { get; set; }
    public string AzureAiServicesName { get; set; }
    public string? ModelVersion { get; set; }
    public string? ModelSource { get; set; }
    public string? SkuName { get; set; }
    public int? SkuCapacity { get; set; }
    public string? ScaleType { get; set; }
    public int? ScaleCapacity { get; set; }
}
