// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.KeyVault.Commands.Secret;
using AzureMcp.Areas.KeyVault.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.KeyVault.UnitTests.Secret;

[Trait("Area", "KeyVault")]
public class SecretGetCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyVaultService _keyVaultService;
    private readonly ILogger<SecretGetCommand> _logger;
    private readonly SecretGetCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private const string _subscriptionId = "sub123";
    private const string _vaultName = "vault123";
    private const string _secretName = "secret1";
    private const string _secretValue = "secret-value-123";

    public SecretGetCommandTests()
    {
        _keyVaultService = Substitute.For<IKeyVaultService>();
        _logger = Substitute.For<ILogger<SecretGetCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_keyVaultService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new SecretGetCommand(_logger);
        _context = new CommandContext(_serviceProvider);
        _parser = new Parser(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceCorrectly()
    {
        // Arrange
        _keyVaultService.GetSecret(
            Arg.Is(_vaultName),
            Arg.Is(_secretName),
            Arg.Is(_subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(_secretValue);

        var args = _parser.Parse([
            "--vault", _vaultName,
            "--secret", _secretName,
            "--subscription", _subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);

        await _keyVaultService.Received(1).GetSecret(
            Arg.Is(_vaultName),
            Arg.Is(_secretName),
            Arg.Is(_subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());

        // Verify response structure
        Assert.NotNull(response.Results);
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<SecretGetResult>(json);

        Assert.NotNull(result);
        Assert.Equal(_secretName, result.Name);
        Assert.Equal(_secretValue, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSecret_WhenSecretExists()
    {
        // Arrange
        _keyVaultService.GetSecret(
            Arg.Is(_vaultName),
            Arg.Is(_secretName),
            Arg.Is(_subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(_secretValue);

        var args = _parser.Parse([
            "--vault", _vaultName,
            "--secret", _secretName,
            "--subscription", _subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<SecretGetResult>(json);

        Assert.NotNull(result);
        Assert.Equal(_secretName, result.Name);
        Assert.Equal(_secretValue, result.Value);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedException = new Exception("Secret not found");
        _keyVaultService.GetSecret(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(expectedException);

        var args = _parser.Parse([
            "--vault", _vaultName,
            "--secret", _secretName,
            "--subscription", _subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Secret not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsInvalidObject_IfSecretNameIsEmpty()
    {
        // Arrange
        _keyVaultService.GetSecret(
            Arg.Any<string>(),
            Arg.Is(""),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new ArgumentException("Secret name cannot be null or empty"));

        var args = _parser.Parse([
            "--vault", _vaultName,
            "--secret", "",
            "--subscription", _subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Secret name cannot be null or empty", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsInvalidObject_IfVaultNameIsEmpty()
    {
        // Arrange
        _keyVaultService.GetSecret(
            Arg.Is(""),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new ArgumentException("Value cannot be null or empty."));

        var args = _parser.Parse([
            "--vault", "",
            "--secret", _secretName,
            "--subscription", _subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Value cannot be null or empty", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsInvalidObject_IfSubscriptionIsEmpty()
    {
        // Arrange
        _keyVaultService.GetSecret(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Is(""),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new ArgumentException("Value cannot be null or empty."));

        var args = _parser.Parse([
            "--vault", _vaultName,
            "--secret", _secretName,
            "--subscription", ""
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Value cannot be null or empty", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_PassesTenantIdCorrectly()
    {
        // Arrange
        const string tenantId = "tenant123";
        _keyVaultService.GetSecret(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Is(tenantId),
            Arg.Any<RetryPolicyOptions>())
            .Returns(_secretValue);

        var args = _parser.Parse([
            "--vault", _vaultName,
            "--secret", _secretName,
            "--subscription", _subscriptionId,
            "--tenant", tenantId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);

        await _keyVaultService.Received(1).GetSecret(
            Arg.Is(_vaultName),
            Arg.Is(_secretName),
            Arg.Is(_subscriptionId),
            Arg.Is(tenantId),
            Arg.Any<RetryPolicyOptions>());
    }

    private class SecretGetResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("value")]
        public string Value { get; set; } = string.Empty;
    }
}
