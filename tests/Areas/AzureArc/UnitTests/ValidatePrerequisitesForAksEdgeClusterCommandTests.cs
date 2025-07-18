using System.CommandLine.Parsing;
using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Arc;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.AzureArc.UnitTests;

[Trait("Area", "AzureArc")]
public sealed class ValidatePrerequisitesForAksEdgeClusterCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IArcServices _arcService;
    private readonly ILogger<ValidatePrerequisitesForAksEdgeClusterCommand> _logger;
    private readonly ValidatePrerequisitesForAksEdgeClusterCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public ValidatePrerequisitesForAksEdgeClusterCommandTests()
    {
        _arcService = Substitute.For<IArcServices>();
        _logger = Substitute.For<ILogger<ValidatePrerequisitesForAksEdgeClusterCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_arcService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("validate-prerequisites-aksee-cluster", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsSuccess()
    {
        // Arrange
        var expectedResult = new DeploymentResult
        {
            Success = true,
            Steps = "AKS Edge Essentials cluster prerequisites validated successfully"
        };

        _arcService.ValidatePrerequisitesForAksEdgeClusterAsync()
            .Returns(expectedResult);

        var args = "--path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Contains("AKS Edge Essentials cluster prerequisites validated successfully", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceReturnsFalse_ReturnsError()
    {
        // Arrange
        var expectedResult = new DeploymentResult
        {
            Success = false,
            Steps = "Failed to validate AKS Edge Essentials cluster prerequisites"
        };

        _arcService.ValidatePrerequisitesForAksEdgeClusterAsync()
            .Returns(expectedResult);

        var args = "--path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Failed to validate AKS Edge Essentials cluster prerequisites", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        var exceptionMessage = "Test exception";
        _arcService.ValidatePrerequisitesForAksEdgeClusterAsync()
            .ThrowsAsync(new Exception(exceptionMessage));

        var args = "--path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains(exceptionMessage, response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceWithCorrectParameters()
    {
        // Arrange
        var userProvidedPath = "/temp/path";
        var expectedResult = new DeploymentResult
        {
            Success = true,
            Steps = "Success"
        };

        _arcService.ValidatePrerequisitesForAksEdgeClusterAsync()
            .Returns(expectedResult);

        var args = $"--path {userProvidedPath}";
        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _arcService.Received(1).ValidatePrerequisitesForAksEdgeClusterAsync();
    }

    [Fact]
    public async Task ExecuteAsync_NoParameters_ReturnsResult()
    {
        // Arrange
        var expectedResult = new DeploymentResult
        {
            Success = true,
            Steps = "Prerequisites validation completed successfully"
        };

        _arcService.ValidatePrerequisitesForAksEdgeClusterAsync()
            .Returns(expectedResult);

        var args = "";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal(expectedResult.Steps, response.Message);
    }
}
