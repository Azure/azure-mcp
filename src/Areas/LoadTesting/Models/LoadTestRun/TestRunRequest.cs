// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.LoadTesting.Models.LoadTest;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTestRun;

public class TestRunRequest
{
    [JsonPropertyName("testId")]
    public string TestId { get; set; } = string.Empty;

    [JsonPropertyName("displayName")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("secrets")]
    public IDictionary<string, string> Secrets { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("certificate")]
    public string? Certificate { get; set; } = null;

    [JsonPropertyName("environmentVariables")]
    public IDictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;

    [JsonPropertyName("loadTestConfiguration")]
    public LoadTestConfiguration LoadTestConfiguration { get; set; } = new LoadTestConfiguration();

    [JsonPropertyName("debugLogsEnabled")]
    public bool? DebugLogsEnabled { get; set; } = false;

    [JsonPropertyName("requestDataLevel")]
    public RequestDataLevel? RequestDataLevel { get; set; }
}
