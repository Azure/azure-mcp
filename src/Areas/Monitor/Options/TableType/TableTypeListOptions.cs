// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.Monitor.TableType;

public class TableTypeListOptions : BaseMonitorOptions, IWorkspaceOptions
{
    public string? Workspace { get; set; }
}
