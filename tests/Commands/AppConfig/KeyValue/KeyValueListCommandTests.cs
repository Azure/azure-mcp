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

public class KeyValueListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<KeyValueListCommand> _logger;

    public KeyValueListCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<KeyValueListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appConfigService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSettings_WhenSettingsExist()
    {
        // Arrange
        var expectedSettings = new List<KeyValueSetting>
        {
            new() { Key = "key1", Value = "value1", Label = "prod" },
            new() { Key = "key2", Value = "value2", Label = "dev" }
        };
        _appConfigService.ListKeyValues(
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<RetryPolicyOptions>())
          .Returns(expectedSettings);

        var command = new KeyValueListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--account-name", "account1"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(2, result.Settings.Count);
        Assert.Equal("key1", result.Settings[0].Key);
        Assert.Equal("key2", result.Settings[1].Key);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFilteredSettings_WhenKeyFilterProvided()
    {
        // Arrange
        var expectedSettings = new List<KeyValueSetting>
        {
            new() { Key = "key1", Value = "value1", Label = "prod" }
        };
        _appConfigService.ListKeyValues(
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<RetryPolicyOptions>())
          .Returns(expectedSettings);

        var command = new KeyValueListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--account-name", "account1", "--key", "key1"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).ListKeyValues("account1", "sub123", "key1", null, null, Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFilteredSettings_WhenLabelFilterProvided()
    {
        // Arrange
        var expectedSettings = new List<KeyValueSetting>
        {
            new() { Key = "key1", Value = "value1", Label = "prod" }
        };
        _appConfigService.ListKeyValues(
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<string>(),
          Arg.Any<RetryPolicyOptions>())
          .Returns(expectedSettings);

        var command = new KeyValueListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--account-name", "account1", "--label", "prod"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).ListKeyValues("account1", "sub123", null, "prod", null, Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_Returns500_WhenServiceThrowsException()
    {
        // Arrange
        _appConfigService.ListKeyValues(Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<List<KeyValueSetting>>(new Exception("Service error")));

        var command = new KeyValueListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--account-name", "account1"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Service error", response.Message);
    }
    [Theory]
    [InlineData("--account-name", "account1")] // Missing subscription
    [InlineData("--subscription", "sub123")] // Missing account-name
    public async Task ExecuteAsync_Returns400_WhenRequiredParametersAreMissing(params string[] args)
    {
        // Arrange
        var command = new KeyValueListCommand(_logger);
        var parseResult = command.GetCommand().Parse(args);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    private sealed class KeyValueListResult
    {
        [JsonPropertyName("settings")]
        public List<KeyValueSetting> Settings { get; set; } = [];
    }
}
