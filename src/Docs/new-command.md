<!-- Copyright (c) Microsoft Corporation.
<!-- Licensed under the MIT License. -->

# azmcp Command Implementation Guide for AI Assistants

## Command Structure

Commands follow this exact pattern:
```
azmcp <service> <resource> <operation>
```

Where:
- `<service>` - Azure service name (lowercase)
- `<resource>` - Resource type (singular noun, lowercase)
- `<operation>` - Action to perform (verb, lowercase)

Example: `azmcp cosmos database list`

## File Structure

A complete command implementation requires the following files in this exact structure:

1. Arguments class: `src/Arguments/{Service}/{SubService}/{Resource}/{Resource}{Operation}Arguments.cs`
   Example: `src/Arguments/Storage/Blob/Containers/ContainersDetailsArguments.cs`

2. Service interface method: `src/Services/Interfaces/I{Service}Service.cs`
   Example: `src/Services/Interfaces/IStorageService.cs`

3. Service implementation: `src/Services/Azure/{Service}/{Service}Service.cs`
   Example: `src/Services/Azure/Storage/StorageService.cs`

4. Command class: `src/Commands/{Service}/{SubService}/{Resource}/{Resource}{Operation}Command.cs`
   Example: `src/Commands/Storage/Blob/Containers/ContainersDetailsCommand.cs`

5. Registration in: `src/Commands/CommandFactory.cs`

6. Test class: `test/Client/{Service}CommandTests.cs`
   Example: `test/Client/StorageCommandTests.cs`

### File Organization Rules

1. Commands and Arguments must follow exact hierarchical structure:
   ```
   azmcp storage blob container list
   ↓
   src/Arguments/Storage/Blob/Container/ContainerListArguments.cs
   src/Commands/Storage/Blob/Container/ContainerListCommand.cs
   ```

2. Services are flat with standardized locations:
   ```
   src/Services/Interfaces/IStorageService.cs
   src/Services/Azure/Storage/StorageService.cs
   ```

3. Namespaces must exactly match the folder structure:
   ```csharp
   namespace AzureMcp.Commands.Storage.Blob.Containers;
   namespace AzureMcp.Arguments.Storage.Blob.Containers;
   ```

## Step 1: Create Arguments Class

Location: `src/Arguments/{Service}/{SubService}/{Resource}/{Resource}{Operation}Arguments.cs`

Template:
```csharp
namespace AzureMcp.Arguments.{Service}.{SubService}.{Resource};

public class {Resource}{Operation}Arguments : BaseArgumentsWithSubscription
{
    public string? {SpecificParam} { get; set; }
}
```

IMPORTANT:
1. Do not redefine properties from base classes
2. Use `SubscriptionArguments` for commands that require subscription (can be either ID or name)
3. Use `GlobalArguments` only for commands that don't need subscription (rare)

## Step 2: Define Service Interface Method

Location: `src/Services/Interfaces/I{Service}Service.cs`

IMPORTANT: Before adding new methods:
1. Check existing service interface for similar methods that could be reused
2. Look for patterns in existing method signatures
3. Check if base service classes already provide the functionality
4. Only add new methods if existing ones cannot be reused

Template:
```csharp
public interface I{Service}Service
{
    /// <summary>
    /// {Operation description}
    /// </summary>
    /// <param name="requiredParam1">Required parameter description</param>
    /// <param name="subscription">Subscription ID or name</param>
    /// <param name="tenantId">Optional tenant ID for cross-tenant operations</param>
    /// <param name="retryPolicy">Optional retry policy for the operation</param>
    /// <returns>List of results</returns>
    /// <exception cref="AuthenticationFailedException">When authentication fails</exception>
    /// <exception cref="RequestFailedException">When the service request fails</exception>
    Task<List<string>> {Operation}{Resource}(
        string {requiredParam1},
        string subscription,
        AuthMethod? authMethod = null,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null);
}
```

## Step 3: Implement Service Method

Location: `src/Services/Azure/{Service}/{Service}Service.cs`

Template:
```csharp
public class {Service}Service : BaseAzureService, I{Service}Service
{
    public async Task<List<string>> {Operation}{Resource}(
        string {requiredParam1},
        string subscription,
        string? tenantId = null)
    {
        var credential = await GetCredential(tenantId);

        try
        {
            var client = new {ServiceClient}(
                endpoint,
                credential);

            var response = await client.{SdkMethod}Async();

            return response.Value.Select(item => item.Name).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in {nameof({Operation}{Resource})}: {ex.Message}", ex);
        }
    }
}
```

