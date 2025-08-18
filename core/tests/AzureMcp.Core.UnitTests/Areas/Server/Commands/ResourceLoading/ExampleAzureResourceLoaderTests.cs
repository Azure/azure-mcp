// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Areas.Server.Commands.ResourceLoading;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Core.UnitTests.Areas.Server.Commands.ResourceLoading;

public class ExampleAzureResourceLoaderTests
{
    private readonly ILogger<ExampleAzureResourceLoader> _mockLogger;
    private readonly ExampleAzureResourceLoader _resourceLoader;

    public ExampleAzureResourceLoaderTests()
    {
        _mockLogger = Substitute.For<ILogger<ExampleAzureResourceLoader>>();
        _resourceLoader = new ExampleAzureResourceLoader(_mockLogger);
    }

    private static IMcpServer CreateMockServer()
    {
        return Substitute.For<IMcpServer>();
    }

    [Fact]
    public async Task ListResourcesHandler_ShouldReturnExampleResources()
    {
        // Arrange
        var request = new RequestContext<ListResourcesRequestParams>(CreateMockServer())
        {
            Params = new ListResourcesRequestParams()
        };

        // Act
        var result = await _resourceLoader.ListResourcesHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Resources);
        Assert.Equal(5, result.Resources.Count); // Now we have 5 Azure best practices resources
        
        // Verify the resources contain expected URIs
        var uris = result.Resources.Select(r => r.Uri).ToList();
        Assert.Contains("https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-codegen-best-practices.txt", uris);
        Assert.Contains("https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-deployment-best-practices.txt", uris);
        Assert.Contains("https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-codegen-best-practices.txt", uris);
        Assert.Contains("https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-deployment-best-practices.txt", uris);
        Assert.Contains("https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-swa-best-practices.txt", uris);
        
        // Verify all resources have proper names and descriptions
        Assert.All(result.Resources, r => 
        {
            Assert.NotNull(r.Name);
            Assert.NotNull(r.Title);  
            Assert.NotNull(r.Description);
            Assert.NotEmpty(r.Name);
            Assert.NotEmpty(r.Title);
            Assert.NotEmpty(r.Description);
        });
    }

        [Fact]
    public async Task ReadResourceHandler_WithValidBestPracticesUri_ShouldReturnFileContent()
    {
        // Arrange
        const string uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-codegen-best-practices.txt";
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = uri }
        };

        // Act
        var result = await _resourceLoader.ReadResourceHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Contents);
        Assert.Single(result.Contents);
        
        var content = result.Contents.First() as TextResourceContents;
        Assert.NotNull(content);
        Assert.Equal(uri, content.Uri);
        Assert.NotNull(content.Text);
        Assert.NotEmpty(content.Text);
        
        // Verify it contains some expected content from the best practices file
        Assert.Contains("Azure", content.Text);
        Assert.Contains("Managed Identity", content.Text);
        Assert.Contains("security", content.Text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ReadResourceHandler_WithValidDeploymentBestPracticesUri_ShouldReturnFileContent()
    {
        // Arrange
        const string uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-deployment-best-practices.txt";
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = uri }
        };

        // Act
        var result = await _resourceLoader.ReadResourceHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Contents);
        Assert.Single(result.Contents);
        
        var content = result.Contents.First() as TextResourceContents;
        Assert.NotNull(content);
        Assert.Equal(uri, content.Uri);
        Assert.NotNull(content.Text);
        Assert.NotEmpty(content.Text);
        
        // Verify it contains some expected content from the deployment best practices file
        Assert.Contains("Infrastructure as Code", content.Text);
        Assert.Contains("Bicep", content.Text);
        Assert.Contains("deployment", content.Text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ReadResourceHandler_WithValidFunctionsCodegenBestPracticesUri_ShouldReturnFileContent()
    {
        // Arrange
        const string uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-codegen-best-practices.txt";
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = uri }
        };

        // Act
        var result = await _resourceLoader.ReadResourceHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Contents);
        Assert.Single(result.Contents);
        
        var content = result.Contents.First() as TextResourceContents;
        Assert.NotNull(content);
        Assert.Equal(uri, content.Uri);
        Assert.NotNull(content.Text);
        Assert.NotEmpty(content.Text);
        
        // Verify it contains some expected content from the functions codegen best practices file
        Assert.Contains("Azure Functions", content.Text);
        Assert.Contains("programming model", content.Text, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("extension bundles", content.Text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ReadResourceHandler_WithValidFunctionsDeploymentBestPracticesUri_ShouldReturnFileContent()
    {
        // Arrange
        const string uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-functions-deployment-best-practices.txt";
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = uri }
        };

        // Act
        var result = await _resourceLoader.ReadResourceHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Contents);
        Assert.Single(result.Contents);
        
        var content = result.Contents.First() as TextResourceContents;
        Assert.NotNull(content);
        Assert.Equal(uri, content.Uri);
        Assert.NotNull(content.Text);
        Assert.NotEmpty(content.Text);
        
        // Verify it contains some expected content from the functions deployment best practices file
        Assert.Contains("Azure Functions", content.Text);
        Assert.Contains("Flex Consumption", content.Text, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("hosting plan", content.Text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ReadResourceHandler_WithValidSWABestPracticesUri_ShouldReturnFileContent()
    {
        // Arrange
        const string uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-swa-best-practices.txt";
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = uri }
        };

        // Act
        var result = await _resourceLoader.ReadResourceHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Contents);
        Assert.Single(result.Contents);
        
        var content = result.Contents.First() as TextResourceContents;
        Assert.NotNull(content);
        Assert.Equal(uri, content.Uri);
        Assert.NotNull(content.Text);
        Assert.NotEmpty(content.Text);
        
        // Verify it contains some expected content from the SWA best practices file
        Assert.Contains("Static Web Apps", content.Text);
        Assert.Contains("CLI", content.Text);
        Assert.Contains("staticwebapp", content.Text, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task ReadResourceHandler_WithNullUri_ShouldThrowArgumentException()
    {
        // Arrange
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = null! }
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () => 
            await _resourceLoader.ReadResourceHandler(request, CancellationToken.None));
    }

    [Fact]
    public async Task ReadResourceHandler_WithUnknownUri_ShouldThrowInvalidOperationException()
    {
        // Arrange
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = "https://unknown-resource-uri" }
        };

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(async () => 
            await _resourceLoader.ReadResourceHandler(request, CancellationToken.None));
    }

        [Fact]
    public async Task ReadResourceHandler_WithValidGitHubUri_ShouldReturnFileContent()
    {
        // Arrange
        var uri = "https://github.com/Azure/azure-mcp/blob/main/areas/azurebestpractices/src/AzureMcp.AzureBestPractices/Resources/azure-general-codegen-best-practices.txt";
        var request = new RequestContext<ReadResourceRequestParams>(CreateMockServer())
        {
            Params = new ReadResourceRequestParams { Uri = uri }
        };

        // Act
        var result = await _resourceLoader.ReadResourceHandler(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Contents);
        Assert.Equal(uri, result.Contents[0].Uri);
        
        // Cast to TextResourceContents to access the Text property
        Assert.IsType<TextResourceContents>(result.Contents[0]);
        var textContent = (TextResourceContents)result.Contents[0];
        Assert.NotNull(textContent.Text);
        Assert.Contains("Azure", textContent.Text!);
    }

    [Fact]
    public async Task DisposeAsync_ShouldCompleteSuccessfully()
    {
        // Act & Assert - should not throw
        await _resourceLoader.DisposeAsync();
    }
}
