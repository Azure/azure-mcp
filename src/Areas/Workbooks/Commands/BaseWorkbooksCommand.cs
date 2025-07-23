// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Workbooks.Options;
using AzureMcp.Commands;

namespace AzureMcp.Areas.Workbooks.Commands;

public abstract class BaseWorkbooksCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : GlobalCommand<T>
    where T : BaseWorkbooksOptions, new();
