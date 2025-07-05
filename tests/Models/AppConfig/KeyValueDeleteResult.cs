// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Tests.Models.AppConfig;

/// <summary>
/// Represents the result of a key-value delete operation.
/// </summary>
public sealed class KeyValueDeleteResult
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string? Label { get; set; }
}
