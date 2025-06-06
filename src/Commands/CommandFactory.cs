// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;
using AzureMcp.Commands.Server;
using AzureMcp.Commands.Storage.Blob;
using AzureMcp.Commands.Subscription;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands;

public class CommandFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CommandFactory> _logger;
    private readonly RootCommand _rootCommand;
    private readonly CommandGroup _rootGroup;
    private readonly ModelsJsonContext _srcGenWithOptions;

    internal static readonly char Separator = '-';

    /// <summary>
    /// Mapping of tokenized command names to their <see cref="IBaseCommand" />
    /// </summary>
    private readonly Dictionary<string, IBaseCommand> _commandMap;

    // Add this new class inside CommandFactory
    private class StringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetString() ?? string.Empty;
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            var cleanValue = value?.Replace("\r\n", " ").Replace("\n", " ").Replace("\r", " ");
            writer.WriteStringValue(cleanValue);
        }
    }

    public CommandFactory(IServiceProvider serviceProvider, ILogger<CommandFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _rootGroup = new CommandGroup("azmcp", "Azure MCP Server");
        _rootCommand = CreateRootCommand();
        _commandMap = CreateCommmandDictionary(_rootGroup, string.Empty);
        _srcGenWithOptions = new ModelsJsonContext(new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }

    public RootCommand RootCommand => _rootCommand;

    public CommandGroup RootGroup => _rootGroup;

    public IReadOnlyDictionary<string, IBaseCommand> AllCommands => _commandMap;

    public IReadOnlyDictionary<string, IBaseCommand> GroupCommands(string groupName)
    {
        foreach (CommandGroup group in _rootGroup.SubGroup)
        {
            if (string.Equals(group.Name, groupName, StringComparison.OrdinalIgnoreCase))
            {
                return CreateCommmandDictionary(group, string.Empty);
            }
        }

        throw new KeyNotFoundException($"Group '{groupName}' not found in command groups.");
    }

    private void RegisterCommandGroup()
    {
        // Register top-level command groups
        RegisterBestPracticesCommand();
        RegisterCosmosCommands();
        RegisterKeyVaultCommands();
        RegisterKustoCommands();
        RegisterStorageCommands();
        RegisterMonitorCommands();
        RegisterAppConfigCommands();
        RegisterSearchCommands();
        RegisterPostgresCommands();
        RegisterToolsCommands();
        RegisterExtensionCommands();
        RegisterSubscriptionCommands();
        RegisterGroupCommands();
        RegisterMcpServerCommands();
        RegisterServiceBusCommands();
        RegisterRedisCommands();
    }

    private void RegisterBestPracticesCommand()
    {
        // Register Azure Best Practices command at the root level
        var bestPractices = new CommandGroup(
            "bestpractices",
            "Returns secure, production-grade Azure SDK best practices. Call this before generating Azure SDK code."
        );
        _rootGroup.AddSubGroup(bestPractices);
        bestPractices.AddCommand(
            "get",
            new BestPractices.AzureBestPracticesGetCommand(GetLogger<BestPractices.AzureBestPracticesGetCommand>())
        );
    }

    private void RegisterCosmosCommands()
    {
        // Create Cosmos command group
        var cosmos = new CommandGroup("cosmos", "Cosmos DB operations - Commands for managing and querying Azure Cosmos DB resources. Includes operations for databases, containers, and document queries.");
        _rootGroup.AddSubGroup(cosmos);

        // Create Cosmos subgroups
        var databases = new CommandGroup("database", "Cosmos DB database operations - Commands for listing, creating, and managing database within your Cosmos DB accounts.");
        cosmos.AddSubGroup(databases);

        var cosmosContainer = new CommandGroup("container", "Cosmos DB container operations - Commands for listing, creating, and managing container (collection) within your Cosmos DB databases.");
        databases.AddSubGroup(cosmosContainer);

        var cosmosAccount = new CommandGroup("account", "Cosmos DB account operations - Commands for listing and managing Cosmos DB account in your Azure subscription.");
        cosmos.AddSubGroup(cosmosAccount);

        // Create items subgroup for Cosmos
        var cosmosItem = new CommandGroup("item", "Cosmos DB item operations - Commands for querying, creating, updating, and deleting document within your Cosmos DB containers.");
        cosmosContainer.AddSubGroup(cosmosItem);

        // Register Cosmos commands
        databases.AddCommand("list", new Cosmos.DatabaseListCommand(GetLogger<Cosmos.DatabaseListCommand>()));
        cosmosContainer.AddCommand("list", new Cosmos.ContainerListCommand(GetLogger<Cosmos.ContainerListCommand>()));
        cosmosAccount.AddCommand("list", new Cosmos.AccountListCommand(GetLogger<Cosmos.AccountListCommand>()));
        cosmosItem.AddCommand("query", new Cosmos.ItemQueryCommand(GetLogger<Cosmos.ItemQueryCommand>()));
    }

    private void RegisterKustoCommands()
    {
        // Create Kusto command group
        var kusto = new CommandGroup("kusto", "Kusto operations - Commands for managing and querying Azure Kusto clusters.");
        _rootGroup.AddSubGroup(kusto);

        // Create Kusto cluster subgroups
        var clusters = new CommandGroup("cluster", "Kusto cluster operations - Commands for listing clusters in your Azure subscription.");
        kusto.AddSubGroup(clusters);

        var databases = new CommandGroup("database", "Kusto database operations - Commands for listing databases in a cluster.");
        kusto.AddSubGroup(databases);

        var tables = new CommandGroup("table", "Kusto table operations - Commands for listing tables in a database.");
        kusto.AddSubGroup(tables);

        kusto.AddCommand("sample", new Kusto.SampleCommand(GetLogger<Kusto.SampleCommand>()));
        kusto.AddCommand("query", new Kusto.QueryCommand(GetLogger<Kusto.QueryCommand>()));

        clusters.AddCommand("list", new Kusto.ClusterListCommand(GetLogger<Kusto.ClusterListCommand>()));
        clusters.AddCommand("get", new Kusto.ClusterGetCommand(GetLogger<Kusto.ClusterGetCommand>()));

        databases.AddCommand("list", new Kusto.DatabaseListCommand(GetLogger<Kusto.DatabaseListCommand>()));

        tables.AddCommand("list", new Kusto.TableListCommand(GetLogger<Kusto.TableListCommand>()));
        tables.AddCommand("schema", new Kusto.TableSchemaCommand(GetLogger<Kusto.TableSchemaCommand>()));
    }

    private void RegisterPostgresCommands()
    {
        var pg = new CommandGroup("postgres", "PostgreSQL operations - Commands for listing and managing Azure Database for PostgreSQL - Flexible server.");
        _rootGroup.AddSubGroup(pg);

        var database = new CommandGroup("database", "PostgreSQL database operations");
        pg.AddSubGroup(database);
        database.AddCommand("list", new Postgres.Database.DatabaseListCommand(GetLogger<Postgres.Database.DatabaseListCommand>()));
        database.AddCommand("query", new Postgres.Database.DatabaseQueryCommand(GetLogger<Postgres.Database.DatabaseQueryCommand>()));

        var table = new CommandGroup("table", "PostgreSQL table operations");
        pg.AddSubGroup(table);
        table.AddCommand("list", new Postgres.Table.TableListCommand(GetLogger<Postgres.Table.TableListCommand>()));
        table.AddCommand("schema", new Postgres.Table.GetSchemaCommand(GetLogger<Postgres.Table.GetSchemaCommand>()));

        var server = new CommandGroup("server", "PostgreSQL server operations");
        pg.AddSubGroup(server);
        server.AddCommand("list", new Postgres.Server.ServerListCommand(GetLogger<Postgres.Server.ServerListCommand>()));
        server.AddCommand("config", new Postgres.Server.GetConfigCommand(GetLogger<Postgres.Server.GetConfigCommand>()));
        server.AddCommand("param", new Postgres.Server.GetParamCommand(GetLogger<Postgres.Server.GetParamCommand>()));
    }

    private void RegisterStorageCommands()
    {
        // Create Storage command group
        var storage = new CommandGroup("storage", "Storage operations - Commands for managing and accessing Azure Storage resources. Includes operations for containers, blobs, and tables.");
        _rootGroup.AddSubGroup(storage);

        // Create Storage subgroups
        var storageAccount = new CommandGroup("account", "Storage account operations - Commands for listing and managing Storage account in your Azure subscription.");
        storage.AddSubGroup(storageAccount);

        var tables = new CommandGroup("table", "Storage table operations - Commands for working with Azure Table Storage, including listing and querying table.");
        storage.AddSubGroup(tables);

        var blobs = new CommandGroup("blob", "Storage blob operations - Commands for uploading, downloading, and managing blob in your Azure Storage accounts.");
        storage.AddSubGroup(blobs);

        // Create a containers subgroup under blobs
        var blobContainer = new CommandGroup("container", "Storage blob container operations - Commands for managing blob container in your Azure Storage accounts.");
        blobs.AddSubGroup(blobContainer);

        // Register Storage commands
        storageAccount.AddCommand("list", new Storage.Account.AccountListCommand(
            GetLogger<Storage.Account.AccountListCommand>()));
        tables.AddCommand("list", new Storage.Table.TableListCommand(
            GetLogger<Storage.Table.TableListCommand>()));

        blobs.AddCommand("list", new BlobListCommand(GetLogger<BlobListCommand>()));

        blobContainer.AddCommand("list", new Storage.Blob.Container.ContainerListCommand(
            GetLogger<Storage.Blob.Container.ContainerListCommand>()));
        blobContainer.AddCommand("details", new Storage.Blob.Container.ContainerDetailsCommand(
            GetLogger<Storage.Blob.Container.ContainerDetailsCommand>()));
    }

    private void RegisterMonitorCommands()
    {
        // Create Monitor command group
        var monitor = new CommandGroup("monitor", "Azure Monitor operations - Commands for querying and analyzing Azure Monitor logs and metrics.");
        _rootGroup.AddSubGroup(monitor);

        // Create Monitor subgroups
        var logs = new CommandGroup("log", "Azure Monitor logs operations - Commands for querying Log Analytics workspaces using KQL.");
        monitor.AddSubGroup(logs);

        var workspaces = new CommandGroup("workspace", "Log Analytics workspace operations - Commands for managing Log Analytics workspaces.");
        monitor.AddSubGroup(workspaces);

        var monitorTable = new CommandGroup("table", "Log Analytics workspace table operations - Commands for listing tables in Log Analytics workspaces.");
        monitor.AddSubGroup(monitorTable);

        var monitorTableType = new CommandGroup("type", "Log Analytics workspace table type operations - Commands for listing table types in Log Analytics workspaces.");
        monitorTable.AddSubGroup(monitorTableType);

        // Register Monitor commands
        logs.AddCommand("query", new Monitor.Log.LogQueryCommand(GetLogger<Monitor.Log.LogQueryCommand>()));
        workspaces.AddCommand("list", new Monitor.Workspace.WorkspaceListCommand(GetLogger<Monitor.Workspace.WorkspaceListCommand>()));
        monitorTable.AddCommand("list", new Monitor.Table.TableListCommand(GetLogger<Monitor.Table.TableListCommand>()));

        monitorTableType.AddCommand("list", new Monitor.TableType.TableTypeListCommand(GetLogger<Monitor.TableType.TableTypeListCommand>()));

        var health = new CommandGroup("healthmodels", "Azure Monitor Health Models operations - Commands for working with Azure Monitor Health Models.");
        monitor.AddSubGroup(health);

        var entity = new CommandGroup("entity", "Entity operations - Commands for working with entities in Azure Monitor Health Models.");
        health.AddSubGroup(entity);

        entity.AddCommand("gethealth", new Monitor.HealthModels.Entity.EntityGetHealthCommand(GetLogger<Monitor.HealthModels.Entity.EntityGetHealthCommand>()));
    }

    private void RegisterAppConfigCommands()
    {
        var appConfig = new CommandGroup("appconfig", "App Configuration operations - Commands for managing App Configuration stores");
        _rootGroup.AddSubGroup(appConfig);

        var accounts = new CommandGroup("account", "App Configuration store operations");
        appConfig.AddSubGroup(accounts);

        var keyValue = new CommandGroup("kv", "App Configuration key-value setting operations - Commands for managing complete configuration settings including values, labels, and metadata");
        appConfig.AddSubGroup(keyValue);

        accounts.AddCommand("list", new AppConfig.Account.AccountListCommand(GetLogger<AppConfig.Account.AccountListCommand>()));
        keyValue.AddCommand("list", new AppConfig.KeyValue.KeyValueListCommand(GetLogger<AppConfig.KeyValue.KeyValueListCommand>()));
        keyValue.AddCommand("lock", new AppConfig.KeyValue.KeyValueLockCommand(GetLogger<AppConfig.KeyValue.KeyValueLockCommand>()));
        keyValue.AddCommand("unlock", new AppConfig.KeyValue.KeyValueUnlockCommand(GetLogger<AppConfig.KeyValue.KeyValueUnlockCommand>()));
        keyValue.AddCommand("set", new AppConfig.KeyValue.KeyValueSetCommand(GetLogger<AppConfig.KeyValue.KeyValueSetCommand>()));
        keyValue.AddCommand("show", new AppConfig.KeyValue.KeyValueShowCommand(GetLogger<AppConfig.KeyValue.KeyValueShowCommand>()));
        keyValue.AddCommand("delete", new AppConfig.KeyValue.KeyValueDeleteCommand(GetLogger<AppConfig.KeyValue.KeyValueDeleteCommand>()));
    }

    private void RegisterSearchCommands()
    {
        var search = new CommandGroup("search", "Search operations - Commands for managing and listing Azure AI Search services.");
        _rootGroup.AddSubGroup(search);

        var service = new CommandGroup("service", "Azure AI Search service operations - Commands for listing and managing search services in your Azure subscription.");
        search.AddSubGroup(service);

        service.AddCommand("list", new Search.Service.ServiceListCommand(GetLogger<Search.Service.ServiceListCommand>()));

        var index = new CommandGroup("index", "Azure AI Search index operations - Commands for listing and managing search indexes in a specific search service.");
        search.AddSubGroup(index);

        index.AddCommand("list", new Search.Index.IndexListCommand(GetLogger<Search.Index.IndexListCommand>()));
        index.AddCommand("describe", new Search.Index.IndexDescribeCommand(GetLogger<Search.Index.IndexDescribeCommand>()));
        index.AddCommand("query", new Search.Index.IndexQueryCommand(GetLogger<Search.Index.IndexQueryCommand>()));
    }

    private void RegisterKeyVaultCommands()
    {
        var keyVault = new CommandGroup("keyvault", "Key Vault operations - Commands for managing and accessing Azure Key Vault resources.");
        _rootGroup.AddSubGroup(keyVault);

        var keys = new CommandGroup("key", "Key Vault key operations - Commands for managing and accessing keys in Azure Key Vault.");
        keyVault.AddSubGroup(keys);

        var secret = new CommandGroup("secret", "Key Vault secret operations - Commands for managing and accessing secrets in Azure Key Vault.");
        keyVault.AddSubGroup(secret);

        keys.AddCommand("list", new KeyVault.Key.KeyListCommand(GetLogger<KeyVault.Key.KeyListCommand>()));
        keys.AddCommand("get", new KeyVault.Key.KeyGetCommand(GetLogger<KeyVault.Key.KeyGetCommand>()));
        keys.AddCommand("create", new KeyVault.Key.KeyCreateCommand(GetLogger<KeyVault.Key.KeyCreateCommand>()));

        secret.AddCommand("get", new KeyVault.Secret.SecretGetCommand(GetLogger<KeyVault.Secret.SecretGetCommand>(),
            _serviceProvider.GetRequiredService<IKeyVaultService>()));
    }

    private void RegisterToolsCommands()
    {
        // Create Tools command group
        var tools = new CommandGroup("tools", "CLI tools operations - Commands for discovering and exploring the functionality available in this CLI tool.");
        _rootGroup.AddSubGroup(tools);

        tools.AddCommand("list", new Tools.ToolsListCommand(GetLogger<Tools.ToolsListCommand>()));
    }

    private void RegisterExtensionCommands()
    {
        var extension = new CommandGroup("extension", "Extension commands for additional functionality");
        _rootGroup.AddSubGroup(extension);

        extension.AddCommand("az", new Extension.AzCommand(GetLogger<Extension.AzCommand>()));
        extension.AddCommand("azd", new Extension.AzdCommand(GetLogger<Extension.AzdCommand>()));
    }

    private void RegisterSubscriptionCommands()
    {
        // Create Subscription command group
        var subscription = new CommandGroup("subscription", "Azure subscription operations - Commands for listing and managing Azure subscriptions accessible to your account.");
        _rootGroup.AddSubGroup(subscription);

        // Register Subscription commands
        subscription.AddCommand("list", new SubscriptionListCommand(GetLogger<SubscriptionListCommand>()));
    }

    private void RegisterGroupCommands()
    {
        // Create Group command group
        var group = new CommandGroup("group", "Resource group operations - Commands for listing and managing Azure resource groups in your subscriptions.");
        _rootGroup.AddSubGroup(group);

        // Register Group commands
        group.AddCommand("list", new Group.GroupListCommand(GetLogger<Group.GroupListCommand>()));
    }

    private void RegisterMcpServerCommands()
    {
        // Create MCP Server command group
        var mcpServer = new CommandGroup("server", "MCP Server operations - Commands for managing and interacting with the MCP Server.");
        _rootGroup.AddSubGroup(mcpServer);

        // Register MCP Server commands
        var startServer = new ServiceStartCommand();
        mcpServer.AddCommand("start", startServer);

    }

    private void RegisterServiceBusCommands()
    {
        var serviceBus = new CommandGroup("servicebus", "Service Bus operations - Commands for managing Azure Service Bus resources");
        _rootGroup.AddSubGroup(serviceBus);

        var queue = new CommandGroup("queue", "Queue operations - Commands for using Azure Service Bus queues.");
        // queue.AddCommand("peek", new ServiceBus.Queue.QueuePeekCommand());
        queue.AddCommand("details", new ServiceBus.Queue.QueueDetailsCommand());

        var topic = new CommandGroup("topic", "Topic operations - Commands for using Azure Service Bus topics and subscriptions.");
        topic.AddCommand("details", new ServiceBus.Topic.TopicDetailsCommand());

        var subscription = new CommandGroup("subscription", "Subscription operations - Commands for using subscriptions within a Service Bus topic.");
        // subscription.AddCommand("peek", new ServiceBus.Topic.SubscriptionPeekCommand());
        subscription.AddCommand("details", new ServiceBus.Topic.SubscriptionDetailsCommand());

        serviceBus.AddSubGroup(queue);
        serviceBus.AddSubGroup(topic);

        topic.AddSubGroup(subscription);
    }

    private void RegisterRedisCommands()
    {
        var redis = new CommandGroup("redis", "Redis Cache operations - Commands for managing and accessing Azure Redis Cache resources.");
        _rootGroup.AddSubGroup(redis);

        // Azure Cache for Redis
        var cache = new CommandGroup("cache", "Redis Cache resource operations - Commands for listing and managing Redis Cache resources in your Azure subscription.");
        redis.AddSubGroup(cache);

        cache.AddCommand("list", new Redis.CacheForRedis.CacheListCommand(GetLogger<Redis.CacheForRedis.CacheListCommand>()));

        var accessPolicy = new CommandGroup("accesspolicy", "Redis Cluster database operations - Commands for listing and managing Redis Cluster databases in your Azure subscription.");
        cache.AddSubGroup(accessPolicy);

        accessPolicy.AddCommand("list", new Redis.CacheForRedis.AccessPolicyListCommand(GetLogger<Redis.CacheForRedis.AccessPolicyListCommand>()));

        // Azure Managed Redis
        var cluster = new CommandGroup("cluster", "Redis Cluster resource operations - Commands for listing and managing Redis Cluster resources in your Azure subscription.");
        redis.AddSubGroup(cluster);

        cluster.AddCommand("list", new Redis.ManagedRedis.ClusterListCommand(GetLogger<Redis.ManagedRedis.ClusterListCommand>()));

        var database = new CommandGroup("database", "Redis Cluster database operations - Commands for listing and managing Redis Cluster Databases in your Azure subscription.");
        cluster.AddSubGroup(database);

        database.AddCommand("list", new Redis.ManagedRedis.DatabaseListCommand(GetLogger<Redis.ManagedRedis.DatabaseListCommand>()));
    }

    private void ConfigureCommands(CommandGroup group)
    {
        // Configure direct commands in this group
        foreach (var command in group.Commands.Values)
        {
            var cmd = command.GetCommand();

            if (cmd.Handler == null)
            {
                ConfigureCommandHandler(cmd, command);
            }

            group.Command.Add(cmd);
        }

        // Recursively configure subgroup commands
        foreach (var subGroup in group.SubGroup)
        {
            ConfigureCommands(subGroup);
        }
    }

    private RootCommand CreateRootCommand()
    {
        var rootCommand = new RootCommand("Azure MCP Server - A Model Context Protocol (MCP) server that enables AI agents to interact with Azure services through standardized communication patterns.");

        RegisterCommandGroup();

        // Copy the root group's subcommands to the RootCommand
        foreach (var subGroup in _rootGroup.SubGroup)
        {
            rootCommand.Add(subGroup.Command);
        }

        // Configure all commands in the hierarchy
        ConfigureCommands(_rootGroup);

        return rootCommand;
    }

    private void ConfigureCommandHandler(Command command, IBaseCommand implementation)
    {
        command.SetHandler(async context =>
        {
            _logger.LogTrace("Executing '{Command}'.", command.Name);

            var cmdContext = new CommandContext(_serviceProvider);
            var startTime = DateTime.UtcNow;
            try
            {
                var response = await implementation.ExecuteAsync(cmdContext, context.ParseResult);

                // Calculate execution time
                var endTime = DateTime.UtcNow;
                response.Duration = (long)(endTime - startTime).TotalMilliseconds;

                if (response.Status == 200 && response.Results == null)
                {
                    response.Results = ResponseResult.Create(new List<string>(), JsonSourceGenerationContext.Default.ListString);
                }

                Console.WriteLine(JsonSerializer.Serialize(response, _srcGenWithOptions.CommandResponse));
            }
            catch (Exception ex)
            {
                _logger.LogError("An exception occurred while executing '{Command}'. Exception: {Exception}",
                    command.Name, ex);
            }
            finally
            {
                _logger.LogTrace("Finished running '{Command}'.", command.Name);
            }
        });
    }

    private ILogger<T> GetLogger<T>()
    {
        return _serviceProvider.GetRequiredService<ILogger<T>>();
    }

    private static IBaseCommand? FindCommandInGroup(CommandGroup group, Queue<string> nameParts)
    {
        // If we've processed all parts and this group has a matching command, return it
        if (nameParts.Count == 1)
        {
            var commandName = nameParts.Dequeue();
            return group.Commands.GetValueOrDefault(commandName);
        }

        // Find the next subgroup
        var groupName = nameParts.Dequeue();
        var nextGroup = group.SubGroup.FirstOrDefault(g => g.Name == groupName);

        return nextGroup != null ? FindCommandInGroup(nextGroup, nameParts) : null;
    }

    public IBaseCommand? FindCommandByName(string tokenizedName)
    {
        return _commandMap.GetValueOrDefault(tokenizedName);
    }

    private static Dictionary<string, IBaseCommand> CreateCommmandDictionary(CommandGroup node, string prefix)
    {
        var aggregated = new Dictionary<string, IBaseCommand>();
        var updatedPrefix = GetPrefix(prefix, node.Name);

        if (node.Commands != null)
        {
            foreach (var kvp in node.Commands)
            {
                var key = GetPrefix(updatedPrefix, kvp.Key);

                aggregated.Add(key, kvp.Value);
            }
        }

        if (node.SubGroup == null)
        {
            return aggregated;
        }

        foreach (var command in node.SubGroup)
        {
            var childPrefix = GetPrefix(updatedPrefix, command.Name);
            var subcommandsDictionary = CreateCommmandDictionary(command, updatedPrefix);

            foreach (var item in subcommandsDictionary)
            {
                aggregated.Add(item.Key, item.Value);
            }
        }

        return aggregated;
    }

    private static string GetPrefix(string currentPrefix, string additional) => string.IsNullOrEmpty(currentPrefix)
        ? additional
        : currentPrefix + Separator + additional;

    public static IEnumerable<KeyValuePair<string, IBaseCommand>> GetVisibleCommands(IEnumerable<KeyValuePair<string, IBaseCommand>> commands)
    {
        return commands
            .Where(kvp => kvp.Value.GetType().GetCustomAttribute<HiddenCommandAttribute>() == null)
            .OrderBy(kvp => kvp.Key);
    }
}
