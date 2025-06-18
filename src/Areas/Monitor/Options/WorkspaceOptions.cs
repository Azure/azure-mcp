// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Monitor
{
    public abstract class WorkspaceOptions : BaseMonitorOptions, IWorkspaceOptions
    {
        [JsonPropertyName(OptionDefinitions.Monitor.WorkspaceIdOrName)]
        public string? Workspace { get; set; }
    }
}
