// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Deploy.Options;

public sealed class InfraCodeRulesOptions
{
    public string DeploymentTool { get; set; } = string.Empty;
    public string IacType { get; set; } = string.Empty;
    public string ResourceTypes { get; set; } = string.Empty;
}
