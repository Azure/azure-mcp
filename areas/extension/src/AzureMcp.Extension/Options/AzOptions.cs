// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Extension.Options;

public class AzOptions : GlobalOptions
{
    [JsonPropertyName(ExtensionOptionDefinitions.Az.IntentName)]
    public string? Intent { get; set; }
}
