// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Extension.Models;

public class GenerateAzCommandPayload
{
    public required string Question { get; set; }

    [JsonPropertyName("enable_parameter_injection")]
    public bool? EnableParameterInjection { get; set; }
}
