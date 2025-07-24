// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.ContainerApps.Options;

public abstract class BaseContainerAppsOptions : SubscriptionOptions
{
    public string? Environment { get; set; }
}
