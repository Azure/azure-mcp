// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Deploy.Options.Pipeline;

public class GuidanceGetOptions : SubscriptionOptions
{
    [JsonPropertyName("useAZDPipelineConfig")]
    public bool UseAZDPipelineConfig { get; set; }

    [JsonPropertyName("organizationName")]
    public string? OrganizationName { get; set; }

    [JsonPropertyName("repositoryName")]
    public string? RepositoryName { get; set; }

    [JsonPropertyName("githubEnvironmentName")]
    public string? GithubEnvironmentName { get; set; }
}
