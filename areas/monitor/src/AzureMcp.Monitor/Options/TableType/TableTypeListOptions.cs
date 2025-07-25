// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Monitor.Options.TableType;

public class TableTypeListOptions : BaseMonitorOptions, IWorkspaceOptions
{
    public string? Workspace { get; set; }
}
