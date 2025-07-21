<!-- Copyright (c) Microsoft Corporation.
<!-- Licensed under the MIT License. -->

# Implementing a New Command in Azure MCP

This document provides a comprehensive guide for implementing commands in Azure MCP following established patterns.

## Area Pattern: Organizing Service Code

All new services and their commands should use the Area pattern:

- **Service code** goes in `src/Areas/{ServiceName}` (e.g., `src/Areas/Storage`)
- **Tests** go in `tests/Areas/{ServiceName}`

This keeps all code, options, models, and tests for a service together. See `src/Areas/Storage` for a reference implementation.

## Command Architecture

### Command Design Principles

1. **Command Interface**
   - `IBaseCommand` serves as the root interface with core command capabilities:
     - `Name`: Command name for CLI display
     - `Description`: Detailed command description
     - `Title`: Human-readable command title
     - `GetCommand()`: Retrieves System.CommandLine command definition
     - `ExecuteAsync()`: Executes command logic
     - `Validate()`: Validates command inputs

2. **Command Hierarchy**
   All commands must implement the hierarchy pattern:
     ```
     IBaseCommand
     └── BaseCommand
         └── GlobalCommand<TOptions>
             └── SubscriptionCommand<TOptions>
                 └── Service-specific base commands (e.g., BaseSqlCommand)
                     └── Resource-specific commands (e.g., SqlIndexRecommendCommand)
     ```

   IMPORTANT:
   - Commands use primary constructors with ILogger injection
   - Classes are always sealed unless explicitly intended for inheritance
   - Commands inheriting from SubscriptionCommand must handle subscription parameters
   - Service-specific base commands should add service-wide options
   - Commands are marked with [McpServerTool] attribute to define their characteristics

3. **Command Pattern**
   Commands follow the Model-Context-Protocol (MCP) pattern with this naming convention:
   ```
   azmcp <azure service> <resource> <operation>
   ```
   Example: `azmcp storage container list`

   Where:
   - `azure service`: Azure service name (lowercase, e.g., storage, cosmos, kusto)
   - `resource`: Resource type (singular noun, lowercase)
   - `operation`: Action to perform (verb, lowercase)

   Each command is:
   - In code, to avoid ambiguity between service classes and Azure services, we refer to Azure services as Areas
   - Registered in the RegisterCommands method of its service's Areas/{Area}/{Area}Setup.cs file
   - Organized in a hierarchy of command groups
   - Documented with a title, description and examples
   - Validated before execution
   - Returns a standardized response format

   **IMPORTANT**: Command group names cannot contain underscores. Use camelCase or concatenated names or dash separator instead:
   - ✅ Good: `new CommandGroup("entraadmin", "Entra admin operations")`
   - ✅ Good: `new CommandGroup("resourcegroup", "Resource group operations")`
   - ✅ Good:`new CommandGroup("entra-admin", "Entra admin operations")`
   - ❌ Bad: `new CommandGroup("entra_admin", "Entra admin operations")`

### Required Files

A complete command requires:

1. OptionDefinitions static class: `src/Areas/{Area}/Options/{Area}OptionDefinitions.cs`
2. Options class: `src/Areas/{Area}/Options/{Resource}/{Operation}Options.cs`
3. Command class: `src/Areas/{Area}/Commands/{Resource}/{Resource}{Operation}Command.cs`
4. Service interface: `src/Areas/{Area}/Services/I{Area}Service.cs`
5. Service implementation: `src/Areas/{Area}/Services/{Area}Service.cs`
   - It's common for an area to have a single service class named after the
     area but some areas will have multiple service classes
6. Unit test: `tests/Areas/{Area}/UnitTests/{Resource}/{Resource}{Operation}CommandTests.cs`
7. Integration test: `tests/Areas/{Area}/LiveTests/{Area}CommandTests.cs`
8. Command registration in RegisterCommands(): `src/Areas/{Area}/{Area}Setup.cs`
9. Area registration in RegisterAreas(): `src/Program.cs`
10. **Live test infrastructure** (if needed):
   - Bicep template: `/infra/services/{service}.bicep`
   - Module registration in: `/infra/test-resources.bicep`
   - Optional post-deployment script: `/infra/services/{service}-post.ps1`

**IMPORTANT**: If implementing a new area, you must also ensure:
- The Azure Resource Manager package is added to `Directory.Packages.props` first
- The package reference is added to `src/AzureMcp.csproj`
- Models, base commands, and option definitions follow the established patterns
- JSON serialization context includes all new model types
- Service registration in the area setup ConfigureServices method
- **Live test infrastructure**: Add Bicep template to `/infra/services/` and module to `/infra/test-resources.bicep`
- **Test resource deployment**: Ensure resources are properly configured with RBAC for test application
- **Resource naming**: Follow consistent naming patterns - many services use just `baseName`, while others may need suffixes for disambiguation (e.g., `{baseName}-suffix`)

## Implementation Guidelines

### 1. Azure Resource Manager Integration

When creating commands that interact with Azure services, you'll need to:

