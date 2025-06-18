// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Commands.AppConfig.KeyValue;
using AzureMcp.Models.AppConfig;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Commands.AppConfig.KeyValue;

public class KeyValueUnlockCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<KeyValueUnlockCommand> _logger;

    public KeyValueUnlockCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<KeyValueUnlockCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appConfigService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_UnlocksKeyValue_WhenValidParametersProvided()
    {
        // Arrange
        var command = new KeyValueUnlockCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "mykey"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).UnlockKeyValue(
            "account1",
            "mykey",
            "sub123",
            null,
            Arg.Any<RetryPolicyOptions>(),
            null);
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueUnlockResult>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        Assert.NotNull(result);
        Assert.Equal("mykey", result.Key);
    }

    [Fact]
    public async Task ExecuteAsync_UnlocksKeyValueWithLabel_WhenLabelProvided()
    {
        // Arrange
        var command = new KeyValueUnlockCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "mykey",
            "--label", "prod"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).UnlockKeyValue(
            "account1",
            "mykey",
            "sub123",
            null,
            Arg.Any<RetryPolicyOptions>(),
            "prod");
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueUnlockResult>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        Assert.NotNull(result);
        Assert.Equal("mykey", result.Key);
        Assert.Equal("prod", result.Label);
    }

    [Fact]
    public async Task ExecuteAsync_Returns500_WhenServiceThrowsException()
    {
        // Arrange
        _appConfigService.UnlockKeyValue(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<string>())
            .Returns(Task.FromException<KeyValueSetting>(new Exception("Failed to unlock key-value")));

        var command = new KeyValueUnlockCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "mykey"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Failed to unlock key-value", response.Message);
    }
    [Theory]
    [InlineData("--account-name", "account1", "--key", "mykey")] // Missing subscription
    [InlineData("--subscription", "sub123", "--key", "mykey")] // Missing account-name
    [InlineData("--subscription", "sub123", "--account-name", "account1")] // Missing key
    public async Task ExecuteAsync_Returns400_WhenRequiredParametersAreMissing(params string[] args)
    {
        // Arrange
        var command = new KeyValueUnlockCommand(_logger);
        var parseResult = command.GetCommand().Parse(args);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    private sealed class KeyValueUnlockResult
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;

        [JsonPropertyName("label")]
        public string? Label { get; set; }

        [JsonPropertyName("isLocked")]
        public bool IsLocked { get; set; }
    }
}
