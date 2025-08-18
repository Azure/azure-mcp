// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;

namespace AzureMcp.Core.Areas.Server.Commands.ResourceLoading;

/// <summary>
/// Example resource loader that provides Azure documentation and configuration resources.
/// This demonstrates how to implement the IResourceLoader interface.
/// </summary>
public sealed class ExampleAzureResourceLoader(ILogger<ExampleAzureResourceLoader> logger) : BaseResourceLoader(logger)
{
    private static readonly List<Resource> ExampleResources = new()
    {
        new Resource
        {
            Uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-codegen-best-practices.txt",
            Name = "Azure General Code Generation Best Practices",
            Title = "📚 Azure General Code Generation Best Practices",
            Description = "Comprehensive guide for code generation and operations when working with azure. It should be called for any code generation or operation involving Azure, Azure kubernetes Service (AKS), Azure Container Apps (ACA), Bicep, Terraform, Azure Cache, Redis, Cosmosdb, Entra, Azure Active Directory, Azure App Services, or any other azure technology or programming language."
        },
        new Resource
        {
            Uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-deployment-best-practices.txt",
            Name = "Azure General Deployment Best Practices",
            Title = "🚀 Azure General Deployment Best Practices",
            Description = "Comprehensive guide for deployment operations when working with Azure. Covers Infrastructure as Code (IaC), Bicep templates, azd commands, and secure deployment practices for any Azure service or technology."
        },
        new Resource
        {
            Uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-codegen-best-practices.txt",
            Name = "Azure Functions Code Generation Best Practices",
            Title = "⚡ Azure Functions Code Generation Best Practices",
            Description = "Best practices for generating Azure Functions code including programming models, runtime configurations, bindings, triggers, and dependency management for .NET, Python, JavaScript, PowerShell, and Java."
        },
        new Resource
        {
            Uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-deployment-best-practices.txt",
            Name = "Azure Functions Deployment Best Practices",
            Title = "📦 Azure Functions Deployment Best Practices",
            Description = "Best practices for deploying Azure Functions including hosting plans, security configurations, networking, scaling strategies, and infrastructure templates using Azure Verified Modules (AVM)."
        },
        new Resource
        {
            Uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-swa-best-practices.txt",
            Name = "Azure Static Web Apps Best Practices",
            Title = "🌐 Azure Static Web Apps Best Practices",
            Description = "Best practices for creating, deploying, and managing Azure Static Web Apps including CLI usage, configuration management, and deployment strategies."
        }
    };

    /// <summary>
    /// Lists all available example resources.
    /// </summary>
    public override ValueTask<ListResourcesResult> ListResourcesHandler(RequestContext<ListResourcesRequestParams> request, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Listing {Count} example Azure resources", ExampleResources.Count);

        var result = new ListResourcesResult
        {
            Resources = new List<Resource>(ExampleResources)
        };

        return ValueTask.FromResult(result);
    }

    /// <summary>
    /// Reads the content of a specific resource.
    /// </summary>
    public override ValueTask<ReadResourceResult> ReadResourceHandler(RequestContext<ReadResourceRequestParams> request, CancellationToken cancellationToken)
    {
        if (request?.Params?.Uri == null)
        {
            throw new ArgumentException("Resource URI is required", nameof(request));
        }

        var uri = request.Params.Uri;
        _logger.LogDebug("Reading resource: {Uri}", uri);

        var resource = ExampleResources.FirstOrDefault(r => r.Uri == uri);
        if (resource == null)
        {
            throw new InvalidOperationException($"Resource not found: {uri}");
        }

        var content = GenerateResourceContent(uri);
        
        // Create resource contents based on the MCP protocol
        var resourceContents = new TextResourceContents
        {
            Uri = resource.Uri,
            MimeType = resource.MimeType,
            Text = content
        };

        var result = new ReadResourceResult
        {
            Contents = [resourceContents]
        };

        _logger.LogDebug("Successfully read resource {Uri} with {Length} characters", uri, content.Length);
        return ValueTask.FromResult(result);
    }

