# Contributing to Azure MCP

There are many ways to contribute to the Azure MCP project: reporting bugs, submitting pull requests, and creating suggestions.
After cloning and building the repo, check out the [github project](https://github.com/orgs/Azure/projects/812/views/13) and [issues list](https://github.com/Azure/azure-mcp/issues).  Issues labeled [help wanted](https://github.com/Azure/azure-mcp/labels/help%20wanted) are good issues to submit a PR for.  Issues labeled [good first issue](https://github.com/Azure/azure-mcp/labels/good%20first%20issue) are great candidates to pick up if you are in the code for the first time.
>[!IMPORTANT]
If you are contributing significant changes, or if the issue is already assigned to a specific milestone, please discuss with the assignee of the issue first before starting to work on the issue.

## Prerequisites

1. Install either the stable or Insiders release of VS Code.
   * [Stable release](https://code.visualstudio.com/download)
   * [Insiders release](https://code.visualstudio.com/insiders)
2. Install [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) and [GitHub Copilot Chat](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat) extensions.
3. Install [Node.js](https://nodejs.org/en/download) 20 or later
   * Ensure `node` and `npm` are in your path

### Project Structure

The project is organized as follows:
- `src/` - Main source code
  - `Arguments/` - Command argument definitions
  - `Commands/` - Command implementations
  - `Models/` - Data models and interfaces
  - `Services/` - Service implementations
- `tests/` - Test files
- `docs/` - Documentation

### Adding a New Command

1. Create a new issue in this repository with:
   - Title: "Add command: azmcp [service] [resource] [operation]"
   - Description: Detailed explanation of what the command will do

2. Set up your development environment:
   - Open VS Code Insiders
   - Open the Copilot Chat view
   - Select "Agent" mode

3. Use Copilot to generate the command:
   ```
   Execute in Copilot Chat:
   "create [service] [resource] [operation] command using #new-command.md as a reference"
   ```

4. Follow the implementation guidelines in [src/Docs/new-command.md](https://github.com/Azure/azure-mcp/blob/main/src/Docs/new-command.md)

5. Add documentation for new command:
   - [azmcp-commands.md](./docs/azmcp-commands.md)
   - [README.md](./README.md)

6. Create a Pull Request:
   - Reference the issue you created
   - Include tests in the `/tests` folder
   - Ensure all tests pass
   - Follow the code style requirements

## Development Process

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Write or update tests
5. Submit a pull request

## Testing

All commands must have corresponding tests in the `/tests` folder. To run tests:

```pwsh
# Run tests with coverage
./eng/scripts/Test-Code.ps1
```

Test requirements:
- Each command should have unit tests
- Tests should cover success and error scenarios
- Mock external service calls
- Test argument validation

### Live Tests

Before running live tests, install Azure Powershell, login, then run:
```pwsh
./eng/common/TestResources/New-TestResources.ps1 `
  -SubscriptionId YourSubscriptionId `
  -ResourceGroupName YourResourceGroupName `
  -Verbose `
  -Force
```

Omitting the `-ResourceGroupName` parameter will default the name to a hash of your username. Omitting the `-SubscriptionId` parameter will deploy to your default subscription. Internal users omitting the `-SubscriptionId` parameter will deploy to the playground subscription.

After deploying test resources, you should have a `.testsettings.json` file with your deployment information in the root of the repo.  You can run live tests with:
```pwsh
./eng/scripts/Test-Code.ps1 -Live
```

## Installing Git Hooks

To ensure consistent code quality, the pre-commit hook is automatically installed before every build. This hook will ensure that your code adheres to the required standards.

### Automatic Setup

The pre-push hook is installed automatically when you build the project. You do not need to run any additional commands. The hook is located in the `eng/scripts/git-hooks/` directory and is copied to the `.git/hooks/` folder during the build process.

Powershell is required for hook installation. For installation instructions, see [Installing PowerShell](https://learn.microsoft.com/en-us/powershell/scripting/install/installing-powershell).

## Code Style

Run `dotnet format src/AzureMcp.sln` before submitting.

- Follow C# coding conventions
- No comments in implementation code (code should be self-documenting)
- Use descriptive variable and method names
- Follow the exact file structure and naming conventions
- Use proper error handling patterns
- XML documentation for public APIs
- Follow Model Context Protocol (MCP) patterns

## Model Context Protocol (MCP)

The Azure MCP Server implements the [Model Context Protocol specification](https://modelcontextprotocol.io). When adding new commands:

- Follow MCP JSON schema patterns
- Implement proper context handling
- Use standardized response formats
- Handle errors according to MCP specifications
- Provide proper argument suggestions

## Pull Request Process

1. Update documentation reflecting any changes
2. Add or update tests as needed
3. Reference the original issue
4. Wait for review and address any feedback

## Builds and releases (Internal)

The internal pipeline [azure-mcp](https://dev.azure.com/azure-sdk/internal/_build?definitionId=7571) is used for all official
releases and CI builds. On every merge to main, a build will run and will produce a dynamically named prerelease
package on the public dev feed, e.g. [@azure/mcp@0.0.10-beta.4799791](https://dev.azure.com/azure-sdk/public/_artifacts/feed/azure-sdk-for-js/Npm/@azure%2Fmcp/overview/0.0.10-beta.4799791).

Only manual runs of the pipeline sign and publish packages.  Building `main` or `hotfix/*` will publish to `npmjs.com`, all other refs will publish to the [public dev feed](https://dev.azure.com/azure-sdk/public/_artifacts/feed/azure-sdk-for-js).

### PR Validation

To run live tests for a PR, inspect the PR code for any suspicious changes, then add the comment `/azp run azure - mcp` to the pull request.  This will queue a PR triggered run which will build, run unit tests, deploy test resources and run live tests.

If you would like to see the product of a PR as a package on the dev feed, after thoroughly inspecting the change, create a branch in the main repo and manually trigger an [azure - mcp](https://dev.azure.com/azure-sdk/internal/_build?definitionId=7571) pipeline run against that branch. This will queue a manually triggered run which will build, run unit tests, deploy test resources, run live tests, sign and publish the packages to the dev feed.

Instructions for consuming the package from the dev feed can be found in the "Extensions" tab of the pipeline run page.

## Questions and Support

- Create an issue for questions
- Tag @jongio for specific help or clarification
- Join our community discussions

## Additional Resources

- [Azure MCP Documentation](https://github.com/Azure/azure-mcp/blob/main/README.md)
- [Command Implementation Guide](https://github.com/Azure/azure-mcp/blob/main/src/Docs/new-command.md)
- [VS Code Insiders Download](https://code.visualstudio.com/insiders/)
- [GitHub Copilot Documentation](https://docs.github.com/en/copilot)

## Code of Conduct

This project adheres to a standard code of conduct. By participating, you are expected to uphold this code.

## License

By contributing, you agree that your contributions will be licensed under the project's license.
