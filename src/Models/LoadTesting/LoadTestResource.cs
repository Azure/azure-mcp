namespace AzureMcp.Models.LoadTesting;

public class LoadTestResource
{
    public string Id { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Type { get; set; } = string.Empty;
    public string? Location { get; set; } = string.Empty;
    public IDictionary<string, string>? Tags { get; set; } = new Dictionary<string, string>();
    public LoadTestProperties? Properties { get; set; } = new LoadTestProperties();
}