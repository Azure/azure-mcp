// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using System.Text.Json.Serialization;

namespace AzureMcp.AzureManagedLustre.Options.FileSystem;

public sealed class FileSystemSubnetSizeOptions : BaseAzureManagedLustreOptions
{
    [property: JsonPropertyName("sku")]
    public string? Sku { get; set; }

    [property: JsonPropertyName("size")]
    public int Size { get; set; }
}
