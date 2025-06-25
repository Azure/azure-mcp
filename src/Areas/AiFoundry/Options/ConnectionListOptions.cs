using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class ConnectionListOptions : SubscriptionOptions
{
    public string? ProjectEndpoint { get; set; }
} 