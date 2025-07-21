// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Aks.Services
{
    /// <summary>
    /// A class representing the AksClusterProperties data model.
    /// </summary>
    internal sealed class AksClusterProperties
    {
        /// <summary> The current provisioning state. </summary>
        [JsonPropertyName("provisioningState")]
        public string ProvisioningState { get; set; }
        /// <summary> The Power State of the cluster. </summary>
        [JsonPropertyName("powerState")]
        public AksPowerState PowerState { get; set; }
        /// <summary> The max number of agent pools for the managed cluster. </summary>
        [JsonPropertyName("maxAgentPools")]
        public int? MaxAgentPools { get; set; }
        /// <summary> Both patch version &lt;major.minor.patch&gt; (e.g. 1.20.13) and &lt;major.minor&gt; (e.g. 1.20) are supported. When &lt;major.minor&gt; is specified, the latest supported GA patch version is chosen automatically. Updating the cluster with the same &lt;major.minor&gt; once it has been created (e.g. 1.14.x -&gt; 1.14) will not trigger an upgrade, even if a newer patch version is available. When you upgrade a supported AKS cluster, Kubernetes minor versions cannot be skipped. All upgrades must be performed sequentially by major version number. For example, upgrades between 1.14.x -&gt; 1.15.x or 1.15.x -&gt; 1.16.x are allowed, however 1.14.x -&gt; 1.16.x is not allowed. See [upgrading an AKS cluster](https://docs.microsoft.com/azure/aks/upgrade-cluster) for more details. </summary>
        [JsonPropertyName("kubernetesVersion")]
        public string KubernetesVersion { get; set; }
        /// <summary> If kubernetesVersion was a fully specified version &lt;major.minor.patch&gt;, this field will be exactly equal to it. If kubernetesVersion was &lt;major.minor&gt;, this field will contain the full &lt;major.minor.patch&gt; version being used. </summary>
        [JsonPropertyName("currentKubernetesVersion")]
        public string CurrentKubernetesVersion { get; set; }
        /// <summary> This cannot be updated once the Managed Cluster has been created. </summary>
        [JsonPropertyName("dnsPrefix")]
        public string DnsPrefix { get; set; }
        /// <summary> This cannot be updated once the Managed Cluster has been created. </summary>
        [JsonPropertyName("fqdnSubdomain")]
        public string FqdnSubdomain { get; set; }
        /// <summary> The FQDN of the master pool. </summary>
        [JsonPropertyName("fqdn")]
        public string Fqdn { get; set; }
        /// <summary> The FQDN of private cluster. </summary>
        [JsonPropertyName("privateFQDN")]
        public string PrivateFqdn { get; set; }
        /// <summary> The Azure Portal requires certain Cross-Origin Resource Sharing (CORS) headers to be sent in some responses, which Kubernetes APIServer doesn't handle by default. This special FQDN supports CORS, allowing the Azure Portal to function properly. </summary>
        [JsonPropertyName("azurePortalFQDN")]
        public string AzurePortalFqdn { get; set; }
        /// <summary> The agent pool properties. </summary>
        [JsonPropertyName("agentPoolProfiles")]
        public IList<AksAgentPoolProfile> AgentPoolProfiles { get; set; }
        /// <summary> The name of the resource group containing agent pool nodes. </summary>
        [JsonPropertyName("nodeResourceGroup")]
        public string NodeResourceGroup { get; set; }
        /// <summary> Whether to enable Kubernetes Role-Based Access Control. </summary>
        [JsonPropertyName("enableRBAC")]
        public bool? EnableRbac { get; set; }
        /// <summary> The support plan for the Managed Cluster. If unspecified, the default is 'KubernetesOfficial'. </summary>
        [JsonPropertyName("supportPlan")]
        public string SupportPlan { get; set; }
        /// <summary> (DEPRECATED) Whether to enable Kubernetes pod security policy (preview). PodSecurityPolicy was deprecated in Kubernetes v1.21, and removed from Kubernetes in v1.25. Learn more at https://aka.ms/k8s/psp and https://aka.ms/aks/psp. </summary>
        [JsonPropertyName("enablePodSecurityPolicy")]
        public bool? EnablePodSecurityPolicy { get; set; }
        /// <summary> The network configuration profile. </summary>
        [JsonPropertyName("networkProfile")]
        public AksNetworkProfile NetworkProfile { get; set; }
        /// <summary> The Azure Active Directory configuration. </summary>
        [JsonPropertyName("aadProfile")]
        public AksAadProfile AadProfile { get; set; }
        /// <summary> This is of the form: '/subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/Microsoft.Compute/diskEncryptionSets/{encryptionSetName}'. </summary>
        [JsonPropertyName("diskEncryptionSetID")]
        public string DiskEncryptionSetId { get; set; }
        /// <summary> If set to true, getting static credentials will be disabled for this cluster. This must only be used on Managed Clusters that are AAD enabled. For more details see [disable local accounts](https://docs.microsoft.com/azure/aks/managed-aad#disable-local-accounts-preview). </summary>
        [JsonPropertyName("disableLocalAccounts")]
        public bool? DisableLocalAccounts { get; set; }
        /// <summary> The resourceUID uniquely identifies ManagedClusters that reuse ARM ResourceIds (i.e: create, delete, create sequence). </summary>
        [JsonPropertyName("resourceUID")]
        public string ResourceId { get; set; }

        // Read the JSON response content and create a model instance from it.
        public static AksClusterProperties FromJson(BinaryData source)
        {
#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
            return source.ToObjectFromJson<AksClusterProperties>();
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
        }
    }
}
