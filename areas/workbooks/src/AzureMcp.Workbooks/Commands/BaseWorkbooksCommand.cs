// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Core.Commands;
using AzureMcp.Workbooks.Options;

namespace AzureMcp.Workbooks.Commands;

public abstract class BaseWorkbooksCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : GlobalCommand<T>
    where T : BaseWorkbooksOptions, new();
