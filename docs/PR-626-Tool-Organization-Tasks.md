
# MCP/LLM Tool Organization & Task Reference

This document is the authoritative reference for organizing MCP and LLM tools to support a wide variety of Azure code-to-cloud scenarios. It is designed for easy consumption by agents and LLMs, enabling automation, planning, and code generation workflows. Use this guide to plan, build, and evolve granular, reusable tools for Azure migration, IaC generation, CI/CD pipeline creation, and modernization. All tasks and tools are structured for assertive, agentic execution and continuous improvement.

## Shared Goal: Granular, Reusable Tools for Azure App Planning & Code Generation

Work items are broken down into small, focused tools to maximize:
- Reuse across Azure app planning and code generation workflows
- Maintainability and extensibility
- Agentic and CLI-driven automation
- Consistent experience for partners and users

## Scenarios Supported

- Brownfield application migration to Azure with azd
- IaC code generation for application components
- Adding new application components to existing applications
- Generating CI/CD pipelines for azd, az cli; supporting both Github Actions and Azure DevOps
- Generating `azd` compatible projects

All tools and tasks below support these scenarios. Use them to deliver flexible, reusable solutions for Azure app planning and code generation.

## Priority Legend

| Priority | Description                                  |
|----------|----------------------------------------------|
| P0       | Required to merge                            |
| P1       | After initial PR                             |
| P2       | Nice to have, after P1 issues are resolved   |

## Command Groups

Use the following command groups for all tools in this PR.

| Command Group      | Description                                       |
|-------------------|---------------------------------------------------|
| **quota**         | For all quota and usage tools                     |
| **infrastructure**| For all generic infrastructure tools              |
| **azd**           | For all `azd` specific tools                      |
| **az**            | For all `az cli` specific tools                   |
| **architecture**  | For all architecture planning and diagramming tools|
| **pipelines**     | For all generic CI/CD pipeline generation tools   |
| **github**        | For all Github pipeline and integration tools     |
| **azuredevops**   | For all Azure DevOps pipeline and integration tools|
| **bicep**         | For all Bicep IaC generation and validation tools |
| **terraform**     | For all Terraform IaC generation and validation tools|

Deprecate the `deploy` command group. The included tools do not perform application deployment. Focus on migration, architecture planning, and validation to support deployment.

## [ ] Logs command

**Goal:** Expose log access via azd CLI for MCP and all azd users.

**Reason:** Enable standardized log retrieval and automation for MCP and all users.

### Tasks

|   | Priority | Task                                   | Command Group |
|---|----------|----------------------------------------|--------------|
| [ ] | P2       | Implement `azd monitor logs` command  | azd          |

### Links

| Tool/Task Name    | Link                                                                                                   |
|:------------------|:--------------------------------------------------------------------------------------------------------|
| LogsGetCommand    | [areas/deploy/src/AzureMcp.Deploy/Commands/App/LogsGetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/App/LogsGetCommand.cs) |

## [ ] Diagramming Command

**Goal:** Redesign as a standalone tool with simple input and Azure resource group support.

**Reason:** Enable use in any scenario, not just plan-based workflows.

### Tasks

|   | Priority | Tool                                         | Command Group   |
|---|----------|----------------------------------------------|----------------|
| [ ] | P1       | Tool to get topology of application         | architecture   |
| [ ] | P1       | Tool to generate diagram from topology      | architecture   |
| [ ] | P1       | Tool to generate diagram from existing Azure resources | architecture |

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

|   | Priority | Tool                                             | Command Group   |
|---|----------|--------------------------------------------------|----------------|
| [ ] | P1       | Tool for generic pipeline best practices/rules   | pipelines      |
| [ ] | P1       | Tool for pipeline best practices/rules for Github| github         |
| [ ] | P1       | Tool for pipeline best practices/rules for Azure DevOps (azdo) | azuredevops |
| [ ] | P1       | Tool for pipeline best practices/rules for azd   | azd            |

### Links

