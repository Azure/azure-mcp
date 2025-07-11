// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class TestRequestPayload
{
    [JsonPropertyName("testId")]
    public string TestId { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("loadTestConfiguration")]
    public LoadTestConfiguration? LoadTestConfiguration { get; set; } = new();

    [JsonPropertyName("kind")]
    public string Kind { get; set; } = "URL";

    [JsonPropertyName("secrets")]
    public Dictionary<string, string>? Secrets { get; set; } = new();

    [JsonPropertyName("certificate")]
    public string? Certificate { get; set; }

    [JsonPropertyName("environmentVariables")]
    public Dictionary<string, string>? EnvironmentVariables { get; set; } = new();

    [JsonPropertyName("passFailCriteria")]
    public PassFailCriteria? PassFailCriteria { get; set; } = new();

    [JsonPropertyName("autoStopCriteria")]
    public AutoStopCriteria? AutoStopCriteria { get; set; } = new();

    [JsonPropertyName("subnetId")]
    public string? SubnetId { get; set; }

    [JsonPropertyName("publicIPDisabled")]
    public bool PublicIPDisabled { get; set; } = false;

    [JsonPropertyName("keyvaultReferenceIdentityType")]
    public string KeyvaultReferenceIdentityType { get; set; } = "SystemAssigned";

    [JsonPropertyName("keyvaultReferenceIdentityId")]
    public string? KeyvaultReferenceIdentityId { get; set; }

    [JsonPropertyName("metricsReferenceIdentityType")]
    public string MetricsReferenceIdentityType { get; set; } = "SystemAssigned";

    [JsonPropertyName("metricsReferenceIdentityId")]
    public string? MetricsReferenceIdentityId { get; set; }

    [JsonPropertyName("engineBuiltinIdentityType")]
    public string EngineBuiltinIdentityType { get; set; } = "None";

    [JsonPropertyName("engineBuiltinIdentityIds")]
    public string[]? EngineBuiltinIdentityIds { get; set; }
}
