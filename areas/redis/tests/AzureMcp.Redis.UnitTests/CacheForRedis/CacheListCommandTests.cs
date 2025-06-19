// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Redis.Commands.CacheForRedis;
using AzureMcp.Redis.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using CacheModel = AzureMcp.Redis.Models.CacheForRedis.Cache;

namespace AzureMcp.Redis.UnitTests.CacheForRedis;

public class CacheListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisService _redisService;
    private readonly ILogger<CacheListCommand> _logger;
    private readonly CacheListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";

    public CacheListCommandTests()
    {
        _redisService = Substitute.For<IRedisService>();
        _logger = Substitute.For<ILogger<CacheListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_redisService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsCaches_WhenCachesExist()
    {
        var expectedCaches = new CacheModel[] { new() { Name = "cache1" }, new() { Name = "cache2" } };
        _redisService.ListCachesAsync(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<Models.AuthMethod>(), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedCaches);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<CacheListResult>(json, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true
        });

        Assert.NotNull(result);
        Assert.Collection(result.Caches,
            item => Assert.Equal("cache1", item.Name),
            item => Assert.Equal("cache2", item.Name));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoCaches()
    {
        _redisService.ListCachesAsync(Arg.Is(_knownSubscriptionId)).Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<CacheListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Caches);
        Assert.Empty(result.Caches);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        _redisService.ListCachesAsync(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<Models.AuthMethod>(), Arg.Any<RetryPolicyOptions>())
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

    private record CacheListResult(IEnumerable<CacheModel> Caches)
    {
        public IEnumerable<CacheModel> Caches { get; set; } = Caches ?? [];
    }
}
