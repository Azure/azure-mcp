// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Models;

public sealed record DataLakePathInfo(
    string Name,
    string Type,
    long? Size,
    DateTimeOffset? LastModified,
    string? ETag
);
