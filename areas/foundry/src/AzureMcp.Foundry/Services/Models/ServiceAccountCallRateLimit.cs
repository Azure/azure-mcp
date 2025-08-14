// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> The call rate limit Cognitive Services account. </summary>
    internal sealed class ServiceAccountCallRateLimit
    {
        /// <summary> The count value of Call Rate Limit. </summary>
        public float? Count { get; set; }
        /// <summary> The renewal period in seconds of Call Rate Limit. </summary>
        public float? RenewalPeriod { get; set; }
        /// <summary> Gets the rules. </summary>
        public IReadOnlyList<ServiceAccountThrottlingRule> Rules { get; set; }
    }
}