    /// <summary>
    /// Generates content for a specific resource URI.
    /// </summary>
    private string GenerateResourceContent(string uri)
    {
        return uri switch
        {
            "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-codegen-best-practices.txt" => LoadFileContent("areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-codegen-best-practices.txt"),
            "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-deployment-best-practices.txt" => LoadFileContent("areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-deployment-best-practices.txt"),
            "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-codegen-best-practices.txt" => LoadFileContent("areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-codegen-best-practices.txt"),
            "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-deployment-best-practices.txt" => LoadFileContent("areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-deployment-best-practices.txt"),
            "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-swa-best-practices.txt" => LoadFileContent("areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-swa-best-practices.txt"),
            "azure://docs/getting-started" => """
                # Getting Started with Azure MCP Server

                The Azure MCP Server provides programmatic access to Azure services through the Model Context Protocol.

                ## Quick Start

                1. **Installation**
                   ```bash
                   # Install the Azure MCP CLI
                   npm install -g @azure/mcp-cli
                   ```

                2. **Configuration**
                   ```bash
                   # Configure your Azure credentials
                   azmcp auth login
                   ```

                3. **Start the Server**
                   ```bash
                   azmcp server start
                   ```

                ## Available Services

                - **Storage**: Manage Azure Storage accounts, containers, and files
                - **Key Vault**: Secure storage and management of secrets
                - **Monitor**: Query logs and metrics from Azure Monitor
                - **SQL**: Manage Azure SQL databases and queries
                - **And many more...**

                ## Resources and Tools

                - Tools: Interactive commands for performing Azure operations
                - Resources: Static and dynamic content for context and reference

                For more information, visit the [Azure MCP documentation](https://docs.microsoft.com/azure/mcp).
                """,

            "azure://config/server-settings" => """
                {
                  "server": {
                    "name": "Azure MCP Server",
                    "version": "1.0.0-beta",
                    "protocol": "2024-11-05"
                  },
                  "capabilities": {
                    "tools": true,
                    "resources": true,
                    "subscriptions": true
                  },
                  "transport": "stdio",
                  "logging": {
                    "level": "info",
                    "telemetry": true
                  },
                  "features": {
                    "readOnlyMode": false,
                    "namespaces": ["all"]
                  }
                }
                """,

            "azure://tools/available-services" => """
                {
                  "services": [
                    {
                      "name": "storage",
                      "description": "Azure Storage services including Blob, Queue, Table, and File Storage",
                      "tools": ["list", "create", "delete", "upload", "download"],
                      "readonly": false
                    },
                    {
                      "name": "keyvault",
                      "description": "Azure Key Vault for secrets, keys, and certificates management",
                      "tools": ["get-secret", "set-secret", "list-secrets", "delete-secret"],
                      "readonly": false
                    },
                    {
                      "name": "monitor",
                      "description": "Azure Monitor for logs and metrics analysis",
                      "tools": ["query-logs", "get-metrics", "list-workspaces"],
                      "readonly": true
                    },
                    {
                      "name": "sql",
                      "description": "Azure SQL Database management and querying",
                      "tools": ["list-databases", "execute-query", "get-schema"],
                      "readonly": false
                    }
                  ],
                  "totalServices": 4,
                  "lastUpdated": "2025-01-12T15:30:00Z"
                }
                """,

            _ => throw new InvalidOperationException($"Unknown resource: {uri}")
        };
    }

    /// <summary>
    /// Loads content from a file relative to the application's base directory.
    /// </summary>
    private string LoadFileContent(string relativePath)
    {
        try
        {
            // Use AppContext.BaseDirectory which works with single-file apps
            var baseDirectory = AppContext.BaseDirectory;
            
            // Navigate up directories to find the repository root (where AzureMcp.sln should be)
            var currentDir = new DirectoryInfo(baseDirectory);
            while (currentDir != null && !File.Exists(Path.Combine(currentDir.FullName, "AzureMcp.sln")))
            {
                currentDir = currentDir.Parent;
            }
            
            if (currentDir == null)
            {
                _logger.LogWarning("Could not find repository root from base directory: {BaseDirectory}", baseDirectory);
                return $"File content not available (could not locate repository root): {relativePath}";
            }
            
            var fullPath = Path.Combine(currentDir.FullName, relativePath);
            
            if (!File.Exists(fullPath))
            {
                _logger.LogWarning("File not found: {FullPath}", fullPath);
                return $"File not found: {relativePath}";
            }
            
            var content = File.ReadAllText(fullPath);
            _logger.LogDebug("Successfully loaded file content from {FullPath} ({Length} characters)", fullPath, content.Length);
            return content;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading file content from {RelativePath}", relativePath);
            return $"Error loading file content: {ex.Message}";
        }
    }
}
