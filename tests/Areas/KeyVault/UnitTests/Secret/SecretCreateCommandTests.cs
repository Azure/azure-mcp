// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Security.KeyVault.Secrets;
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
public class SecretCreateCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyVaultService _keyVaultService;
    private readonly ILogger<SecretCreateCommand> _logger;
    private readonly SecretCreateCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public SecretCreateCommandTests()
    {
        _keyVaultService = Substitute.For<IKeyVaultService>();
        _logger = Substitute.For<ILogger<SecretCreateCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_keyVaultService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new SecretCreateCommand(_logger);
        _context = new CommandContext(_serviceProvider);
        _parser = new Parser(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_CreatesSecret_WhenValidInput()
    {
        // Arrange
        var subscriptionId = "sub123";
        var vaultName = "vault123";
        var secretName = "secret1";
        var secretValue = "secretValue123";
        var createdOn = DateTimeOffset.UtcNow;
        var updatedOn = DateTimeOffset.UtcNow;

        // Create a real KeyVaultSecret object for testing
        var mockSecret = new KeyVaultSecret(secretName, secretValue);

        _keyVaultService.CreateSecret(Arg.Is(vaultName), Arg.Is(secretName), Arg.Is(secretValue), 
            Arg.Is(subscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns(mockSecret);

        var args = _parser.Parse([
            "--vault", vaultName,
            "--secret", secretName,
            "--value", secretValue,
            "--subscription", subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<SecretCreateResult>(json);

        Assert.NotNull(result);
        Assert.Equal(secretName, result.Name);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";
        var subscriptionId = "sub123";
        var vaultName = "vault123";
        var secretName = "secret1";
        var secretValue = "secretValue123";

        _keyVaultService.CreateSecret(vaultName, secretName, secretValue, Arg.Is(subscriptionId), 
            Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--vault", vaultName,
            "--secret", secretName,
            "--value", secretValue,
            "--subscription", subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class SecretCreateResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("notBefore")]
        public DateTimeOffset? NotBefore { get; set; }

        [JsonPropertyName("expiresOn")]
        public DateTimeOffset? ExpiresOn { get; set; }

        [JsonPropertyName("createdOn")]
        public DateTimeOffset? CreatedOn { get; set; }

        [JsonPropertyName("updatedOn")]
        public DateTimeOffset? UpdatedOn { get; set; }
    }
}
