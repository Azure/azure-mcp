// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Monitor.Options;

namespace AzureMcp.Areas.Monitor.Options.HealthModels.Entity;

public class EntityGetHealthOptions : BaseMonitorOptions
{
    public string? Entity { get; set; }
    public string? HealthModelName { get; set; }
}
