// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.KeyVault.Commands.Certificate;
using AzureMcp.KeyVault.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.KeyVault.UnitTests.Certificate;

public class CertificateImportCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IKeyVaultService _keyVaultService;
    private readonly ILogger<CertificateImportCommand> _logger;
    private readonly CertificateImportCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private const string _knownSubscription = "knownSub";
    private const string _knownVault = "kvtest";
    private const string _knownCertName = "cert1";
    private const string _fakePfxBase64 = "VGhpcyBpcyBhIGZha2UgcGZha2UgcGZ4IGJ5dGVz"; // arbitrary base64

    public CertificateImportCommandTests()
    {
        _keyVaultService = Substitute.For<IKeyVaultService>();
        _logger = Substitute.For<ILogger<CertificateImportCommand>>();

        var services = new ServiceCollection();
        services.AddSingleton(_keyVaultService);
        _serviceProvider = services.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_CallsService_WithExpectedParameters()
    {
        // Arrange
        _keyVaultService.ImportCertificate(
            _knownVault,
            _knownCertName,
            _fakePfxBase64,
            null,
            _knownSubscription,
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception("Test error")); // force exception to avoid building return object

        var args = _parser.Parse([
            "--vault", _knownVault,
            "--certificate", _knownCertName,
            "--certificate-data", _fakePfxBase64,
            "--subscription", _knownSubscription
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        await _keyVaultService.Received(1).ImportCertificate(
            _knownVault,
            _knownCertName,
            _fakePfxBase64,
            null,
            _knownSubscription,
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions>());
        Assert.Equal(500, response.Status); // due to forced exception
    }

    [Fact]
    public async Task ExecuteAsync_Returns400_WhenMissingCertificateData()
    {
        var args = _parser.Parse([
            "--vault", _knownVault,
            "--certificate", _knownCertName,
            "--subscription", _knownSubscription
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceException()
    {
        var expected = "boom";
        _keyVaultService.ImportCertificate(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception(expected));

        var args = _parser.Parse([
            "--vault", _knownVault,
            "--certificate", _knownCertName,
            "--certificate-data", _fakePfxBase64,
            "--subscription", _knownSubscription
        ]);

        var response = await _command.ExecuteAsync(_context, args);

        Assert.Equal(500, response.Status);
        Assert.StartsWith(expected, response.Message);
    }
}
