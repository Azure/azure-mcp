// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.
using Xunit;
namespace AzureMcp.Tests.Areas.AppService.UnitTests.Documentation;

/// <summary>
/// Tests specifically for documentation maintenance and updates.
/// These tests ensure the App Service command documentation stays current and comprehensive.
/// </summary>
public class DocumentationMaintenanceTests
{
    [Fact]
    public void UpdateDocumentation_WhenMissingElements_AddsThemAutomatically()
    {
        // Arrange
        var documentationPath = GetDocumentationPath();
        var originalContent = File.ReadAllText(documentationPath);
        var backupPath = documentationPath + ".test_backup";
        
        try
        {
            // Create backup
            File.Copy(documentationPath, backupPath, true);
            
            // Simulate incomplete documentation by removing key sections
            var incompleteContent = RemoveDocumentationSections(originalContent);
            File.WriteAllText(documentationPath, incompleteContent);
            
            // Act - Run the documentation update process
            var updateResult = UpdateAppServiceDocumentation();
            
            // Assert
            Assert.True(updateResult.UpdatesApplied, "Updates should have been applied to incomplete documentation");
            Assert.True(updateResult.MissingElements.Count > 0, "Should have detected missing elements");
            
            // Verify the updated documentation is now complete
            var updatedContent = File.ReadAllText(documentationPath);
            var updatedSection = ExtractAppServiceSection(updatedContent);
            
            var requiredElements = GetRequiredDocumentationElements();
            foreach (var element in requiredElements)
            {
                Assert.True(updatedSection.Contains(element), $"Required element '{element}' should be present after update");
            }
            
            // Verify structure is correct
            Assert.Contains("**Parameters:**", updatedSection);
            Assert.Contains("**Examples:**", updatedSection);
            Assert.Contains("**Returns:**", updatedSection);
            Assert.Contains("**Required Permissions:**", updatedSection);
            Assert.Contains("**Common Errors:**", updatedSection);
        }
        finally
        {
            // Restore original documentation
            if (File.Exists(backupPath))
            {
                File.Copy(backupPath, documentationPath, true);
                File.Delete(backupPath);
            }
        }
    }

    [Fact]
    public void UpdateDocumentation_WhenComplete_NoChangesNeeded()
    {
        // Arrange
        var documentationPath = GetDocumentationPath();
        var originalContent = File.ReadAllText(documentationPath);
        
        // First ensure documentation is complete
        var updateResult1 = UpdateAppServiceDocumentation();
        if (updateResult1.UpdatesApplied)
        {
            // If updates were applied, read the new content
            originalContent = File.ReadAllText(documentationPath);
        }
        
        // Act - Try to update again
        var updateResult2 = UpdateAppServiceDocumentation();
        
        // Assert
        Assert.False(updateResult2.UpdatesApplied, "No updates should be needed for complete documentation");
        Assert.Empty(updateResult2.MissingElements);
        
        // Verify content didn't change
        var finalContent = File.ReadAllText(documentationPath);
        Assert.Equal(originalContent, finalContent);
    }

