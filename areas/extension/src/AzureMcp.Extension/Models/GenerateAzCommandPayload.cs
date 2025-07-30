// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Extension.Models;

public sealed class GenerateAzCommandPayload
{
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("enable_parameter_injection")]
    public bool EnableParameterInjection { get; set; } = true;
}
