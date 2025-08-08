// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Quota.Models;

public class CognitiveServiceProperties
{
    public string? ModelName { get; set; } = string.Empty;

    public string? ModelVersion { get; set; } = string.Empty;

    public string? DeploymentSkuName { get; set; } = string.Empty;
}
