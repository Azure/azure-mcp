// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Core.Areas.Server.Models;

/// <summary>
/// Represents an array definition within a tool's input schema.
/// </summary>
public sealed class ArrayItemsSchema
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
}
