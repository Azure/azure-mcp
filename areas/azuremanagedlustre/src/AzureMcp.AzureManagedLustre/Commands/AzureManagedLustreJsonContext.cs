// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.AzureManagedLustre.Commands.FileSystem;
using AzureMcp.AzureManagedLustre.Models;

namespace AzureMcp.AzureManagedLustre.Commands;

[JsonSerializable(typeof(FileSystemSubnetSizeCommand.FileSystemSubnetSizeResult))]
[JsonSerializable(typeof(FileSystemListCommand.FileSystemListResult))]
[JsonSerializable(typeof(LustreFileSystem))]
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
internal partial class AzureManagedLustreJsonContext : JsonSerializerContext;
