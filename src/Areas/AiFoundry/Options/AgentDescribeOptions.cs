using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class AgentDescribeOptions : SubscriptionOptions
{
    public string? ProjectEndpoint { get; set; }
    public string? AgentId { get; set; }
} 