Notes:
- Use the appropriate Azure SDK client
- Always handle exceptions properly but allow them to propagate
- Transform responses to simple formats (typically lists of strings)

## Step 4: Create Command Class

Location: `src/Commands/{Service}/{SubService}/{Resource}/{Resource}{Operation}Command.cs`

Template:
```csharp
using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using ModelContextProtocol.Server;

namespace AzureMcp.Commands.{Service}.{SubService}.{Resource};

public sealed class {Resource}{Operation}Command : Base{Service}Command<{Resource}{Operation}Arguments>
{
    private readonly Option<string> _resourceOption = ArgumentDefinitions.{Service}.Resource.ToOption();

    protected override string GetCommandName() => "{operation}";

    protected override string GetCommandDescription() =>
        $"""
        {Detailed description of what the command does}.
        Returns {description of return format}.
        
        Required arguments:
        - {ArgumentDefinitions.{Service}.Resource.Name}
        """;

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_resourceOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(Create{Resource}Argument());
    }

    protected override {Resource}{Operation}Arguments BindArguments(ParseResult parseResult)
    {
        var args = base.BindArguments(parseResult);
        args.Resource = parseResult.GetValueForOption(_resourceOption);
        return args;
    }

    [McpServerTool(Destructive = false, ReadOnly = true)]
    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArguments(context, args))
            {
                return context.Response;
            }

            var service = context.GetService<I{Service}Service>();
            var results = await service.{Operation}{Resource}(
                args.Resource!,
                args.Subscription!,
                args.Tenant,
                args.RetryPolicy);

            context.Response.Results = results?.Count > 0 ? 
                new { results } : 
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}
```

> **IMPORTANT NOTES**:
> 1. Do not define literal string values for option names or descriptions in the command class. Always use ArgumentDefinitions.
> 2. Register command examples using GetCommandExample() helper method from ArgumentDefinitions.
> 3. Options must be initialized using ToOption() on the appropriate ArgumentDefinition.

## Step 5: Register Command in CommandFactory

After creating your new command, update CommandFactory to register it. For instance, if adding a "table-type list" command, add:

```csharp
// In RegisterMonitorCommands()
var monitorTableType = new CommandGroup("table-type", "Log Analytics workspace table type operations - Commands for listing table types in Log Analytics workspaces.");
monitor.AddSubGroup(monitorTableType);
monitorTableType.AddCommand("list", new Monitor.TableType.TableTypeListCommand(GetLogger<Monitor.TableType.TableTypeListCommand>()));
```

This ensures future requests include registration updates.

Commands must be registered in service-specific helper methods following these rules:

1. Group organization:
```csharp
private void RegisterMonitorCommands()
{
    // Create service group with clear description
    var monitor = new CommandGroup("monitor", 
        "Azure Monitor operations - Commands for querying and analyzing Azure Monitor logs and metrics.");
    _rootGroup.AddSubGroup(monitor);

    // Create logical subgroups
    var logs = new CommandGroup("log", 
        "Azure Monitor logs operations - Commands for querying Log Analytics workspaces using KQL.");
    monitor.AddSubGroup(logs);

    // Register commands under appropriate groups
    logs.AddCommand("query", new Monitor.Log.LogQueryCommand());
}
```

2. Command Registration Order:
```csharp
private void RegisterCommandGroup()
{
    // 1. Core Azure Services (alphabetically)
    RegisterAppConfigCommands();
    RegisterCosmosCommands(); 
    RegisterMonitorCommands();
    RegisterStorageCommands();

    // 2. Resource Management
    RegisterGroupCommands();
    RegisterSubscriptionCommands();

    // 3. Tools & Extensions (always last)
    RegisterToolsCommands();
    RegisterExtensionCommands();
    RegisterMcpServerCommands();
}
```

3. Service Description Format:
```csharp
"{Service} operations - Commands for {main purpose}. {Additional details about capabilities}."

Example:
"Storage operations - Commands for managing and accessing Azure Storage resources. Includes operations for containers, blobs, and tables."
```

4. Command Group Hierarchy:
- Two levels max for most services
- Three levels only for complex services (Storage, Cosmos)
- Consistent naming patterns within a service

Examples:
```
storage/
  ├─ account/
  │   └─ list
  ├─ blob/
  │   ├─ container/
  │   │   ├─ list
  │   │   └─ details
  │   └─ list
  └─ table/
      └─ list

cosmos/
  ├─ database/
  │   ├─ container/
  │   │   ├─ list
  │   │   └─ query
  │   └─ list
  └─ account/
      └─ list
```

