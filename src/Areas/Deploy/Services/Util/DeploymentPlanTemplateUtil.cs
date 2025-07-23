// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Deploy.Models;

namespace AzureMcp.Areas.Deploy.Services.Util;

/// <summary>
/// Utility class for generating deployment plan templates.
/// </summary>
public static class DeploymentPlanTemplateUtil
{
    /// <summary>
    /// Generates a deployment plan template with the specified project name.
    /// </summary>
    /// <param name="projectName">The name of the project. Can be null or empty.</param>
    /// <returns>A formatted deployment plan template string.</returns>
    public static string GetPlanTemplate(string projectName, string targetAppService, string provisioningTool, string? azdIacOptions = "")
    {
        // Default values for optional parameters
        if (provisioningTool == "azd" && string.IsNullOrWhiteSpace(azdIacOptions))
        {
            azdIacOptions = "bicep";
        }
        var azureComputeHost = targetAppService.ToLowerInvariant() switch
        {
            "containerapp" => "Azure Container Apps",
            "webapp" => "Azure Web App Service",
            "functionapp" => "Azure Functions",
            "aks" => "Azure Kubernetes Service",
            _ => "Azure Container Apps"
        };

        var aksDeploySteps = """
        2. Build and Deploy the Application
            1. Build and Push Docker Image: {Agent should check if Dockerfile exists, if not add the step: "generate a Dockerfile for the application deployment", if does, list the Dockerfile path}.
            2. Prepare Kubernetes Manifests: {Agent should check if Kubernetes YAML files exists, if not add the step: "generate for the application deployment", if does, list the yaml files path}.
            3. Deploy to AKS: Use `kubectl apply` to deploy manifests to the AKS cluster
        3. Validation:
            1. Verify pods are running and services are exposed
        """;

        var summary = "Summarize the deployment result and save to '.azure/summary.copilotmd'. It should list all changes deployment files and brief description of each file. Then have a diagram showing the provisioned azure resource.";
        var steps = new List<string>();

        if (provisioningTool.ToLowerInvariant() == "azd")
        {
            var deployTitle = targetAppService.ToLowerInvariant() == "aks"
                ? ""
                : " And Deploy the Application";
            var checkLog = targetAppService.ToLowerInvariant() == "aks"
                ? ""
                : "6. Check the application log with tool `azd-app-log-get` to ensure the services are running.";
            steps.Add($"""
            1. Provision Azure Infrastructure{deployTitle}:
                1. Based on following required Azure resources in plan, get the IaC rules from the tool `iac-rules-get`
                2. Generate IaC ({azdIacOptions} files) for required azure resources based on the plan.
                3. Pre-check: use `get_errors` tool to check generated Bicep grammar errors. Fix the errors if exist.
                4. Run the AZD command `azd up` to provision the resources and confirm each resource is created or already exists.
                5. Check the deployment output to ensure the resources are provisioned successfully.
                {checkLog}
            """);
            if (targetAppService.ToLowerInvariant() == "aks")
            {
                steps.Add(aksDeploySteps);
                steps.Add($$"""
                4: Summary:
                    1. {{summary}}
                """);
            }
            else
            {
                steps.Add($$"""
                2: Summary:
                    1. {{summary}}
                """);
            }


        }
        else if (provisioningTool.Equals(DeploymentTool.AzCli, StringComparison.OrdinalIgnoreCase))
        {
            steps.Add("""
            1. Provision Azure Infrastructure:
                1. Generate Azure CLI scripts for required azure resources based on the plan.
                2. Check and fix the generated Azure CLI scripts for grammar errors.
                3. Run the Azure CLI scripts to provision the resources and confirm each resource is created or already exists
            """);
            if (targetAppService.ToLowerInvariant() == "aks")
            {
                steps.Add(aksDeploySteps);
            }
            else
            {
                var isContainerApp = targetAppService.ToLowerInvariant() == "containerapp";
                var containerAppOptions = isContainerApp ? "    1. Build and Push Docker Image: Agent should check if Dockerfile exists, if not add the step: 'generate a Dockerfile for the application deployment', if it does, list the Dockerfile path" : "";
                var orderList = isContainerApp ? "2." : "1.";
                steps.Add($$"""
                2. Build and Deploy the Application:
                    {{containerAppOptions}}
                    {{orderList}} Deploy to {{azureComputeHost}}: Use Azure CLI command to deploy the application
                3. Validation:
                    1. Verify command output to ensure the application is deployed successfully
                """);
            }
            steps.Add($$"""
            4: Summary:
                1 {{summary}}
            """);
        }
        var title = string.IsNullOrWhiteSpace(projectName)
               ? "Azure Deployment Plan"
                   : $"Azure Deployment Plan for {projectName} Project";

        return $$"""
{Agent should fill in and polish the markdown template below to generate a deployment plan for the project. Then save it to '.azure/plan.copilotmd' file. Don't add cost estimation! Don't add extra validation steps unless it is required! Don't change the tool name!}

#Title: {{title}}
## **Goal**
Based on the project to provide a plan to deploy the project to Azure using AZD. It will generate Bicep files and Azure YAML configuration.


## **Project Information**
{
briefly summarize the project structure, services, and configurations, example:
AppName: web
- **Technology Stack**: ASP.NET Core 7.0 Razor Pages application
- **Application Type**: Task Manager web application with client-side JavaScript
- **Containerization**: Ready for deployment with existing Dockerfile
- **Dependencies**: No external dependencies detected (database, APIs, etc.)
- **Hosting Recommendation**: Azure Container Apps for scalable, serverless container hosting
}

## **Azure Resources Architecture**
> **Install the mermaid extension in IDE to view the architecture.**
{a mermaid graph of following recommended azure resource architecture. Only keep the most important edges to make structure clear and readable.}
{
List how data flows between the services, example:
- The container app gets its image from the Azure Container Registry.
- The container app gets requests and interacts with the Azure SQL Database for data storage and retrieval.
}


## **Recommended Azure Resources**

Recommended App service hosting the project //agent should fulfill this for each app instance
- Application {{projectName}}
  - Hosting Service Type: {{azureComputeHost}} // it can be Azure Container Apps, Web App Service, Azure Functions, Azure Kubernetes Service. Recommend one based on the project.
  - SKU // recommend a sku based on the project, show its performance. Don't estimate the cost.
  - Configuration:
    - language: {language}  //detect from the project, it can be nodejs, python, dotnet, etc.
    - dockerFilePath: {dockerFilePath}// fulfill this if service.azureComputeHost is ContainerApp
    - dockerContext: {dockerContext}// fulfill this if service.azureComputeHost is ContainerApp
    - Environment Variables: [] // the env variables that are used in the project/required by service
  - Dependencies Resource
    - Dependency Name
    - SKU // recommend a sku, show its performance.
    - Service Type // it can be Azure SQL, Azure Cosmos DB, Azure Storage, etc.
    - Connection Type // it can be connection string, managed identity, etc.
    - Environment Variables: [] // the env variables that are used in the project/required by dependency

Recommended Supporting Services
- Application Insights
- Log Analytics Workspace: set all app service to connect to this
- Key Vault(Optional): If there are dependencies such as postgresql/sql/mysql, create a Key Vault to store connection string. If not, the resource should not show.
If there is a Container App, the following resources are required:
- Container Registry
If there is a WebApp(App Service):
- App Service Site Extension (Microsoft.Web/sites/siteextensions): Required for App Service deployments.

Recommended Security Configurations
If there is a Container App
- User managed identity: Must be assigned to the container app.
- AcrPull role assignment: User managed identity must have **AcrPull** role ("7f951dda-4ed3-4680-a7ca-43fe172d538d") assigned to the container registry.

## **Execution Step**
> **Below are the steps for Copilot to follow; ask Copilot to update or execute this plan.**
{{string.Join(Environment.NewLine, steps)}}

""";
    }
}
