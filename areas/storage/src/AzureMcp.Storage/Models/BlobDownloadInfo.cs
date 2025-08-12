// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Models;

public record BlobDownloadInfo(
    [property: JsonPropertyName("blobName")] string BlobName,
    [property: JsonPropertyName("containerName")] string ContainerName,
    [property: JsonPropertyName("downloadLocation")] string DownloadLocation,
    [property: JsonPropertyName("blobSize")] long BlobSize,
    [property: JsonPropertyName("lastModified")] DateTimeOffset LastModified,
    [property: JsonPropertyName("etag")] string ETag,
    [property: JsonPropertyName("md5Hash")] string? MD5Hash,
    [property: JsonPropertyName("wasLocalFileOverwritten")] bool WasLocalFileOverwritten);
