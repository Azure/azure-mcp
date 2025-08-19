// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Deploy.Services.Templates;
using Xunit;

namespace AzureMcp.Deploy.Services.Templates;

public sealed class TemplateServiceTests
{
    [Fact]
    public void LoadTemplate_ValidTemplate_ReturnsContent()
    {
        // Arrange
        var templateName = "Plan/deployment-plan-base";

        // Act
        var result = TemplateService.LoadTemplate(templateName);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Contains("{{Title}}", result);
        Assert.Contains("{{ProjectName}}", result);
    }

    [Fact]
    public void LoadTemplate_InvalidTemplate_ThrowsFileNotFoundException()
    {
        // Arrange
        var templateName = "Plan/non-existent-template";

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => TemplateService.LoadTemplate(templateName));
    }

    [Fact]
    public void ProcessTemplate_WithReplacements_ReplacesPlaceholders()
    {
        // Arrange
        var templateName = "Plan/deployment-plan-base";
        var replacements = new Dictionary<string, string>
        {
            { "Title", "Test Deployment Plan" },
            { "ProjectName", "TestProject" },
            { "ProvisioningTool", "AZD" }
        };

        // Act
        var result = TemplateService.ProcessTemplate(templateName, replacements);

        // Assert
        Assert.Contains("Test Deployment Plan", result);
        Assert.Contains("TestProject", result);
        Assert.Contains("AZD", result);
        Assert.DoesNotContain("{{Title}}", result);
        Assert.DoesNotContain("{{ProjectName}}", result);
        Assert.DoesNotContain("{{ProvisioningTool}}", result);
    }

    [Fact]
    public void ProcessTemplateContent_WithReplacements_ReplacesPlaceholders()
    {
        // Arrange
        var templateContent = "Hello {{Name}}, welcome to {{Project}}!";
        var replacements = new Dictionary<string, string>
        {
            { "Name", "John" },
            { "Project", "Azure MCP" }
        };

        // Act
        var result = TemplateService.ProcessTemplateContent(templateContent, replacements);

        // Assert
        Assert.Equal("Hello John, welcome to Azure MCP!", result);
    }

}
