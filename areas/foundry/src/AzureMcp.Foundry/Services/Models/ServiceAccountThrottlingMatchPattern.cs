// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

namespace AzureMcp.Foundry.Services.Models
{
    /// <summary> The ServiceAccountThrottlingMatchPattern. </summary>
    internal sealed class ServiceAccountThrottlingMatchPattern
    {
        /// <summary> Gets the path. </summary>
        public string Path { get; set; }
        /// <summary> Gets the method. </summary>
        public string Method { get; set; }
    }
}
