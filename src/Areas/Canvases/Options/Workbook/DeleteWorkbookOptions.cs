// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Canvases.Options;
using AzureMcp.Options;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Canvases.Options.Workbook;

public class DeleteWorkbookOptions : GlobalOptions
{
    [JsonPropertyName(WorkbooksOptionDefinitions.WorkbookIdText)]
    public string? WorkbookId { get; set; }
}
