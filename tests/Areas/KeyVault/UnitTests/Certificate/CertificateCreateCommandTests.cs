// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Security.KeyVault.Certificates;
using AzureMcp.Areas.KeyVault.Commands.Certificate;
using AzureMcp.Areas.KeyVault.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.KeyVault.UnitTests.Certificate;

[Trait("Area", "KeyVault")]
public class CertificateCreateCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyVaultService _keyVaultService;
    private readonly ILogger<CertificateCreateCommand> _logger;
    private readonly CertificateCreateCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private readonly string _knownSubscriptionId = "knownSubscription";
    private readonly string _knownVaultName = "knownVaultName";
    private readonly string _knownCertificateName = "knownCertificateName";

    public CertificateCreateCommandTests()
    {
        _keyVaultService = Substitute.For<IKeyVaultService>();
        _logger = Substitute.For<ILogger<CertificateCreateCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_keyVaultService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceCorrectly()
    {
        // Arrange
        var expectedError = "Expected test error";

        // TODO (vcolin7): Find a way to mock CertificateOperation
        // We'll test that the service is called correctly, but let it fail since mocking the return is complex
        _keyVaultService.CreateCertificate(
            Arg.Is(_knownVaultName),
            Arg.Is(_knownCertificateName),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--vault", _knownVaultName,
            "--certificate", _knownCertificateName,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert - Verify the service was called with correct parameters
        await _keyVaultService.Received(1).CreateCertificate(
            _knownVaultName,
            _knownCertificateName,
            _knownSubscriptionId,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());

        // Should handle the exception
        Assert.Equal(500, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";

        _keyVaultService.CreateCertificate(
            Arg.Is(_knownVaultName),
            Arg.Is(_knownCertificateName),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--vault", _knownVaultName,
            "--certificate", _knownCertificateName,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class CertificateCreateResult
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("requestId")]
        public string RequestId { get; set; } = string.Empty;
    }
}