## Step 6: Register Dependencies and Namespaces

Your service must be properly registered in two files:

1. Register the service dependency in Program.cs:
```csharp
private static void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton<IYourService, YourService>();
}
```

2. Add the required using statements in Program.cs:
```csharp
using AzureMcp.Services.Azure.YourService;  // For YourService implementation
```

This ensures your service is available when the MCP Server is running in both STDIO and SSE modes.

## Step 7: Update README.md Documentation

Location: `README.md`

1. Identify or create the appropriate service section in the "Available Commands" section.
   If a new section is needed, follow this format:
   ```bash
   #### {Service} Operations
   ```

2. Add your command under the section using this format:
   ```bash
   # {Description of what the command does}
   azmcp {service} {resource} {operation} --required-arg <required-arg> [--optional-arg <optional-arg>]
   ```

Example:
```bash
#### Resource Group Operations
```bash
# List resource groups in a subscription
azmcp groups list --subscription <subscription> [--tenant-id <tenant-id>]
```

3. Place new sections in alphabetical order, but keep these sections in fixed positions:
   - "Server Operations" always first
   - "CLI Utilities" always last
   - Everything else alphabetically in between

4. For commands that support multiple output formats or have complex options, include example usages:
```bash
# Query logs from a specific table
azmcp monitor logs query --subscription <subscription> \
                        --workspace-id <workspace-id> \
                        --table "AppEvents_CL" \
                        --query "| order by TimeGenerated desc"
```

## Argument Chain System Reference

### Creating Arguments

There are two ways to create arguments:

1. Using base command helper methods (preferred):
```csharp
RegisterArgumentChain(
    CreateAccountArgument(),
    CreateContainerArgument()
);
```

2. Creating custom arguments:
```csharp
ArgumentBuilder<TArgs>
    .Create("name", "description")
    .WithValueAccessor(args => args.Property)
    .WithValueLoader(async (context, args) => await LoadValues())
    .WithIsRequired(true);
```

### Argument Creation Best Practices

When implementing argument chains:

1. Use type-safe argument creation:
```csharp
protected ArgumentBuilder<TArgs> CreateResourceArgument()
{
    return ArgumentBuilder<TArgs>
        .Create(ArgumentDefinitions.Service.Resource.Name, ArgumentDefinitions.Service.Resource.Description)
        .WithValueAccessor(args => args.Resource ?? string.Empty)
        .WithIsRequired(ArgumentDefinitions.Service.Resource.Required);
}
```

### Base Command Helper Methods

Base command classes provide these argument creation methods:

```csharp
CreateAuthMethodArgument()
CreateTenantIdArgument()
CreateSubscriptionIdArgument()

CreateAccountArgument(accountOptionsLoader)
CreateContainerArgument(containerOptionsLoader)

CreateAccountArgument()
CreateDatabaseArgument()
CreateContainerArgument()
CreateQueryArgument()

CreateWorkspaceIdArgument(workspaceOptionsLoader)
```

### Argument Chain Registration Rules

The RegisterArgumentChain method:
1. Always adds AuthMethod (optional)
2. Always adds TenantId (optional)
3. Always adds SubscriptionId (required)
4. Then adds your custom arguments

Key rules:
```csharp
public abstract class BaseServiceCommand<TArgs> : BaseCommand<TArgs>
{
    protected BaseServiceCommand()
    {
        _accountOption = ArgumentDefinitions.Service.Account.ToOption();
    }
}

public class AccountListCommand : BaseServiceCommand<AccountListArguments>
{
    public AccountListCommand() : base()
    {
        RegisterArgumentChain();  // Uses only base arguments
    }
}

public class ContainersListCommand : BaseServiceCommand<ContainersListArguments>
{
    public ContainersListCommand() : base()
    {
        RegisterArgumentChain(
            CreateAccountArgument(),
            CreateContainerArgument()
        );
    }
}

