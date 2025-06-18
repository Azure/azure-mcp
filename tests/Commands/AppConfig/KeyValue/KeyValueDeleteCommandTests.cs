// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Commands.AppConfig.KeyValue;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Commands.AppConfig.KeyValue;

public class KeyValueDeleteCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<KeyValueDeleteCommand> _logger;

    public KeyValueDeleteCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<KeyValueDeleteCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appConfigService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_DeletesKeyValue_WhenValidParametersProvided()
    {
        // Arrange
        var command = new KeyValueDeleteCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "my-key"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).DeleteKeyValue(
            "account1",
            "my-key",
            "sub123",
            null,
            Arg.Any<RetryPolicyOptions>(), null);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueDeleteResult>(json);

        Assert.NotNull(result);
        Assert.Equal("my-key", result.Key);
    }

    [Fact]
    public async Task ExecuteAsync_DeletesKeyValueWithLabel_WhenLabelProvided()
    {
        // Arrange
        var command = new KeyValueDeleteCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "my-key",
            "--label", "prod"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).DeleteKeyValue(
            "account1",
            "my-key",
            "sub123", null,
            Arg.Any<RetryPolicyOptions>(),
            "prod");

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueDeleteResult>(json);

        Assert.NotNull(result);
        Assert.Equal("my-key", result.Key);
        Assert.Equal("prod", result.Label);
    }

    [Fact]
    public async Task ExecuteAsync_Returns500_WhenServiceThrowsException()
    {
        // Arrange
        _appConfigService.DeleteKeyValue(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<string>())
            .Returns(Task.FromException<bool>(new Exception("Failed to delete key-value")));

        var command = new KeyValueDeleteCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "my-key"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Failed to delete key-value", response.Message);
    }
    [Theory]
    [InlineData("--account-name", "account1", "--key", "my-key")] // Missing subscription
    [InlineData("--subscription", "sub123", "--key", "my-key")] // Missing account-name
    [InlineData("--subscription", "sub123", "--account-name", "account1")] // Missing key
    public async Task ExecuteAsync_Returns400_WhenRequiredParametersAreMissing(params string[] args)
    {
        // Arrange
        var command = new KeyValueDeleteCommand(_logger);
        var parseResult = command.GetCommand().Parse(args);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    private sealed class KeyValueDeleteResult
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("label")]
        public string? Label { get; set; }
    }
}
