using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.BicepSchema
{
    public class BaseBicepSchemaOptions : SubscriptionOptions
    {
        [JsonPropertyName(OptionDefinitions.BicepSchema.ResourceType)]
        public string? ResourceType { get; set; }
    }
}
