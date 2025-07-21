// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.Aks.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;

namespace AzureMcp.Areas.Aks.Commands;

public abstract class BaseAksCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : BaseAksOptions, new();
