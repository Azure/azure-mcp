// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Postgres.Commands.Database;
using AzureMcp.Postgres.Commands.Server;
using AzureMcp.Postgres.Commands.Table;
using AzureMcp.Postgres.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Postgres;

public class PostgresSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IPostgresService, PostgresService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        var pg = new CommandGroup("postgres", "PostgreSQL operations - Commands for listing and managing Azure Database for PostgreSQL - Flexible server.");
        rootGroup.AddSubGroup(pg);

        var database = new CommandGroup("database", "PostgreSQL database operations");
        pg.AddSubGroup(database);
        database.AddCommand("list", new DatabaseListCommand(loggerFactory.CreateLogger<DatabaseListCommand>()));
        database.AddCommand("query", new DatabaseQueryCommand(loggerFactory.CreateLogger<DatabaseQueryCommand>()));

        var table = new CommandGroup("table", "PostgreSQL table operations");
        pg.AddSubGroup(table);
        table.AddCommand("list", new TableListCommand(loggerFactory.CreateLogger<TableListCommand>()));
        table.AddCommand("schema", new GetSchemaCommand(loggerFactory.CreateLogger<GetSchemaCommand>()));

        var server = new CommandGroup("server", "PostgreSQL server operations");
        pg.AddSubGroup(server);
        server.AddCommand("list", new ServerListCommand(loggerFactory.CreateLogger<ServerListCommand>()));
        server.AddCommand("config", new GetConfigCommand(loggerFactory.CreateLogger<GetConfigCommand>()));
        server.AddCommand("param", new GetParamCommand(loggerFactory.CreateLogger<GetParamCommand>()));
        server.AddCommand("setparam", new SetParamCommand(loggerFactory.CreateLogger<SetParamCommand>()));
    }
}
