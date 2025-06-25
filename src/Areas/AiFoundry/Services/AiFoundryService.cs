using Azure;
using Azure.AI.Projects;
using Azure.Core;
using Azure.ResourceManager;
using Azure.ResourceManager.Resources;
using AzureMcp.Areas.AiFoundry.Models;
using AzureMcp.Services.Azure;
using AzureMcp.Services.Azure.Tenant;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AiFoundry.Services;

/// <summary>
/// Service implementation for Azure AI Foundry operations
/// </summary>
public class AiFoundryService : BaseAzureService, IAiFoundryService
{
    private readonly ILogger<AiFoundryService> _logger;

    public AiFoundryService(ILogger<AiFoundryService> logger, ITenantService? tenantService = null)
        : base(tenantService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<IEnumerable<ProjectInfo>> ListProjectsAsync(
        string subscriptionId,
        string? resourceGroup = null)
    {
        try
        {
            _logger.LogInformation("Listing AI Foundry projects in subscription {SubscriptionId}", subscriptionId);
            
            var armClient = await CreateArmClientAsync();
            var subscription = armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(subscriptionId));
            
            var projects = new List<ProjectInfo>();
            
            // Get resource groups to search
            var resourceGroups = resourceGroup != null
                ? [(await subscription.GetResourceGroups().GetAsync(resourceGroup)).Value]
                : await subscription.GetResourceGroups().GetAllAsync().ToListAsync();

            foreach (var rg in resourceGroups.Where(rg => rg != null))
            {
                // Find AI Foundry projects (they are Machine Learning workspaces with specific tags/properties)
                await foreach (var resource in rg!.GetGenericResourcesAsync(
                    filter: "resourceType eq 'Microsoft.MachineLearningServices/workspaces'"))
                {
                    // Check if this is an AI Foundry project by examining its properties
                    if (IsAiFoundryProject(resource))
                    {
                        var projectInfo = CreateProjectInfoAsync(resource);
                        if (projectInfo != null)
                        {
                            projects.Add(projectInfo);
                        }
                    }
                }
            }

            _logger.LogInformation("Found {Count} AI Foundry projects", projects.Count);
            return projects;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list AI Foundry projects");
            throw;
        }
    }

    public async Task<ProjectInfo?> GetProjectAsync(
        string subscriptionId,
        string resourceGroup,
        string projectName)
    {
        try
        {
            _logger.LogInformation("Getting AI Foundry project {ProjectName} in resource group {ResourceGroup}", 
                projectName, resourceGroup);
            
            var armClient = await CreateArmClientAsync();
            var subscription = armClient.GetSubscriptionResource(SubscriptionResource.CreateResourceIdentifier(subscriptionId));
            
            var resourceId = $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.MachineLearningServices/workspaces/{projectName}";
            var resource = armClient.GetGenericResource(new ResourceIdentifier(resourceId));
            
            try
            {
                var resourceResponse = await resource.GetAsync();
                var resourceObj = resourceResponse.Value;
                
                if (IsAiFoundryProject(resourceObj))
                {
                    return CreateProjectInfoAsync(resourceObj);
                }
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                // Resource not found, return null
            }
            
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get AI Foundry project {ProjectName}", projectName);
            throw;
        }
    }

    public async Task<IEnumerable<ModelInfo>> ListModelsAsync(
        string projectEndpoint,
        string? modelProvider = null,
        string? category = null)
    {
        try
        {
            _logger.LogInformation("Listing models for project endpoint {ProjectEndpoint}", projectEndpoint);
            
            var projectClient = await CreateProjectClientAsync(projectEndpoint);
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var models = new ModelInfo[]
            {
                new()
                {
                    Id = "gpt-4-turbo",
                    Name = "GPT-4 Turbo",
                    Publisher = "OpenAI",
                    Version = "1.0",
                    Description = "Advanced large language model with improved efficiency",
                    ModelType = "chat",
                    IsAvailable = true,
                    ContextLength = 128000,
                    Categories = ["chat", "completion"]
                }
            };
            
            return models;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list models for project {ProjectEndpoint}", projectEndpoint);
            throw;
        }
    }

