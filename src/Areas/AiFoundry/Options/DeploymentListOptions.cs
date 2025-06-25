using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class DeploymentListOptions : SubscriptionOptions
{
    public string? ProjectEndpoint { get; set; }
} 