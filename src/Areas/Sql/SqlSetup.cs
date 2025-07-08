// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Sql.Commands.Database;
using AzureMcp.Areas.Sql.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.Sql;

public class SqlSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ISqlService, SqlService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var sql = new CommandGroup("sql", "Azure SQL operations - Commands for managing Azure SQL databases and servers.");
        rootGroup.AddSubGroup(sql);

        var database = new CommandGroup("db", "SQL database operations");
        sql.AddSubGroup(database);

        database.AddCommand("show", new DatabaseShowCommand(loggerFactory.CreateLogger<DatabaseShowCommand>()));
    }
}
