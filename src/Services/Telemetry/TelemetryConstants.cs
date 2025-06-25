// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Services.Telemetry;

internal static class TelemetryConstants
{
    /// <summary>
    /// Name of tags published.
    /// </summary>
    internal class TagName
    {
        internal const string AzureMcpVersion = "Version";
        internal const string SubscriptionGuid = "AzSubscriptionGuid";
        internal const string ResourceHash = "AzResourceHash";
        internal const string MacAddressHash = "MacAddressHash";
        internal const string ClientName = "ClientName";
        internal const string ClientVersion = "ClientVersion";
        internal const string ToolName = "ToolName";
    }

    internal class ActivityName
    {
        internal const string CommandExecuted = "CommandExecuted";
        internal const string ToolExecuted = "ToolExecuted";
    }
}