**Package Management:**
- Add the appropriate Azure Resource Manager package to both `Directory.Packages.props` and `AzureMcp.csproj`
- Example: `<PackageVersion Include="Azure.ResourceManager.Sql" Version="1.3.0" />`

**Subscription Resolution:**
- Always use `ISubscriptionService.GetSubscription()` to resolve subscription ID or name
- Inject `ISubscriptionService` into your service constructor
- This handles both subscription IDs and subscription names automatically
- Example pattern:
```csharp
public class MyService(ISubscriptionService subscriptionService, ITenantService tenantService) 
    : BaseAzureService(tenantService), IMyService
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService;
    
    public async Task<MyResource> GetResourceAsync(string subscription, ...)
    {
        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
        // Use subscriptionResource instead of creating one manually
    }
}
```

**API Pattern Discovery:**
- Study existing services (e.g., Postgres, Redis) to understand resource access patterns
- Use resource collections correctly: `.GetSqlServers().GetAsync(serverName)` not `.GetSqlServerAsync(serverName, cancellationToken)`
- Check Azure SDK documentation for correct method signatures and property names

**Common Azure Resource Manager Patterns:**
```csharp
// Correct pattern for subscription resolution and resource access
var subscriptionResource = await _subscriptionService.GetSubscription(subscription, tenant, retryPolicy);

var resourceGroupResource = await subscriptionResource
    .GetResourceGroupAsync(resourceGroup, cancellationToken);

var sqlServerResource = await resourceGroupResource.Value
    .GetSqlServers()
    .GetAsync(serverName);

var databaseResource = await sqlServerResource.Value
    .GetSqlDatabases()
    .GetAsync(databaseName);
```

**Property Access Issues:**
- Azure SDK property names may differ from expected names (e.g., `CreatedOn` not `CreationDate`)
- Check actual property availability using IntelliSense or SDK documentation
- Some properties are objects that need `.ToString()` conversion (e.g., `Location.ToString()`)
- Be aware of nullable properties and use appropriate null checks

**Compilation Error Resolution:**
- When you see `cannot convert from 'System.Threading.CancellationToken' to 'string'`, check method parameter order
- For `'SqlDatabaseData' does not contain a definition for 'X'`, verify property names in the actual SDK types
- Use existing service implementations as reference for correct property access patterns

### 2. Options Class

```csharp
public class {Resource}{Operation}Options : Base{Area}Options
{
    // Only add properties not in base class
    public string? NewOption { get; set; }
}
```

IMPORTANT:
- Inherit from appropriate base class (Base{Area}Options, GlobalOptions, etc.)
- Never redefine properties from base classes
- Make properties nullable if not required
- Use consistent parameter names across services:
  - **CRITICAL**: Always use `subscription` (never `subscriptionId`) for subscription parameters - this allows the parameter to accept both subscription IDs and subscription names, which are resolved internally by `ISubscriptionService.GetSubscription()`
  - Use `resourceGroup` instead of `resourceGroupName`
  - Use singular nouns for resource names (e.g., `server` not `serverName`)
  - Keep parameter names consistent with Azure SDK parameters when possible
  - If services share similar operations (e.g., ListDatabases), use the same parameter order and names

### 2. Command Class

```csharp
public sealed class {Resource}{Operation}Command(ILogger<{Resource}{Operation}Command> logger)
    : Base{Area}Command<{Resource}{Operation}Options>
{
    private const string CommandTitle = "Human Readable Title";
    private readonly ILogger<{Resource}{Operation}Command> _logger = logger;

    // Define options from OptionDefinitions
    private readonly Option<string> _newOption = {Area}OptionDefinitions.NewOption;

    public override string Name => "operation";

    public override string Description =>
        """
        Detailed description of what the command does.
        Returns description of return format.
          Required options:
        - list required options
        """;

    public override string Title => CommandTitle;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_newOption);
    }

    protected override {Resource}{Operation}Options BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.NewOption = parseResult.GetValueForOption(_newOption);
        return options;
    }

    [McpServerTool(
        Destructive = false,     // Set to true for commands that modify resources
        ReadOnly = true,        // Set to false for commands that modify resources
        Title = CommandTitle)]  // Display name shown in UI
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var options = BindOptions(parseResult);

        try
        {
            // Required validation step
            if (!Validate(parseResult.CommandResult, context.Response).IsValid)
            {
                return context.Response;
            }

            context.Activity?.WithSubscriptionTag(options); 

            // Get the appropriate service from DI
            var service = context.GetService<I{Area}Service>();

            // Call service operation(s) with required parameters
            var results = await service.{Operation}(
                options.RequiredParam!,  // Required parameters end with !
                options.OptionalParam,   // Optional parameters are nullable
                options.Subscription!,   // From SubscriptionCommand
                options.RetryPolicy);    // From GlobalCommand

            // Set results if any were returned
            context.Response.Results = results?.Count > 0 ?
                ResponseResult.Create(
                    new {Operation}CommandResult(results),
                    {Area}JsonContext.Default.{Operation}CommandResult) :
                null;
        }
        catch (Exception ex)
        {
            // Log error with all relevant context
            _logger.LogError(ex,
                "Error in {Operation}. Required: {Required}, Optional: {Optional}, Options: {@Options}",
                Name, options.RequiredParam, options.OptionalParam, options);
            HandleException(context, ex);
        }

        return context.Response;
    }

    // Implementation-specific error handling
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx when reqEx.Status == 404 =>
            "Resource not found. Verify the resource exists and you have access.",
        Azure.RequestFailedException reqEx when reqEx.Status == 403 =>
            $"Authorization failed accessing the resource. Details: {reqEx.Message}",
        Azure.RequestFailedException reqEx => reqEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        Azure.RequestFailedException reqEx => reqEx.Status,
        _ => base.GetStatusCode(ex)
    };

    // Strongly-typed result records
    internal record {Resource}{Operation}CommandResult(List<ResultType> Results);
}

### 3. Base Service Command Classes

Each service has its own hierarchy of base command classes that inherit from `GlobalCommand` or `SubscriptionCommand`. Services that work with Azure resources should inject `ISubscriptionService` for subscription resolution. For example:

```csharp
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Areas.{Area}.Options;
using AzureMcp.Commands;

