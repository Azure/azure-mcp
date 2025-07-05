// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.AppConfig.Models;

namespace AzureMcp.Tests.Models.AppConfig;

/// <summary>
/// Represents the result of a key-value show operation.
/// </summary>
public sealed class KeyValueShowResult
{
    [JsonPropertyName("setting")]
    public KeyValueSetting Setting { get; set; } = new();
}
