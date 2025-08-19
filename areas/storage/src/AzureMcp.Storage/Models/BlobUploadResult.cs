// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Models;

public sealed record BlobUploadResult(
    [property: JsonPropertyName("blob")] string Blob,
    [property: JsonPropertyName("container")] string Container,
    [property: JsonPropertyName("uploadedFile")] string UploadedFile,
    [property: JsonPropertyName("lastModifed")] DateTimeOffset LastModified,
    [property: JsonPropertyName("eTag")] string ETag,
    [property: JsonPropertyName("md5Hash")] string? MD5Hash
);
