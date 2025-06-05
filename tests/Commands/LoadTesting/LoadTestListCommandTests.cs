using System.CommandLine;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Commands.LoadTesting.LoadTest;
using AzureMcp.Models.Command;
using AzureMcp.Models.LoadTesting;
using AzureMcp.Options;
using AzureMcp.Options.LoadTesting.LoadTest;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
public class LoadTestListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILoadTestingService _service;
    private readonly ILogger<LoadTestListCommand> _logger;
    private readonly LoadTestListCommand _command;

    public LoadTestListCommandTests()
    {
        _service = Substitute.For<ILoadTestingService>();
        _logger = Substitute.For<ILogger<LoadTestListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_service);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsLoadTests_WhenLoadTestsExist()
    {
        // Arrange
        var expectedLoadTests = new List<LoadTestResource> { new LoadTestResource { Id = "Id1", Name = "loadTest1" }, new LoadTestResource { Id = "Id2", Name = "loadTest2" } };
        _service.GetLoadTestsForSubscriptionAsync(Arg.Is("sub123"), Arg.Is("tenant123"), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedLoadTests);

        var command = new LoadTestListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--tenant", "tenant123"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<LoadTestListCommandResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedLoadTests.Count, result.LoadTests.Count);
        Assert.Collection(result.LoadTests,
            item => Assert.Equal("Id1", item.Id),
            item => Assert.Equal("loadTest2", item.Name));
    }


    [Fact]
    public async Task ExecuteAsync_ReturnsLoadTests_WhenLoadTestsNotExist()
    {
        // Arrange
        _service.GetLoadTestsForSubscriptionAsync(Arg.Is("sub123"), Arg.Is("tenant123"), Arg.Any<RetryPolicyOptions>())
             .Returns([]);

        var command = new LoadTestListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--tenant", "tenant123"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _service.GetLoadTestsForSubscriptionAsync(Arg.Is("sub123"), Arg.Is("tenant123"), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<List<LoadTestResource>>(new Exception("Test error")));

        var context = new CommandContext(_serviceProvider);
        var parseResult = _command.GetCommand().Parse(["--subscription", "sub123", "--tenant", "tenant123"]);

        // Act
        var response = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    private class LoadTestListCommandResult
    {
        public List<LoadTestResource> LoadTests { get; set; } = [];
    }
}
