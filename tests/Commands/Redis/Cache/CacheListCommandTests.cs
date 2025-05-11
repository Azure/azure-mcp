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
using CacheForRedis = AzureMcp.Models.Redis.CacheForRedis;

namespace AzureMcp.Tests.Commands.Redis.Cache;

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
        //var expectedCaches = new[] { "cache1", "cache2" };
        var expectedCaches = new CacheForRedis.Cache[] { new() { Name = "cache1" }, new() { Name = "cache2" } };
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
        Assert.True(result.Equals(expectedCaches));
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
        var args = command.GetCommand().Parse(new string[]
        {
            missingParameter == "--subscription" ? "" : "--subscription", "sub123",
        });

        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing required arguments: {missingParameter.TrimStart('-')}", response.Message);
    }

    private class CacheListResult
    {
        public IEnumerable<CacheForRedis.Cache> Caches { get; set; } = [];

        // Only checks that cache names match
        public override bool Equals(object? obj)
        {
            var other = obj as IEnumerable<CacheForRedis.Cache>;

            if (other is null)
            {
                return false;
            }

            if (Caches.Count() != other.Count())
            {
                return false;
            }

            foreach (var cache in Caches)
            {
                if (!other.Any(otherCache => otherCache.Name == cache.Name))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

}
