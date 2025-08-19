// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Linq;
using System.Reflection;
using System.Text;
using AzureMcp.Core.Areas.Server.Commands.Discovery;
using AzureMcp.Core.Areas.Server.Commands.Runtime;
using AzureMcp.Core.Areas.Server.Commands.ToolLoading;
using AzureMcp.Core.Areas.Server.Options;
using AzureMcp.Core.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Core.Areas.Server.Commands;

// This is intentionally placed after the namespace declaration to avoid
// conflicts with AzureMcp.Core.Areas.Server.Options
using Options = Microsoft.Extensions.Options.Options;

/// <summary>
/// Extension methods for configuring Azure MCP server services.
/// </summary>
public static class AzureMcpServiceCollectionExtensions
{
    private const string DefaultServerName = "Azure MCP Server";

    /// <summary>
    /// Adds the Azure MCP server services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <param name="serviceStartOptions">The options for configuring the server.</param>
    /// <returns>The service collection with MCP server services added.</returns>
    public static IServiceCollection AddAzureMcpServer(this IServiceCollection services, ServiceStartOptions serviceStartOptions)
    {
        // Register HTTP client services
        services.AddHttpClientServices();

        // Register options for service start
        services.AddSingleton(serviceStartOptions);
        services.AddSingleton(Options.Create(serviceStartOptions));

        // Register default tool loader options from service start options
        var defaultToolLoaderOptions = new ToolLoaderOptions
        {
            Namespace = serviceStartOptions.Namespace,
            ReadOnly = serviceStartOptions.ReadOnly ?? false,
        };

        if (serviceStartOptions.Mode == ModeTypes.NamespaceProxy)
        {
            if (defaultToolLoaderOptions.Namespace == null || defaultToolLoaderOptions.Namespace.Length == 0)
            {
                defaultToolLoaderOptions = defaultToolLoaderOptions with { Namespace = ["extension"] };
            }
        }

        services.AddSingleton(defaultToolLoaderOptions);
        services.AddSingleton(Options.Create(defaultToolLoaderOptions));

        // Register tool loader strategies
        services.AddSingleton<CommandFactoryToolLoader>();
        services.AddSingleton(sp =>
        {
            return new RegistryToolLoader(
                sp.GetRequiredService<RegistryDiscoveryStrategy>(),
                sp.GetRequiredService<IOptions<ToolLoaderOptions>>(),
                sp.GetRequiredService<ILogger<RegistryToolLoader>>()
            );
        });

        services.AddSingleton<SingleProxyToolLoader>();
        services.AddSingleton<CompositeToolLoader>();
        services.AddSingleton<ServerToolLoader>();

        // Register server discovery strategies
        services.AddSingleton<CommandGroupDiscoveryStrategy>();
        services.AddSingleton<CompositeDiscoveryStrategy>();
        services.AddSingleton<RegistryDiscoveryStrategy>();

        // Register server providers
        services.AddSingleton<CommandGroupServerProvider>();
        services.AddSingleton<RegistryServerProvider>();

        // Register MCP runtimes
        services.AddSingleton<IMcpRuntime, McpRuntime>();

        // Register MCP discovery strategies based on proxy mode
        if (serviceStartOptions.Mode == ModeTypes.SingleToolProxy || serviceStartOptions.Mode == ModeTypes.NamespaceProxy)
        {
            services.AddSingleton<IMcpDiscoveryStrategy>(sp =>
            {
                var discoveryStrategies = new List<IMcpDiscoveryStrategy>
                {
                    sp.GetRequiredService<RegistryDiscoveryStrategy>(),
                    sp.GetRequiredService<CommandGroupDiscoveryStrategy>(),
                };

                var logger = sp.GetRequiredService<ILogger<CompositeDiscoveryStrategy>>();
                return new CompositeDiscoveryStrategy(discoveryStrategies, logger);
            });
        }

        // Configure tool loading based on mode
        if (serviceStartOptions.Mode == ModeTypes.SingleToolProxy)
        {
            services.AddSingleton<IToolLoader, SingleProxyToolLoader>();
        }
        else if (serviceStartOptions.Mode == ModeTypes.NamespaceProxy)
        {
            services.AddSingleton<IToolLoader>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var toolLoaders = new List<IToolLoader>
                {
                    sp.GetRequiredService<ServerToolLoader>(),
                };

                // Append extension commands when no other namespaces are specified.
                if (defaultToolLoaderOptions.Namespace?.SequenceEqual(["extension"]) == true)
                {
                    toolLoaders.Add(sp.GetRequiredService<CommandFactoryToolLoader>());
                }

                return new CompositeToolLoader(toolLoaders, loggerFactory.CreateLogger<CompositeToolLoader>());
            });
        }
        else if (serviceStartOptions.Mode == ModeTypes.All)
        {
            services.AddSingleton<IMcpDiscoveryStrategy, RegistryDiscoveryStrategy>();
            services.AddSingleton<IToolLoader>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var toolLoaders = new List<IToolLoader>
                {
                    sp.GetRequiredService<RegistryToolLoader>(),
                    sp.GetRequiredService<CommandFactoryToolLoader>(),
                };

                return new CompositeToolLoader(toolLoaders, loggerFactory.CreateLogger<CompositeToolLoader>());
            });
        }

        var mcpServerOptions = services
            .AddOptions<McpServerOptions>()
            .Configure<IMcpRuntime>((mcpServerOptions, mcpRuntime) =>
            {
                var mcpServerOptionsBuilder = services.AddOptions<McpServerOptions>();
                var entryAssembly = Assembly.GetEntryAssembly();
                var assemblyName = entryAssembly?.GetName();
                var serverName = entryAssembly?.GetCustomAttribute<AssemblyTitleAttribute>()?.Title ?? DefaultServerName;

                mcpServerOptions.ProtocolVersion = "2024-11-05";
                mcpServerOptions.ServerInfo = new Implementation
                {
                    Name = serverName,
                    Version = assemblyName?.Version?.ToString() ?? "1.0.0-beta"
                };

                mcpServerOptions.Capabilities = new ServerCapabilities
                {
                    Tools = new ToolsCapability()
                    {
                        CallToolHandler = mcpRuntime.CallToolHandler,
                        ListToolsHandler = mcpRuntime.ListToolsHandler,
                    }
                };

                // Add instructions for the server
                mcpServerOptions.ServerInstructions = GetServerInstructions();
            });

        var mcpServerBuilder = services.AddMcpServer();
        mcpServerBuilder.WithStdioServerTransport();

        return services;
    }

    /// <summary>
    /// Generates comprehensive instructions for using the Azure MCP Server effectively.
    /// Includes Azure best practices from embedded resource files.
    /// </summary>
    /// <returns>Instructions text for LLM interactions with the Azure MCP Server.</returns>
    private static string GetServerInstructions()
    {
        var instructions = new StringBuilder();
        
        // Base instructions for Azure MCP Server
        instructions.AppendLine("You are interacting with the Azure MCP Server, which provides secure access to Azure services and resources. Here's how to use it effectively:");
        instructions.AppendLine();
        
        instructions.AppendLine("## Available Capabilities");
        instructions.AppendLine("- **Azure Resource Management**: List, query, and manage Azure resources across 28+ Azure services");
        instructions.AppendLine("- **Read and Write Operations**: Full CRUD operations on Azure resources (when not in read-only mode)");
        instructions.AppendLine("- **Multi-Service Support**: Storage, Key Vault, Cosmos DB, Monitor, SQL, AKS, and many more");
        instructions.AppendLine("- **Flexible Modes**: Can operate as individual tools, grouped by service, or as a single routing tool");
        instructions.AppendLine();
        
        instructions.AppendLine("## Key Usage Guidelines");
        instructions.AppendLine("1. **Authentication**: Ensure you're authenticated to Azure before making requests");
        instructions.AppendLine("2. **Resource Identification**: Use specific resource names, resource groups, and subscription IDs when possible");
        instructions.AppendLine("3. **Error Handling**: If a command fails, check authentication, permissions, and resource existence");
        instructions.AppendLine("4. **Read-Only Mode**: Some servers may be configured in read-only mode, preventing write operations");
        instructions.AppendLine();
        
        instructions.AppendLine("## Common Patterns");
        instructions.AppendLine("- Start with listing operations to discover available resources (e.g., \"list storage accounts\")");
        instructions.AppendLine("- Use resource group and subscription filters to narrow down results");
        instructions.AppendLine("- For queries, provide specific parameters like table names, database names, etc.");
        instructions.AppendLine("- When creating resources, specify required parameters like location, SKU, and configuration settings");
        instructions.AppendLine();
        
        instructions.AppendLine("## Security Notes");
        instructions.AppendLine("- The server respects Azure RBAC permissions - you can only access resources you have permission to");
        instructions.AppendLine("- Sensitive data like connection strings and keys are handled securely");
        instructions.AppendLine("- Always use managed identities and secure authentication methods when possible");
        instructions.AppendLine();
        
        // Add Azure best practices from embedded resources
        instructions.AppendLine("## Azure Development Best Practices");
        instructions.AppendLine("The following guidelines ensure you follow Azure best practices when developing solutions:");
        instructions.AppendLine();
        
        try
        {
            var bestPracticesContent = LoadAzureBestPractices();
            if (!string.IsNullOrEmpty(bestPracticesContent))
            {
                instructions.AppendLine(bestPracticesContent);
            }
        }
        catch (Exception)
        {
            // Fallback if resources are not available
            instructions.AppendLine("**Note**: Azure best practices resources are not available in this configuration.");
            instructions.AppendLine("An error occurred while loading Azure best practices.");
        }
        
        instructions.AppendLine();
        instructions.AppendLine("For detailed command documentation, refer to the Azure MCP Server documentation.");
        
        return instructions.ToString();
    }

    /// <summary>
    /// Loads Azure best practices from embedded resource files.
    /// </summary>
    /// <returns>Combined content from all Azure best practices resource files.</returns>
    private static string LoadAzureBestPractices()
    {
        var coreAssembly = typeof(AzureMcpServiceCollectionExtensions).Assembly;
        var bestPracticesContent = new StringBuilder();

        // List of known best practices resource files
        var resourceFiles = new[]
        {
            "azure-general-codegen-best-practices.txt",
            "azure-general-deployment-best-practices.txt", 
            "azure-functions-codegen-best-practices.txt",
            "azure-functions-deployment-best-practices.txt",
            "azure-swa-best-practices.txt"
        };

        foreach (var resourceFile in resourceFiles)
        {
            try
            {
                string resourceName = EmbeddedResourceHelper.FindEmbeddedResource(coreAssembly, resourceFile);
                string content = EmbeddedResourceHelper.ReadEmbeddedResource(coreAssembly, resourceName);
                
                // Add section header based on filename
                var sectionTitle = GetSectionTitle(resourceFile);
                bestPracticesContent.AppendLine($"### {sectionTitle}");
                bestPracticesContent.AppendLine();
                bestPracticesContent.AppendLine(content);
                bestPracticesContent.AppendLine();
            }
            catch (Exception)
            {
                // Log the error but continue processing other files
                bestPracticesContent.AppendLine($"### Error loading {resourceFile}");
                bestPracticesContent.AppendLine("An error occurred while loading this section."); 
                bestPracticesContent.AppendLine();
            }
        }

        return bestPracticesContent.ToString();
    }

    /// <summary>
    /// Generates a human-readable section title from a resource filename.
    /// </summary>
    /// <param name="filename">The resource filename.</param>
    /// <returns>A formatted section title.</returns>
    private static string GetSectionTitle(string filename)
    {
        return filename switch
        {
            "azure-general-codegen-best-practices.txt" => "General Azure Code Generation Best Practices",
            "azure-general-deployment-best-practices.txt" => "General Azure Deployment Best Practices",
            "azure-functions-codegen-best-practices.txt" => "Azure Functions Code Generation Best Practices",
            "azure-functions-deployment-best-practices.txt" => "Azure Functions Deployment Best Practices", 
            "azure-swa-best-practices.txt" => "Azure Static Web Apps Best Practices",
            _ => filename.Replace("-", " ").Replace(".txt", "").Replace("azure ", "Azure ")
        };
    }
}