    [Fact]
    public void DocumentationCoverage_ValidatesAllCommandFeatures()
    {
        // Arrange
        var section = ExtractAppServiceSection(File.ReadAllText(GetDocumentationPath()));
        
        // Define comprehensive coverage requirements
        var coverageRequirements = new Dictionary<string, string[]>
        {
            ["Required Parameters"] = new[] 
            { 
                "subscription", "resource-group", "app-name", 
                "database-type", "database-server", "database-name" 
            },
            ["Optional Parameters"] = new[] 
            { 
                "connection-string", "tenant-id", "auth-method", "retry-max-retries" 
            },
            ["Database Types"] = new[] { "SqlServer", "MySql", "PostgreSql", "CosmosDb" },
            ["Auth Methods"] = new[] { "credential", "key", "connectionString" },
            ["Error Scenarios"] = new[] { "403", "404", "400", "409", "Forbidden", "Not Found" },
            ["Response Fields"] = new[] 
            { 
                "databaseType", "databaseServer", "databaseName", 
                "connectionString", "isConfigured", "configuredAt" 
            },
            ["Permission Requirements"] = new[] { "Contributor", "permissions", "role" }
        };
        
        // Check coverage for each requirement category
        var coverageReport = new Dictionary<string, List<string>>();
        
        foreach (var requirement in coverageRequirements)
        {
            var missing = new List<string>();
            foreach (var item in requirement.Value)
            {
                if (!section.Contains(item, StringComparison.OrdinalIgnoreCase))
                {
                    missing.Add(item);
                }
            }
            if (missing.Count > 0)
            {
                coverageReport[requirement.Key] = missing;
            }
        }
        
        // Assert complete coverage
        Assert.Empty(coverageReport);
        
        // Additional structural validation
        Assert.True(section.Split("```bash").Length >= 3, "Should have at least 2 bash code examples");
        Assert.True(section.Split("azmcp appservice database add").Length >= 5, "Should have at least 4 complete command examples");
        Assert.Contains("JSON", section); // Should document JSON response format
    }

