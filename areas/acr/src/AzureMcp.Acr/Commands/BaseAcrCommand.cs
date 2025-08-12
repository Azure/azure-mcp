// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Acr.Options;
using AzureMcp.Core.Commands.Subscription;

namespace AzureMcp.Acr.Commands;

public abstract class BaseAcrCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : BaseAcrOptions, new();
