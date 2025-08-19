// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Models;

public record BlobUploadResult(
    string Blob,
    string Container,
    string UploadedFile,
    DateTimeOffset LastModified,
    string ETag,
    string? MD5Hash
);
