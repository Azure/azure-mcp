// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

namespace AzureMcp.Areas.Aks.Services
{
    /// <summary> Describes the Power State of the cluster. </summary>
    internal sealed class AksPowerState
    {
        /// <summary> Tells whether the cluster is Running or Stopped. </summary>
        public string Code { get; set; }
    }
}
