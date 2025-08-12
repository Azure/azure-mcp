// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Options.Blob;

public class BlobDownloadOptions : BaseBlobOptions
{
    public string? LocalFilePath { get; set; }
    public bool Overwrite { get; set; }
}