namespace AzureMcp.Areas.{Area}.Commands;

// Base command for all service commands (if no members needed, use concise syntax)
public abstract class Base{Area}Command<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : Base{Area}Options, new();

// Base command for all service commands (if members are needed, use full syntax)
public abstract class Base{Area}Command<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : Base{Area}Options, new()
{
    protected readonly Option<string> _commonOption = {Area}OptionDefinitions.CommonOption;
    protected readonly Option<string> _resourceGroupOption = OptionDefinitions.Common.ResourceGroup;
    protected virtual bool RequiresResourceGroup => true;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_commonOption);

        // Add resource group option if required
        if (RequiresResourceGroup)
        {
            command.AddOption(_resourceGroupOption);
        }
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.CommonOption = parseResult.GetValueForOption(_commonOption);

        if (RequiresResourceGroup)
        {
            options.ResourceGroup = parseResult.GetValueForOption(_resourceGroupOption);
        }

        return options;
    }
}

// Service implementation example with subscription resolution
public class {Area}Service(ISubscriptionService subscriptionService, ITenantService tenantService) 
    : BaseAzureService(tenantService), I{Area}Service
{
    private readonly ISubscriptionService _subscriptionService = subscriptionService ?? throw new ArgumentNullException(nameof(subscriptionService));

    public async Task<{Resource}> GetResourceAsync(string subscription, string resourceGroup, string resourceName, RetryPolicyOptions? retryPolicy)
    {
        // Always use subscription service for resolution
        var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
        
        var resourceGroupResource = await subscriptionResource
            .GetResourceGroupAsync(resourceGroup, cancellationToken);
        // Continue with resource access...
    }
}
```

### 4. Unit Tests

Unit tests follow a standardized pattern that tests initialization, validation, and execution:

```csharp
public class {Resource}{Operation}CommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly I{Area}Service _service;
    private readonly ILogger<{Resource}{Operation}Command> _logger;
    private readonly {Resource}{Operation}Command _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public {Resource}{Operation}CommandTests()
    {
        _service = Substitute.For<I{Area}Service>();
        _logger = Substitute.For<ILogger<{Resource}{Operation}Command>>();

        var collection = new ServiceCollection().AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("operation", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Theory]
    [InlineData("--required value", true)]
    [InlineData("--optional-param value --required value", true)]
    [InlineData("", false)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _service.{Operation}(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
                .Returns(new List<ResultType>());
        }

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _service.{Operation}(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<List<ResultType>>(new Exception("Test error")));

        var parseResult = _parser.Parse(["--required", "value"]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }
}
```

### 5. Integration Tests

Integration tests inherit from `CommandTestsBase` and use test fixtures:

```csharp
[Trait("Area", "{Area}")]
[Trait("Category", "Live")]
public class {Area}CommandTests : CommandTestsBase, IClassFixture<LiveTestFixture>
{
    protected const string TenantNameReason = "Service principals cannot use TenantName for lookup";
    protected LiveTestSettings Settings { get; }
    protected StringBuilder FailureOutput { get; } = new();
    protected ITestOutputHelper Output { get; }
    protected IMcpClient Client { get; }

    public {Area}CommandTests(LiveTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
        Client = fixture.Client;
        Settings = fixture.Settings;
        Output = output;
    }

    [Theory]
    [InlineData(AuthMethod.Credential)]
    [InlineData(AuthMethod.Key)]
    public async Task Should_{Operation}_{Resource}_WithAuth(AuthMethod authMethod)
    {
        // Arrange
        var result = await CallToolAsync(
            "azmcp_{area}_{resource}_{operation}",
            new()
            {
                { "subscription", Settings.Subscription },
                { "resource-group", Settings.ResourceGroup },
                { "auth-method", authMethod.ToString().ToLowerInvariant() }
            });

        // Assert
        var items = result.AssertProperty("items");
        Assert.Equal(JsonValueKind.Array, items.ValueKind);

        // Check results format
        foreach (var item in items.EnumerateArray())
        {
            Assert.True(item.TryGetProperty("name", out _));
            Assert.True(item.TryGetProperty("type", out _));
        }
    }

    [Theory]
    [InlineData("--invalid-param")]
    [InlineData("--subscription invalidSub")]
    public async Task Should_Return400_WithInvalidInput(string args)
    {
        var result = await CallToolAsync(
            $"azmcp_{area}_{resource}_{operation} {args}");

        Assert.Equal(400, result.GetProperty("status").GetInt32());
        Assert.Contains("required",
            result.GetProperty("message").GetString()!.ToLower());
    }
}
```

### 6. Command Registration

```csharp
private void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
{
    var service = new CommandGroup(
        "{area}",
        "{Area} operations");
    rootGroup.AddSubGroup(service);

    var resource = new CommandGroup(
        "{resource}",
        "{Resource} operations");
    service.AddSubGroup(resource);

    resource.AddCommand("operation", new {Area}.{Resource}{Operation}Command(
        loggerFactory.CreateLogger<{Resource}{Operation}Command>()));
}
```

**IMPORTANT**: Command group names cannot contain underscores. Use camelCase or concatenated names or dash separator instead:
- ✅ Good: `"entraadmin"`, `"resourcegroup"`, `"storageaccount"`, `"entra-admin"`
- ❌ Bad: `"entra_admin"`, `"resource_group"`, `"storage_account"`

### 7. Area registration
```csharp
    private static IAreaSetup[] RegisterAreas()
    {
        return [
            new AzureMcp.Areas.AppConfig.AppConfigSetup(),
            new AzureMcp.Areas.{Area}.{Area}Setup(),
            new AzureMcp.Areas.Storage.StorageSetup(),
        ];
    }
```

## Error Handling

Commands in Azure MCP follow a standardized error handling approach using the base `HandleException` method inherited from `BaseCommand`. Here are the key aspects:

### 1. Status Code Mapping
The base implementation handles common status codes:
```csharp
protected virtual int GetStatusCode(Exception ex) => ex switch
{
    // Common error response codes
    AuthenticationFailedException => 401,   // Unauthorized
    RequestFailedException rfEx => rfEx.Status,  // Service-reported status
    HttpRequestException => 503,   // Service unavailable
    ValidationException => 400,    // Bad request
    _ => 500  // Unknown errors
};
```

### 2. Error Message Formatting
Error messages should be user-actionable and help debug issues:
```csharp
protected virtual string GetErrorMessage(Exception ex) => ex switch
{
    AuthenticationFailedException authEx =>
        $"Authentication failed. Please run 'az login' to sign in. Details: {authEx.Message}",
    RequestFailedException rfEx => rfEx.Message,
    HttpRequestException httpEx =>
        $"Service unavailable or connectivity issues. Details: {httpEx.Message}",
    _ => ex.Message
};
```

### 3. Response Format
The base `HandleException` combines status, message and details:
```csharp
protected virtual void HandleException(CommandContext context, Exception ex)
{
    // Create a strongly typed exception result
    var result = new ExceptionResult(
        Message: ex.Message,
        StackTrace: ex.StackTrace,
        Type: ex.GetType().Name);

    response.Status = GetStatusCode(ex);
    // Add link to troubleshooting guide
    response.Message = GetErrorMessage(ex) +
        ". To mitigate this issue, please refer to the troubleshooting guidelines at https://aka.ms/azmcp/troubleshooting.";
    response.Results = ResponseResult.Create(
        result, JsonSourceGenerationContext.Default.ExceptionResult);
}
```

### 4. Service-Specific Errors
Commands should override error handlers to add service-specific mappings:
```csharp
protected override string GetErrorMessage(Exception ex) => ex switch
{
    // Add service-specific cases
    ResourceNotFoundException =>
        "Resource not found. Verify name and permissions.",
    ServiceQuotaExceededException =>
        "Service quota exceeded. Request quota increase.",
    _ => base.GetErrorMessage(ex) // Fall back to base implementation
};
```

### 5. Error Context Logging
Always log errors with relevant context information:
```csharp
catch (Exception ex)
{
    _logger.LogError(ex,
        "Error in {Operation}. Resource: {Resource}, Options: {@Options}",
        Name, resourceId, options);
    HandleException(context.Response, ex);
}
```

### 6. Common Error Scenarios to Handle

1. **Authentication/Authorization**
   - Azure credential expiry
   - Missing RBAC permissions
   - Invalid connection strings

2. **Validation**
   - Missing required parameters
   - Invalid parameter formats
   - Conflicting options

3. **Resource State**
   - Resource not found
   - Resource locked/in use
   - Invalid resource state

4. **Service Limits**
   - Throttling/rate limits
   - Quota exceeded
   - Service capacity

5. **Network/Connectivity**
   - Service unavailable
   - Request timeouts
   - Network failures

## Testing Requirements

### Unit Tests
Core test cases for every command:
```csharp
[Theory]
[InlineData("", false, "Missing required options")]  // Validation
[InlineData("--param invalid", false, "Invalid format")] // Input format
[InlineData("--param value", true, null)]  // Success case
public async Task ExecuteAsync_ValidatesInput(
    string args, bool shouldSucceed, string expectedError)
{
    var response = await ExecuteCommand(args);
    Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
    if (!shouldSucceed)
        Assert.Contains(expectedError, response.Message);
}

[Fact]
public async Task ExecuteAsync_HandlesServiceError()
{
    // Arrange
    _service.Operation()
        .Returns(Task.FromException(new ServiceException("Test error")));

    // Act
    var response = await ExecuteCommand("--param value");

    // Assert
    Assert.Equal(500, response.Status);
    Assert.Contains("Test error", response.Message);
    Assert.Contains("troubleshooting", response.Message);
}
```

**Running Tests Efficiently:**
When developing new commands, run only your specific tests to save time:
```bash
# Run only tests for your specific command class
dotnet test --filter "FullyQualifiedName~YourCommandNameTests" --verbosity normal

# Example: Run only SQL AD Admin tests
dotnet test --filter "FullyQualifiedName~EntraAdminListCommandTests" --verbosity normal

# Run all tests for a specific area
dotnet test --filter "Area=Sql" --verbosity normal
```

### Integration Tests
Services requiring test resource deployment should add a bicep template to `/infra/services/` and import that template as a module in `/infra/test-resources.bicep`. If additional logic needs to be performed after resource deployment, but before any live tests are run, add a `{service}-post.ps1` script to the `/infra/services/` folder. See `/infra/services/storage.bicep` and `/infra/services/storage-post.ps1` for canonical examples.

#### Live Test Resource Infrastructure

**1. Create Service Bicep Template (`/infra/services/{service}.bicep`)**

Follow this pattern for your service's infrastructure:

```bicep
targetScope = 'resourceGroup'

@minLength(3)
@maxLength(17)  // Adjust based on service naming limits
@description('The base resource name. Service names have specific length restrictions.')
param baseName string = resourceGroup().name

@description('The location of the resource. By default, this is the same as the resource group.')
param location string = resourceGroup().location

@description('The client OID to grant access to test resources.')
param testApplicationOid string

// Optional: Additional service-specific parameters
@description('Service-specific configuration parameter.')
param serviceSpecificParam string = 'defaultValue'

@description('Service administrator password.')
@secure()
param adminPassword string = newGuid()

// Main service resource
resource serviceResource 'Microsoft.{Provider}/{resourceType}@{apiVersion}' = {
  name: baseName
  location: location
  properties: {
    // Service-specific properties
  }

  // Child resources (databases, containers, etc.)
  resource testResource 'childResourceType@{apiVersion}' = {
    name: 'test{resource}'
    properties: {
      // Test resource properties
    }
  }
}

// Role assignment for test application
resource serviceRoleDefinition 'Microsoft.Authorization/roleDefinitions@2018-01-01-preview' existing = {
  scope: subscription()
  // Use appropriate built-in role for your service
  // See https://learn.microsoft.com/azure/role-based-access-control/built-in-roles
  name: '{role-guid}'
}

resource appServiceRoleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = {
  name: guid(serviceRoleDefinition.id, testApplicationOid, serviceResource.id)
  scope: serviceResource
  properties: {
    principalId: testApplicationOid
    roleDefinitionId: serviceRoleDefinition.id
    description: '{Role Name} for testApplicationOid'
  }
}

// Outputs for test consumption
output serviceResourceName string = serviceResource.name
output testResourceName string = serviceResource::testResource.name
// Add other outputs as needed for tests
```

**Key Bicep Template Requirements:**
- Use `baseName` parameter with appropriate length restrictions
- Include `testApplicationOid` for RBAC assignments
- Deploy test resources (databases, containers, etc.) needed for integration tests
- Assign appropriate built-in roles to the test application
- Output resource names and identifiers for test consumption

**Cost and Resource Considerations:**
- Use minimal SKUs (Basic, Standard S0, etc.) for cost efficiency
- Deploy only resources needed for command testing
- Consider using shared resources where possible
- Set appropriate retention policies and limits
- Use resource naming that clearly identifies test purposes

**Common Resource Naming Patterns:**
- Main service: `baseName` (most common, e.g., `mcp12345`) or `{baseName}-{service}` if disambiguation needed
- Child resources: `test{resource}` (e.g., `testdb`, `testcontainer`)
- Follow Azure naming conventions and length limits
- Ensure names are unique within resource group scope
- Check existing services in `/infra/services/` for consistent patterns

**2. Add Module to Main Template (`/infra/test-resources.bicep`)**

```bicep
module {area} 'services/{service}.bicep' = if (empty(areas) || contains(areas, '{service}')) {
  name: '${deploymentName}-{service}'
  params: {
    baseName: baseName
    location: location
    testApplicationOid: testApplicationOid
    // Add service-specific parameters if needed
  }
}
```

**3. Optional: Post-Deployment Script (`/infra/services/{service}-post.ps1`)**

Create if additional setup is needed after resource deployment:

```powershell
#!/usr/bin/env pwsh

# Copyright (c) Microsoft Corporation.
# Licensed under the MIT License.

#Requires -Version 6.0
#Requires -PSEdition Core

[CmdletBinding()]
param (
    [Parameter(Mandatory)]
    [hashtable] $DeploymentOutputs,
    
    [Parameter(Mandatory)]
    [hashtable] $AdditionalParameters
)

Write-Host "Running {Area} post-deployment setup..."

try {
    # Extract outputs from deployment
    $serviceName = $DeploymentOutputs['{area}']['serviceResourceName']['value']
    $resourceGroup = $AdditionalParameters['ResourceGroupName']
    
    # Perform additional setup (e.g., create sample data, configure settings)
    Write-Host "Setting up test data for $serviceName..."
    
    # Example: Run Azure CLI commands for additional setup
    # az {service} {operation} --name $serviceName --resource-group $resourceGroup
    
    Write-Host "{Area} post-deployment setup completed successfully."
}
catch {
    Write-Error "Failed to complete {Area} post-deployment setup: $_"
    throw
}
```

**4. Update Live Tests to Use Deployed Resources**

Integration tests should use the deployed infrastructure:

```csharp
[Trait("Area", "{Area}")]
[Trait("Category", "Live")]
public class {Area}CommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output), IClassFixture<LiveTestFixture>
{
    [Fact]
    public async Task Should_Get{Resource}_Successfully()
    {
        // Use the deployed test resources
        var serviceName = Settings.ResourceBaseName;
        var resourceName = "test{resource}";
        
        var result = await CallToolAsync(
            "azmcp_{area}_{resource}_show",
            new()
            {
                { "subscription", Settings.SubscriptionId },
                { "resource-group", Settings.ResourceGroupName },
                { "service-name", serviceName },
                { "resource-name", resourceName }
            });

        // Verify successful response
        var resource = result.AssertProperty("{resource}");
        Assert.Equal(JsonValueKind.Object, resource.ValueKind);
        
        // Verify resource properties
        var name = resource.GetProperty("name").GetString();
        Assert.Equal(resourceName, name);
    }

