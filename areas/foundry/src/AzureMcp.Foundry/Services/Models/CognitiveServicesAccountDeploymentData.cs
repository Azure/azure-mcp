// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary>
    /// A class representing the CognitiveServicesAccountDeployment data model.
    /// Cognitive Services account deployment.
    /// </summary>
    internal sealed class CognitiveServicesAccountDeploymentData
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
        public string Location { get; set; }
        /// <summary> The resource model definition representing SKU. </summary>
        public CognitiveServicesSku Sku { get; set; }
        /// <summary> Resource Etag. </summary>
        [JsonPropertyName("etag")]
        public ETag ETag { get; set; }
        /// <summary> Resource tags. </summary>
        public IDictionary<string, string> Tags { get; set; }
        /// <summary> Properties of Cognitive Services account deployment. </summary>
        public CognitiveServicesAccountDeploymentProperties Properties { get; set; }
    }
}
