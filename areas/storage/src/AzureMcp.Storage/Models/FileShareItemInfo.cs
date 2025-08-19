// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Models;

public record FileShareItemInfo(
    string Name,
    bool IsDirectory,
    long? Size,
    DateTimeOffset? LastModified,
    string? ETag
);
