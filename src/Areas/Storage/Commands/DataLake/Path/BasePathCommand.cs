// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Storage.Commands.DataLake.FileSystem;
using AzureMcp.Areas.Storage.Options.DataLake;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Storage.Commands.DataLake.Path;

public abstract class BasePathCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : BaseFileSystemCommand<TOptions> where TOptions : BaseFileSystemOptions, new();