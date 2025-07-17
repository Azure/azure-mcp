// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Options;

namespace AzureMcp.Areas.Canvases.Options;

public class BaseWorkbooksOptions : GlobalOptions
{
    [JsonPropertyName(WorkbooksOptionDefinitions.MessageText)]
    public string? Message { get; set; }
}