    [Theory]
    [InlineData("--invalid-param", new string[0])]
    [InlineData("--subscription", new[] { "invalidSub" })]
    [InlineData("--subscription", new[] { "sub", "--resource-group", "rg" })]  // Missing required params
    public async Task Should_Return400_WithInvalidInput(string firstArg, string[] remainingArgs)
    {
        var allArgs = new[] { firstArg }.Concat(remainingArgs);
        var argsString = string.Join(" ", allArgs);
        
        var result = await CallToolAsync(
            "azmcp_{area}_{resource}_show",
            new()
            {
                { "args", argsString }
            });

        // Should return validation error
        Assert.NotEqual(200, result.Status);
    }
}
```

**5. Deploy and Test Resources**

Use the deployment script with your service area:

```powershell
# Deploy test resources for your service
./eng/scripts/Deploy-TestResources.ps1 -Areas "{Area}" -Location "East US"

# Run live tests
dotnet test --filter "Category=Live&Area={Area}"
```

Live test scenarios should include:
```csharp
[Theory]
[InlineData(AuthMethod.Credential)]  // Default auth
[InlineData(AuthMethod.Key)]         // Key based auth
public async Task Should_HandleAuth(AuthMethod method)
{
    var result = await CallCommand(new()
    {
        { "auth-method", method.ToString() }
    });
    // Verify auth worked
    Assert.Equal(200, result.Status);
}

