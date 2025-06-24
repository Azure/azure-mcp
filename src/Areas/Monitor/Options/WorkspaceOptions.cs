// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Monitor.Options
{
    public abstract class WorkspaceOptions : BaseMonitorOptions, IWorkspaceOptions
    {
        [JsonPropertyName(WorkspaceLogQueryOptionDefinitions.WorkspaceIdOrName)]
        public string? Workspace { get; set; }
    }
}
