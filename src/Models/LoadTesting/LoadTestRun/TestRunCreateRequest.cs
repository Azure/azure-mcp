
namespace AzureMcp.Models.LoadTesting.LoadTestRun;

public class TestRunCreateRequest
{
    public string TestId { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public IDictionary<string, string> Secrets { get; set; } = new Dictionary<string, string>();
    public string? Certificate { get; set; } = null;
    public IDictionary<string, string> EnvironmentVariables { get; set; } = new Dictionary<string, string>();
    public string? Description { get; set; } = null;
    public LoadTestConfiguration LoadTestConfiguration { get; set; } = new LoadTestConfiguration();
    public bool DebugLogsEnabled { get; set; } = false;
    public string RequestDataLevel { get; set; } = string.Empty;
}

public class LoadTestConfiguration
{
    public object? OptionalLoadTestConfig { get; set; } = null;
}