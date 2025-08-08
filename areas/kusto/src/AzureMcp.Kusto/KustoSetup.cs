// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas;
using AzureMcp.Core.Commands;
using AzureMcp.Kusto.Commands;
using AzureMcp.Kusto.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Kusto;

public class KustoSetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IKustoService, KustoService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create Kusto command group
        var kusto = new CommandGroup("kusto", "Kusto operations - Commands for managing and querying Azure Data Explorer (Kusto) clusters for big data analytics, log analysis, and telemetry processing using KQL (Kusto Query Language). Use this tool when you need to list Data Explorer clusters and databases, execute KQL queries against large datasets, retrieve table schemas, analyze time-series data, or work with high-volume analytics workloads from IoT, applications, or infrastructure logs. This tool is ideal for complex analytics queries and data exploration scenarios. Do not use this tool for transactional database operations, simple key-value lookups, Azure SQL Database queries, or Azure Monitor Log Analytics workspace operations - use the appropriate database or monitor tools instead. This tool is a hierarchical MCP command router where sub-commands are routed to MCP servers that require specific fields inside the \"parameters\" object. To invoke a command, set \"command\" and wrap its arguments in \"parameters\". Set \"learn=true\" to discover available sub-commands for different Kusto cluster and query operations. Note that this tool requires appropriate Azure Data Explorer permissions and will only access clusters and data accessible to the authenticated user.");
        rootGroup.AddSubGroup(kusto);

        // Create Kusto cluster subgroups
        var clusters = new CommandGroup("cluster", "Kusto cluster operations - Commands for listing clusters in your Azure subscription.");
        kusto.AddSubGroup(clusters);

        var databases = new CommandGroup("database", "Kusto database operations - Commands for listing databases in a cluster.");
        kusto.AddSubGroup(databases);

        var tables = new CommandGroup("table", "Kusto table operations - Commands for listing tables in a database.");
        kusto.AddSubGroup(tables);

        kusto.AddCommand("sample", new SampleCommand(loggerFactory.CreateLogger<SampleCommand>()));
        kusto.AddCommand("query", new QueryCommand(loggerFactory.CreateLogger<QueryCommand>()));

        clusters.AddCommand("list", new ClusterListCommand(loggerFactory.CreateLogger<ClusterListCommand>()));
        clusters.AddCommand("get", new ClusterGetCommand(loggerFactory.CreateLogger<ClusterGetCommand>()));

        databases.AddCommand("list", new DatabaseListCommand(loggerFactory.CreateLogger<DatabaseListCommand>()));

        tables.AddCommand("list", new TableListCommand(loggerFactory.CreateLogger<TableListCommand>()));
        tables.AddCommand("schema", new TableSchemaCommand(loggerFactory.CreateLogger<TableSchemaCommand>()));
    }
}
