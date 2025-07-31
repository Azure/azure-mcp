// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Sql.Commands.Database;
using AzureMcp.Sql.Commands.ElasticPool;
using AzureMcp.Sql.Commands.EntraAdmin;
using AzureMcp.Sql.Commands.FirewallRule;
using AzureMcp.Sql.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Sql;

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
        database.AddCommand("list", new DatabaseListCommand(loggerFactory.CreateLogger<DatabaseListCommand>()));

        var server = new CommandGroup("server", "SQL server operations");
        sql.AddSubGroup(server);

        var elasticPool = new CommandGroup("elastic-pool", "SQL elastic pool operations");
        sql.AddSubGroup(elasticPool);
        elasticPool.AddCommand("list", new ElasticPoolListCommand(loggerFactory.CreateLogger<ElasticPoolListCommand>()));

        var entraAdmin = new CommandGroup("entra-admin", "SQL server Microsoft Entra ID administrator operations");
        server.AddSubGroup(entraAdmin);
        entraAdmin.AddCommand("list", new EntraAdminListCommand(loggerFactory.CreateLogger<EntraAdminListCommand>()));

        var firewallRule = new CommandGroup("firewall-rule", "SQL server firewall rule operations");
        server.AddSubGroup(firewallRule);

        firewallRule.AddCommand("list", new FirewallRuleListCommand(loggerFactory.CreateLogger<FirewallRuleListCommand>()));
    }
}
