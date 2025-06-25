using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class VectorStoreDescribeOptions : SubscriptionOptions
{
    public string ProjectEndpoint { get; set; } = string.Empty;
    public string VectorStoreId { get; set; } = string.Empty;
} 