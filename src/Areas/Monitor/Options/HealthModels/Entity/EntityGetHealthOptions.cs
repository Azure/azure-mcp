// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.Monitor.HealthModels.Entity;

public class EntityGetHealthOptions : BaseMonitorOptions
{
    public string? Entity { get; set; }
    public string? HealthModelName { get; set; }
}