[Theory]
[InlineData("--invalid-value")]    // Bad input
[InlineData("--missing-required")] // Missing params
public async Task Should_Return400_ForInvalidInput(string args)
{
    var result = await CallCommand(args);
    Assert.Equal(400, result.Status);
    Assert.Contains("validation", result.Message.ToLower());
}
```

If your live test class needs to implement `IAsyncLifetime` or override `Dispose`, you must call `Dispose` on your base class:
```cs
public class MyCommandTests(LiveTestFixture liveTestFixture, ITestOutputHelper output)
    : CommandTestsBase(liveTestFixture, output),
    IClassFixture<LiveTestFixture>, IAsyncLifetime
{
    public ValueTask DisposeAsync()
    {
        base.Dispose();
        return ValueTask.CompletedTask;
    }
}
```

Failure to call `base.Dispose()` will prevent request and response data from `CallCommand` from being written to failing test results.

## Best Practices

1. Command Structure:
   - Make command classes sealed
   - Use primary constructors
   - Follow exact namespace hierarchy
   - Register all options in RegisterOptions
   - Handle all exceptions

2. Error Handling:
   - Return 400 for validation errors
   - Return 401 for authentication failures
   - Return 500 for unexpected errors
   - Return service-specific status codes from RequestFailedException
   - Add troubleshooting URL to error messages
   - Log errors with context information
   - Override GetErrorMessage and GetStatusCode for custom error handling

3. Response Format:
   - Always set Results property for success
   - Set Status and Message for errors
   - Use consistent JSON property names
   - Follow existing response patterns

4. Documentation:
   - Clear command description without repeating the service name (e.g., use "List and manage clusters" instead of "AKS operations - List and manage AKS clusters")
   - List all required options
   - Describe return format
   - Include examples in description

5. Live Test Infrastructure:
   - Use minimal resource configurations for cost efficiency
   - Follow naming conventions: `baseName` (most common) or `{baseName}-{area}` if needed
   - Include proper RBAC assignments for test application
   - Output all necessary identifiers for test consumption
   - Use appropriate Azure service API versions
   - Consider resource location constraints and availability

## Common Pitfalls to Avoid

1. Do not:
   - **CRITICAL**: Use `subscriptionId` as parameter name - Always use `subscription` to support both IDs and names
   - Redefine base class properties in Options classes
   - Skip base.RegisterOptions() call
   - Skip base.Dispose() call
   - Use hardcoded option strings
   - Return different response formats
   - Leave command unregistered
   - Skip error handling
   - Miss required tests
   - Deploy overly expensive test resources
   - Forget to assign RBAC permissions to test application
   - Hard-code resource names in live tests
   - Use dashes in command group names

2. Always:
   - Create a static {Area}OptionDefinitions class for the area
   - Use OptionDefinitions for options
   - Follow exact file structure
   - Implement all base members
   - Add both unit and integration tests
   - Register in CommandFactory
   - Handle all error cases
   - Use primary constructors
   - Make command classes sealed
   - Include live test infrastructure for Azure services
   - Use consistent resource naming patterns (check existing services in `/infra/services/`)
   - Output resource identifiers from Bicep templates
   - Use concatenated all lowercase names for command groups (no dashes)

## Troubleshooting Common Issues

### Azure Resource Manager Compilation Errors

**Issue: Subscription not properly resolved**
- **Cause**: Using direct ARM client creation instead of subscription service
- **Solution**: Always inject and use `ISubscriptionService.GetSubscription()`
- **Fix**: Replace manual subscription resource creation with service call
- **Pattern**:
```csharp
// Wrong - manual creation
var armClient = await CreateArmClientAsync(null, retryPolicy);
var subscriptionResource = armClient.GetSubscriptionResource(new ResourceIdentifier($"/subscriptions/{subscription}"));

