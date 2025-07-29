// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Storage.Models;

public record FileUploadResult(
    string FilePath,
    long FileSize,
    string ContentMd5,
    DateTimeOffset LastModified,
    bool IsOverwrite)
{
    public string Status => IsOverwrite ? "Overwritten" : "Created";
}
