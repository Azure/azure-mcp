using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class DatasetListOptions : SubscriptionOptions
{
    public string ProjectEndpoint { get; set; } = string.Empty;
} 