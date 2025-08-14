// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Core.Services.Telemetry;

internal static class AzureTelemetryConstants
{
    /// <summary>
    /// Azure-specific telemetry tag names.
    /// </summary>
    internal class TagName
    {
        public const string ResourceHash = "AzResourceHash";
        public const string SubscriptionGuid = "AzSubscriptionGuid";
    }
}