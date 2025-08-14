// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Internal use only. </summary>
    internal sealed class DeploymentCapacitySettings
    {
        /// <summary> The designated capacity. </summary>
        public int? DesignatedCapacity { get; set; }
        /// <summary> The priority of this capacity setting. </summary>
        public int? Priority { get; set; }
    }
}
