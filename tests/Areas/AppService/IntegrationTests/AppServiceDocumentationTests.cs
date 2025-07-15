// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Text.Json;

namespace AzureMcp.Tests.Areas.AppService.IntegrationTests;

[Collection("IntegrationTests")]
public class AppServiceDocumentationTests
{
    [Fact]
    [Trait("Category", "Documentation")]
    public async Task AppServiceCommand_ShowsInHelpOutput()
    {
        // Arrange
        var startInfo = new ProcessStartInfo
        {
            FileName = "azmcp",
            Arguments = "appservice --help",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Act
        using var process = Process.Start(startInfo);
        if (process != null)
        {
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            // Assert
            Assert.Contains("database", output);
            Assert.Contains("App Service operations", output);
            Assert.Empty(error);
        }
        else
        {
            Assert.True(false, "Failed to start azmcp process");
        }
    }

    [Fact]
    [Trait("Category", "Documentation")]
    public async Task AppServiceDatabaseCommand_ShowsInHelpOutput()
    {
        // Arrange
        var startInfo = new ProcessStartInfo
        {
            FileName = "azmcp",
            Arguments = "appservice database --help",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Act
        using var process = Process.Start(startInfo);
        if (process != null)
        {
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            // Assert
            Assert.Contains("add", output);
            Assert.Contains("database operations", output);
            Assert.Empty(error);
        }
        else
        {
            Assert.True(false, "Failed to start azmcp process");
        }
    }

    [Fact]
    [Trait("Category", "Documentation")]
    public async Task AppServiceDatabaseAddCommand_ShowsCorrectHelpOutput()
    {
        // Arrange
        var startInfo = new ProcessStartInfo
        {
            FileName = "azmcp",
            Arguments = "appservice database add --help",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Act
        using var process = Process.Start(startInfo);
        if (process != null)
        {
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            // Assert
            Assert.Contains("--app-name", output);
            Assert.Contains("--resource-group", output);
            Assert.Contains("--database-type", output);
            Assert.Contains("--database-server", output);
            Assert.Contains("--database-name", output);
            Assert.Contains("--connection-string", output);
            Assert.Contains("Add Database to App Service", output);
            Assert.Empty(error);
        }
        else
        {
            Assert.True(false, "Failed to start azmcp process");
        }
    }

    [Fact]
    [Trait("Category", "Documentation")]
    public void DocumentationFile_ContainsAppServiceCommandReference()
    {
        // Arrange
        var documentationPath = Path.Combine(GetProjectRoot(), "docs", "azmcp-commands.md");
        
        // Act
        var content = File.ReadAllText(documentationPath);
        
        // Assert
        Assert.Contains("### Azure App Service Operations", content);
        Assert.Contains("azmcp appservice database add", content);
        
        // Verify all required parameters are documented
        var requiredParameters = new[]
        {
            "--subscription",
            "--resource-group", 
            "--app-name",
            "--database-type",
            "--database-server",
            "--database-name"
        };
        
        foreach (var parameter in requiredParameters)
        {
            Assert.Contains(parameter, content);
        }
        
        // Verify optional parameters are documented
        Assert.Contains("--connection-string", content);
        
        // Verify examples are present
        Assert.Contains("SqlServer", content);
        Assert.Contains("PostgreSql", content);
    }
    
    [Fact]
    [Trait("Category", "Documentation")]
    public void DocumentationFile_AppServiceSectionIsWellFormed()
    {
        // Arrange
        var documentationPath = Path.Combine(GetProjectRoot(), "docs", "azmcp-commands.md");
        var content = File.ReadAllText(documentationPath);
        
        // Extract App Service section
        var sectionStart = content.IndexOf("### Azure App Service Operations");
        var nextSectionStart = content.IndexOf("## Response Format", sectionStart);
        var section = content.Substring(sectionStart, nextSectionStart - sectionStart);
        
        // Assert proper formatting
        Assert.Contains("```bash", section);
        Assert.Contains("**Parameters:**", section);
        Assert.Contains("**Example:**", section);
        
        // Verify parameter descriptions
        Assert.Contains("- `--app-name`: Name of the Azure App Service application", section);
        Assert.Contains("- `--database-type`: Type of database (SqlServer, MySql, PostgreSql, CosmosDb)", section);
        
        // Verify examples are complete commands
        Assert.Contains("azmcp appservice database add --subscription", section);
        Assert.Contains("12345678-1234-1234-1234-123456789abc", section);
    }

    private static string GetProjectRoot()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        while (currentDirectory != null && !File.Exists(Path.Combine(currentDirectory, "AzureMcp.sln")))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }
        return currentDirectory ?? throw new DirectoryNotFoundException("Could not find project root");
    }
}
