using System.Text.Json.Serialization;

namespace AzureMcp.ApplicationInsights.Models;

public class AppImpactResult
{
    [JsonPropertyName("cloud_RoleName")]
    public string CloudRoleName { get; set; } = string.Empty;

    [JsonPropertyName("impactedInstances")]
    public int ImpactedInstances { get; set; }

    [JsonPropertyName("impactedCount")]
    public int ImpactedCount { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("totalInstances")]
    public int TotalInstances { get; set; }

    [JsonPropertyName("impactedCountPercent")]
    public double ImpactedCountPercent { get; set; }

    [JsonPropertyName("impactedInstancePercent")]
    public double ImpactedInstancePercent { get; set; }
}
