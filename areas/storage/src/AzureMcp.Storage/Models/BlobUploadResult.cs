// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Models;

public record BlobUploadResult(
    [property: JsonPropertyName("blobName")] string BlobName,
    [property: JsonPropertyName("containerName")] string ContainerName,
    [property: JsonPropertyName("uploadedFile")] string UploadedFile,
    [property: JsonPropertyName("lastModified")] DateTimeOffset LastModified,
    [property: JsonPropertyName("etag")] string ETag,
    [property: JsonPropertyName("md5Hash")] string? MD5Hash,
    [property: JsonPropertyName("wasOverwritten")] bool WasOverwritten
);