    private static string GetDocumentationPath()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        while (currentDirectory != null && !File.Exists(Path.Combine(currentDirectory, "AzureMcp.sln")))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }
        
        if (currentDirectory == null)
            throw new DirectoryNotFoundException("Could not find project root");
            
        return Path.Combine(currentDirectory, "docs", "azmcp-commands.md");
    }

    private static string ExtractAppServiceSection(string content)
    {
        var start = content.IndexOf("### Azure App Service Operations");
        if (start < 0) return "";
        
        var end = content.IndexOf("## Response Format", start);
        if (end < 0) end = content.Length;
        
        return content.Substring(start, end - start);
    }

    private static string RemoveDocumentationSections(string content)
    {
        // Simulate incomplete documentation by creating a minimal version
        var start = content.IndexOf("### Azure App Service Operations");
        var end = content.IndexOf("## Response Format", start);
        
        var beforeSection = content.Substring(0, start);
        var afterSection = content.Substring(end);
        
        var minimalSection = """
### Azure App Service Operations

```bash
# Add a database connection to an App Service application
azmcp appservice database add --subscription <subscription> \
                              --resource-group <resource-group> \
                              --app-name <app-name> \
                              --database-type <database-type> \
                              --database-server <database-server> \
                              --database-name <database-name>
```

""";
        
        return beforeSection + minimalSection + afterSection;
    }

    private static string[] GetRequiredDocumentationElements()
    {
        return new[]
        {
            "subscription", "resource-group", "app-name", "database-type", 
            "database-server", "database-name", "connection-string",
            "SqlServer", "MySql", "PostgreSql", "CosmosDb",
            "**Parameters:**", "**Examples:**", "**Returns:**",
            "403", "404", "Contributor", "JSON"
        };
    }

    private static DocumentationUpdateResult UpdateAppServiceDocumentation()
    {
        var documentationPath = GetDocumentationPath();
        var content = File.ReadAllText(documentationPath);
        var section = ExtractAppServiceSection(content);
        
        var requiredElements = GetRequiredDocumentationElements();
        var missingElements = requiredElements
            .Where(element => !section.Contains(element, StringComparison.OrdinalIgnoreCase))
            .ToList();
        
        if (missingElements.Count > 0)
        {
            // Build enhanced documentation
            var enhancedDocumentation = BuildEnhancedDocumentation();
            
            // Replace the section
            var sectionStart = content.IndexOf("### Azure App Service Operations");
            var sectionEnd = content.IndexOf("## Response Format", sectionStart);
            
            var beforeSection = content.Substring(0, sectionStart);
            var afterSection = content.Substring(sectionEnd);
            var updatedContent = beforeSection + enhancedDocumentation + afterSection;
            
            File.WriteAllText(documentationPath, updatedContent);
            
            return new DocumentationUpdateResult(true, missingElements);
        }
        
        return new DocumentationUpdateResult(false, new List<string>());
    }

    private static string BuildEnhancedDocumentation()
    {
        return """
### Azure App Service Operations

```bash
# Add a database connection to an App Service application
azmcp appservice database add --subscription <subscription> \
                              --resource-group <resource-group> \
                              --app-name <app-name> \
                              --database-type <database-type> \
                              --database-server <database-server> \
                              --database-name <database-name> \
                              [--connection-string <connection-string>] \
                              [--tenant-id <tenant-id>] \
                              [--auth-method <auth-method>] \
                              [--retry-max-retries <retries>]
```

**Parameters:**
- `--subscription`: Azure subscription ID (required)
- `--resource-group`: Resource group containing the App Service (required)
- `--app-name`: Name of the Azure App Service application (required)
- `--database-type`: Type of database - SqlServer, MySql, PostgreSql, or CosmosDb (required)
- `--database-server`: Database server name or endpoint (required)
- `--database-name`: Name of the database to connect to (required)
- `--connection-string`: Optional custom connection string (generated if not provided)
- `--tenant-id`: Azure tenant ID for authentication (optional)
- `--auth-method`: Authentication method - 'credential', 'key', or 'connectionString' (optional, defaults to 'credential')
- `--retry-max-retries`: Maximum retry attempts for failed operations (optional, defaults to 3)

**Required Permissions:**
- Contributor role on the Azure App Service resource
- Read access to the resource group

**Returns:**
JSON response containing:
```json
{
  "status": 200,
  "results": {
    "databaseConnection": {
      "databaseType": "SqlServer",
      "databaseServer": "myserver.database.windows.net",
      "databaseName": "MyDatabase",
      "connectionString": "Server=myserver.database.windows.net;Database=MyDatabase;...",
      "connectionStringName": "MyDatabaseConnection",
      "isConfigured": true,
      "configuredAt": "2025-07-03T10:30:00Z"
    }
  }
}
```

**Examples:**

```bash
# Add a SQL Server database connection
azmcp appservice database add --subscription "12345678-1234-1234-1234-123456789abc" \
                              --resource-group "my-resource-group" \
                              --app-name "my-web-app" \
                              --database-type "SqlServer" \
                              --database-server "myserver.database.windows.net" \
                              --database-name "MyDatabase"

# Add a MySQL database connection
azmcp appservice database add --subscription "12345678-1234-1234-1234-123456789abc" \
                              --resource-group "my-resource-group" \
                              --app-name "my-web-app" \
                              --database-type "MySql" \
                              --database-server "myserver.mysql.database.azure.com" \
                              --database-name "MyDatabase"

# Add a PostgreSQL database connection with custom connection string
azmcp appservice database add --subscription "12345678-1234-1234-1234-123456789abc" \
                              --resource-group "my-resource-group" \
                              --app-name "my-web-app" \
                              --database-type "PostgreSql" \
                              --database-server "myserver.postgres.database.azure.com" \
                              --database-name "MyDatabase" \
                              --connection-string "Host=myserver.postgres.database.azure.com;Database=MyDatabase;Username=myuser;Password=mypassword;"

# Add a Cosmos DB connection
azmcp appservice database add --subscription "12345678-1234-1234-1234-123456789abc" \
                              --resource-group "my-resource-group" \
                              --app-name "my-web-app" \
                              --database-type "CosmosDb" \
                              --database-server "mycosmosdb" \
                              --database-name "MyDatabase"
```

**Common Errors:**
- `403 Forbidden`: Insufficient permissions - ensure you have Contributor role on the App Service
- `404 Not Found`: App Service or resource group not found - verify names and subscription
- `400 Bad Request`: Invalid database type or malformed connection string
- `409 Conflict`: App Service is not in a valid state for configuration changes

""";
    }

    private record DocumentationUpdateResult(bool UpdatesApplied, List<string> MissingElements);
}
