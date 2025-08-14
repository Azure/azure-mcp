// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureManagedLustre.Options.FileSystem;

public sealed class FileSystemSubnetSizeOptions : BaseAzureManagedLustreOptions
{
    public string? Sku { get; set; } 
    public int Size { get; set; }
}
