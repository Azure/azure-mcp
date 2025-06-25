using AzureMcp.Models.Option;

namespace AzureMcp.Areas.AiFoundry.Options;

/// <summary>
/// Option definitions for Azure AI Foundry commands
/// </summary>
public static class AiFoundryOptionDefinitions
{
    public const string ProjectNameName = "project-name";

    public static readonly Option<string> ProjectName = new(
        $"--{ProjectNameName}",
        "The name of the Azure AI Foundry project."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> OptionalResourceGroup = new(
        $"--{OptionDefinitions.Common.ResourceGroupName}",
        "The name of the Azure resource group. This is a logical container for Azure resources."
    )
    {
        IsRequired = false
    };

    public const string ProjectEndpointName = "project-endpoint";

    public static readonly Option<string> ProjectEndpoint = new(
        $"--{ProjectEndpointName}",
        "The Azure AI Foundry project endpoint URL."
    )
    {
        IsRequired = true
    };

    public const string ModelProviderName = "model-provider";

    public static readonly Option<string> ModelProvider = new(
        $"--{ModelProviderName}",
        "Filter models by provider (e.g., OpenAI, Microsoft, Meta)."
    )
    {
        IsRequired = false
    };

    public const string ModelCategoryName = "category";

    public static readonly Option<string> ModelCategory = new(
        $"--{ModelCategoryName}",
        "Filter models by category (e.g., chat, embedding, vision)."
    )
    {
        IsRequired = false
    };

    public const string ModelIdName = "model-id";

    public static readonly Option<string> ModelId = new(
        $"--{ModelIdName}",
        "The model identifier."
    )
    {
        IsRequired = true
    };

    public const string DeploymentNameName = "deployment-name";

    public static readonly Option<string> DeploymentName = new(
        $"--{DeploymentNameName}",
        "The deployment name."
    )
    {
        IsRequired = true
    };

    public const string ConnectionNameName = "connection-name";

    public static readonly Option<string> ConnectionName = new(
        $"--{ConnectionNameName}",
        "The connection name."
    )
    {
        IsRequired = true
    };

    public const string AgentIdName = "agent-id";

    public static readonly Option<string> AgentId = new(
        $"--{AgentIdName}",
        "The agent identifier."
    )
    {
        IsRequired = true
    };

    public const string DatasetIdName = "dataset-id";

    public static readonly Option<string> DatasetId = new(
        $"--{DatasetIdName}",
        "The dataset identifier."
    )
    {
        IsRequired = true
    };

    public const string VectorStoreIdName = "vectorstore-id";

    public static readonly Option<string> VectorStoreId = new(
        $"--{VectorStoreIdName}",
        "The vector store identifier."
    )
    {
        IsRequired = true
    };
} 