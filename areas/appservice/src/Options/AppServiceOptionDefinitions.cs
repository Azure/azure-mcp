// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.AppService.Options;

public static class AppServiceOptionDefinitions
{
    public const string AppNameName = "app-name";
    public const string DatabaseTypeName = "database-type";
    public const string DatabaseServerName = "database-server";
    public const string DatabaseNameName = "database-name";
    public const string ConnectionStringName = "connection-string";

    public static readonly Option<string> AppName = new(
        $"--{AppNameName}",
        "The name of the Azure App Service application."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> DatabaseType = new(
        $"--{DatabaseTypeName}",
        "The type of database to add (e.g., 'SqlServer', 'MySql', 'PostgreSql', 'CosmosDb')."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> DatabaseServer = new(
        $"--{DatabaseServerName}",
        "The name or connection string of the database server."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> DatabaseName = new(
        $"--{DatabaseNameName}",
        "The name of the database to connect to."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> ConnectionString = new(
        $"--{ConnectionStringName}",
        "The complete connection string for the database (optional, will be constructed if not provided)."
    )
    {
        IsRequired = false
    };
}
