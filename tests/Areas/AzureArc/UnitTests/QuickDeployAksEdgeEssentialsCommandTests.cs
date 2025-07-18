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
public sealed class QuickDeployAksEdgeEssentialsCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IArcServices _arcService;
    private readonly ILogger<QuickDeployAksEdgeEssentialsCommand> _logger;
    private readonly QuickDeployAksEdgeEssentialsCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public QuickDeployAksEdgeEssentialsCommandTests()
    {
        _arcService = Substitute.For<IArcServices>();
        _logger = Substitute.For<ILogger<QuickDeployAksEdgeEssentialsCommand>>();

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
        Assert.Equal("quick-deploy-aks-edge-essentials", command.Name);
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
            Steps = "AKS Edge Essentials deployed successfully"
        };

        _arcService.QuickDeployAksEdgeEssentialsAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(expectedResult);

        var args = "--cluster-name test-cluster --resource-group-name test-rg --subscription-id sub123 --tenant-id tenant123 --location eastus --user-provided-path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Contains("AKS Edge Essentials deployed successfully", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceReturnsFalse_ReturnsError()
    {
        // Arrange
        var expectedResult = new DeploymentResult
        {
            Success = false,
            Steps = "Failed to deploy AKS Edge Essentials"
        };

        _arcService.QuickDeployAksEdgeEssentialsAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(expectedResult);

        var args = "--cluster-name test-cluster --resource-group-name test-rg --subscription-id sub123 --tenant-id tenant123 --location eastus --user-provided-path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Failed to deploy AKS Edge Essentials", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        var exceptionMessage = "Test exception";
        _arcService.QuickDeployAksEdgeEssentialsAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .ThrowsAsync(new Exception(exceptionMessage));

        var args = "--cluster-name test-cluster --resource-group-name test-rg --subscription-id sub123 --tenant-id tenant123 --location eastus --user-provided-path /temp";
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
        var clusterName = "test-cluster";
        var resourceGroupName = "test-rg";
        var subscriptionId = "sub123";
        var tenantId = "tenant123";
        var location = "eastus";
        var userProvidedPath = "/temp/path";

        var expectedResult = new DeploymentResult
        {
            Success = true,
            Steps = "Success"
        };

        _arcService.QuickDeployAksEdgeEssentialsAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(expectedResult);

        var args = $"--cluster-name {clusterName} --resource-group-name {resourceGroupName} --subscription-id {subscriptionId} --tenant-id {tenantId} --location {location} --user-provided-path {userProvidedPath}";
        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _arcService.Received(1).QuickDeployAksEdgeEssentialsAsync(
            clusterName, resourceGroupName, subscriptionId, tenantId, location, userProvidedPath);
    }

    [Fact]
    public async Task ExecuteAsync_MissingParameters_ThrowsException()
    {
        // Arrange
        var args = "";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
    }
}
