using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Arguments;
using AzureMcp.Commands.Redis.CacheForRedis;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using CacheModel = AzureMcp.Models.Redis.CacheForRedis.Cache;

namespace AzureMcp.Tests.Commands.Redis.CacheForRedis;

public class CacheListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IRedisService _redisService;
    private readonly ILogger<CacheListCommand> _logger;

    public CacheListCommandTests()
    {
        _redisService = Substitute.For<IRedisService>();
        _logger = Substitute.For<ILogger<CacheListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_redisService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsCaches_WhenCachesExist()
    {
        var expectedCaches = new CacheModel[] { new() { Name = "cache1" }, new() { Name = "cache2" } };
        _redisService.ListCachesAsync("sub123", Arg.Any<string>(), Arg.Any<Models.AuthMethod>(), Arg.Any<RetryPolicyArguments>())
            .Returns(expectedCaches);

        var command = new CacheListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123"]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

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
    public async Task ExecuteAsync_ReturnsNull_WhenNoCaches()
    {
        _redisService.ListCachesAsync("sub123").Returns([]);

        var command = new CacheListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(["--subscription", "sub123"]);
        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        _redisService.ListCachesAsync("sub123", Arg.Any<string>(), Arg.Any<Models.AuthMethod>(), Arg.Any<RetryPolicyArguments>())
            .ThrowsAsync(new Exception("Test error"));

        var command = new CacheListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(["--subscription", "sub123"]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--subscription")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var command = new CacheListCommand(_logger);
        var args = command.GetCommand().Parse(
        [
            missingParameter == "--subscription" ? "" : "--subscription", "sub123",
        ]);

        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing required arguments: {missingParameter.TrimStart('-')}", response.Message);
    }

    private record CacheListResult(IEnumerable<CacheModel> Caches);
}