// Correct - use service
var subscriptionResource = await _subscriptionService.GetSubscription(subscription, null, retryPolicy);
```

**Issue: `cannot convert from 'System.Threading.CancellationToken' to 'string'`**
- **Cause**: Wrong parameter order in resource manager method calls
- **Solution**: Check method signatures; many Azure SDK methods don't take CancellationToken as second parameter
- **Fix**: Use `.GetAsync(resourceName)` instead of `.GetAsync(resourceName, cancellationToken)`

**Issue: `'SqlDatabaseData' does not contain a definition for 'CreationDate'`**
- **Cause**: Property names in Azure SDK differ from expected/documented names
- **Solution**: Use IntelliSense to explore actual property names
- **Common fixes**:
  - `CreationDate` → `CreatedOn`
  - `EarliestRestoreDate` → `EarliestRestoreOn`
  - `Edition` → `CurrentSku?.Name`

**Issue: `Operator '?' cannot be applied to operand of type 'AzureLocation'`**
- **Cause**: Some Azure SDK types are structs, not nullable reference types
- **Solution**: Convert to string: `Location.ToString()` instead of `Location?.Name`

**Issue: Wrong resource access pattern**
- **Problem**: Using `.GetSqlServerAsync(name, cancellationToken)` 
- **Solution**: Use resource collections: `.GetSqlServers().GetAsync(name)`
- **Pattern**: Always access through collections, not direct async methods

### Live Test Infrastructure Issues

**Issue: Bicep template validation fails**
- **Cause**: Invalid parameter constraints, missing required properties, or API version issues
- **Solution**: Use `az bicep build --file infra/services/{service}.bicep` to validate template
- **Fix**: Check Azure Resource Manager template reference for correct syntax and required properties

**Issue: Live tests fail with "Resource not found"**
- **Cause**: Test resources not deployed or wrong naming pattern used
- **Solution**: Verify resource deployment and naming in Azure portal
- **Fix**: Ensure live tests use `Settings.ResourceBaseName` pattern for resource names (or appropriate service-specific pattern)

**Issue: Permission denied errors in live tests**
- **Cause**: Missing or incorrect RBAC assignments in Bicep template
- **Solution**: Verify role assignment scope and principal ID
- **Fix**: Check that `testApplicationOid` is correctly passed and role definition GUID is valid

**Issue: Deployment fails with template validation errors**
- **Cause**: Parameter constraints, resource naming conflicts, or invalid configurations
- **Solution**: Review deployment logs and error messages
- **Common fixes**:
  - Adjust `@minLength`/`@maxLength` for service naming limits
  - Ensure unique resource names within scope
  - Use supported API versions for resource types
  - Verify location support for specific resource types

**Issue: High deployment costs during testing**
- **Cause**: Using expensive SKUs or resource configurations
- **Solution**: Use minimal configurations for test resources
- **Best practices**:
  - SQL: Use Basic tier with small capacity
  - Storage: Use Standard LRS with minimal replication
  - Cosmos: Use serverless or minimal RU/s allocation
  - Always specify cost-effective options in Bicep templates

### Service Implementation Issues

**Issue: HandleException parameter mismatch**
- **Cause**: Different base classes have different HandleException signatures
- **Solution**: Check base class implementation; use `HandleException(context.Response, ex)` not `HandleException(context, ex)`

**Issue: Missing AddSubscriptionInformation**
- **Cause**: Subscription commands need telemetry context
- **Solution**: Add `context.Activity?.WithSubscriptionTag(options);` or use `AddSubscriptionInformation(context.Activity, options);`

**Issue: Service not registered in DI**
- **Cause**: Forgot to register service in area setup
- **Solution**: Add `services.AddSingleton<IServiceInterface, ServiceImplementation>();` in ConfigureServices

### Base Command Class Issues

**Issue: Wrong logger type in base command constructor**
- **Example**: `ILogger<BaseSqlCommand<TOptions>>` in `BaseDatabaseCommand`
- **Solution**: Use correct generic type: `ILogger<BaseDatabaseCommand<TOptions>>`

**Issue: Missing using statements for TrimAnnotations**
- **Solution**: Add `using AzureMcp.Commands;` for `TrimAnnotations.CommandAnnotations`

## Checklist

Before submitting:

- [ ] Options class follows inheritance pattern
- [ ] Command class implements all required members
- [ ] Command uses proper OptionDefinitions
- [ ] Service interface and implementation complete
- [ ] Unit tests cover all paths
- [ ] Integration tests added
- [ ] Registered in CommandFactory
- [ ] Follows file structure exactly
- [ ] Error handling implemented
- [ ] Documentation complete
- [ ] No compiler warnings
- [ ] Tests pass (run specific tests: `dotnet test --filter "FullyQualifiedName~YourCommandTests"`)
- [ ] Build succeeds with `dotnet build`
- [ ] Code formatting applied with `dotnet format`
- [ ] Spelling check passes with `.\eng\common\spelling\Invoke-Cspell.ps1`
- [ ] **Remove unnecessary using statements from all C# files** (use IDE cleanup or `dotnet format analyzers`)
- [ ] Azure Resource Manager package added to both Directory.Packages.props and AzureMcp.csproj
- [ ] All Azure SDK property names verified and correct
- [ ] Resource access patterns use collections (e.g., `.GetSqlServers().GetAsync()`
- [ ] Subscription resolution uses `ISubscriptionService.GetSubscription()`
- [ ] Service constructor includes `ISubscriptionService` injection for Azure resources
- [ ] JSON serialization context includes all new model types
- [ ] Live test infrastructure created (Bicep template in `/infra/services/`)
- [ ] Test resources module added to `/infra/test-resources.bicep`
- [ ] RBAC permissions configured for test application in Bicep template
- [ ] Live tests use deployed resources via `Settings.ResourceBaseName` pattern
- [ ] Resource outputs defined in Bicep template for test consumption

### Documentation Requirements

**REQUIRED**: All new commands must update the following documentation files:

- [ ] **CHANGELOG.md**: Add entry under "Unreleased" section describing the new command(s)
- [ ] **docs/azmcp-commands.md**: Add command documentation with description, syntax, parameters, and examples
- [ ] **README.md**: Update the supported services table and add example prompts demonstrating the new command(s) in the appropriate service section
- [ ] **e2eTests/e2eTestPrompts.md**: Add test prompts for end-to-end validation of the new command(s)

**Documentation Standards**:
- Use consistent command paths in all documentation (e.g., `azmcp sql db show`, not `azmcp sql database show`)
- Organize example prompts by service in README.md under service-specific sections (e.g., `### 🗄️ Azure SQL Database`)
- Place new commands in the appropriate service section, or create a new service section if needed
- Provide clear, actionable examples that users can run with placeholder values
- Include parameter descriptions and required vs optional indicators in azmcp-commands.md
- Keep CHANGELOG.md entries concise but descriptive of the capability added
- Add test prompts to e2eTestPrompts.md following the established naming convention and provide multiple prompt variations

**README.md Table Formatting Standards**:
- Badge text must use the pattern `Install_{namespace}` (e.g., `Install_storage`, `Install_cosmos`)
- All badge URLs must use stable `vscode.dev` format with proper URL encoding
- Use blue badge color `#0098FF` consistently across all install buttons
- Service descriptions should be concise (under 50 characters), action-oriented, and end with a period
- Follow the pattern: "Manage/Query/Monitor [what] [and/or additional context]."
- Examples: "Manage storage accounts and blob data.", "Query AI Search services and indexes."
- Ensure proper URL encoding in badge links (e.g., `Azure%20Foundry` not `Azure%Foundry`)
