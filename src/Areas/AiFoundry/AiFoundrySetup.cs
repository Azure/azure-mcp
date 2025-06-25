using AzureMcp.Areas.AiFoundry.Services;
using AzureMcp.Commands;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Areas.AiFoundry;

/// <summary>
/// Setup class for Azure AI Foundry area registration
/// </summary>
public class AiFoundrySetup : IAreaSetup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAiFoundryService, AiFoundryService>();
    }

    public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
    {
        // Create AI Foundry command group
        var aiFoundry = new CommandGroup("ai-foundry", "Azure AI Foundry operations - Commands for managing and working with Azure AI Foundry projects, models, and deployments.");
        rootGroup.AddSubGroup(aiFoundry);

        // Create project subgroup
        var project = new CommandGroup("project", "AI Foundry project operations - Commands for listing and managing AI Foundry projects in your Azure subscription.");
        aiFoundry.AddSubGroup(project);

        // Register project commands
        project.AddCommand("list", new Commands.ProjectListCommand(loggerFactory.CreateLogger<Commands.ProjectListCommand>()));
        project.AddCommand("describe", new Commands.ProjectDescribeCommand(loggerFactory.CreateLogger<Commands.ProjectDescribeCommand>()));

        // Create model subgroup
        var model = new CommandGroup("model", "AI model catalog operations - Commands for browsing and discovering available AI models in the model catalog.");
        aiFoundry.AddSubGroup(model);

        // Register model commands
        model.AddCommand("list", new Commands.ModelListCommand(loggerFactory.CreateLogger<Commands.ModelListCommand>()));
        model.AddCommand("describe", new Commands.ModelDescribeCommand(loggerFactory.CreateLogger<Commands.ModelDescribeCommand>()));

        // Create deployment subgroup
        var deployment = new CommandGroup("deployment", "Model deployment operations - Commands for managing and monitoring model deployments in AI Foundry projects.");
        aiFoundry.AddSubGroup(deployment);

        // Register deployment commands
        deployment.AddCommand("list", new Commands.DeploymentListCommand(loggerFactory.CreateLogger<Commands.DeploymentListCommand>()));
        deployment.AddCommand("describe", new Commands.DeploymentDescribeCommand(loggerFactory.CreateLogger<Commands.DeploymentDescribeCommand>()));

        // Create connection subgroup
        var connection = new CommandGroup("connection", "Project connection operations - Commands for managing external service connections and integrations in AI Foundry projects.");
        aiFoundry.AddSubGroup(connection);

        // Register connection commands
        connection.AddCommand("list", new Commands.ConnectionListCommand(loggerFactory.CreateLogger<Commands.ConnectionListCommand>()));
        connection.AddCommand("describe", new Commands.ConnectionDescribeCommand(loggerFactory.CreateLogger<Commands.ConnectionDescribeCommand>()));

        // Create agent subgroup
        var agent = new CommandGroup("agent", "AI agent operations - Commands for managing and configuring AI agents and assistants in AI Foundry projects.");
        aiFoundry.AddSubGroup(agent);

        // Register agent commands
        agent.AddCommand("list", new Commands.AgentListCommand(loggerFactory.CreateLogger<Commands.AgentListCommand>()));
        agent.AddCommand("describe", new Commands.AgentDescribeCommand(loggerFactory.CreateLogger<Commands.AgentDescribeCommand>()));

        // Create dataset subgroup
        var dataset = new CommandGroup("dataset", "Dataset operations - Commands for managing and working with datasets in AI Foundry projects.");
        aiFoundry.AddSubGroup(dataset);

        // Register dataset commands
        dataset.AddCommand("list", new Commands.DatasetListCommand(loggerFactory.CreateLogger<Commands.DatasetListCommand>()));
        dataset.AddCommand("describe", new Commands.DatasetDescribeCommand(loggerFactory.CreateLogger<Commands.DatasetDescribeCommand>()));

        // Create vector store subgroup
        var vectorStore = new CommandGroup("vectorstore", "Vector store operations - Commands for managing and working with vector stores in AI Foundry projects.");
        aiFoundry.AddSubGroup(vectorStore);

        // Register vector store commands
        vectorStore.AddCommand("list", new Commands.VectorStoreListCommand(loggerFactory.CreateLogger<Commands.VectorStoreListCommand>()));
        vectorStore.AddCommand("describe", new Commands.VectorStoreDescribeCommand(loggerFactory.CreateLogger<Commands.VectorStoreDescribeCommand>()));
    }
} 