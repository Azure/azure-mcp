using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class ModelDescribeOptions : SubscriptionOptions
{
    public string? ProjectEndpoint { get; set; }
    public string? ModelId { get; set; }
} 