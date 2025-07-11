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
public class KeyValueListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<KeyValueListCommand> _logger;
    private readonly KeyValueListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public KeyValueListCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<KeyValueListCommand>>();

        _command = new(_logger);
        _parser = new(_command.GetCommand());
        _serviceProvider = new ServiceCollection()
            .AddSingleton(_appConfigService)
            .BuildServiceProvider();
        _context = new(_serviceProvider);
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

        var args = _parser.Parse(["--subscription", "sub123", "--account-name", "account1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyValueListResult>(json, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

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

        var args = _parser.Parse(["--subscription", "sub123", "--account-name", "account1", "--key", "key1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(200, response.Status);
        await _appConfigService.Received(1).ListKeyValues("account1", "sub123", "key1", null, null, Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFilteredSettings_WhenLabelFilterProvided()
    {
        var args = _parser.Parse(["--subscription", "sub123", "--account-name", "account1", "--label", "prod"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

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

        var args = _parser.Parse(["--subscription", "sub123", "--account-name", "account1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

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
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }
}