    public async Task<ModelInfo?> GetModelAsync(
        string projectEndpoint,
        string modelId)
    {
        try
        {
            _logger.LogInformation("Getting model {ModelId} for project {ProjectEndpoint}", modelId, projectEndpoint);
            
            var projectClient = await CreateProjectClientAsync(projectEndpoint);
            
            // TODO: Implement model details retrieval using Azure.AI.Projects SDK
            // This is a placeholder implementation - will be completed in the next phase
            
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get model {ModelId}", modelId);
            throw;
        }
    }

    public async Task<IEnumerable<DeploymentInfo>> ListDeploymentsAsync(string projectEndpoint)
    {
        try
        {
            _logger.LogInformation("Listing deployments for project endpoint {ProjectEndpoint}", projectEndpoint);
            
            var projectClient = await CreateProjectClientAsync(projectEndpoint);
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var deployments = new DeploymentInfo[]
            {
                new()
                {
                    Name = "GPT-4 Production",
                    ModelId = "gpt-4-turbo",
                    Status = "Running",
                    CreatedAt = DateTimeOffset.UtcNow.AddDays(-7),
                    UpdatedAt = DateTimeOffset.UtcNow.AddHours(-1),
                    DeploymentType = "Standard",
                    InstanceCount = 2
                }
            };
            
            return deployments;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list deployments for project {ProjectEndpoint}", projectEndpoint);
            throw;
        }
    }

    public async Task<DeploymentInfo?> GetDeploymentAsync(
        string projectEndpoint,
        string deploymentName)
    {
        try
        {
            _logger.LogInformation("Getting deployment {DeploymentName} for project {ProjectEndpoint}", 
                deploymentName, projectEndpoint);
            
            var projectClient = await CreateProjectClientAsync(projectEndpoint);
            
            // TODO: Implement deployment details retrieval using Azure.AI.Projects SDK
            // This is a placeholder implementation - will be completed in the next phase
            
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get deployment {DeploymentName}", deploymentName);
            throw;
        }
    }

    public async Task<IEnumerable<ConnectionInfo>> ListConnectionsAsync(string projectEndpoint)
    {
        try
        {
            _logger.LogInformation("Listing connections for project endpoint {ProjectEndpoint}", projectEndpoint);
            
            var projectClient = await CreateProjectClientAsync(projectEndpoint);
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var connections = new ConnectionInfo[]
            {
                new()
                {
                    Name = "Primary Storage",
                    ConnectionType = "AzureBlob",
                    Status = "Connected",
                    CreatedAt = DateTimeOffset.UtcNow.AddDays(-10),
                    UpdatedAt = DateTimeOffset.UtcNow.AddDays(-1),
                    IsActive = true,
                    Target = "https://storage.blob.core.windows.net"
                }
            };
            
            return connections;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list connections for project {ProjectEndpoint}", projectEndpoint);
            throw;
        }
    }

    public async Task<ConnectionInfo?> GetConnectionAsync(
        string projectEndpoint,
        string connectionName)
    {
        try
        {
            _logger.LogInformation("Getting connection {ConnectionName} for project {ProjectEndpoint}", 
                connectionName, projectEndpoint);
            
            var projectClient = await CreateProjectClientAsync(projectEndpoint);
            
            // TODO: Implement connection details retrieval using Azure.AI.Projects SDK
            // This is a placeholder implementation - will be completed in the next phase
            
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get connection {ConnectionName}", connectionName);
            throw;
        }
    }

    public Task<IEnumerable<AgentInfo>> ListAgentsAsync(string projectEndpoint)
    {
        _logger.LogInformation("Listing AI agents for project endpoint: {ProjectEndpoint}", projectEndpoint);
        
        try
        {
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var agents = new AgentInfo[]
            {
                new()
                {
                    Id = "agent-1",
                    Name = "Customer Support Agent",
                    Description = "Handles customer inquiries and support requests",
                    Model = "gpt-4",
                    Instructions = "You are a helpful customer support agent.",
                    Tools = ["function_calling", "code_interpreter"],
                    CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
                    UpdatedAt = DateTimeOffset.UtcNow.AddHours(-2)
                }
            };
            
            return Task.FromResult<IEnumerable<AgentInfo>>(agents);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list AI agents for project endpoint: {ProjectEndpoint}", projectEndpoint);
            throw;
        }
    }
    
