// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

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
public sealed class RemoveAksEdgeCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IArcServices _arcService;
    private readonly ILogger<RemoveAksEdgeCommand> _logger;
    private readonly RemoveAksEdgeCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public RemoveAksEdgeCommandTests()
    {
        _arcService = Substitute.For<IArcServices>();
        _logger = Substitute.For<ILogger<RemoveAksEdgeCommand>>();

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
        Assert.Equal("remove-Aks-Edge-installation", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_ValidParameters_ReturnsSuccess()
    {
        // Arrange
        _arcService.RemoveAksEdgeAsync(Arg.Any<string>())
            .Returns(true);

        var args = "--path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Contains("AKS Edge Essentials removed successfully", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceReturnsFalse_ReturnsError()
    {
        // Arrange
        _arcService.RemoveAksEdgeAsync(Arg.Any<string>())
            .Returns(false);

        var args = "--path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Failed to remove AKS Edge Essentials", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        var exceptionMessage = "Test exception";
        _arcService.RemoveAksEdgeAsync(Arg.Any<string>())
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
        _arcService.RemoveAksEdgeAsync(Arg.Any<string>())
            .Returns(true);

        var args = $"--path {userProvidedPath}";
        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _arcService.Received(1).RemoveAksEdgeAsync(userProvidedPath);
    }

    [Fact]
    public async Task ExecuteAsync_MissingPath_ThrowsArgumentException()
    {
        // Arrange
        var args = "";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("path parameter is required", response.Message);
    }
}
