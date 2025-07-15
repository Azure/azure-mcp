// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Areas.ContainerApps.Options;
using AzureMcp.Commands;
using AzureMcp.Commands.Subscription;

namespace AzureMcp.Areas.ContainerApps.Commands;

public abstract class BaseContainerAppsCommand<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] T>
    : SubscriptionCommand<T> where T : BaseContainerAppsOptions, new()
{
}