    public Task<AgentInfo?> GetAgentAsync(string projectEndpoint, string agentId)
    {
        _logger.LogInformation("Getting AI agent {AgentId} for project endpoint: {ProjectEndpoint}", agentId, projectEndpoint);
        
        try
        {
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            if (agentId == "agent-1")
            {
                var agent = new AgentInfo
                {
                    Id = agentId,
                    Name = "Customer Support Agent",
                    Description = "Handles customer inquiries and support requests",
                    Model = "gpt-4",
                    Instructions = "You are a helpful customer support agent.",
                    Tools = ["function_calling", "code_interpreter"],
                    CreatedAt = DateTimeOffset.UtcNow.AddDays(-5),
                    UpdatedAt = DateTimeOffset.UtcNow.AddHours(-2)
                };
                return Task.FromResult<AgentInfo?>(agent);
            }
            
            return Task.FromResult<AgentInfo?>(null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get AI agent {AgentId} for project endpoint: {ProjectEndpoint}", agentId, projectEndpoint);
            throw;
        }
    }
    
    public Task<IEnumerable<DatasetInfo>> ListDatasetsAsync(string projectEndpoint)
    {
        _logger.LogInformation("Listing datasets for project endpoint: {ProjectEndpoint}", projectEndpoint);
        
        try
        {
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var datasets = new DatasetInfo[]
            {
                new()
                {
                    Id = "dataset-1",
                    Name = "Customer Reviews Dataset",
                    Description = "Collection of customer feedback and reviews",
                    DataType = "text",
                    Version = "1.0",
                    Size = 1024 * 1024 * 10, // 10MB
                    ItemCount = 5000,
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-10),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddDays(-1)
                },
                new()
                {
                    Id = "dataset-2",
                    Name = "Product Images",
                    Description = "Training images for product classification",
                    DataType = "image",
                    Version = "2.1",
                    Size = 1024 * 1024 * 500, // 500MB
                    ItemCount = 12000,
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-30),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddDays(-7)
                }
            };
            
            return Task.FromResult<IEnumerable<DatasetInfo>>(datasets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list datasets for project endpoint: {ProjectEndpoint}", projectEndpoint);
            throw;
        }
    }
    
