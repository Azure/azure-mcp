
# Release History

## 0.10.0-alpha.1 (Unreleased)

### Features Added

### Breaking Changes

### Bugs Fixed

### Other Changes

## 0.9.0 (2025-04-10)

### Features Added

 - [[#4995]](https://github.com/Azure/azure-dev/pull/4995) Add Up Flag for Init

### Other Changes

 - updating npm packages

## 0.8.4 (2024-10-28)

### Other Changes
 - [[#4420]](https://github.com/Azure/azure-dev/pull/4420) Add option to suppress readme
 - [[#3894]](https://github.com/Azure/azure-dev/pull/3894) Add agent skill wrapping for down command

## 0.8.3 (2024-05-07)

### Other Changes
 - [[#3845]](https://github.com/Azure/azure-dev/pull/3845) A small change to the Initialize App command to improve user experience.

## 0.8.2 (2024-04-24)

### Features Added

- [[#3754]](https://github.com/Azure/azure-dev/pull/3754) A small change to the Install, Login, Initialize App, Up, and Pipeline Config commands to make them programmatically accessible.

### Breaking Changes

- [[#3621]](https://github.com/Azure/azure-dev/pull/3621) The Azure Developer CLI is now required to be at version 1.8.0 or higher. If an older version is installed, you will be prompted to update.

## 0.8.1 (2024-03-06)

### Features Added

- [[#3353]](https://github.com/Azure/azure-dev/pull/3353) A small change to the Initialize App command to make it programmatically accessible.

## 0.8.0 (2023-11-15)

### Features Added

- [[#2541]](https://github.com/Azure/azure-dev/pull/2541) Support has been added for the Azure Developer CLI to fetch authentication tokens from VSCode, reducing the need to re-authenticate. Use the setting `azure-dev.auth.useIntegratedAuth` to try this feature.
- [[#2771]](https://github.com/Azure/azure-dev/pull/2771) Commands to enable or disable Dev Center mode have been added. Dev Center mode allows `azd` to leverage Infrastructure as Code (IaC) templates from Dev Center's centrally managed catalogs, manage remote Azure Deployment Environments (ADE) and seamlessly deploy applications to ADE environments using existing `azd deploy` commands.

## 0.7.0 (2023-07-12)

### Features Added

- [[#2396]](https://github.com/Azure/azure-dev/pull/2396) Diagnostics have been added for `azure.yaml` files for when a path referenced as a project does not exist.
- [[#2447]](https://github.com/Azure/azure-dev/pull/2447) An experience has been added to easily rename project paths referenced in `azure.yaml`.
- [[#2448]](https://github.com/Azure/azure-dev/pull/2448) Services can be added to `azure.yaml` by dragging a folder and then holding `Shift` and dropping it into `azure.yaml`.

### Bugs Fixed

- [[#2504]](https://github.com/Azure/azure-dev/pull/2504) Fixed an issue where the "Azure Developer CLI (azd): Initialize App (init)" command would fail on Windows when executed immediately after installing AZD.

## 0.6.0 (2023-05-17)

### Features Added

- [[#2122]](https://github.com/Azure/azure-dev/pull/2122) The appropriate schema for `azure.yaml` has been associated for use by the optional [YAML extension](https://marketplace.visualstudio.com/items?itemName=redhat.vscode-yaml). The YAML extension can offer syntax and schema validation, completions, hover tooltips, and more.

### Other Changes

- [[#2190]](https://github.com/Azure/azure-dev/pull/2190) Command names have been altered to appear more consistent with VS Code conventions. Commands have been grouped into submenus.

## 0.5.0 (2023-04-05)

### Features Added

- [[#1849]](https://github.com/Azure/azure-dev/pull/1849) Support for the `azd package` command has been added for both the entire application and individual services.

### Breaking Changes

- [[#1798]](https://github.com/Azure/azure-dev/pull/1798) Version 0.8.0 or higher of the Azure Developer CLI is now required. If an older version is installed, you will be prompted to update.
- [[#1658]](https://github.com/Azure/azure-dev/pull/1658) Version 1.76.0 or higher of VS Code is now required.

## 0.4.2 (2023-03-15)

### Bugs Fixed

- [[#1735]](https://github.com/Azure/azure-dev/pull/1735) Fixed an issue with the login command not working immediately after install.

## 0.4.1 (2023-03-14)

### Bugs Fixed

- [[#1724]](https://github.com/Azure/azure-dev/pull/1724) Refine conditions for displaying the prompt to install the CLI.

## 0.4.0 (2023-03-08)

### Added

- [[#853]](https://github.com/Azure/azure-dev/pull/853) Integration with the Azure Resources extension's workspace view. Requires version 0.6.1 of the [Azure Resources](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureresourcegroups) extension.
- [[#1644]](https://github.com/Azure/azure-dev/pull/1644) Added a walkthrough experience for using the extension.

## 0.3.0 (2022-09-14)

### Added

- [[#493]](https://github.com/Azure/azure-dev/pull/493) Show README file after successful init/up.

### Fixed

- [[#498]](https://github.com/Azure/azure-dev/pull/498) Use `azd template list` to populate template list in VS Code (now always consistent with the CLI).
- [[#556]](https://github.com/Azure/azure-dev/pull/556) Improve error message when no environments are found.

## 0.2.0 (2022-08-02)

### Changed

- [[#189]](https://github.com/Azure/azure-dev/pull/189) Bump bicep minimum version to v0.8.9

### Added

- [[#151]](https://github.com/Azure/azure-dev/pull/151) Detect and warn the user if `azd` CLI is not installed.

### Fixed

- [[#159]](https://github.com/Azure/azure-dev/pull/159) Enable user feedback via surveys.
- [[#170]](https://github.com/Azure/azure-dev/pull/170) Enable gradual rollout of new features.

## 0.1.0 (2022-07-11)

- Initial release.
