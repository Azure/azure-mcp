// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Commands.Subscription;
using AzureMcp.Models;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using NSubstitute;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using Xunit;

namespace AzureMcp.Tests.Commands.Subscription;

public class SubscriptionListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMcpServer _mcpServer;
    private readonly ILogger<SubscriptionListCommand> _logger;
    private readonly ISubscriptionService _subscriptionService;
    private readonly SubscriptionListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public SubscriptionListCommandTests()
    {
        _mcpServer = Substitute.For<IMcpServer>();
        _subscriptionService = Substitute.For<ISubscriptionService>();
        _logger = Substitute.For<ILogger<SubscriptionListCommand>>();
        var collection = new ServiceCollection()
            .AddSingleton(_mcpServer)
            .AddSingleton(_subscriptionService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_NoParameters_ReturnsSubscriptions()
    {
        // Arrange
        var expectedSubscriptions = new List<ArgumentOption>
        {
            new() { Id = "sub1", Name = "Subscription 1" },
            new() { Id = "sub2", Name = "Subscription 2" }
        };

        _subscriptionService
            .GetSubscriptions(Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
            .Returns(expectedSubscriptions);

        var args = _parser.Parse("");

        // Act
        var result = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);
        await _subscriptionService.Received(1).GetSubscriptions(Arg.Any<string>(), Arg.Any<RetryPolicyArguments>());
    }

    [Fact]
    public async Task ExecuteAsync_WithTenantId_PassesTenantToService()
    {
        // Arrange
        var tenantId = "test-tenant-id";
        var args = _parser.Parse($"--tenant {tenantId}");

        _subscriptionService
            .GetSubscriptions(Arg.Is<string>(x => x == tenantId), Arg.Any<RetryPolicyArguments>())
            .Returns([new() { Id = "sub1", Name = "Sub1" }]);

        // Act
        var result = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        await _subscriptionService.Received(1).GetSubscriptions(
            Arg.Is<string>(x => x == tenantId),
            Arg.Any<RetryPolicyArguments>());
    }

    [Fact]
    public async Task ExecuteAsync_EmptySubscriptionList_ReturnsNullResults()
    {
        // Arrange
        _subscriptionService
            .GetSubscriptions(Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
            .Returns([]);

        var args = _parser.Parse("");

        // Act
        var result = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.Null(result.Results);
    }

    [Fact]
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsErrorInResponse()
    {
        // Arrange
        var expectedError = "Test error message";
        _subscriptionService
            .GetSubscriptions(Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
            .Returns(Task.FromException<List<ArgumentOption>>(new Exception(expectedError)));

        var args = _parser.Parse("");

        // Act
        var result = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(500, result.Status);
        Assert.Contains(expectedError, result.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithAuthMethod_PassesAuthMethodToCommand()
    {
        // Arrange
        var authMethod = AuthMethod.Credential.ToString().ToLowerInvariant();
        var args = _parser.Parse($"--auth-method {authMethod}");

        _subscriptionService
            .GetSubscriptions(Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
            .Returns([new() { Id = "sub1", Name = "Sub1" }]);

        // Act
        var result = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        await _subscriptionService.Received(1).GetSubscriptions(
            Arg.Any<string>(),
            Arg.Any<RetryPolicyArguments>());
    }

    [Fact]
    public async Task ExecuteAsync_GetBySubscriptionId_ReturnsMatchingSubscription()
    {
        // Arrange
        var expectedSubscriptionId = "test-subscription-id";
        var expectedSubscriptions = new List<ArgumentOption>
        {
            new() { Id = expectedSubscriptionId, Name = "Test Subscription" }
        };

        _subscriptionService
            .GetSubscriptions(Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
            .Returns(expectedSubscriptions);

        var args = _parser.Parse($"--subscription {expectedSubscriptionId}");

        // Act
        var result = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);

        var json = JsonSerializer.Serialize(result.Results);
        var jsonDoc = JsonDocument.Parse(json);
        var subscriptions = jsonDoc.RootElement
            .GetProperty("subscriptions")
            .Deserialize<List<ArgumentOption>>();

        Assert.NotNull(subscriptions);
        Assert.Single(subscriptions);
        Assert.Equal(expectedSubscriptionId, subscriptions[0].Id);
    }

    [Fact]
    public async Task ExecuteAsync_GetBySubscriptionName_ReturnsMatchingSubscription()
    {
        // Arrange
        var expectedSubscriptionName = "Test Subscription";
        var expectedSubscriptions = new List<ArgumentOption>
        {
            new() { Id = "test-subscription-id", Name = expectedSubscriptionName }
        };

        _subscriptionService
            .GetSubscriptions(Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
            .Returns(expectedSubscriptions);

        var args = _parser.Parse($"--subscription {expectedSubscriptionName}");

        // Act
        var result = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);

        var json = JsonSerializer.Serialize(result.Results);
        var jsonDoc = JsonDocument.Parse(json);
        var subscriptions = jsonDoc.RootElement
            .GetProperty("subscriptions")
            .Deserialize<List<ArgumentOption>>();

        Assert.NotNull(subscriptions);
        Assert.Single(subscriptions);
        Assert.Equal(expectedSubscriptionName, subscriptions[0].Name);
    }
}