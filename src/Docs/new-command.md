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
   - In code, to avoid ambiguity between service classes and Azure services, we
     refer to Azure services as Areas
   - Registered in the RegisterCommands method of its service's Areas/{Area}/{Area}Setup.cs file
   - Organized in a hierarchy of command groups
   - Documented with a title, description and examples
   - Validated before execution
   - Returns a standardized response format


### Required Files

A complete command requires:

1. Options class: `src/Areas/{Area}/Options/{Resource}/{Operation}Options.cs`
2. Command class: `src/Areas/{Area}/Commands/{Resource}/{Resource}{Operation}Command.cs`
3. Service interface: `src/Areas/{Area}/Services/I{Service}Service.cs`
4. Service implementation: `src/Areas/{Area}/Services/{Service}Service.cs`
   - {Area} and {Service} should not be considered synonymous
   - It's common for an area to have a single service class named after the
     area but some areas will have multiple service classes
5. Unit test: `tests/Areas/{Area}/UnitTests/{Resource}/{Resource}{Operation}CommandTests.cs`
6. Integration test: `tests/Areas/{Area}/LiveTests/{Area}CommandTests.cs`
7. Command registration in RegisterCommands(): `src/Areas/{Area}/{Area}Setup.cs`
9. Area registration in RegisterAreas(): `src/Program.cs`

## Implementation Guidelines

### 1. Options Class

```csharp
public class {Resource}{Operation}Options : Base{Service}Options
{
    // Only add properties not in base class
    public string? NewOption { get; set; }
}
```

IMPORTANT:
- Inherit from appropriate base class (BaseServiceOptions, GlobalOptions, etc.)
- Never redefine properties from base classes
- Make properties nullable if not required
- Use consistent parameter names across services:
  - Use `subscription` instead of `subscriptionId`
  - Use `resourceGroup` instead of `resourceGroupName`
  - Use singular nouns for resource names (e.g., `server` not `serverName`)
  - Keep parameter names consistent with Azure SDK parameters when possible
  - If services share similar operations (e.g., ListDatabases), use the same parameter order and names

### 2. Command Class

```csharp
public sealed class {Resource}{Operation}Command(ILogger<{Resource}{Operation}Command> logger)
    : Base{Service}Command<{Resource}{Operation}Options>
{
    private const string CommandTitle = "Human Readable Title";
    private readonly ILogger<{Resource}{Operation}Command> _logger = logger;

    // Define options from OptionDefinitions
    private readonly Option<string> _newOption = OptionDefinitions.Service.NewOption;

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

            AddSubscriptionInformation(context.Activity, options); 

            // Get the appropriate service from DI
            var service = context.GetService<I{Service}Service>();

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
                    {Service}JsonContext.Default.{Operation}CommandResult) :
                null;
        }
        catch (Exception ex)
        {
            // Log error with all relevant context
            _logger.LogError(ex,
                "Error in {Operation}. Required: {Required}, Optional: {Optional}, Options: {@Options}",
                Name, options.RequiredParam, options.OptionalParam, options);
            HandleException(context.Response, ex);
        }

        return context.Response;
    }

    // Implementation-specific error handling
    protected override string GetErrorMessage(Exception ex) => ex switch
    {
        ResourceNotFoundException => "Resource not found. Verify the resource exists and you have access.",
        AuthorizationException authEx =>
            $"Authorization failed accessing the resource. Details: {authEx.Message}",
        ServiceException svcEx => svcEx.Message,
        _ => base.GetErrorMessage(ex)
    };

    protected override int GetStatusCode(Exception ex) => ex switch
    {
        ResourceNotFoundException => 404,
        AuthorizationException => 403,
        ServiceException svcEx => svcEx.Status,
        _ => base.GetStatusCode(ex)
    };

    // Strongly-typed result records
    internal record {Resource}{Operation}CommandResult(List<ResultType> Results);
}

### 3. Base Service Command Classes

Each service has its own hierarchy of base command classes that inherit from `GlobalCommand` or `SubscriptionCommand`. For example:

```csharp
// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models.Option;
using AzureMcp.Options.{Service};
using Azure.Core;
using AzureMcp.Models;
using Microsoft.Extensions.Logging;

namespace AzureMcp.Commands.{Service};

