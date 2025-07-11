// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class Test
{
    [JsonPropertyName("environmentVariables")]
    public Dictionary<string, string> EnvironmentVariables { get; set; } = new();

    [JsonPropertyName("loadTestConfiguration")]
    public LoadTestConfiguration LoadTestConfiguration { get; set; } = new();

    [JsonPropertyName("inputArtifacts")]
    public InputArtifacts InputArtifacts { get; set; } = new();

    [JsonPropertyName("kind")]
    public string Kind { get; set; } = string.Empty;

    [JsonPropertyName("publicIPDisabled")]
    public bool PublicIPDisabled { get; set; }

    [JsonPropertyName("metricsReferenceIdentityType")]
    public string MetricsReferenceIdentityType { get; set; } = string.Empty;

    [JsonPropertyName("testId")]
    public string TestId { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("createdDateTime")]
    public DateTimeOffset CreatedDateTime { get; set; }

    [JsonPropertyName("createdBy")]
    public string CreatedBy { get; set; } = string.Empty;

    [JsonPropertyName("lastModifiedDateTime")]
    public DateTimeOffset LastModifiedDateTime { get; set; }

    [JsonPropertyName("lastModifiedBy")]
    public string LastModifiedBy { get; set; } = string.Empty;
}
