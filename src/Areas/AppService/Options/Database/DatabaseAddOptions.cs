// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.AppService.Options.Database;

public class DatabaseAddOptions : BaseAppServiceOptions
{
    public string? DatabaseType { get; set; }
    public string? DatabaseServer { get; set; }
    public string? DatabaseName { get; set; }
    public string? ConnectionString { get; set; }
}
