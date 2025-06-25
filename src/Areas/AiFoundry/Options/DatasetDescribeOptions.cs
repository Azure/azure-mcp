using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class DatasetDescribeOptions : SubscriptionOptions
{
    public string ProjectEndpoint { get; set; } = string.Empty;
    public string DatasetId { get; set; } = string.Empty;
} 