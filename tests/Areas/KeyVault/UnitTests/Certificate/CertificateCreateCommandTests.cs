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

    public CertificateCreateCommandTests()
    {
        _keyVaultService = Substitute.For<IKeyVaultService>();
        _logger = Substitute.For<ILogger<CertificateCreateCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_keyVaultService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new CertificateCreateCommand(_logger);
        _context = new CommandContext(_serviceProvider);
        _parser = new Parser(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_CallsServiceCorrectly()
    {
        // Arrange
        var subscriptionId = "sub123";
        var vaultName = "vault123";
        var certificateName = "cert1";
        var subject = "CN=test.example.com";

        // Since CertificateOperation is complex to mock, test service call and exception handling
        _keyVaultService.CreateCertificate(Arg.Is(vaultName), Arg.Is(certificateName), Arg.Is(subject), 
            Arg.Is(subscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception("Expected test failure"));

        var args = _parser.Parse([
            "--vault", vaultName,
            "--certificate", certificateName,
            "--subject", subject,
            "--subscription", subscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert - Verify the service was called with correct parameters
        await _keyVaultService.Received(1).CreateCertificate(vaultName, certificateName, subject, subscriptionId, 
            Arg.Any<string>(), Arg.Any<RetryPolicyOptions>());
        
        // Should handle the exception
        Assert.Equal(500, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";
        var subscriptionId = "sub123";
        var vaultName = "vault123";
        var certificateName = "cert1";
        var subject = "CN=test.example.com";

        _keyVaultService.CreateCertificate(vaultName, certificateName, subject, Arg.Is(subscriptionId), 
            Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--vault", vaultName,
            "--certificate", certificateName,
            "--subject", subject,
            "--subscription", subscriptionId
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
        public string? RequestId { get; set; }
    }
}
