using AzureMcp.Areas.AiFoundry.Models;
using AzureMcp.Areas.AiFoundry.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.AiFoundry.UnitTests;

public class AiFoundryServiceTests
{
    private readonly ILogger<AiFoundryService> _mockLogger;
    private readonly AiFoundryService _service;

    public AiFoundryServiceTests()
    {
        _mockLogger = Substitute.For<ILogger<AiFoundryService>>();
        _service = new AiFoundryService(_mockLogger);
    }

    [Fact]
    public async Task ListDatasetsAsync_ReturnsDatasetCollection()
    {
        // Arrange
        var projectEndpoint = "https://test-project.cognitiveservices.azure.com";

        // Act
        var result = await _service.ListDatasetsAsync(projectEndpoint);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Fact]
    public async Task GetDatasetAsync_WithValidId_ReturnsDataset()
    {
        // Arrange
        var projectEndpoint = "https://test-project.cognitiveservices.azure.com";
        var datasetId = "dataset-1";

        // Act
        var result = await _service.GetDatasetAsync(projectEndpoint, datasetId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(datasetId, result.Id);
    }

    [Fact]
    public async Task GetDatasetAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var projectEndpoint = "https://test-project.cognitiveservices.azure.com";
        var datasetId = "nonexistent-dataset";

        // Act
        var result = await _service.GetDatasetAsync(projectEndpoint, datasetId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task ListVectorStoresAsync_ReturnsVectorStoreCollection()
    {
        // Arrange
        var projectEndpoint = "https://test-project.cognitiveservices.azure.com";

        // Act
        var result = await _service.ListVectorStoresAsync(projectEndpoint);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Any());
    }

    [Fact]
    public async Task GetVectorStoreAsync_WithValidId_ReturnsVectorStore()
    {
        // Arrange
        var projectEndpoint = "https://test-project.cognitiveservices.azure.com";
        var vectorStoreId = "vectorstore-1";

        // Act
        var result = await _service.GetVectorStoreAsync(projectEndpoint, vectorStoreId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(vectorStoreId, result.Id);
    }

    [Fact]
    public async Task GetVectorStoreAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var projectEndpoint = "https://test-project.cognitiveservices.azure.com";
        var vectorStoreId = "nonexistent-vectorstore";

        // Act
        var result = await _service.GetVectorStoreAsync(projectEndpoint, vectorStoreId);

        // Assert
        Assert.Null(result);
    }
} 