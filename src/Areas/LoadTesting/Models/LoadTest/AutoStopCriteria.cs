using System.Text.Json.Serialization;


namespace AzureMcp.Areas.LoadTesting.Models.LoadTest;

public class AutoStopCriteria
{
    [JsonPropertyName("autoStopDisabled")]
    public bool AutoStopDisabled { get; set; } = false;

    [JsonPropertyName("errorRate")]
    public int ErrorRate { get; set; } = 90;

    [JsonPropertyName("errorRateTimeWindowInSeconds")]
    public int ErrorRateTimeWindowInSeconds { get; set; } = 60;

    [JsonPropertyName("maximumVirtualUsersPerEngine")]
    public int MaximumVirtualUsersPerEngine { get; set; } = 5000;
}