// Base command for all service commands
public abstract class Base{Service}Command<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : SubscriptionCommand<TOptions> where TOptions : Base{Service}Options, new()
{
    protected readonly Option<string> _commonOption = OptionDefinitions.Service.CommonOption;
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

// Base command for resource-specific commands
public abstract class Base{Resource}Command<
    [DynamicallyAccessedMembers(TrimAnnotations.CommandAnnotations)] TOptions>
    : Base{Service}Command<TOptions>
    where TOptions : Base{Resource}Options, new()
{
    protected readonly Option<string> _resourceOption = OptionDefinitions.Service.Resource;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceOption);
    }

    protected override TOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Resource = parseResult.GetValueForOption(_resourceOption);
        return options;
    }
}
```

### 4. Unit Tests

Unit tests follow a standardized pattern that tests initialization, validation, and execution:

```csharp
public class {Resource}{Operation}CommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly I{Service}Service _service;
    private readonly ILogger<{Resource}{Operation}Command> _logger;
    private readonly {Resource}{Operation}Command _command;

    public {Resource}{Operation}CommandTests()
    {
        _service = Substitute.For<I{Service}Service>();
        _logger = Substitute.For<ILogger<{Resource}{Operation}Command>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
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
            _service.{Operation}(Arg.Any<{Resource}{Operation}Options>())
                .Returns(new List<ResultType>());
        }

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse(args);

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

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
        _service.{Operation}(Arg.Any<{Resource}{Operation}Options>())
            .Returns(Task.FromException<List<ResultType>>(new Exception("Test error")));

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse("--required value");

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

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
public class {Service}CommandTests : CommandTestsBase, IClassFixture<LiveTestFixture>
{
    protected const string TenantNameReason = "Service principals cannot use TenantName for lookup";
    protected LiveTestSettings Settings { get; }
    protected StringBuilder FailureOutput { get; } = new();
    protected ITestOutputHelper Output { get; }
    protected IMcpClient Client { get; }

    public {Service}CommandTests(LiveTestFixture fixture, ITestOutputHelper output)
        : base(fixture, output)
    {
        Client = fixture.Client;
        Settings = fixture.Settings;
        Output = output;
    }

    [Theory]
    [InlineData(AuthMethod.Credential)]
    [InlineData(AuthMethod.Key)]
    [Trait("Category", "Live")]
    public async Task Should_{Operation}_{Resource}_WithAuth(AuthMethod authMethod)
    {
        // Arrange
        var result = await CallToolAsync(
            "azmcp-{service}-{resource}-{operation}",
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
    [Trait("Category", "Live")]
    public async Task Should_Return400_WithInvalidInput(string args)
    {
        var result = await CallToolAsync(
            $"azmcp-{service}-{resource}-{operation} {args}");

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
        "{service}",
        "{Service} operations");
    rootGroup.AddSubGroup(service);

    var resource = new CommandGroup(
        "{resource}",
        "{Resource} operations");
    service.AddSubGroup(resource);

    resource.AddCommand("operation", new {Service}.{Resource}{Operation}Command(
        loggerFactory.CreateLogger<{Resource}{Operation}Command>()));
```

### 7. Area registration
```csharp
    private static IAreaSetup[] RegisterAreas()
    {
        return [
            new AzureMcp.Areas.AppConfig.AppConfigSetup(),
            new AzureMcp.Areas.{Service}.{Service}Setup(),
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
protected virtual void HandleException(CommandResponse response, Exception ex)
{
    // Create a strongly typed exception result
    var result = new ExceptionResult(
        Message: ex.Message,
        StackTrace: ex.StackTrace,
        Type: ex.GetType().Name);

    response.Status = GetStatusCode(ex);
    // Add link to troubleshooting guide
    response.Message = GetErrorMessage(ex) +
        ". Details at https://aka.ms/azmcp/troubleshooting";
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

### Integration Tests
Services requiring test resource deployment should add a bicep template to `/infra/services/` and import that template as a module in `/infra/test-resources.bicep`. If additional logic needs to be performed after resource deployment, but before any live tests are run, add a `{service}-post.ps1` script to the `/infra/services/` folder. See `/infra/services/storage.bicep` and `/infra/services/storage-post.ps1` for canonical examples.

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
   - Clear command description
   - List all required options
   - Describe return format
   - Include examples in description

## Common Pitfalls to Avoid

1. Do not:
   - Redefine base class properties in Options classes
   - Skip base.RegisterOptions() call
   - Skip base.Dispose() call
   - Use hardcoded option strings
   - Return different response formats
   - Leave command unregistered
   - Skip error handling
   - Miss required tests

2. Always:
   - Use OptionDefinitions for options
   - Follow exact file structure
   - Implement all base members
   - Add both unit and integration tests
   - Register in CommandFactory
   - Handle all error cases
   - Use primary constructors
   - Make command classes sealed

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
- [ ] Tests pass
