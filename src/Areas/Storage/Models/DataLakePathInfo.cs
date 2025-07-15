// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.Storage.Models;

public record DataLakePathInfo(
    string Name,
    string Type,
    long? Size,
    DateTimeOffset? LastModified,
    string? ETag);