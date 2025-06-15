using System.Text.Json.Serialization;

namespace AzureMcp.Models.Foundry;

public class ModelDeploymentInformation
{
    [JsonPropertyName("openai")] public bool OpenAI { get; set; }

    [JsonPropertyName("serverless_endpoint")]
    public bool ServerlessEndpoint { get; set; }

    [JsonPropertyName("managed_compute")] public bool ManagedCompute { get; set; }

    [JsonPropertyName("free_playground")] public bool FreePlayground { get; set; }
}
