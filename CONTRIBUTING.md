# Contributing to Azure MCP

Thank you for your interest in contributing to Azure MCP! This document provides guidelines and steps for contributing to this project.

## Getting Started

### Prerequisites

1. Install either the stable or Insiders release of VS Code.
   * [Stable release](https://code.visualstudio.com/download)
   * [Insiders release](https://code.visualstudio.com/insiders)
2. Install [GitHub Copilot](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot) and [GitHub Copilot Chat](https://marketplace.visualstudio.com/items?itemName=GitHub.copilot-chat) extensions.

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

5. Create a Pull Request:
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