public class SubscriptionListCommand : BaseCommand<SubscriptionListArguments>
{
    public SubscriptionListCommand() : base()
    {
    }
}
```

IMPORTANT:
- DO NOT create argument helper methods in individual command classes
- DO create argument helper methods in the base command class if they are used by multiple commands
- DO use the base command's helper methods in command classes
- DO NOT duplicate argument creation logic across commands
- DO add ALL argument helper methods to RegisterArgumentChain, even if they're optional
- DO NOT forget to add the corresponding Option field and initialization in the command class

This ensures proper type checking and inheritance from BaseArguments.

## Using ArgumentDefinitions

ArgumentDefinitions provide centralized definitions for all command arguments. They are used in three key ways:

1. Creating Command Options:
```csharp
protected BaseStorageCommand() : base()
{
    _accountOption = ArgumentDefinitions.Storage.Account.ToOption();
    _containerOption = ArgumentDefinitions.Storage.Container.ToOption();
}
```

2. Creating Argument Chains:
```csharp
protected ArgumentChain<TArgs> CreateAccountArgument()
{
    return ArgumentChain<TArgs>
        .Create(
            ArgumentDefinitions.Storage.Account.Name,
            ArgumentDefinitions.Storage.Account.Description)
        .WithValueAccessor(args => ((dynamic)args).Account ?? string.Empty)
        .WithIsRequired(true);
}
```

3. JSON Property Names:
```csharp
public class StorageArguments
{
    [JsonPropertyName(ArgumentDefinitions.Storage.AccountName)]
    public string? Account { get; set; }
}
```

### Available Argument Definitions

The system provides these predefined argument sets:

```csharp
ArgumentDefinitions.Common
    .TenantId
    .SubscriptionId
    .ResourceGroup
    .AuthMethod

ArgumentDefinitions.RetryPolicy
    .Delay
    .MaxDelay
    .MaxRetries
    .Mode
    .NetworkTimeout

ArgumentDefinitions.Storage
    .Account
    .Container
    .Table

ArgumentDefinitions.Cosmos
    .Account
    .Database
    .Container
    .Query

ArgumentDefinitions.Monitor
    .WorkspaceId
    .WorkspaceName
    .TableType
    .Query
    .Hours
    .Limit
```

### Best Practices for ArgumentDefinitions

1. Always use ArgumentDefinitions instead of hardcoding names:
```csharp
protected Option<string> _accountOption = ArgumentDefinitions.Storage.Account.ToOption();
```

2. Keep JSON property names consistent:
```csharp
[JsonPropertyName(ArgumentDefinitions.Storage.AccountName)]
public string? Account { get; set; }
```

3. Use constants for dynamic access:
```csharp
var accountName = args.GetType().GetProperty(ArgumentDefinitions.Storage.AccountName)?.GetValue(args);
```

4. Creating new ArgumentDefinitions:
```csharp
public static class YourService
{
    public const string ResourceName = "resource-name";

    public static readonly ArgumentDefinition<string> Resource = new(
        ResourceName,
        "Detailed description of the resource argument.",
        required: true
    );
}
```

## Error Handling

Your command should handle errors by implementing these methods:

```csharp
protected override string GetErrorMessage(Exception ex) => ex switch
{
    AuthenticationFailedException authEx =>
        $"Authentication failed. Please run 'az login' to sign in to Azure. Details: {authEx.Message}",
    RequestFailedException rfEx => rfEx.Message,
    HttpRequestException httpEx =>
        $"Service unavailable or network connectivity issues. Details: {httpEx.Message}",
    _ => base.GetErrorMessage(ex)
};

protected override int GetStatusCode(Exception ex) => ex switch
{
    AuthenticationFailedException => 401,
    RequestFailedException rfEx => rfEx.Status,
    HttpRequestException => 503,
    _ => 500
};
```

Key error handling principles:
1. Don't catch exceptions in service methods, let them propagate
2. Use service-specific exception types (e.g., CosmosException) in command classes
3. Map all error codes to appropriate HTTP status codes
4. Provide clear error messages that help users resolve the issue
5. Include troubleshooting hints for common errors (e.g., "Please run 'az login'")

## Complete Example

Here's a complete implementation example for reference:

### Arguments Class
```csharp
namespace AzureMcp.Arguments.Storage;

public class ContainersListArguments : BaseStorageArguments
{
    public string? Container { get; set; }
}
```

### Service Interface Method
```csharp
public interface IStorageService
{
    Task<List<string>> ListContainers(
        string accountName,
        string subscriptionId,
        string? tenantId = null,
        RetryPolicyArguments? retryPolicy = null);
}
```

### Command Class
```csharp
namespace AzureMcp.Commands.Storage;

public class ContainersListCommand : BaseStorageCommand
{
    public ContainersListCommand() : base()
    {
        RegisterArgumentChain<ContainersListArguments>(
            CreateAccountArgument<ContainersListArguments>()
        );
    }

