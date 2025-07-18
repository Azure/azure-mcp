// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Workbooks.Options;
using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Workbooks.Options.Workbook;

public class UpdateWorkbooksOptions : BaseWorkbooksOptions
{
    [JsonPropertyName(WorkbooksOptionDefinitions.WorkbookIdText)]
    public string? WorkbookId { get; set; }

    [JsonPropertyName(WorkbooksOptionDefinitions.DisplayNameText)]
    public string? DisplayName { get; set; }

    [JsonPropertyName(WorkbooksOptionDefinitions.SerializedContentText)]
    public string? SerializedContent { get; set; }
}
