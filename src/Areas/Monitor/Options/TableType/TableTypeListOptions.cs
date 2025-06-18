// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Monitor.Options;

namespace AzureMcp.Areas.Monitor.Options.TableType;

public class TableTypeListOptions : BaseMonitorOptions, IWorkspaceOptions
{
    public string? Workspace { get; set; }
}
