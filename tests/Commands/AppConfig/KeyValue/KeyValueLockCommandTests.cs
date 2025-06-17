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

public class KeyValueLockCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<KeyValueLockCommand> _logger;

    public KeyValueLockCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<KeyValueLockCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appConfigService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_LocksKeyValue_WhenValidParametersProvided()
    {
        // Arrange
        var command = new KeyValueLockCommand(_logger);
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
        await _appConfigService.Received(1).LockKeyValue(
            "account1", 
            "mykey", 
            "sub123", 
            null, 
            Arg.Any<RetryPolicyOptions>(),            null);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueLockResult>(json);
        
        Assert.NotNull(result);
        Assert.Equal("mykey", result.Key);
    }

    [Fact]
    public async Task ExecuteAsync_LocksKeyValueWithLabel_WhenLabelProvided()
    {
        // Arrange
        var command = new KeyValueLockCommand(_logger);
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
        await _appConfigService.Received(1).LockKeyValue(
            "account1", 
            "mykey", 
            "sub123",            null, 
            Arg.Any<RetryPolicyOptions>(), 
            "prod");

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueLockResult>(json);
        
        Assert.NotNull(result);
        Assert.Equal("mykey", result.Key);
        Assert.Equal("prod", result.Label);
    }

    [Fact]
    public async Task ExecuteAsync_Returns500_WhenServiceThrowsException()
    {
        // Arrange
        _appConfigService.LockKeyValue(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<string>())
            .Returns(Task.FromException<KeyValueSetting>(new Exception("Failed to lock key-value")));

        var command = new KeyValueLockCommand(_logger);
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
        Assert.Contains("Failed to lock key-value", response.Message);
    }    [Theory]
    [InlineData("")]
    [InlineData("--subscription sub123")]
    [InlineData("--subscription sub123 --account-name account1")]
    public async Task ExecuteAsync_Returns400_WhenRequiredParametersAreMissing(string args)
    {
        // Arrange
        var command = new KeyValueLockCommand(_logger);
        var parsedArgs = command.GetCommand().Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parsedArgs);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    private sealed class KeyValueLockResult
    {
        [JsonPropertyName("key")]
        public string Key { get; set; } = string.Empty;
        
        [JsonPropertyName("label")]
        public string? Label { get; set; }
        
        [JsonPropertyName("isLocked")]
        public bool IsLocked { get; set; }
    }
}
