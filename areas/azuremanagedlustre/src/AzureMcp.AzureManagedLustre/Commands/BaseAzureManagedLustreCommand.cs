// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.AzureManagedLustre.Options;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Commands.Subscription;
using Microsoft.Extensions.Logging;

namespace AzureMcp.AzureManagedLustre.Commands;

public abstract class BaseAzureManagedLustreCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>(ILogger<BaseAzureManagedLustreCommand<TOptions>> logger)
    : SubscriptionCommand<TOptions> where TOptions : BaseAzureManagedLustreOptions, new()
{
    // Currently no additional options beyond subscription + resource group
    protected readonly ILogger<BaseAzureManagedLustreCommand<TOptions>> _logger = logger;
}
