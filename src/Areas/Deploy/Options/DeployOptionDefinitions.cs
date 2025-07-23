// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Nodes;
using AzureMcp.Areas.Server.Commands;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Options;

namespace AzureMcp.Areas.Deploy.Options;

public static class DeployOptionDefinitions
{
    public static class RawMcpToolInput
    {
        public const string RawMcpToolInputName = CommandFactoryToolLoader.RawMcpToolInputOptionName;

        public static readonly Option<string> RawMcpToolInputOption = new(
            $"--{RawMcpToolInputName}",
            AppTopologySchema.Schema.ToJsonString()
        )
        {
            IsRequired = true
        };
    }

    public class AzdAppLogOptions : SubscriptionOptions
    {
        public const string WorkspaceFolderName = "workspace-folder";
        public const string AzdEnvNameName = "azd-env-name";
        public const string LimitName = "limit";

        public static readonly Option<string> WorkspaceFolder = new(
            $"--{WorkspaceFolderName}",
            "The full path of the workspace folder."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> AzdEnvName = new(
            $"--{AzdEnvNameName}",
            "The name of the environment created by azd (AZURE_ENV_NAME) during `azd init` or `azd up`. If not provided in context, try to find it in the .azure directory in the workspace or use 'azd env list'."
        )
        {
            IsRequired = true
        };

        public static readonly Option<int> Limit = new(
            $"--{LimitName}",
            () => 200,
            "The maximum row number of logs to retrieve. Use this to get a specific number of logs or to avoid the retrieved logs from reaching token limit. Default is 200."
        )
        {
            IsRequired = false
        };
    }

    public class PipelineGenerateOptions : SubscriptionOptions
    {
        public const string UseAZDPipelineConfigName = "use-azd-pipeline-config";
        public const string OrganizationNameName = "organization-name";
        public const string RepositoryNameName = "repository-name";
        public const string GithubEnvironmentNameName = "github-environment-name";

