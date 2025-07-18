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
public sealed class DisconnectFromAzureArcCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IArcServices _arcService;
    private readonly ILogger<DisconnectFromAzureArcCommand> _logger;
    private readonly DisconnectFromAzureArcCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public DisconnectFromAzureArcCommandTests()
    {
        _arcService = Substitute.For<IArcServices>();
        _logger = Substitute.For<ILogger<DisconnectFromAzureArcCommand>>();

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
        Assert.Equal("disconnect-from-azure-arc", command.Name);
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
            Steps = "Arc disconnection completed successfully."
        };

        _arcService.DisconnectFromAzureArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(expectedResult);

        var args = "--resource-group-name test-rg --cluster-name test-cluster --user-provided-path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceReturnsFailure_ReturnsError()
    {
        // Arrange
        var expectedResult = new DeploymentResult
        {
            Success = false,
            Steps = "Arc disconnection failed."
        };

        _arcService.DisconnectFromAzureArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(expectedResult);

        var args = "--resource-group-name test-rg --cluster-name test-cluster --user-provided-path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);

    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        var exceptionMessage = "Test exception";
        _arcService.DisconnectFromAzureArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .ThrowsAsync(new Exception(exceptionMessage));

        var args = "--resource-group-name test-rg --cluster-name test-cluster --user-provided-path /temp";
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
        var resourceGroupName = "test-rg";
        var clusterName = "test-cluster";
        var userProvidedPath = "/temp/path";

        var expectedResult = new DeploymentResult { Success = true, Steps = "Success" };
        _arcService.DisconnectFromAzureArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(expectedResult);

        var args = $"--resource-group-name {resourceGroupName} --cluster-name {clusterName} --user-provided-path {userProvidedPath}";
        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _arcService.Received(1).DisconnectFromAzureArcAsync(
            resourceGroupName, clusterName, userProvidedPath);
    }
}
