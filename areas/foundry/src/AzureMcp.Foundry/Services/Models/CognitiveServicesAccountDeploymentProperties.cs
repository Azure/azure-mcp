// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Properties of Cognitive Services account deployment. </summary>
    internal sealed class CognitiveServicesAccountDeploymentProperties
    {
        /// <summary> Gets the status of the resource at the time the operation was called. </summary>
        public string ProvisioningState { get; set; }
        /// <summary> Properties of Cognitive Services account deployment model. </summary>
        public CognitiveServicesAccountDeploymentModel Model { get; set; }
        /// <summary> Properties of Cognitive Services account deployment model. (Deprecated, please use Deployment.sku instead.). </summary>
        public CognitiveServicesAccountDeploymentScaleSettings ScaleSettings { get; set; }
        /// <summary> The capabilities. </summary>
        public IReadOnlyDictionary<string, string> Capabilities { get; set; }
        /// <summary> The name of RAI policy. </summary>
        public string RaiPolicyName { get; set; }
        /// <summary> The call rate limit Cognitive Services account. </summary>
        public ServiceAccountCallRateLimit CallRateLimit { get; set; }
        /// <summary> Gets the rate limits. </summary>
        public IReadOnlyList<ServiceAccountThrottlingRule> RateLimits { get; set; }
        /// <summary> Deployment model version upgrade option. </summary>
        public string VersionUpgradeOption { get; set; }
        /// <summary> If the dynamic throttling is enabled. </summary>
        [JsonPropertyName("dynamicThrottlingEnabled")]
        public bool? IsDynamicThrottlingEnabled { get; set; }
        /// <summary> The current capacity. </summary>
        public int? CurrentCapacity { get; set; }
        /// <summary> Internal use only. </summary>
        public DeploymentCapacitySettings CapacitySettings { get; set; }
        /// <summary> The name of parent deployment. </summary>
        public string ParentDeploymentName { get; set; }
        /// <summary> Specifies the deployment name that should serve requests when the request would have otherwise been throttled due to reaching current deployment throughput limit. </summary>
        public string SpilloverDeploymentName { get; set; }
    }
}
