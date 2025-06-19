// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Search.Commands.Service;
using AzureMcp.Search.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Search.UnitTests.Service;

public class ServiceListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISearchService _searchService;
    private readonly ILogger<ServiceListCommand> _logger;
    private readonly ServiceListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";

    public ServiceListCommandTests()
    {
        _searchService = Substitute.For<ISearchService>();
        _logger = Substitute.For<ILogger<ServiceListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_searchService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsServices_WhenServicesExist()
    {
        // Arrange
        var expectedServices = new List<string> { "service1", "service2" };
        _searchService.ListServices(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedServices);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ServiceListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedServices, result.Services);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoServices()
    {
        // Arrange
        _searchService.ListServices(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(new List<string>());

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ServiceListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Services);
        Assert.Empty(result.Services);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";

        _searchService.ListServices(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class ServiceListResult
    {
        [JsonPropertyName("services")]
        public List<string> Services { get; set; } = [];
    }
}
