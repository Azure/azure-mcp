// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Storage.Options.DataLake.Path;

public class PathListOptions : BaseFileSystemOptions
{
    [JsonPropertyName(StorageOptionDefinitions.PathPrefix)]
    public string? PathPrefix { get; set; }
}