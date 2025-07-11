// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Areas.AppConfig.Commands.KeyValue;
using AzureMcp.Areas.AppConfig.Models;
using AzureMcp.Areas.AppConfig.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using AzureMcp.Tests.Models.AppConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.AppConfig.UnitTests.KeyValue;

[Trait("Area", "AppConfig")]
public class KeyValueSetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<KeyValueSetCommand> _logger;

    public KeyValueSetCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<KeyValueSetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appConfigService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_SetsKeyValue_WhenValidParametersProvided()
    {
        // Arrange
        var command = new KeyValueSetCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "my-key",
            "--value", "my-value"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).SetKeyValue(
            "account1",
            "my-key",
            "my-value",
            "sub123", null,
            Arg.Any<RetryPolicyOptions>(),
            null);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueSetResult>(json);

        Assert.NotNull(result);
        Assert.Equal("my-key", result.Key);
        Assert.Equal("my-value", result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_SetsKeyValueWithLabel_WhenLabelProvided()
    {
        // Arrange
        var command = new KeyValueSetCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "my-key",
            "--value", "my-value",
            "--label", "prod"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).SetKeyValue(
            "account1",
            "my-key",
            "my-value", "sub123", null,
            Arg.Any<RetryPolicyOptions>(),
            "prod");

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueSetResult>(json);

        Assert.NotNull(result);
        Assert.Equal("my-key", result.Key);
        Assert.Equal("my-value", result.Value);
        Assert.Equal("prod", result.Label);
    }

    [Fact]
    public async Task ExecuteAsync_Returns500_WhenServiceThrowsException()
    {
        // Arrange
        _appConfigService.SetKeyValue(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<string>())
            .Returns(Task.FromException<KeyValueSetting>(new Exception("Failed to set key-value")));

        var command = new KeyValueSetCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--account-name", "account1",
            "--key", "my-key",
            "--value", "my-value"
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Failed to set key-value", response.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData("--subscription sub123")]
    [InlineData("--subscription sub123 --account-name account1")]
    [InlineData("--subscription sub123 --account-name account1 --key my-key")]
    public async Task ExecuteAsync_Returns400_WhenRequiredParametersAreMissing(string args)
    {
        // Arrange
        var command = new KeyValueSetCommand(_logger);
        var parsedArgs = command.GetCommand().Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parsedArgs);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }
}
