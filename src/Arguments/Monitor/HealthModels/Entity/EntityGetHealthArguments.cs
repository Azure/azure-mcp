// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Arguments.Monitor.HealthModels.Entity;

public class EntityGetHealthArguments : BaseMonitorArguments
{
    public string? Entity { get; set; }
    public string? HealthModelName { get; set; }
}
