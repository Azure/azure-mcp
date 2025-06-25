using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class DeploymentDescribeOptions : SubscriptionOptions
{
    public string? ProjectEndpoint { get; set; }
    public string? DeploymentName { get; set; }
} 