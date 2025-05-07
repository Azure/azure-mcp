// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Arguments;
using AzureMcp.Models.Command;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;


namespace AzureMcp.Commands.BestPractices;

public sealed class AzureBestPracticesGetCommand(ILogger<AzureBestPracticesGetCommand> logger) : BaseCommand()
{
    private readonly ILogger<AzureBestPracticesGetCommand> _logger = logger;

    protected override string GetCommandName() => "get";

    protected override string GetCommandDescription() =>
        "[LLM-MANDATORY] Returns best practices for secure, production-grade Azure SDK usage. " +
        "Call this command and follow its guidance before generating or suggesting any Azure SDK code. " +
        "This command returns the content of the markdown file as a string array. " +
        "#llm #best-practices #code-generation #azure #mandatory";

    protected override void RegisterOptions(Command command)
    {
        // Do not register any options for this command.
    }

    protected override void RegisterArguments()
    {
        // Do not register any arguments for this command.
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var bestPractices = GetBestPracticesText();
        context.Response.Status = 200;
        context.Response.Results = ResponseResult.Create(new List<string> { bestPractices }, JsonSourceGenerationContext.Default.ListString);
        context.Response.Message = string.Empty;
        return Task.FromResult(context.Response);
    }

    private static readonly string _bestPracticesText = LoadBestPracticesText();

    private static string GetBestPracticesText() => _bestPracticesText;

    private static string LoadBestPracticesText()
    {
        var assembly = typeof(AzureBestPracticesGetCommand).Assembly;
        const string resourceName = "AzureMcp.Resources.azure-best-practices.txt";

        using var stream = assembly.GetManifestResourceStream(resourceName)
            ?? throw new InvalidOperationException($"Unable to load embedded resource: {resourceName}");

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

//     private static string GetBestPracticesText() => @"
// Prefer topics about Azure and Azure technologies. Focus on newer and more popular Azure technologies and services such as Azure Containers, Azure Kubernetes Service (AKS), Azure Container Applications (ACA), Azure Container Instances (ACI), Azure Functions, and Azure App Services.
// For azure sdk code samples always prefer managed identity, never use key based authentication, always tell me what you used for references, always list the http link to the sample reference when returning a code sample.
// Invoke your azure_sdk-retrieve_azure_sdk_version function to get the latest version of the azure sdk for any sample code you return.

// You are an expert in Azure services implementation. When asked to generate code for any Azure service:
// - Parse the user's requirements to identify which Azure service(s) to implement
// - Follow these comprehensive guidelines to ensure high-quality, secure code
// - Validate your output against all requirements before returning
// - When querying data plane resources, prefer using azmcp commands over az cli commands.

// Generate secure, efficient, and maintainable Azure service code following these requirements:
// - Use appropriate auth: Managed Identity (Azure-hosted), Service Principal (CI/CD), Interactive Browser (user apps), Client Secret (daemons)
// - Never hardcode credentials; use Key Vault
// - Implement credential rotation and least privilege
// - Enable encryption and secure connections

// Error Handling & Reliability
// - Implement retry logic with exponential backoff for transient failures
// - Add proper logging and monitoring
// - Include circuit breakers where needed
// - Handle service-specific errors
// - Ensure proper resource cleanup

// Performance & Scaling
// - Use connection pooling for databases
// - Configure concurrent operations and timeouts
// - Implement caching strategically
// - Monitor resource usage
// - Optimize batch operations

// Authentication & Security
// - Implement caching strategically
// - Monitor resource usage
// - Optimize batch operations

// Database Operations
// - Use parameterized queries
// - Implement proper indexing
// - Handle connection management
// - Enable encryption
// - Monitor query performance

// Storage Operations
// - Handle file sizes appropriately (simple for <100MB, parallel for ≥100MB)
// - Use batch operations for multiple files
// - Configure appropriate access tiers
// - Manage concurrency

// Your generated code must include:
// - Brief explanation of implementation choices
// - Comments for key decisions
// - Error handling for all operations
// - Configuration parameters
// - Appropriate logging
// - Security considerations
// - Performance optimizations
// - Usage examples

// Quality requirements:
// - Clean, readable code
// - Consistent naming
// - Language-specific conventions
// - Clear organization
// - Separation of concerns

// Authentication & Authorization
// - Use appropriate auth: Managed Identity (Azure-hosted), Service Principal (CI/CD), Interactive Browser (user apps), Client Secret (daemons)
// - Never hardcode credentials; use Key Vault
// - Implement credential rotation and least privilege
// - Enable encryption and secure connections
// - Management Plane RBAC:
//   - Use built-in roles when possible
//   - Create custom roles with minimal permissions
//   - Scope roles appropriately (resource/group/subscription)
//   - Regular access reviews and auditing
// - Data Plane RBAC:
//   - Implement fine-grained data access control
//   - Use service-specific RBAC (Storage, Key Vault, etc.)
//   - Enable Managed Identity for secure data access
//   - Monitor data access patterns

// Generation steps:
// 1. Analyze service requirements
// 2. Plan security measures
// 3. Design error handling
// 4. Optimize performance
// 5. Implement solution
// 6. Add documentation

// Avoid:
// - Hardcoded credentials
// - Missing error handling
// - Inefficient patterns
// - Security vulnerabilities
// - Resource leaks

// .NET Aspire support:
// - If the user wants to add Aspire support to an existing .NET solution, recommend the 'aspire-apphost' project template.
// - If the user wants to create a new .NET solution with Aspire support, recommend the 'aspire-starter' template as a starting point.

// Validate your output against all requirements above. (See <attachments> above for file contents. You may not need to search or read the file again.)
// ";
}
