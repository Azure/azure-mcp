// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Commands.Cosmos;
using AzureMcp.Commands.Server;
using AzureMcp.Commands.Storage.Blob;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.CommandLine;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureMcp.Commands;

public class CommandFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CommandFactory> _logger;
    private readonly RootCommand _rootCommand;
    private readonly CommandGroup _rootGroup;
    private readonly JsonSerializerOptions _jsonOptions;

    internal static readonly char Separator = '-';

    /// <summary>
    /// Mapping of hyphenated command names to their <see cref="IBaseCommand" />
    /// </summary>
    private readonly Dictionary<string, IBaseCommand> _commandMap;

    public CommandFactory(IServiceProvider serviceProvider, ILogger<CommandFactory> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _rootGroup = new CommandGroup("azmcp", "Azure MCP Server");
        _rootCommand = CreateRootCommand();
        _commandMap = CreateCommmandDictionary(_rootGroup, string.Empty);
        _jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    public RootCommand RootCommand => _rootCommand;

    public CommandGroup RootGroup => _rootGroup;

    public IReadOnlyDictionary<string, IBaseCommand> AllCommands => _commandMap;

    private void RegisterCommandGroup()
    {
        // Register top-level command groups
        RegisterCosmosCommands();
        RegisterStorageCommands();
        RegisterMonitorCommands();
        RegisterAppConfigCommands();
        RegisterToolsCommands();
        RegisterExtensionCommands();
        RegisterSubscriptionCommands();
        RegisterGroupCommands();
        RegisterMcpServerCommands();
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
        databases.AddCommand("list", new DatabaseListCommand(GetLogger<DatabaseListCommand>()));
        cosmosContainer.AddCommand("list", new Cosmos.ContainerListCommand(GetLogger<Cosmos.ContainerListCommand>()));
        cosmosAccount.AddCommand("list", new Cosmos.AccountListCommand(GetLogger<Cosmos.AccountListCommand>()));
        cosmosItem.AddCommand("query", new ItemQueryCommand(GetLogger<ItemQueryCommand>()));
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

        // Register Monitor commands
        logs.AddCommand("query", new Monitor.Log.LogQueryCommand(
            GetLogger<Monitor.Log.LogQueryCommand>()));
        workspaces.AddCommand("list", new Monitor.Workspace.WorkspaceListCommand(
            GetLogger<Monitor.Workspace.WorkspaceListCommand>()));
        monitorTable.AddCommand("list", new Monitor.Table.TableListCommand(
            GetLogger<Monitor.Table.TableListCommand>()));
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
        var startServer = new ServiceStartCommand(_serviceProvider);
        mcpServer.AddCommand("start", startServer);

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
                    response.Results = new List<object>();
                }

                Console.WriteLine(JsonSerializer.Serialize(response, _jsonOptions));
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

    public IBaseCommand? FindCommandByName(string hyphenatedName)
    {
        return _commandMap.GetValueOrDefault(hyphenatedName);
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