using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.BicepSchema.Options
{
    public class BaseBicepSchemaOptions : SubscriptionOptions
    {
        [JsonPropertyName(BicepSchemaOptionDefinitions.ResourceType)]
        public string? ResourceType { get; set; }
    }
}
