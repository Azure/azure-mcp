// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Canvases.Options;
using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Canvases.Options.Workbook;

public class UpdateWorkbooksOptions : BaseWorkbooksOptions
{
    [JsonPropertyName(WorkbooksOptionDefinitions.WorkbookIdText)]
    public string? WorkbookId { get; set; }

    [JsonPropertyName(WorkbooksOptionDefinitions.TitleText)]
    public string? Title { get; set; }

    [JsonPropertyName(WorkbooksOptionDefinitions.SerializedContentText)]
    public string? SerializedContent { get; set; }
}
