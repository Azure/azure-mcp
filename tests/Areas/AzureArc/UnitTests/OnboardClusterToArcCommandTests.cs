// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.AzureArc.Services;
using AzureMcp.Commands.Arc;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.AzureArc.UnitTests;

[Trait("Area", "AzureArc")]
public sealed class OnboardClusterToArcCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IArcServices _arcService;
    private readonly ILogger<OnboardClusterToArcCommand> _logger;
    private readonly OnboardClusterToArcCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public OnboardClusterToArcCommandTests()
    {
        _arcService = Substitute.For<IArcServices>();
        _logger = Substitute.For<ILogger<OnboardClusterToArcCommand>>();

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
        Assert.Equal("onboard-cluster-to-arc", command.Name);
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
            Steps = "Arc Onboarding completed successfully."
        };

        _arcService.OnboardClusterToArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>()).Returns(expectedResult);

        var args = "--cluster-name test-cluster --resource-group-name test-rg --location eastus --subscription-id sub123 --tenant-id tenant123 --kube-config-path /path/to/config --user-provided-path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal(expectedResult.Steps, response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceReturnsFailure_ReturnsError()
    {
        // Arrange
        var expectedResult = new DeploymentResult
        {
            Success = false,
            Steps = "Arc Onboarding failed."
        };

        _arcService.OnboardClusterToArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>()).Returns(expectedResult);

        var args = "--cluster-name test-cluster --resource-group-name test-rg --location eastus --subscription-id sub123 --tenant-id tenant123 --kube-config-path /path/to/config --user-provided-path /temp";
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedResult.Steps, response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsError()
    {
        // Arrange
        var exceptionMessage = "Test exception";
        _arcService.OnboardClusterToArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>()).ThrowsAsync(new Exception(exceptionMessage));

        var args = "--cluster-name test-cluster --resource-group-name test-rg --location eastus --subscription-id sub123 --tenant-id tenant123 --kube-config-path /path/to/config --user-provided-path /temp";
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
        var location = "eastus";
        var subscriptionId = "sub123";
        var tenantId = "tenant123";
        var kubeConfigPath = "/path/to/config";
        var userProvidedPath = "/temp";

        var expectedResult = new DeploymentResult { Success = true, Steps = "Success" };
        _arcService.OnboardClusterToArcAsync(
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
            Arg.Any<string>()).Returns(expectedResult);

        var args = $"--cluster-name {clusterName} --resource-group-name {resourceGroupName} --location {location} --subscription-id {subscriptionId} --tenant-id {tenantId} --kube-config-path {kubeConfigPath} --user-provided-path {userProvidedPath}";
        var parseResult = _parser.Parse(args);

        // Act
        await _command.ExecuteAsync(_context, parseResult);

        // Assert
        await _arcService.Received(1).OnboardClusterToArcAsync(
            clusterName, resourceGroupName, location, subscriptionId, tenantId, kubeConfigPath, userProvidedPath);
    }
}
