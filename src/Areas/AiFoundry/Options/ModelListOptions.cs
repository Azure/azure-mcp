using AzureMcp.Options;

namespace AzureMcp.Areas.AiFoundry.Options;

public class ModelListOptions : SubscriptionOptions
{
    public string? ProjectEndpoint { get; set; }
    public string? ModelProvider { get; set; }
    public string? Category { get; set; }
} 