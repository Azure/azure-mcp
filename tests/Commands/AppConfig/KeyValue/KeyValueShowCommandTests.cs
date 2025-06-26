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
using AzureMcp.Tests.Models.AppConfig;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Commands.AppConfig.KeyValue;

public class KeyValueShowCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<KeyValueShowCommand> _logger;

    public KeyValueShowCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<KeyValueShowCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appConfigService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSetting_WhenSettingExists()
    {
        // Arrange
        var expectedSetting = new KeyValueSetting
        {
            Key = "my-key",
            Value = "my-value",
            Label = "prod",
            ContentType = "text/plain",
            Locked = false
        };
        _appConfigService.GetKeyValue(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>(),
            Arg.Any<string?>())
            .Returns(expectedSetting);

        var command = new KeyValueShowCommand(_logger);
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
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueShowResult>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        Assert.NotNull(result);
        Assert.Equal("my-key", result.Setting.Key);
        Assert.Equal("my-value", result.Setting.Value);
        Assert.Equal("prod", result.Setting.Label);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSetting_WhenNoLabelProvided()
    {
        // Arrange
        var expectedSetting = new KeyValueSetting
        {
            Key = "my-key",
            Value = "my-value",
            Label = "",
            ContentType = "text/plain",
            Locked = false
        };
        _appConfigService.GetKeyValue(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<string>())
            .Returns(expectedSetting);

        var command = new KeyValueShowCommand(_logger);
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
        await _appConfigService.Received(1).GetKeyValue("account1", "my-key", "sub123", null, Arg.Any<RetryPolicyOptions>(), null);
    }

    [Fact]
    public async Task ExecuteAsync_Returns500_WhenServiceThrowsException()
    {
        // Arrange
        _appConfigService.GetKeyValue(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<string>())
            .Returns(Task.FromException<KeyValueSetting>(new Exception("Setting not found")));

        var command = new KeyValueShowCommand(_logger);
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
        Assert.Contains("Setting not found", response.Message);
    }

    [Theory]
    [InlineData("--account-name", "account1", "--key", "my-key")] // Missing subscription
    [InlineData("--subscription", "sub123", "--key", "my-key")] // Missing account-name  
    [InlineData("--subscription", "sub123", "--account-name", "account1")] // Missing key
    public async Task ExecuteAsync_Returns400_WhenRequiredParametersAreMissing(params string[] args)
    {
        // Arrange
        var command = new KeyValueShowCommand(_logger);
        var parseResult = command.GetCommand().Parse(args);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }
}
