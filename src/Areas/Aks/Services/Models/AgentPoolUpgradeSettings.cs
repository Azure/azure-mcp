// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Aks.Services
{
    /// <summary> Settings for upgrading an agentpool. </summary>
    internal sealed class AgentPoolUpgradeSettings
    {
        /// <summary> This can either be set to an integer (e.g. '5') or a percentage (e.g. '50%'). If a percentage is specified, it is the percentage of the total agent pool size at the time of the upgrade. For percentages, fractional nodes are rounded up. If not specified, the default is 1. For more information, including best practices, see: https://docs.microsoft.com/azure/aks/upgrade-cluster#customize-node-surge-upgrade. </summary>
        [JsonPropertyName("maxSurge")]
        public string MaxSurge { get; set; }
        /// <summary> The amount of time (in minutes) to wait on eviction of pods and graceful termination per node. This eviction wait time honors waiting on pod disruption budgets. If this time is exceeded, the upgrade fails. If not specified, the default is 30 minutes. </summary>
        [JsonPropertyName("drainTimeoutInMinutes")]
        public int? DrainTimeoutInMinutes { get; set; }
    }
}
