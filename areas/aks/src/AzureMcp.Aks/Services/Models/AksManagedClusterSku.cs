// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Aks.Services
{
    /// <summary> The SKU of a Managed Cluster. </summary>
    internal sealed class ManagedClusterSku
    {
        /// <summary> The name of a managed cluster SKU. </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
        /// <summary> If not specified, the default is 'Free'. See [AKS Pricing Tier](https://learn.microsoft.com/azure/aks/free-standard-pricing-tiers) for more details. </summary>
        [JsonPropertyName("tier")]
        public string Tier { get; set; }
    }
}
