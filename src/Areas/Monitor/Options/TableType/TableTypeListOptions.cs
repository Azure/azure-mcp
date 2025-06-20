// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Monitor.Options.TableType;

public class TableTypeListOptions : BaseMonitorOptions, IWorkspaceOptions
{
    public string? Workspace { get; set; }
}
