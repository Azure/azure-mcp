using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTest;

public class AutoStopCriteria
{
    [JsonProperty("autoStopDisabled")]
    public bool AutoStopDisabled { get; set; }

    [JsonProperty("errorRate")]
    public int ErrorRate { get; set; }

    [JsonProperty("errorRateTimeWindowInSeconds")]
    public int ErrorRateTimeWindowInSeconds { get; set; }

    [JsonProperty("maximumVirtualUsersPerEngine")]
    public int MaximumVirtualUsersPerEngine { get; set; }
}