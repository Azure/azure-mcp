// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.LoadTesting.Models.LoadTestRun;

public class TestRun
{
    [JsonPropertyName("testId")]
    public string TestId { get; set; } = string.Empty;
    
    [JsonPropertyName("testRunId")]
    public string? TestRunId { get; set; } = string.Empty;
    
    [JsonPropertyName("displayName")]
    public string? DisplayName { get; set; } = string.Empty;
    
    [JsonPropertyName("virtualUsers")]
    public int? VirtualUsers { get; set; } = 0;
    
    [JsonPropertyName("status")]
    public string? Status { get; set; } = string.Empty;
    
    [JsonPropertyName("startDateTime")]
    public DateTimeOffset? StartDateTime { get; set; } = null;
    
    [JsonPropertyName("endDateTime")]
    public DateTimeOffset? EndDateTime { get; set; } = null;
    
    [JsonPropertyName("executedDateTime")]
    public DateTimeOffset? ExecutedDateTime { get; set; } = null;
    
    [JsonPropertyName("portalUrl")]
    public string? PortalUrl { get; set; } = string.Empty;
    
    [JsonPropertyName("duration")]
    public int? Duration { get; set; } = 0;
    
    [JsonPropertyName("createdDateTime")]
    public DateTimeOffset? CreatedDateTime { get; set; } = null;
    
    [JsonPropertyName("createdBy")]
    public string? CreatedBy { get; set; } = string.Empty;
    
    [JsonPropertyName("lastModifiedDateTime")]
    public DateTimeOffset? LastModifiedDateTime { get; set; } = null;
    
    [JsonPropertyName("lastModifiedBy")]
    public string? LastModifiedBy { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string? Description { get; set; } = string.Empty;

    [JsonPropertyName("testResult")]
    public string? TestResult { get; set; } = string.Empty;
}