// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.Workbooks.Models;

namespace AzureMcp.Workbooks.Options.Workbook;

public class ListWorkbooksOptions : SubscriptionOptions
{
    public string? Kind { get; set; }
    public string? Category { get; set; }
    public string? SourceId { get; set; }

    /// <summary>
    /// Creates a WorkbookFilters object from the command options.
    /// </summary>
    public WorkbookFilters ToFilters() => new()
    {
        Kind = Kind,
        Category = Category,
        SourceId = SourceId
    };
}
