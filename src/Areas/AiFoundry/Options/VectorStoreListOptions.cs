using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class VectorStoreListOptions : SubscriptionOptions
{
    public string ProjectEndpoint { get; set; } = string.Empty;
} 