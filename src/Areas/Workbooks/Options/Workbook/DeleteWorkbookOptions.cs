// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;
using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Workbooks.Options.Workbook;

public class DeleteWorkbookOptions : GlobalOptions
{
    [JsonPropertyName(WorkbooksOptionDefinitions.WorkbookIdText)]
    public string? WorkbookId { get; set; }
}
