// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Aks.Services
{
    /// <summary> For more details see [managed AAD on AKS](https://docs.microsoft.com/azure/aks/managed-aad). </summary>
    internal sealed class AksAadProfile
    {
        /// <summary> Whether to enable managed AAD. </summary>
        [JsonPropertyName("managed")]
        public bool? IsManagedAadEnabled { get; set; }
        /// <summary> Whether to enable Azure RBAC for Kubernetes authorization. </summary>
        [JsonPropertyName("enableAzureRBAC")]
        public bool? IsAzureRbacEnabled { get; set; }
        /// <summary> The list of AAD group object IDs that will have admin role of the cluster. </summary>
        [JsonPropertyName("adminGroupObjectIDs")]
        public IList<Guid> AdminGroupObjectIds { get; }
        /// <summary> (DEPRECATED) The client AAD application ID. Learn more at https://aka.ms/aks/aad-legacy. </summary>
        [JsonPropertyName("clientAppID")]
        public Guid? ClientAppId { get; set; }
        /// <summary> (DEPRECATED) The server AAD application ID. Learn more at https://aka.ms/aks/aad-legacy. </summary>
        [JsonPropertyName("serverAppID")]
        public Guid? ServerAppId { get; set; }
        /// <summary> (DEPRECATED) The server AAD application secret. Learn more at https://aka.ms/aks/aad-legacy. </summary>
        [JsonPropertyName("serverAppSecret")]
        public string ServerAppSecret { get; set; }
        /// <summary> The AAD tenant ID to use for authentication. If not specified, will use the tenant of the deployment subscription. </summary>
        [JsonPropertyName("tenantID")]
        public Guid? TenantId { get; set; }
    }
}
