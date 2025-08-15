
# MCP/LLM Tool Organization & Task Reference

This document is the reference for organizing MCP and LLM tools to support a wide variety of Azure code-to-cloud scenarios. It is designed for easy consumption by agents and LLMs, enabling automation, planning, and code generation workflows. Use this guide to plan, build, and evolve granular, reusable tools for Azure migration, IaC generation, CI/CD pipeline creation, and modernization. All tasks and tools are structured for assertive, agentic execution and continuous improvement.

## Shared Goal: Granular, Reusable Tools for Azure App Planning & Code Generation

Work items are broken down into small, focused tools to maximize:

- Reuse across Azure app planning and code generation workflows
- Maintainability and extensibility
- Agentic and CLI-driven automation
- Consistent experience for partners and users
- Smaller tools can be used in isolation or as part of orchestrating larger scenarios

## Scenarios Supported

- Brownfield application migration to Azure with azd
- IaC code generation for application components
- Adding new application components to existing applications
- Generating CI/CD pipelines for azd, az cli; supporting both Github Actions and Azure DevOps
- Generating `azd` compatible projects

All tools and tasks below support these scenarios. Use them to deliver flexible, reusable solutions for Azure app planning and code generation.

## Priority Legend

| Priority | Description                                  |
|:--------:|----------------------------------------------|
| P0       | Required to merge                            |
| P1       | After initial PR                             |
| P2       | Nice to have, after P1 issues are resolved   |

## Command Groups

Update to use the command groups for all the tools within this PR.

|  x  | Priority | Command Group      | Description                                          |
|:---:|:--------:|-------------------|------------------------------------------------------ |
| [ ] | P0       | **quota**         | For all quota and usage tools                         |
| [ ] | P0       | **infrastructure**| For all generic infrastructure tools                  |
| [ ] | P0       | **azd**           | For all `azd` specific tools                          |
| [ ] | P0       | **az**            | For all `az cli` specific tools                       |
| [ ] | P0       | **architecture**  | For all architecture planning and diagramming tools   |
| [ ] | P0       | **pipelines**     | For all generic CI/CD pipeline generation tools       |
| [ ] | P0       | **github**        | For all Github pipeline and integration tools         |
| [ ] | P0       | **azuredevops**   | For all Azure DevOps pipeline and integration tools   |
| [ ] | P0       | **bicep**         | For all Bicep IaC generation and validation tools     |
| [ ] | P0       | **terraform**     | For all Terraform IaC generation and validation tools |

Deprecate the `deploy` command group. The included tools do not perform application deployment. Focus on migration, architecture planning, and validation to support deployment.

## [ ] Logs command

**Goal:** Expose log access via azd CLI for MCP and all azd users.

**Reason:** Enable standardized log retrieval and automation for MCP and all users.

### Tasks

|  x  | Priority | Task                                   | Command Group |
|:---:|:--------:|----------------------------------------|---------------|
| [ ] | P2       | Implement `azd monitor logs` command   | azd           |

### Links

| Tool/Task Name    | Link                                                                                                   |
|:------------------|:--------------------------------------------------------------------------------------------------------|
| LogsGetCommand    | [areas/deploy/src/AzureMcp.Deploy/Commands/App/LogsGetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/App/LogsGetCommand.cs) |

## [ ] Diagramming Command

**Goal:** Redesign as a standalone tool with simple input and Azure resource group support.

**Reason:** Enable use in any scenario, not just plan-based workflows.

### Tasks

|  x  | Priority | Tool                                                   | Command Group  |
|:---:|:--------:|--------------------------------------------------------|----------------|
| [ ] | P0       | Tool to get topology of application                    | architecture   |
| [ ] | P0       | Tool to generate diagram from topology                 | architecture   |
| [ ] | P1       | Tool to generate diagram from existing Azure resources | architecture   |

### Links

| Tool/Task Name                | Link                                                                                                                      |
|:------------------------------|:-------------------------------------------------------------------------------------------------------------------------|
| DiagramGenerateCommand        | [areas/deploy/src/AzureMcp.Deploy/Commands/Architecture/DiagramGenerateCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Architecture/DiagramGenerateCommand.cs) |
| GenerateMermaidChart          | [areas/deploy/src/AzureMcp.Deploy/Commands/Architecture/GenerateMermaidChart.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Architecture/GenerateMermaidChart.cs)   |
| Architecture Diagram Template | [areas/deploy/src/AzureMcp.Deploy/Templates/Architecture/architecture-diagram.md](areas/deploy/src/AzureMcp.Deploy/Templates/Architecture/architecture-diagram.md)   |

## [ ] Pipeline Commands

