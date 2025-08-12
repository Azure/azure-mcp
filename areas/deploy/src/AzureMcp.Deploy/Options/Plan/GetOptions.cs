// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Deploy.Options.Plan;

public sealed class GetOptions
{
    [JsonPropertyName("workspaceFolder")]
    public string WorkspaceFolder { get; set; } = string.Empty;

    [JsonPropertyName("projectName")]
    public string ProjectName { get; set; } = string.Empty;

    [JsonPropertyName("targetAppService")]
    public string TargetAppService { get; set; } = string.Empty;

    [JsonPropertyName("provisioningTool")]
    public string ProvisioningTool { get; set; } = string.Empty;

    [JsonPropertyName("azdIacOptions")]
    public string? AzdIacOptions { get; set; } = string.Empty;
}
