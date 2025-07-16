// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Workbooks.Options;
using AzureMcp.Options;
using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Workbooks.Options.Workbook;

public class CreateWorkbookOptions : SubscriptionOptions
{
  [JsonPropertyName(WorkbooksOptionDefinitions.TitleText)]
  public string? Title { get; set; }

  [JsonPropertyName(WorkbooksOptionDefinitions.SerializedContentText)]
  public string? SerializedContent { get; set; }

  [JsonPropertyName(WorkbooksOptionDefinitions.SourceIdText)]
  public string? SourceId { get; set; }
}