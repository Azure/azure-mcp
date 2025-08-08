// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Aks.Services
{
    /// <summary> Profile of network configuration. </summary>
    internal sealed class AksNetworkProfile
    {
        /// <summary> Network plugin used for building the Kubernetes network. </summary>
        [JsonPropertyName("networkPlugin")]
        public string NetworkPlugin { get; set; }
        /// <summary> The mode the network plugin should use. </summary>
        [JsonPropertyName("networkPluginMode")]
        public string NetworkPluginMode { get; set; }
        /// <summary> Network policy used for building the Kubernetes network. </summary>
        [JsonPropertyName("networkPolicy")]
        public string NetworkPolicy { get; set; }
        /// <summary> This cannot be specified if networkPlugin is anything other than 'azure'. </summary>
        [JsonPropertyName("networkMode")]
        public string NetworkMode { get; set; }
        /// <summary> Network dataplane used in the Kubernetes cluster. </summary>
        [JsonPropertyName("networkDataplane")]
        public string NetworkDataplane { get; set; }
        /// <summary> A CIDR notation IP range from which to assign pod IPs when kubenet is used. </summary>
        [JsonPropertyName("podCidr")]
        public string PodCidr { get; set; }
        /// <summary> A CIDR notation IP range from which to assign service cluster IPs. It must not overlap with any Subnet IP ranges. </summary>
        [JsonPropertyName("serviceCidr")]
        public string ServiceCidr { get; set; }
        /// <summary> An IP address assigned to the Kubernetes DNS service. It must be within the Kubernetes service address range specified in serviceCidr. </summary>
        [JsonPropertyName("dnsServiceIP")]
        public string DnsServiceIP { get; set; }
        /// <summary> This can only be set at cluster creation time and cannot be changed later. For more information see [egress outbound type](https://docs.microsoft.com/azure/aks/egress-outboundtype). </summary>
        [JsonPropertyName("outboundType")]
        public string OutboundType { get; set; }
        /// <summary> The default is 'standard'. See [Azure Load Balancer SKUs](https://docs.microsoft.com/azure/load-balancer/skus) for more information about the differences between load balancer SKUs. </summary>
        [JsonPropertyName("loadBalancerSku")]
        public string LoadBalancerSku { get; set; }
        /// <summary> One IPv4 CIDR is expected for single-stack networking. Two CIDRs, one for each IP family (IPv4/IPv6), is expected for dual-stack networking. </summary>
        [JsonPropertyName("podCidrs")]
        public IList<string> PodCidrs { get; set; }
        /// <summary> One IPv4 CIDR is expected for single-stack networking. Two CIDRs, one for each IP family (IPv4/IPv6), is expected for dual-stack networking. They must not overlap with any Subnet IP ranges. </summary>
        [JsonPropertyName("serviceCidrs")]
        public IList<string> ServiceCidrs { get; set; }
        /// <summary> IP families are used to determine single-stack or dual-stack clusters. For single-stack, the expected value is IPv4. For dual-stack, the expected values are IPv4 and IPv6. </summary>
        [JsonPropertyName("ipFamilies")]
        public IList<string> IPFamilies { get; set; }
    }
}
