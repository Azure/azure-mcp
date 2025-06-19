// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Redis.Commands.ManagedRedis;
using AzureMcp.Redis.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using ClusterModel = AzureMcp.Redis.Models.ManagedRedis.Cluster;

namespace AzureMcp.Redis.UnitTests.ManagedRedis;

public class ClusterListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisService _redisService;
    private readonly ILogger<ClusterListCommand> _logger;
    private readonly ClusterListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";

    public ClusterListCommandTests()
    {
        _redisService = Substitute.For<IRedisService>();
        _logger = Substitute.For<ILogger<ClusterListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_redisService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsClusters_WhenClustersExist()
    {
        var expectedClusters = new ClusterModel[] { new() { Name = "cluster1" }, new() { Name = "cluster2" } };
        _redisService.ListClustersAsync(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<Models.AuthMethod>(), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedClusters);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ClusterListResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Collection(result.Clusters,
            item => Assert.Equal("cluster1", item.Name),
            item => Assert.Equal("cluster2", item.Name));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoClusters()
    {
        _redisService.ListClustersAsync(Arg.Is(_knownSubscriptionId)).Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<ClusterListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Clusters);
        Assert.Empty(result.Clusters);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        _redisService.ListClustersAsync(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<Models.AuthMethod>(), Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception("Test error"));

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var args = _parser.Parse(
        [
            missingParameter == "--subscription" ? "" : "--subscription", _knownSubscriptionId,
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }

    private record ClusterListResult(IEnumerable<ClusterModel> Clusters)
    {
        public IEnumerable<ClusterModel> Clusters { get; set; } = Clusters ?? [];
    }
}
