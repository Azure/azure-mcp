using Newtonsoft.Json;

namespace AzureMcp.Models.LoadTesting.LoadTestRun;

public class TestRunCreateRequest
{
    [JsonProperty("testRunRequest")]
    public TestRunRequest TestRunRequest { get; set; } = new();
}