// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.MySql.Commands.Database;
using AzureMcp.MySql.Commands.Server;
using AzureMcp.MySql.Commands.Table;
using AzureMcp.MySql.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.MySql;

public class MySqlSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IMySqlService, MySqlService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var mysql = new CommandGroup("mysql", "MySQL operations - Commands for managing Azure Database for MySQL Flexible Server resources. Includes operations for listing servers and databases, executing SQL queries, managing table schemas, and configuring server parameters.");
        rootGroup.AddSubGroup(mysql);

        var database = new CommandGroup("database", "MySQL database operations");
        mysql.AddSubGroup(database);
        database.AddCommand("list", new DatabaseListCommand(loggerFactory.CreateLogger<DatabaseListCommand>()));
        database.AddCommand("query", new DatabaseQueryCommand(loggerFactory.CreateLogger<DatabaseQueryCommand>()));

        var table = new CommandGroup("table", "MySQL table operations");
        mysql.AddSubGroup(table);
        table.AddCommand("list", new TableListCommand(loggerFactory.CreateLogger<TableListCommand>()));
        table.AddCommand("schema", new TableSchemaCommand(loggerFactory.CreateLogger<TableSchemaCommand>()));

        var server = new CommandGroup("server", "MySQL server operations");
        mysql.AddSubGroup(server);
        server.AddCommand("list", new ServerListCommand(loggerFactory.CreateLogger<ServerListCommand>()));
        server.AddCommand("config", new ServerConfigCommand(loggerFactory.CreateLogger<ServerConfigCommand>()));
        server.AddCommand("param", new ServerParamCommand(loggerFactory.CreateLogger<ServerParamCommand>()));
        server.AddCommand("setparam", new ServerSetParamCommand(loggerFactory.CreateLogger<ServerSetParamCommand>()));
    }
}