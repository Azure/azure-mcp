// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Models;

// Lightweight projection of StorageAccountData with commonly useful metadata.
// Keep property names stable; only add new nullable properties to extend.
public sealed record StorageAccountInfo(
    string Name,
    string? Location,
    string? Kind,
    string? SkuName,
    string? SkuTier,
    [property: JsonPropertyName("hnsEnabled")] bool? IsHnsEnabled,
    bool? AllowBlobPublicAccess,
    bool? EnableHttpsTrafficOnly);
