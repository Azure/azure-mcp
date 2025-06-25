using AzureMcp.Areas.AiFoundry.Models;

namespace AzureMcp.Areas.AiFoundry.Services;

/// <summary>
/// Service interface for Azure AI Foundry operations
/// </summary>
public interface IAiFoundryService
{
    /// <summary>
    /// Lists Azure AI Foundry projects in the specified subscription
    /// </summary>
    /// <param name="subscriptionId">The Azure subscription ID</param>
    /// <param name="resourceGroup">Optional resource group filter</param>
    /// <returns>Collection of project information</returns>
    Task<IEnumerable<ProjectInfo>> ListProjectsAsync(
        string subscriptionId,
        string? resourceGroup = null);

    /// <summary>
    /// Gets detailed information about a specific Azure AI Foundry project
    /// </summary>
    /// <param name="subscriptionId">The Azure subscription ID</param>
    /// <param name="resourceGroup">The resource group name</param>
    /// <param name="projectName">The project name</param>
    /// <returns>Project information or null if not found</returns>
    Task<ProjectInfo?> GetProjectAsync(
        string subscriptionId,
        string resourceGroup,
        string projectName);

    /// <summary>
    /// Lists available models in the model catalog for a project
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <param name="modelProvider">Optional model provider filter</param>
    /// <param name="category">Optional category filter</param>
    /// <returns>Collection of model information</returns>
    Task<IEnumerable<ModelInfo>> ListModelsAsync(
        string projectEndpoint,
        string? modelProvider = null,
        string? category = null);

    /// <summary>
    /// Gets detailed information about a specific model
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <param name="modelId">The model identifier</param>
    /// <returns>Model information or null if not found</returns>
    Task<ModelInfo?> GetModelAsync(
        string projectEndpoint,
        string modelId);

    /// <summary>
    /// Lists model deployments in a project
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <returns>Collection of deployment information</returns>
    Task<IEnumerable<DeploymentInfo>> ListDeploymentsAsync(string projectEndpoint);

    /// <summary>
    /// Gets detailed information about a specific deployment
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <param name="deploymentName">The deployment name</param>
    /// <returns>Deployment information or null if not found</returns>
    Task<DeploymentInfo?> GetDeploymentAsync(
        string projectEndpoint,
        string deploymentName);

    /// <summary>
    /// Lists connections configured in a project
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <returns>Collection of connection information</returns>
    Task<IEnumerable<ConnectionInfo>> ListConnectionsAsync(string projectEndpoint);

    /// <summary>
    /// Gets detailed information about a specific connection
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <param name="connectionName">The connection name</param>
    /// <returns>Connection information or null if not found</returns>
    Task<ConnectionInfo?> GetConnectionAsync(
        string projectEndpoint,
        string connectionName);

    /// <summary>
    /// Lists agents configured in a project
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <returns>Collection of agent information</returns>
    Task<IEnumerable<AgentInfo>> ListAgentsAsync(string projectEndpoint);

    /// <summary>
    /// Gets detailed information about a specific agent
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <param name="agentId">The agent identifier</param>
    /// <returns>Agent information or null if not found</returns>
    Task<AgentInfo?> GetAgentAsync(
        string projectEndpoint,
        string agentId);

    /// <summary>
    /// Lists datasets in a project
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <returns>Collection of dataset information</returns>
    Task<IEnumerable<DatasetInfo>> ListDatasetsAsync(string projectEndpoint);

    /// <summary>
    /// Gets detailed information about a specific dataset
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <param name="datasetId">The dataset identifier</param>
    /// <returns>Dataset information or null if not found</returns>
    Task<DatasetInfo?> GetDatasetAsync(string projectEndpoint, string datasetId);

    /// <summary>
    /// Lists vector stores in a project
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <returns>Collection of vector store information</returns>
    Task<IEnumerable<VectorStoreInfo>> ListVectorStoresAsync(string projectEndpoint);

    /// <summary>
    /// Gets detailed information about a specific vector store
    /// </summary>
    /// <param name="projectEndpoint">The project endpoint URL</param>
    /// <param name="vectorStoreId">The vector store identifier</param>
    /// <returns>Vector store information or null if not found</returns>
    Task<VectorStoreInfo?> GetVectorStoreAsync(string projectEndpoint, string vectorStoreId);
} 