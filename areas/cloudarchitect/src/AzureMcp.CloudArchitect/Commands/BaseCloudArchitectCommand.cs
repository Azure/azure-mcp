// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.CloudArchitect.Options;
using AzureMcp.Core.Commands;

namespace AzureMcp.CloudArchitect.Commands;

public abstract class BaseCloudArchitectCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : GlobalCommand<T>
    where T : BaseCloudArchitectOptions, new()
{
}
