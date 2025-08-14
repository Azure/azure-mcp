// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.


#nullable disable

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> Properties of Cognitive Services account deployment model. </summary>
    internal sealed class CognitiveServicesAccountDeploymentModel
    {
        /// <summary> Deployment model publisher. </summary>
        public string Publisher { get; set; }
        /// <summary> Deployment model format. </summary>
        public string Format { get; set; }
        /// <summary> Deployment model name. </summary>
        public string Name { get; set; }
        /// <summary> Optional. Deployment model version. If version is not specified, a default version will be assigned. The default version is different for different models and might change when there is new version available for a model. Default version for a model could be found from list models API. </summary>
        public string Version { get; set; }
        /// <summary> Optional. Deployment model source ARM resource ID. </summary>
        public string Source { get; set; }
        /// <summary> Optional. Source of the model, another Microsoft.CognitiveServices accounts ARM resource ID. </summary>
        public string SourceAccount { get; set; }
        /// <summary> The call rate limit Cognitive Services account. </summary>
        public ServiceAccountCallRateLimit CallRateLimit { get; set; }
    }
}