        public static readonly Option<bool> UseAZDPipelineConfig = new(
            $"--{UseAZDPipelineConfigName}",
            () => false,
            "Whether to use azd tool to set up the deployment pipeline. Set to true ONLY if azure.yaml is provided or the context suggests AZD tools."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> OrganizationName = new(
            $"--{OrganizationNameName}",
            "The name of the organization or the user account name of the current Github repository. DO NOT fill this in if you're not sure."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> RepositoryName = new(
            $"--{RepositoryNameName}",
            "The name of the current Github repository. DO NOT fill this in if you're not sure."
        )
        {
            IsRequired = false
        };

        public static readonly Option<string> GithubEnvironmentName = new(
            $"--{GithubEnvironmentNameName}",
            "The name of the environment to which the deployment pipeline will be deployed. DO NOT fill this in if you're not sure."
        )
        {
            IsRequired = false
        };

    }

    public static class PlanGet
    {
        public const string WorkspaceFolderName = "workspace-folder";
        public const string ProjectNameName = "project-name";
        public const string TargetAppServiceName = "target-app-service";
        public const string ProvisioningToolName = "provisioning-tool";
        public const string AzdIacOptionsName = "azd-iac-options";

        public static readonly Option<string> WorkspaceFolder = new(
            $"--{WorkspaceFolderName}",
            "The full path of the workspace folder."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> ProjectName = new(
            $"--{ProjectNameName}",
            "The name of the project to generate the deployment plan for. If not provided, will be inferred from the workspace."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> TargetAppService = new(
            $"--{TargetAppServiceName}",
            "The Azure service to deploy the application. Valid values: ContainerApp, WebApp, FunctionApp, AKS. Recommend one based on user application."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> ProvisioningTool = new(
            $"--{ProvisioningToolName}",
            "The tool to use for provisioning Azure resources. Valid values: AZD, AzCli. Use AzCli if TargetAppService is AKS."
        )
        {
            IsRequired = true
        };

        public static readonly Option<string> AzdIacOptions = new(
            $"--{AzdIacOptionsName}",
            "The Infrastructure as Code option for azd. Valid values: bicep, terraform."
        )
        {
            IsRequired = false
        };
    }

    public static class IaCRules
    {
        public static readonly Option<string> DeploymentTool = new(
            "--deployment-tool",
            "The deployment tool to use. Valid values: AZD, AzCli")
        {
            IsRequired = true
        };

        public static readonly Option<string> IacType = new(
            "--iac-type",
            "The Infrastructure as Code type. Valid values: bicep, terraform")
        {
            IsRequired = true
        };

        public static readonly Option<string> ResourceTypes = new(
            "--resource-types",
            "Comma-separated list of Azure resource types to generate rules for. Supported values: 'appservice' (App Service) and/or 'containerapp' (Container App) and/or 'function' (Function App). Other resources do not have special rules.")
        {
            IsRequired = true,
            AllowMultipleArgumentsPerToken = true
        };
    }
}

public static class AppTopologySchema
{
    public static readonly JsonObject Schema = new JsonObject
    {
        ["type"] = "object",
        ["properties"] = new JsonObject
        {
            ["workspaceFolder"] = new JsonObject
            {
                ["type"] = "string",
                ["description"] = "The full path of the workspace folder."
            },
            ["projectName"] = new JsonObject
            {
                ["type"] = "string",
                ["description"] = "The name of the project. This is used to generate the resource names."
            },
            ["services"] = new JsonObject
            {
                ["type"] = "array",
                ["description"] = "An array of service parameters.",
                ["items"] = new JsonObject
                {
                    ["type"] = "object",
                    ["properties"] = new JsonObject
                    {
                        ["name"] = new JsonObject
                        {
                            ["type"] = "string",
                            ["description"] = "The name of the service."
                        },
                        ["path"] = new JsonObject
                        {
                            ["type"] = "string",
                            ["description"] = "The relative path of the service main project folder"
                        },
                        ["language"] = new JsonObject
                        {
                            ["type"] = "string",
                            ["description"] = "The programming language of the service."
                        },
                        ["port"] = new JsonObject
                        {
                            ["type"] = "string",
                            ["description"] = "The port number the service uses. Get this from Dockerfile for container apps. If not available, default to '80'."
                        },
                        ["azureComputeHost"] = new JsonObject
                        {
                            ["type"] = "string",
                            ["description"] = "The appropriate azure service that should be used to host this service. Use containerapp if the service is containerized and has a Dockerfile.",
                            ["enum"] = new JsonArray("appservice", "containerapp", "function", "staticwebapp")
                        },
                        ["dockerSettings"] = new JsonObject
                        {
                            ["type"] = "object",
                            ["description"] = "Docker settings for the service. This is only needed if the service's azureComputeHost is containerapp.",
                            ["properties"] = new JsonObject
                            {
                                ["dockerFilePath"] = new JsonObject
                                {
                                    ["type"] = "string",
                                    ["description"] = "The absolute path to the Dockerfile for the service. If the service's azureComputeHost is not containerapp, leave blank."
                                },
                                ["dockerContext"] = new JsonObject
                                {
                                    ["type"] = "string",
                                    ["description"] = "The absolute path to the Docker build context for the service. If the service's azureComputeHost is not containerapp, leave blank."
                                }
                            },
                            ["required"] = new JsonArray("dockerFilePath", "dockerContext")
                        },
                        ["dependencies"] = new JsonObject
                        {
                            ["type"] = "array",
                            ["description"] = "An array of dependent services. A compute service may have a dependency on another compute service.",
                            ["items"] = new JsonObject
                            {
                                ["type"] = "object",
                                ["properties"] = new JsonObject
                                {
                                    ["name"] = new JsonObject
                                    {
                                        ["type"] = "string",
                                        ["description"] = "The name of the dependent service. Can be arbitrary, or must reference another service in the services array if referencing azureappservice, azurecontainerapp, azurestaticwebapps, or azurefunctions."
                                    },
                                    ["serviceType"] = new JsonObject
                                    {
                                        ["type"] = "string",
                                        ["description"] = "The name of the azure service that can be used for this dependent service.",
                                        ["enum"] = new JsonArray("azureaisearch", "azureaiservices", "appservice", "azureapplicationinsights", "azurebotservice", "containerapp", "azurecosmosdb", "functionapp", "azurekeyvault", "azuredatabaseformysql", "azureopenai", "azuredatabaseforpostgresql", "azureprivateendpoint", "azurecacheforredis", "azuresqldatabase", "azurestorageaccount", "staticwebapp", "azureservicebus", "azuresignalrservice", "azurevirtualnetwork", "azurewebpubsub")
                                    },
                                    ["connectionType"] = new JsonObject
                                    {
                                        ["type"] = "string",
                                        ["description"] = "The connection authentication type of the dependency.",
                                        ["enum"] = new JsonArray("http", "secret", "system-identity", "user-identity", "bot-connection")
                                    },
                                    ["environmentVariables"] = new JsonObject
                                    {
                                        ["type"] = "array",
                                        ["description"] = "An array of environment variables defined in source code to set up the connection.",
                                        ["items"] = new JsonObject
                                        {
                                            ["type"] = "string"
                                        }
                                    }
                                },
                                ["required"] = new JsonArray("name", "serviceType", "connectionType", "environmentVariables")
                            }
                        },
                        ["settings"] = new JsonObject
                        {
                            ["type"] = "array",
                            ["description"] = "An array of environment variables needed to run this service.  Please search the entire codebase to find environment variables.",
                            ["items"] = new JsonObject
                            {
                                ["type"] = "string"
                            }
                        }
                    },
                    ["required"] = new JsonArray("name", "path", "azureComputeHost", "language", "port", "dependencies", "settings")
                }
            }
        },
        ["required"] = new JsonArray("workspaceFolder", "services")
    };
}
