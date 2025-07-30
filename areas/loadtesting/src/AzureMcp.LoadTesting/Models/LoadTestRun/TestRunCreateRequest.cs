// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
namespace AzureMcp.LoadTesting.Models.LoadTestRun;

public class TestRunCreateRequest
{
    /// <summary>
    /// Gets or sets the test run request details.
    /// </summary>
    [JsonPropertyName("testRunRequest")]
    public TestRunRequest TestRunRequest { get; set; } = new();
}
