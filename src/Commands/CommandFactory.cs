using AzureMCP.Commands.Cosmos;
using AzureMCP.Commands.Server;
using AzureMCP.Commands.Storage.Blob;
using AzureMCP.Commands.Subscriptions;
using AzureMCP.Commands.Tools;
using AzureMCP.Models;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureMCP.Commands;

public class CommandFactory
{

    private readonly IServiceProvider _serviceProvider;
    private readonly RootCommand _rootCommand;
    private readonly CommandGroup _rootGroup;

    internal static readonly char Separator = '-';

    /// <summary>
    /// Mapping of hyphenated command names to their <see cref="ICommand" />
    /// </summary>
    private readonly Dictionary<string, ICommand> _commandMap;

    public CommandFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _rootGroup = new CommandGroup("azmcp", "Azure MCP Server");
        _rootCommand = CreateRootCommand();
        _commandMap = CreateCommmandDictionary(_rootGroup, string.Empty);
    }

    public RootCommand RootCommand => _rootCommand;

    public CommandGroup RootGroup => _rootGroup;

    public IReadOnlyDictionary<string, ICommand> AllCommands => _commandMap;

    private void RegisterCommandGroup()
    {
        // Register top-level command groups
        RegisterCosmosCommands();
        RegisterStorageCommands();
        RegisterMonitorCommands();
        RegisterToolsCommands();
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
        databases.AddCommand("list", new DatabaseListCommand());
        cosmosContainer.AddCommand("list", new Cosmos.ContainerListCommand());
        cosmosAccount.AddCommand("list", new Cosmos.AccountListCommand());
        cosmosItem.AddCommand("query", new ItemQueryCommand());


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
        storageAccount.AddCommand("list", new Storage.Account.AccountListCommand());
        tables.AddCommand("list", new Storage.Table.TableListCommand());
        blobs.AddCommand("list", new BlobListCommand());
        blobContainer.AddCommand("list", new Storage.Blob.Container.ContainerListCommand());
        blobContainer.AddCommand("details", new Storage.Blob.Container.ContainerDetailsCommand());
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
        logs.AddCommand("query", new Monitor.Log.LogQueryCommand());
        workspaces.AddCommand("list", new Monitor.Workspace.WorkspaceListCommand());
        monitorTable.AddCommand("list", new Monitor.Table.TableListCommand());


    }

    private void RegisterToolsCommands()
    {
        // Create Tools command group
        var tools = new CommandGroup("tools", "CLI tools operations - Commands for discovering and exploring the functionality available in this CLI tool.");
        _rootGroup.AddSubGroup(tools);

        // Register Tools commands
        tools.AddCommand("list", new ToolsListCommand());

    }

    private void RegisterSubscriptionCommands()
    {
        // Create Subscription command group
        var subscription = new CommandGroup("subscription", "Azure subscription operations - Commands for listing and managing Azure subscriptions accessible to your account.");
        _rootGroup.AddSubGroup(subscription);

        // Register Subscription commands
        subscription.AddCommand("list", new SubscriptionsListCommand());
    }

    private void RegisterGroupCommands()
    {
        // Create Group command group
        var group = new CommandGroup("group", "Resource group operations - Commands for listing and managing Azure resource groups in your subscriptions.");
        _rootGroup.AddSubGroup(group);

        // Register Group commands
        group.AddCommand("list", new Group.GroupListCommand());
    }

    private void RegisterMcpServerCommands()
    {
        // Create MCP Server command group
        var mcpServer = new CommandGroup("server", "MCP server operations - Commands for managing and interacting with the MCP server.");
        _rootGroup.AddSubGroup(mcpServer);

        // Register MCP Server commands
        var startServer = new McpStartServerCommand(_serviceProvider);
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
        var rootCommand = new RootCommand("Azure AI Data Plane CLI - A comprehensive command-line interface for interacting with Azure data services. This CLI provides direct access to Azure data plane operations, allowing you to manage and query your Azure resources efficiently without switching between multiple tools.");

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

    private void ConfigureCommandHandler(Command command, ICommand implementation)
    {
        command.SetHandler(async (InvocationContext context) =>
        {
            var startTime = DateTime.UtcNow;
            var cmdContext = new CommandContext(_serviceProvider);
            var response = await implementation.ExecuteAsync(cmdContext, context.ParseResult);

            // Calculate execution time
            var endTime = DateTime.UtcNow;
            response.Duration = (long)(endTime - startTime).TotalMilliseconds;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            Console.WriteLine(JsonSerializer.Serialize(response, options));
        });
    }

    private ICommand? FindCommandInGroup(CommandGroup group, Queue<string> nameParts)
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

    public ICommand? FindCommandByName(string hyphenatedName)
    {
        return _commandMap.GetValueOrDefault(hyphenatedName);
    }

    private Dictionary<string, ICommand> CreateCommmandDictionary(CommandGroup node, string prefix)
    {
        var aggregated = new Dictionary<string, ICommand>();
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
}