    public override Command GetCommand()
    {
        var command = new Command(
            "list",
            "List all containers in a storage account.");

        AddBaseOptionsToCommand(command);
        return command;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, object commandOptions)
    {
        var parseResult = (System.CommandLine.Parsing.ParseResult)commandOptions;
        var options = ParseOptions<ContainersListArguments>(parseResult);

        try
        {
            if (!await ProcessArgumentChain(context, args))
            {
                return context.Response;
            }

            var storageService = context.GetService<IStorageService>();
            var containers = await storageService.ListContainers(
                options.Account!,
                options.Subscription!,
                options.TenantId,
                options.RetryPolicy);

            context.Response.Results = containers?.Count > 0 ? 
                new { containers } : 
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}
```

### CommandFactory Registration
```csharp
private void RegisterCommandGroup()
{
    var storage = new CommandGroup("storage", "Storage operations");
    _rootGroup.AddSubGroup(storage);

    var container = new CommandGroup(
        "container",
        "Storage container operations");
    storage.AddSubGroup(container);

    container.AddCommand("list", new Storage.ContainerListCommand());
}

## Service-Specific Base Commands

When creating a new base command class for a service:

1. Required using directives:
```csharp
using System.CommandLine;
using AzureMcp.Models;
using AzureMcp.Services.Interfaces;
using AzureMcp.Arguments;
```

2. Always add proper type constraints:
```csharp
public abstract class Base{Service}Command : BaseCommand
{
    protected ArgumentChain<TArgs> Create{Resource}Argument<TArgs>()
        where TArgs : BaseArguments
    {
    }
}
```

3. Add helper methods for common arguments:
```csharp
public abstract class BaseAppConfigCommand<TArgs> : BaseCommand<TArgs>
{
    // Add helper methods for arguments used by multiple commands
    protected ArgumentChain<TArgs> CreateAccountArgument()
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.AppConfig.Account.Name, ArgumentDefinitions.AppConfig.Account.Description)
            .WithValueAccessor(args => ((dynamic)args).Account ?? string.Empty)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.AppConfig.Account))
            .WithIsRequired(ArgumentDefinitions.AppConfig.Account.Required);
    }

    // Helper method for creating key arguments
    protected ArgumentChain<TArgs> CreateKeyArgument()
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.AppConfig.Key.Name, ArgumentDefinitions.AppConfig.Key.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.AppConfig.Key))
            .WithValueAccessor(args => ((dynamic)args).Key ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.AppConfig.Key.Required);
    }

    // Helper method for creating value arguments
    protected ArgumentChain<TArgs> CreateValueArgument()
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.AppConfig.Value.Name, ArgumentDefinitions.AppConfig.Value.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.AppConfig.Value))
            .WithValueAccessor(args => ((dynamic)args).Value ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.AppConfig.Value.Required);
    }

    // Helper method for creating label arguments
    protected ArgumentChain<TArgs> CreateLabelArgument()
    {
        return ArgumentChain<TArgs>
            .Create(ArgumentDefinitions.AppConfig.Label.Name, ArgumentDefinitions.AppConfig.Label.Description)
            .WithCommandExample(ArgumentDefinitions.GetCommandExample(GetCommandPath(), ArgumentDefinitions.AppConfig.Label))
            .WithValueAccessor(args => ((dynamic)args).Label ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.AppConfig.Label.Required);
    }
}
```

4. Use helper methods in command classes:
```csharp
public class KeyValueLockCommand : BaseAppConfigCommand<KeyValueLockArguments>
{
    public KeyValueLockCommand() : base()
    {
        RegisterArgumentChain(
            CreateAccountArgument(),  // Use base class helper method
            CreateKeyArgument()       // Use base class helper method
        );
    }
}
```

IMPORTANT:
- DO NOT create argument helper methods in individual command classes
- DO create argument helper methods in the base command class if they are used by multiple commands
- DO use the base command's helper methods in command classes
- DO NOT duplicate argument creation logic across commands

This ensures proper type checking and inheritance from BaseArguments.

## Common Implementation Pitfalls

1. **Wrong Option Loader**: Always use service-specific option loaders:
   ```csharp
   CreateAccountArgument<TArgs>(GetStorageAccountOptions)
   ```

2. **Base Arguments Properties**: Don't redefine properties that exist in base classes:
   ```csharp
   public class ContainersListArguments : BaseStorageArguments
   {
       public string? Container { get; set; }
   }
   ```

3. **Option IsRequired Property**: Never set IsRequired directly on System.CommandLine.Option instances:
   ```csharp
   protected Option<string> _workspaceOption = ArgumentDefinitions.Storage.Account.ToOption();
   ```

4. **Argument Chain Registration**: Always register the chain even if only using base arguments:
   ```csharp
   public YourCommand() : base()
   {
       RegisterArgumentChain();  // Do not specify type parameter when using only base arguments
   }
   ```

Exception: Commands that explicitly don't want certain base arguments (like `SubscriptionListCommand`)
should not call `RegisterArgumentChain()` and instead manage their own argument chain.

> **IMPORTANT**: Base command classes should not call `RegisterArgumentChain()` in their constructor
> since it would prevent derived classes from registering their specific arguments.

## Additional Command Implementation Rules

1. Command Description Guidelines:
   ```csharp
   var command = new Command("list",
       "List all blobs in a Storage container. This command retrieves and " +
       "displays all blobs available in the specified container and Storage account. " +
       "Results include blob names, sizes, and content types, returned as a JSON array. " +
       "You must specify both an account name and a container name. " +
       "Use this command to explore your container contents or to verify blob existence " +
       "before performing operations on specific blobs.");
   ```

2. Command Hierarchy Rules:
   - Storage pattern: `storage -> blobs -> containers -> {command}`
   - Cosmos pattern: `cosmos -> databases -> containers -> {command}`
   - Monitor pattern: `monitor -> logs/workspaces -> {command}`

3. Base Command Behavior:
   ```csharp
   - Authentication method handling
   - Tenant ID handling
   - Subscription ID handling (for BaseArgumentsWithSubscription)
   - Retry policy options
   - Argument chain infrastructure

   - Service-specific option handling (_accountOption, etc)
   - GetPath() implementation for proper command paths
   - Service-specific argument creation methods
   ```

4. Special Case Commands:
   ```csharp
   public class ToolsListCommand : BaseCommandWithoutArgs
   {
       public override Command GetCommand() =>
           new Command("list", "List all available commands...");
   }

   public class SubscriptionListCommand : BaseSubscriptionCommand<SubscriptionListArguments>
   {
       public SubscriptionListCommand() : base()
       {
           _argumentChain.Clear();
           _argumentChain.Add(CreateTenantIdArgument());
       }
   }
   ```

## Error Handling Updates

1. Service-Specific Exception Handling:
```csharp
protected override string GetErrorMessage(Exception ex) => ex switch
{
    CosmosException cosmosEx when cosmosEx.Status == 404 => 
        $"Cosmos DB container not found. Please check the container name and try again.",
    StorageException storageEx when storageEx.ErrorCode == "ContainerNotFound" =>
        $"Storage container not found. Please check the container name and try again.",
    MonitorException monitorEx when monitorEx.Code == "ResourceNotFound" =>
        $"Log Analytics workspace not found. Please check the workspace ID and try again.",
    _ => base.GetErrorMessage(ex)
};
```

2. Default Status Code Handling:
```csharp
protected override int GetStatusCode(Exception ex) => ex switch
{
    AuthenticationFailedException => 401,
    RequestFailedException rfEx => rfEx.Status,
    HttpRequestException => 503,
    ResourceNotFoundException => 404,
    ValidationException => 400,
    _ => 500
};
```

3. Common Error Patterns:
- Authentication: 401 for missing/invalid credentials
- Authorization: 403 for permission issues
- Validation: 400 for invalid input
- Not Found: 404 for missing resources
- Service Issues: 503 for connectivity/availability

4. Error Response Structure:
```json
{
    "status": 404,
    "message": "Storage container not found. Please check the container name and try again.",
    "results": null,
}
```

## CommandFactory Guidelines

Here's an example of how command groups should be registered:

```csharp
private void RegisterMonitorCommands()
{
    // Create Monitor command group with clear description
    var monitor = new CommandGroup("monitor",
        "Azure Monitor operations - Commands for querying and analyzing Azure Monitor logs and metrics.");
    _rootGroup.AddSubGroup(monitor);

    // Create descriptive subgroups
    var logs = new CommandGroup("log",
        "Azure Monitor logs operations - Commands for querying Log Analytics workspaces using KQL.");
    monitor.AddSubGroup(logs);

    var workspaces = new CommandGroup("workspace",
        "Log Analytics workspace operations - Commands for managing Log Analytics workspaces.");
    monitor.AddSubGroup(workspaces);

    // Register commands under appropriate groups
    logs.AddCommand("query", new Monitor.Log.LogQueryCommand());
    workspaces.AddCommand("list", new Monitor.Workspace.WorkspaceListCommand());
}
```

The root command group registration follows this pattern:
```csharp
private void RegisterCommandGroup()
{
    // Register top-level command groups
    RegisterCosmosCommands();
    RegisterStorageCommands();
    RegisterMonitorCommands();
    RegisterToolsCommands();
    RegisterSubscriptionCommands();
    RegisterGroupCommands();
    RegisterMcpServerCommands();
}
```

## Building and Testing

After implementing a new command, follow these steps to verify your changes:

1. Local Build Verification:
```bash
# Build and verify the changes locally
./eng/scripts/Build-Local.ps1 -UsePaths -VerifyNpx
```

2. Run Tests:
- Write unit tests for each new command in `tests/Commands/{Service}`
- Follow the test naming convention: `{Command}Tests.cs`
- Test failure scenarios and error handling
- Verify argument validation
- Mock service dependencies using NSubstitute

Example test structure:
```csharp
public class ContainerListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ContainerListCommand _command;
    private readonly CommandContext _context;
    private readonly ParseResult _parser;

    public ContainerListCommandTests()
    {
        var storageService = Substitute.For<IStorageService>();
        var collection = new ServiceCollection()
            .AddSingleton(storageService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new();
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        // Assert
        Assert.Equal("list", _command.GetCommand().Name);
        Assert.NotEmpty(_command.GetCommand().Description);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidInput_ReturnsSuccess()
    {
        // Arrange
        var storageService = _serviceProvider.GetRequiredService<IStorageService>();
        storageService.ListContainers(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>())
            .Returns([]);

        // Act
        var result = await _command.ExecuteAsync(_context, _parser);

        // Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);
    }
}
```

3. Verify Pipeline Compliance:
- Use dotnet format to ensure code style compliance
- Run code analysis and fix any warnings
- Check test coverage (should be >80% for new code)

4. Update Test Documentation:
- Add test examples to test-cmds.txt
- Document any special test requirements
- Include sample data if needed

Remember to run `dotnet build` after any changes and fix all warnings.

## Code Style Requirements

1. Comments Policy:
   - NO COMMENTS in implementation code
   - Code must be self-documenting through clear naming and structure
   - Even "helpful" or "clarifying" comments should be avoided
   - Convert would-be comments into well-named methods or variables
   - Only exception: XML documentation for public APIs

   Examples:
   ```csharp
   // BAD - uses comments
   // Get the storage account client
   var client = new StorageAccountClient(endpoint, credential);

   // GOOD - self-documenting code
   var storageClient = new StorageAccountClient(endpoint, credential);

   // BAD - commented steps
   // First validate the input
   if (string.IsNullOrEmpty(accountName)) return false;
   // Then check permissions
   if (!await HasPermissions()) return false;

   // GOOD - extracted to meaningful method
   if (!await ValidateAccountAccess(accountName)) return false;
   ```

2. Example of proper code style:
```csharp
public class ContainersListCommand : BaseStorageCommand<ContainersListArguments>
{
    public ContainersListCommand() : base()
    {
        RegisterArgumentChain(
            CreateAccountArgument(GetStorageAccountOptions),
            CreateContainerArgument(GetContainerOptions)
        );
    }

    public override Command GetCommand()
    {
        var command = new Command(
            "list",
            "List all containers in a storage account. Returns an array of container names.");

        AddBaseOptionsToCommand(command);
        command.AddOption(_accountOption);
        return command;
    }

    public override async Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var args = BindArguments(parseResult);

        try
        {
            if (!await ProcessArgumentChain(context, args))
            {
                return context.Response;
            }

            var service = context.GetService<IStorageService>();
            var containers = await service.ListContainers(
                options.Account!,
                options.Subscription!,
                options.AuthMethod ?? AuthMethod.Credential,
                options.TenantId,
                options.RetryPolicy);

            context.Response.Results = containers?.Count > 0 ? 
                new { containers } : 
                null;
        }
        catch (Exception ex)
        {
            HandleException(context.Response, ex);
        }

        return context.Response;
    }
}
```

This example demonstrates:
1. Using base command helper methods for argument creation
2. Proper argument chain registration
3. Clear command description
4. Proper error handling
5. Consistent response format
6. Using ArgumentDefinitions for option names and descriptions
7. Following the command structure pattern
8. Proper service interface and implementation separation

## Testing Best Practices

When testing commands, ensure you cover these scenarios:

1. Error Path Testing:
```csharp
[Fact]
public async Task ExecuteAsync_WithMissingArguments_ReturnsBadRequest()
{
    // Arrange
    _parser = _parser.WithOption(_accountOption, string.Empty);

    // Act
    var result = await _command.ExecuteAsync(_context, _parser);

    // Assert
    Assert.Equal(400, result.Status);
    Assert.Contains("Missing required arguments", result.Message);
}

[Fact]
public async Task ExecuteAsync_WithServiceError_HandlesErrorCorrectly()
{
    // Arrange
    var service = _serviceProvider.GetRequiredService<IStorageService>();
    service.ListContainers(Arg.Any<string>(), Arg.Any<string>())
        .Throws(new RequestFailedException(404, "Not found"));

    // Act
    var result = await _command.ExecuteAsync(_context, _parser);

    // Assert
    Assert.Equal(404, result.Status);
    Assert.Contains("Not found", result.Message);
}
```

3. Authentication Testing:
```csharp
[Fact]
public async Task ExecuteAsync_WithAuthFailure_ReturnsUnauthorized()
{
    // Arrange
    var service = _serviceProvider.GetRequiredService<IStorageService>();
    service.ListContainers(Arg.Any<string>(), Arg.Any<string>())
        .Throws(new AuthenticationFailedException("Not authenticated"));

    // Act
    var result = await _command.ExecuteAsync(_context, _parser);

    // Assert
    Assert.Equal(401, result.Status);
    Assert.Contains("Please run 'az login'", result.Message);
}
```

## Base Command Implementation

Here's the inheritance hierarchy and responsibility of each layer:

1. `IBaseCommand` - Core interface:
```csharp
public interface IBaseCommand
{
    Command GetCommand();
    Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult commandOptions);
    IEnumerable<ArgumentDefinition<string>>? GetArguments();
    void ClearArguments();
    void AddArgument(ArgumentDefinition<string> argument);
}
```

2. `BaseCommand` - Base implementation:
```csharp
public abstract class BaseCommand : IBaseCommand
{
    protected readonly HashSet<string> _registeredArgumentNames = [];
    protected readonly List<ArgumentDefinition<string>> _arguments = [];
    private readonly Command? _command;

    protected BaseCommand()
    {
        _command = new Command(GetCommandName(), GetCommandDescription());
        RegisterOptions(_command);
        RegisterArguments();
    }

    protected abstract string GetCommandName();
    protected abstract string GetCommandDescription();
    protected virtual void RegisterOptions(Command command) { }
    protected virtual void RegisterArguments() { }
    protected virtual void HandleException(CommandResponse response, Exception ex)
    {
        response.Status = GetStatusCode(ex);
        response.Message = GetErrorMessage(ex);
        response.Results = null;
    }
}
```

3. `GlobalCommand<TArgs>` - Adds common Azure functionality:
```csharp
public abstract class GlobalCommand<TArgs> : BaseCommand
    where TArgs : GlobalArguments, new()
{
    protected readonly Option<string> _tenantOption = ArgumentDefinitions.Common.Tenant.ToOption();
    protected readonly Option<AuthMethod> _authMethodOption = ArgumentDefinitions.Common.AuthMethod.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_tenantOption);
        command.AddOption(_authMethodOption);
        // Add retry options...
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateAuthMethodArgument());
        AddArgument(CreateTenantArgument());
        foreach (var argument in CreateRetryArguments())
        {
            AddArgument(argument);
        }
    }
}
```

4. Service-Specific Base Commands - Add service functionality:
```csharp
public abstract class BaseStorageCommand<T> : SubscriptionCommand<T>
    where T : BaseStorageArguments, new()
{
    protected readonly Option<string> _accountOption = ArgumentDefinitions.Storage.Account.ToOption();

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_accountOption);
    }

    protected override void RegisterArguments()
    {
        base.RegisterArguments();
        AddArgument(CreateAccountArgument());
    }

    protected ArgumentBuilder<T> CreateAccountArgument()
    {
        return ArgumentBuilder<T>
            .Create(ArgumentDefinitions.Storage.Account.Name,
                   ArgumentDefinitions.Storage.Account.Description)
            .WithValueAccessor(args => args.Account ?? string.Empty)
            .WithIsRequired(ArgumentDefinitions.Storage.Account.Required);
    }
}
```

Key Implementation Rules:
1. Always call base.RegisterOptions() and base.RegisterArguments() first
2. Use ArgumentDefinitions for all option and argument definitions
3. Put common functionality in the most appropriate base class
4. Use primary constructors where possible
5. Make command classes sealed unless designed for inheritance
6. Override GetErrorMessage() and GetStatusCode() for service-specific errors
