// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Workbooks.Options;

namespace AzureMcp.Workbooks.Options.Workbook;

public class ShowWorkbooksOptions : BaseWorkbooksOptions
{
    [JsonPropertyName(WorkbooksOptionDefinitions.WorkbookIdText)]
    public string? WorkbookId { get; set; }
}
