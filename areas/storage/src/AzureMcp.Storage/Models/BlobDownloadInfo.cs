// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Models;

public class BlobDownloadInfo
{
    public string BlobName { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
    public string DownloadLocation { get; set; } = string.Empty;
    public long BlobSize { get; set; }
    public DateTimeOffset LastModified { get; set; }
    public string ETag { get; set; } = string.Empty;
    public string? MD5Hash { get; set; }
    public bool WasLocalFileOverwritten { get; set; }
}