| Tool/Task Name         | Link                                                                                                                      |
|:---------------------- |:-------------------------------------------------------------------------------------------------------------------------|
| GuidanceGetCommand     | [areas/deploy/src/AzureMcp.Deploy/Commands/Pipeline/GuidanceGetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Pipeline/GuidanceGetCommand.cs) |
| Pipeline Templates     | [areas/deploy/src/AzureMcp.Deploy/Templates/Pipeline/](areas/deploy/src/AzureMcp.Deploy/Templates/Pipeline/)                                                   |

## [ ] Application Migration

**Goal:** Enable migration and extension of apps to Azure cloud platform.

**Reason:** Streamline migration, extension, and validation workflows.

### Tasks

|   | Priority | Tool                                             | Command Group   |
|---|----------|--------------------------------------------------|----------------|
| [ ] | P1       | Discover/analyze app components                | architecture   |
| [ ] | P1       | Generate infrastructure files                  | infrastructure |
| [ ] | P1       | Containerize apps (dockerfiles)                | architecture   |
| [ ] | P1       | Generate azure.yaml                            | azd            |
| [ ] | P1       | Generate CI/CD pipelines                       | pipelines      |
| [ ] | P1       | Generate architecture artifacts (plan, diagrams)| architecture   |
| [ ] | P1       | Validate app for deployment                    | azd            |
| [ ] | P1       | azure_yaml_schema_validation tool              | azd            |

### Links

| Tool/Task Name | Link                                                                                                   |
|:---------------|:--------------------------------------------------------------------------------------------------------|
| GetCommand     | [areas/deploy/src/AzureMcp.Deploy/Commands/Plan/GetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Plan/GetCommand.cs)                   |
| Plan Templates | [areas/deploy/src/AzureMcp.Deploy/Templates/Plan/](areas/deploy/src/AzureMcp.Deploy/Templates/Plan/)                                           |

## [ ] IaC Code Generation

**Goal:** Standardize and improve IaC code generation and validation.

**Reason:** Enforce best practices and compatibility across platforms.

### Tasks

|   | Priority | Tool                                             | Command Group   |
|---|----------|--------------------------------------------------|----------------|
| [ ] | P0       | Leverage existing bicep/terraform best practices tools |                |
| [ ] | P1       | Tool for generic IaC rules                          | infrastructure |
| [ ] | P1       | Tool for bicep rules                                 | bicep          |
| [ ] | P1       | Tool for terraform rules                             | terraform      |
| [ ] | P1       | Tool for required azd rules                          | azd            |
| [ ] | P2       | Tool for required az cli rules                       | az             |

### Links

| Tool/Task Name         | Link                                                                                                   |
|:-----------------------|:--------------------------------------------------------------------------------------------------------|
| RulesGetCommand        | [areas/deploy/src/AzureMcp.Deploy/Commands/Infrastructure/RulesGetCommand.cs](areas/deploy/src/AzureMcp.Deploy/Commands/Infrastructure/RulesGetCommand.cs) |
| IaC Rules Templates    | [areas/deploy/src/AzureMcp.Deploy/Templates/IaCRules/](areas/deploy/src/AzureMcp.Deploy/Templates/IaCRules/)                                 |

## [ ] Quota Commands

**Goal:** Refactor usage checker implementations to a single shared implementation across resource types.

**Reason:** Eliminate duplication and improve maintainability.

### Tasks

|   | Priority | Task                                                      | Command Group |
|---|----------|-----------------------------------------------------------|--------------|
| [ ] | P2       | Refactor usage checker implementations to a single shared implementation across resource types | quota        |

### Examples

- [MachineLearningUsageChecker.cs](areas/quota/src/AzureMcp.Quota/Services/Util/Usage/MachineLearningUsageChecker.cs)
- [ComputeUsageChecker.cs](areas/quota/src/AzureMcp.Quota/Services/Util/Usage/ComputeUsageChecker.cs)

> Note: There are many more very similar examples in the codebase
