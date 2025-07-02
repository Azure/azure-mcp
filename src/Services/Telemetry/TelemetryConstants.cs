﻿// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Services.Telemetry;

public static class TelemetryConstants
{
    /// <summary>
    /// Name of tags published.
    /// </summary>
    public class TagName
    {
        public const string AzureMcpVersion = "Version";
        public const string SubscriptionGuid = "AzSubscriptionGuid";
        public const string ResourceHash = "AzResourceHash";
        public const string MacAddressHash = "MacAddressHash";
        public const string ClientName = "ClientName";
        public const string ClientVersion = "ClientVersion";
        public const string ToolName = "ToolName";
    }

    public class ActivityName
    {
        public const string CommandExecuted = "CommandExecuted";
        public const string ToolExecuted = "ToolExecuted";
    }
}
