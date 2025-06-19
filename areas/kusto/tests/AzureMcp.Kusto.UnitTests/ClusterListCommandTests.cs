// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Kusto.Commands;
using AzureMcp.Kusto.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Kusto.UnitTests;

public sealed class ClusterListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IKustoService _kustoService;
    private readonly ILogger<ClusterListCommand> _logger;
    private readonly ClusterListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";

    public ClusterListCommandTests()
    {
        _kustoService = Substitute.For<IKustoService>();
        _logger = Substitute.For<ILogger<ClusterListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_kustoService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsClusters_WhenClustersExist()
    {
        // Arrange
        var expectedClusters = new List<string> { "clusterA", "clusterB" };
        _kustoService.ListClusters(
            Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedClusters);

        var args = _parser.Parse(["--subscription", _knownSubscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ClusterListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedClusters, result.Clusters);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoClustersExist()
    {
        // Arrange
        _kustoService.ListClusters(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns([]);

        var args = _parser.Parse(["--subscription", _knownSubscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ClusterListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Clusters);
        Assert.Empty(result.Clusters);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException_AndSetsException()
    {
        // Arrange
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";

        // Arrange
        _kustoService.ListClusters(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<List<string>>(new Exception("Test error")));

        var args = _parser.Parse(["--subscription", _knownSubscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    private sealed class ClusterListResult
    {
        [JsonPropertyName("clusters")]
        public List<string> Clusters { get; set; } = [];
    }
}
