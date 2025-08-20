// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Storage.Models;

public record BlobDownloadInfo(
    [property: JsonPropertyName("blob")] string Blob,
    [property: JsonPropertyName("container")] string Container,
    [property: JsonPropertyName("downloadLocation")] string DownloadLocation,
    [property: JsonPropertyName("blobSize")] long BlobSize,
    [property: JsonPropertyName("lastModified")] DateTimeOffset LastModified,
    [property: JsonPropertyName("eTag")] string ETag,
    [property: JsonPropertyName("md5Hash")] string? MD5Hash,
    [property: JsonPropertyName("wasLocalFileOverwritten")] bool WasLocalFileOverwritten);
