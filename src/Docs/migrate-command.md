<!-- Copyright (c) Microsoft Corporation.
<!-- Licensed under the MIT License. -->

# Migrating Commands from Old Structure to Area Pattern

This guide provides step-by-step instructions for migrating existing commands from the old centralized structure to the new Area pattern. The Area pattern organizes all service-related code into dedicated folders, improving maintainability and reducing coupling.

The pre-area pattern used the term "Service" with ambiguity.  We'll now refer to azure services as Area for this document.

## Overview of Changes

The migration involves moving from a centralized structure to service-specific areas:

### Old Structure (Pre-Area Pattern)
```
src/
├── Commands/{Area}/{Resource}/           # Commands
├── Options/{Area}/{Resource}/            # Options
├── Services/Interfaces/                  # Service interfaces
├── Services/Azure/{Area}/                # Service implementations
└── Commands/CommandFactory.cs            # Central registration

tests/
├── Commands/{Area}/{Resource}/           # Unit tests
└── Client/                               # Integration tests

infra/
└── Services/
    ├── {Area}.bicep                     #service specific bicep template
    └── {Area}-post.ps1                  #service specific bicep template
```



### New Structure (Area Pattern)
```
src/Areas/{Area}/
├── Commands/{Resource}/                     # Commands
├── Options/{Resource}/                      # Options
├── Services/I{Area}Service.cs               # Service interface
├── Services/{Area}Service.cs                # Service implementation
├── {Area}Setup.cs                           # Area registration
└── Models/                                  # Service-specific models

tests/Areas/{Area}/
├── UnitTests/{Resource}/                    # Unit tests
└── LiveTests/                               # Integration tests
    ├── test-resources.bicep
    └── test-resources-post.ps1
```

## Migration Steps

For each step, perform moves as a single batch call.
To check if a directory is empty or only contains empty subdirectories, do a recursive search for files in the directory `Get-ChildItem <directory> -Recurse -File`

### Step 1: Create Area Structure

For each service being migrated:

1. **Create the area directory structure:**
   ```
   src/Areas/{Area}/
   ├── Commands/
   ├── Options/
   ├── Models/
   └── Services/


   tests/Areas/{Area}/
   ├── UnitTests/
   └── LiveTests/
   ```

### Step 2: Move files
**IMPORTANT** Don't update namespaces. Namespaces will be updated manually after all areas are migrated.
**IMPORTANT** Don't waste time cleaning up empty directories

1. Move service files in one powershell task:
   - Move Option files:
      - From: `src/Options/{Area}/**`
      - To: `src/Areas/{Area}/Options/`

   - Move Command Classes:
      - From: `src/Commands/{Area}/**`
      - Into: `src/Areas/{Area}/Commands/`

   - Move Command Classes:
      - From: `src/Models/{Area}/**`
      - Into: `src/Areas/{Area}/Models/`

   - Move service interface:
      - From: `src/Services/Interfaces/I{Area}Service.cs`
      - Into: `src/Areas/{Area}/Services/`

   - Move service implementation:
      - From: `src/Services/Azure/{Area}/**`
      - Into: `src/Areas/{Area}/Services/`

   - Move unit test files:
      - From: `tests/Commands/{Area}/**`
      - Into: `tests/Areas/{Area}/UnitTests/`

   - Move integration test files:
      - From: `tests/Client/{Area}CommandTests.cs`
      - Into: `tests/Areas/{Area}/LiveTests/`

   - Move service-specific models to `src/Areas/{Area}/Models/`

### Step 3. Option definitions subclass
Move service-specific option definitions subclass from the common `src/Models/Option/OptionDefinitions.cs` partial class to `src/Areas/{Area}/Options/OptionDefinitions.cs` partial class

### Step 4. Create the area setup file
   Create `src/Areas/{Area}/{Area}Setup.cs`:
   ```csharp
   // Copyright (c) Microsoft Corporation.
   // Licensed under the MIT License.

   using AzureMcp.Commands;
   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.Extensions.Logging;

   namespace AzureMcp.Areas.{Area};

   public class {Area}Setup : IAreaSetup
   {
       public void ConfigureServices(IServiceCollection services)
       {
           services.AddSingleton<I{Area}Service, {Area}Service>();
       }

       public void RegisterCommands(CommandGroup rootGroup, ILoggerFactory loggerFactory)
       {

       }
   }
   ```

   Make sure the service's namespace is added to the using statements.
   The command namespaces should also be added to the using statements.  Read the correct namespace from each migrated command file.

   For the RegisterCommands method, use the content from the `Register{Area}Commands()` method in `src/Commands/CommandFactory.cs`
   - Replace `_rootGroup` with `rootGroup`
   - Replace GetLogger<T>() with loggerFactory.CreateLogger<T>()
   - Prefer just class names over partially/fully qualified type names in AddCommand statements:
     - yes: databases.AddCommand("list", new DatabaseListCommand(loggerFactory.CreateLogger<DatabaseListCommand>()));
     - no: databases.AddCommand("list", new Cosmos.Database.DatabaseListCommand(loggerFactory.CreateLogger<Cosmos.Database.DatabaseListCommand>()));
     - This may require additional namespace using statements

### Step 45: Update Registration

1. **Remove old CommandFactory registration:**
   - Delete the `Register{Area}Commands()` method from `src/Commands/CommandFactory.cs`
   - The contents of that method should have already been copied into `src/Areas/{Area}/{Area}Setup.cs`
   - The area's commands will now be dynamically registered through the `IEnumerable<IAreaSetup>` passed to CommandFactory's constructor.

2. **Register the area in Program.cs:**
   Update the `RegisterAreas()` method in `src/Program.cs`:
   ```csharp
   private static IAreaSetup[] RegisterAreas()
   {
       return [
           // ... existing areas ...
           new AzureMcp.Areas.{Area}.{Area}Setup(),
           // ... other areas ...
       ];
   }
   ```
3. **Code Style**
   Sometimes, copilot will remove the blank lines between methods. Check Program.cs and CommandFactory.cs and make sure there are blank lines between each method.

## Shared Classes
- Don't move shared / generic models, services, etc.

## Verification
- run `./eng/scripts/Test-Code.ps1` to build and run unit tests