**Goal:** Deliver standalone pipeline config, agentic support, and azd/az cli split.

**Reason:** Enable flexible, best-practice pipeline generation for all platforms.

### Tasks

|  x  | Priority | Tool                                                           | Command Group  |
|:---:|:--------:|----------------------------------------------------------------|----------------|
| [ ] | P0       | Tool for generic pipeline best practices/rules                 | pipelines      |
| [ ] | P0       | Tool for pipeline best practices/rules for Github              | github         |
| [ ] | P0       | Tool for pipeline best practices/rules for azd                 | azd            |
| [ ] | P1       | Tool for pipeline best practices/rules for Azure DevOps (azdo) | azuredevops    |

### Links

| Tool/Task Name         | Link                                                                                                                      |
|:---------------------- |:-------------------------------------------------------------------------------------------------------------------------|
| GuidanceGetCommand     | [areas/deploy/src/AzureMcp.Deploy/Commands/Pipeline/GuidanceGetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Pipeline/GuidanceGetCommand.cs) |
| Pipeline Templates     | [areas/deploy/src/AzureMcp.Deploy/Templates/Pipeline/](areas/deploy/src/AzureMcp.Deploy/Templates/Pipeline/)                                                   |

## [ ] Application Migration

**Goal:** Enable migration and extension of apps to Azure cloud platform.

**Reason:** Streamline migration, extension, and validation workflows.

### Tasks

|  x  | Priority | Tool                                             | Command Group  |
|:---:|:--------:|--------------------------------------------------|----------------|
| [ ] | P0       | Discover/analyze app components                  | architecture   |
| [ ] | P0       | Generate infrastructure files                    | infrastructure |
| [ ] | P0       | Containerize apps (dockerfiles)                  | architecture   |
| [ ] | P0       | Generate CI/CD pipelines                         | pipelines      |
| [ ] | P0       | Generate architecture artifacts (plan, diagrams) | architecture   |
| [ ] | P0       | Generate azure.yaml                              | azd            |
| [ ] | P0       | Validate app for azd deployment                  | azd            |
| [ ] | P1       | Azure Yaml validation tool                       | azd            |

### Links

| Tool/Task Name | Link                                                                                                   |
|:---------------|:--------------------------------------------------------------------------------------------------------|
| GetCommand     | [areas/deploy/src/AzureMcp.Deploy/Commands/Plan/GetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Plan/GetCommand.cs)                   |
| Plan Templates | [areas/deploy/src/AzureMcp.Deploy/Templates/Plan/](areas/deploy/src/AzureMcp.Deploy/Templates/Plan/)                                           |

## [ ] IaC Code Generation

**Goal:** Standardize and improve IaC code generation and validation.

**Reason:** Enforce best practices and compatibility across platforms.

### Tasks

|  x  | Priority | Tool                                                   | Command Group  |
|:---:|:--------:|--------------------------------------------------------|----------------|
| [ ] | P0       | Leverage existing bicep/terraform best practices tools |                |
| [ ] | P0       | Tool for generic IaC best practices /rules             | infrastructure |
| [ ] | P0       | Tool for bicep best practices / rules                  | bicep          |
| [ ] | P0       | Tool for terraform best practices / rules              | terraform      |
| [ ] | P0       | Tool for required azd rules                            | azd            |
| [ ] | P2       | Tool for required az cli rules                         | az             |

### Links

| Tool/Task Name         | Link                                                                                                   |
|:-----------------------|:--------------------------------------------------------------------------------------------------------|
| RulesGetCommand        | [areas/deploy/src/AzureMcp.Deploy/Commands/Infrastructure/RulesGetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Infrastructure/RulesGetCommand.cs) |
| IaC Rules Templates    | [areas/deploy/src/AzureMcp.Deploy/Templates/IaCRules/](areas/deploy/src/AzureMcp.Deploy/Templates/IaCRules/)                                 |

## [ ] Quota Commands

**Goal:** Refactor usage checker implementations to a single shared implementation across resource types.

**Reason:** Eliminate duplication and improve maintainability.

### Tasks

|  x  | Priority | Task                                                                                           | Command Group |
|:---:|:--------:|------------------------------------------------------------------------------------------------|---------------|
| [ ] | P2       | Refactor usage checker implementations to a single shared implementation across resource types | quota         |

### Examples

- [MachineLearningUsageChecker.cs](areas/quota/src/AzureMcp.Quota/Services/Util/Usage/MachineLearningUsageChecker.cs)
- [ComputeUsageChecker.cs](areas/quota/src/AzureMcp.Quota/Services/Util/Usage/ComputeUsageChecker.cs)

> Note: There are many more very similar examples in the codebase