    public Task<DatasetInfo?> GetDatasetAsync(string projectEndpoint, string datasetId)
    {
        _logger.LogInformation("Getting dataset {DatasetId} for project endpoint: {ProjectEndpoint}", datasetId, projectEndpoint);
        
        try
        {
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var result = datasetId switch
            {
                "dataset-1" => new DatasetInfo
                {
                    Id = datasetId,
                    Name = "Customer Reviews Dataset",
                    Description = "Collection of customer feedback and reviews",
                    DataType = "text",
                    Version = "1.0",
                    Size = 1024 * 1024 * 10, // 10MB
                    ItemCount = 5000,
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-10),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddDays(-1)
                },
                "dataset-2" => new DatasetInfo
                {
                    Id = datasetId,
                    Name = "Product Images",
                    Description = "Training images for product classification",
                    DataType = "image",
                    Version = "2.1",
                    Size = 1024 * 1024 * 500, // 500MB
                    ItemCount = 12000,
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-30),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddDays(-7)
                },
                _ => null
            };
            
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get dataset {DatasetId} for project endpoint: {ProjectEndpoint}", datasetId, projectEndpoint);
            throw;
        }
    }
    
    public Task<IEnumerable<VectorStoreInfo>> ListVectorStoresAsync(string projectEndpoint)
    {
        _logger.LogInformation("Listing vector stores for project endpoint: {ProjectEndpoint}", projectEndpoint);
        
        try
        {
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var vectorStores = new VectorStoreInfo[]
            {
                new()
                {
                    Id = "vectorstore-1",
                    Name = "Knowledge Base Embeddings",
                    Description = "Vector embeddings for company knowledge base",
                    Dimensions = 1536,
                    VectorCount = 50000,
                    IndexType = "hnsw",
                    EmbeddingModel = "text-embedding-ada-002",
                    Status = "ready",
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-15),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddHours(-3)
                },
                new()
                {
                    Id = "vectorstore-2",
                    Name = "Product Catalog Vectors",
                    Description = "Embeddings for product search and recommendations",
                    Dimensions = 768,
                    VectorCount = 25000,
                    IndexType = "flat",
                    EmbeddingModel = "text-embedding-3-small",
                    Status = "indexing",
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-3),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddMinutes(-30)
                }
            };
            
            return Task.FromResult<IEnumerable<VectorStoreInfo>>(vectorStores);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to list vector stores for project endpoint: {ProjectEndpoint}", projectEndpoint);
            throw;
        }
    }
    
    public Task<VectorStoreInfo?> GetVectorStoreAsync(string projectEndpoint, string vectorStoreId)
    {
        _logger.LogInformation("Getting vector store {VectorStoreId} for project endpoint: {ProjectEndpoint}", vectorStoreId, projectEndpoint);
        
        try
        {
            // TODO: Implement using Azure.AI.Projects SDK when available
            // For now, return mock data for testing
            var result = vectorStoreId switch
            {
                "vectorstore-1" => new VectorStoreInfo
                {
                    Id = vectorStoreId,
                    Name = "Knowledge Base Embeddings",
                    Description = "Vector embeddings for company knowledge base",
                    Dimensions = 1536,
                    VectorCount = 50000,
                    IndexType = "hnsw",
                    EmbeddingModel = "text-embedding-ada-002",
                    Status = "ready",
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-15),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddHours(-3)
                },
                "vectorstore-2" => new VectorStoreInfo
                {
                    Id = vectorStoreId,
                    Name = "Product Catalog Vectors",
                    Description = "Embeddings for product search and recommendations",
                    Dimensions = 768,
                    VectorCount = 25000,
                    IndexType = "flat",
                    EmbeddingModel = "text-embedding-3-small",
                    Status = "indexing",
                    CreatedDateTime = DateTimeOffset.UtcNow.AddDays(-3),
                    ModifiedDateTime = DateTimeOffset.UtcNow.AddMinutes(-30)
                },
                _ => null
            };
            
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to get vector store {VectorStoreId} for project endpoint: {ProjectEndpoint}", vectorStoreId, projectEndpoint);
            throw;
        }
    }

    #region Private Helper Methods

    private async Task<AIProjectClient> CreateProjectClientAsync(string projectEndpoint)
    {
        var credential = await GetCredential();
        return new AIProjectClient(new Uri(projectEndpoint), credential);
    }

    private static bool IsAiFoundryProject(GenericResource resource)
    {
        // AI Foundry projects are Machine Learning workspaces with specific characteristics
        // We can identify them by checking for specific tags, properties, or other indicators
        
        // For now, we'll assume all ML workspaces could be AI Foundry projects
        // In practice, you might need to check specific properties or tags
        return resource.Data.ResourceType == "Microsoft.MachineLearningServices/workspaces";
    }

    private static ProjectInfo? CreateProjectInfoAsync(GenericResource resource)
    {
        var data = resource.Data;
        
        return new ProjectInfo
        {
            Name = data.Name,
            ResourceGroup = ExtractResourceGroupFromId(data.Id),
            SubscriptionId = ExtractSubscriptionIdFromId(data.Id),
            Location = data.Location.Name ?? "Unknown",
            Endpoint = BuildProjectEndpoint(data.Name, data.Location.Name ?? "eastus"),
            CreatedAt = data.CreatedOn,
            Tags = data.Tags?.ToDictionary(t => t.Key, t => t.Value) ?? new Dictionary<string, string>()
        };
    }

    private static string ExtractResourceGroupFromId(ResourceIdentifier resourceId)
    {
        return resourceId.ResourceGroupName ?? "Unknown";
    }

    private static string ExtractSubscriptionIdFromId(ResourceIdentifier resourceId)
    {
        return resourceId.SubscriptionId ?? "Unknown";
    }

    private static string BuildProjectEndpoint(string projectName, string location)
    {
        // AI Foundry project endpoints follow a specific pattern
        return $"https://{projectName}.{location}.api.azureml.ms";
    }

    #endregion
} 