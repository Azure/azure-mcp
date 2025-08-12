// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Aks.Commands;

namespace AzureMcp.Aks.Services
{
    /// <summary>
    /// A class representing the AksCluster data model.
    /// </summary>
    internal sealed class AksCluster
    {
        /// <summary> The resource ID for the resource. </summary>
        [JsonPropertyName("id")]
        public string ResourceId { get; set; }
        /// <summary> The type of the resource. </summary>
        [JsonPropertyName("type")]
        public string ResourceType { get; set; }
        /// <summary> The name of the resource. </summary>
        [JsonPropertyName("name")]
        public string ResourceName { get; set; }
        /// <summary> The location of the resource. </summary>
        [JsonPropertyName("location")]
        public string Location { get; set; }
        /// <summary> The SKU of the resource. </summary>
        [JsonPropertyName("sku")]
        public ManagedClusterSku Sku { get; set; }
        /// <summary> The identity type of the resource. </summary>
        [JsonPropertyName("identityType")]
        public string IdentityType { get; set; }
        /// <summary> The tags of the resource. </summary>
        [JsonPropertyName("tags")]
        public IDictionary<string, string> Tags { get; set; }
        /// <summary> The properties of the cluster. </summary>
        [JsonPropertyName("properties")]
        public AksClusterProperties Properties { get; set; }

        // Read the JSON response content and create a model instance from it.
        public static AksCluster FromJson(JsonElement source)
        {
            return JsonSerializer.Deserialize<AksCluster>(source, AksJsonContext.Default.AksCluster);
        }
    }
}
