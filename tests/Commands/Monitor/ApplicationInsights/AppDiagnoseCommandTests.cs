// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Commands.Monitor.ApplicationInsights;
using AzureMcp.Models.Command;
using AzureMcp.Services.Azure.Monitor;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Monitor.ApplicationInsights;

public class AppDiagnoseCommandTests
{    private readonly IServiceProvider _serviceProvider;
    private readonly IMcpServer _mcpServer;
    private readonly ILogger<AppDiagnoseCommand> _logger;
    private readonly ISubscriptionService _subscriptionService;
    private readonly ITenantService _tenantService;
    private readonly IResourceGroupService _resourceGroupService;
    private readonly ILogsQueryService _logsQueryService;
    private readonly AppDiagnoseCommand _command;
    private readonly IApplicationInsightsService _applicationInsightsService;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public AppDiagnoseCommandTests()
    {
        _logger = Substitute.For<ILogger<AppDiagnoseCommand>>();
        _mcpServer = Substitute.For<IMcpServer>();
        _subscriptionService = Substitute.For<ISubscriptionService>();
        _tenantService = Substitute.For<ITenantService>();
        _resourceGroupService = Substitute.For<IResourceGroupService>();
        _logsQueryService = Substitute.For<ILogsQueryService>();
        
        // Use the real ApplicationInsightsService implementation with mocked dependencies
        _applicationInsightsService = new ApplicationInsightsService(_subscriptionService, _tenantService, _logsQueryService);

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton(_logger);
        serviceCollection.AddSingleton(_mcpServer);
        serviceCollection.AddSingleton(_subscriptionService);
        serviceCollection.AddSingleton(_resourceGroupService);
        serviceCollection.AddSingleton(_tenantService);
        serviceCollection.AddSingleton<ILogsQueryService>(_logsQueryService);
        serviceCollection.AddSingleton<IApplicationInsightsService>(_applicationInsightsService);
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
        _context = new CommandContext(_serviceProvider);
        _command = new AppDiagnoseCommand(_logger);
        _parser = new Parser(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_WithValidOptions_ReturnsResult()
    {
        // Arrange
        var subscription = "test-sub";
        var appId = "/subscriptions/123/resourceGroups/test/providers/microsoft.insights/components/testapp";
        var startTime = "2025-01-01T00:00:00Z";
        var endTime = "2025-01-02T00:00:00Z";
        var symptoms = "Requests are failing with 500 errors";
        
        var parseResult = _parser.Parse($"--subscription {subscription} --app-id {appId} --start-time {startTime} --end-time {endTime} --symptoms \"{symptoms}\"");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_WithMissingRequiredOptions_ReturnsValidationError()
    {
        // Arrange
        var parseResult = _parser.Parse("--subscription test-sub");

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("Required option", response.Message);
    }
}
