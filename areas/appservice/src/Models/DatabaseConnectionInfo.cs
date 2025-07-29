// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.AppService.Models;

public class DatabaseConnectionInfo
{
    public string DatabaseType { get; set; } = string.Empty;
    public string DatabaseServer { get; set; } = string.Empty;
    public string DatabaseName { get; set; } = string.Empty;
    public string ConnectionString { get; set; } = string.Empty;
    public string ConnectionStringName { get; set; } = string.Empty;
    public bool IsConfigured { get; set; }
    public DateTime ConfiguredAt { get; set; }
}
