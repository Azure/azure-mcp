// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Sql.Commands.Database;
using AzureMcp.Areas.Sql.Commands.EntraAdmin;
using AzureMcp.Areas.Sql.Commands.FirewallRule;
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

        var server = new CommandGroup("server", "SQL server operations");
        sql.AddSubGroup(server);

        var entraAdmin = new CommandGroup("entra-admin", "SQL server Microsoft Entra ID administrator operations");
        server.AddSubGroup(entraAdmin);

        entraAdmin.AddCommand("list", new EntraAdminListCommand(loggerFactory.CreateLogger<EntraAdminListCommand>()));

        var firewallRule = new CommandGroup("firewall-rule", "SQL server firewall rule operations");
        server.AddSubGroup(firewallRule);

        firewallRule.AddCommand("list", new FirewallRuleListCommand(loggerFactory.CreateLogger<FirewallRuleListCommand>()));
    }
}
