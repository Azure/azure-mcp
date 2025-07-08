using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class AutoStopCriteria
{
    [JsonProperty("autoStopDisabled")]
    public bool AutoStopDisabled { get; set; } = false;

    [JsonProperty("errorRate")]
    public int ErrorRate { get; set; } = 90;

    [JsonProperty("errorRateTimeWindowInSeconds")]
    public int ErrorRateTimeWindowInSeconds { get; set; } = 60;

    [JsonProperty("maximumVirtualUsersPerEngine")]
    public int MaximumVirtualUsersPerEngine { get; set; } = 5000;
}