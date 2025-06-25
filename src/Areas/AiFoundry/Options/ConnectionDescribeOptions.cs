using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class ConnectionDescribeOptions : SubscriptionOptions
{
    public string? ProjectEndpoint { get; set; }
    public string? ConnectionName { get; set; }
} 