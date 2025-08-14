// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Properties of Cognitive Services account deployment model. (Deprecated, please use Deployment.sku instead.). </summary>
    internal sealed class CognitiveServicesAccountDeploymentScaleSettings
    {
        /// <summary> Deployment scale type. </summary>
        public string ScaleType { get; set; }
        /// <summary> Deployment capacity. </summary>
        public int? Capacity { get; set; }
        /// <summary> Deployment active capacity. This value might be different from `capacity` if customer recently updated `capacity`. </summary>
        public int? ActiveCapacity { get; set; }
    }
}
