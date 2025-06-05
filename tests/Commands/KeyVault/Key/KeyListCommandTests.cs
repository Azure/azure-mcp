// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Commands.KeyVault.Key;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Commands.KeyVault.Key;

public class KeyListCommandTests
{

    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyVaultService _keyVaultService;
    private readonly ILogger<KeyListCommand> _logger;

    public KeyListCommandTests()
    {
        _keyVaultService = Substitute.For<IKeyVaultService>();
        _logger = Substitute.For<ILogger<KeyListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_keyVaultService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsKeys_WhenKeysExist()
    {
        // Arrange
        var subscriptionId = "sub123";
        var vaultName = "vault123";
        var expectedKeys = new List<string> { "key1", "key2" };

        _keyVaultService.ListKeys(Arg.Is(vaultName), Arg.Any<bool>(), Arg.Is(subscriptionId), Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
                .Returns(expectedKeys);

        var command = new KeyListCommand(_logger);
        var args = command.GetCommand().Parse(["--vault", vaultName, "--subscription", subscriptionId]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<KeyListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedKeys, result.Keys);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNull_WhenNoKeys()
    {
        // Arrange
        var subscriptionId = "sub123";
        var vaultName = "vault123";

        _keyVaultService.ListKeys(vaultName, Arg.Any<bool>(), Arg.Is(subscriptionId), Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
                .Returns([]);

        var command = new KeyListCommand(_logger);
        var args = command.GetCommand().Parse(["--vault", vaultName, "--subscription", subscriptionId]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";
        var subscriptionId = "sub123";
        var vaultName = "vault123";

        _keyVaultService.ListKeys(vaultName, Arg.Any<bool>(), Arg.Is(subscriptionId), Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
                .ThrowsAsync(new Exception(expectedError));

        var command = new KeyListCommand(_logger);
        var args = command.GetCommand().Parse(["--vault", vaultName, "--subscription", subscriptionId]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class KeyListResult
    {
        [JsonPropertyName("keys")]
        public List<string> Keys { get; set; } = [];
    }
}
