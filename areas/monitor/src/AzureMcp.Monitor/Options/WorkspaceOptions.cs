// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Monitor.Options
{
    public abstract class WorkspaceOptions : BaseMonitorOptions, IWorkspaceOptions
    {
        [JsonPropertyName(WorkspaceOptionDefinitions.WorkspaceIdOrName)]
        public string? Workspace { get; set; }
    }
}
