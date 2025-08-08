// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Storage.Commands.Blob.Batch;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Storage.UnitTests.Blob.Batch;

public class BatchDeleteCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStorageService _storageService;
    private readonly ILogger<BatchDeleteCommand> _logger;
    private readonly BatchDeleteCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownAccountName = "account123";
    private readonly string _knownContainerName = "container123";
    private readonly string _knownSubscriptionId = "sub123";
    private readonly string[] _knownBlobNames = ["blob1.txt", "blob2.txt", "blob3.txt"];

    public BatchDeleteCommandTests()
    {
        _storageService = Substitute.For<IStorageService>();
        _logger = Substitute.For<ILogger<BatchDeleteCommand>>();

        var collection = new ServiceCollection().AddSingleton(_storageService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("delete", command.Name);
        Assert.Contains("Delete multiple blobs", command.Description);
        Assert.True(_command.Metadata.Destructive);
        Assert.False(_command.Metadata.ReadOnly);
    }

    [Fact]
    public void GetCommand_HasRequiredOptions()
    {
        var command = _command.GetCommand();
        var options = command.Options.ToList();

        Assert.Contains(options, o => o.Name == "--subscription");
        Assert.Contains(options, o => o.Name == "--account-name");
        Assert.Contains(options, o => o.Name == "--container-name");
        Assert.Contains(options, o => o.Name == "--blob-names");
    }

    [Fact]
    public async Task ExecuteAsync_WithValidParameters_ReturnsSuccess()
    {
        // Arrange
        var successfulBlobs = new List<string> { "blob1.txt", "blob2.txt" };
        var failedBlobs = new List<string>();

        _storageService.DeleteBlobsBatch(
            _knownAccountName,
            _knownContainerName,
            _knownBlobNames,
            _knownSubscriptionId,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns((successfulBlobs, failedBlobs));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--account-name", _knownAccountName,
            "--container-name", _knownContainerName,
            "--blob-names", "blob1.txt", "blob2.txt", "blob3.txt"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        await _storageService.Received(1).DeleteBlobsBatch(
            _knownAccountName,
            _knownContainerName,
            Arg.Is<string[]>(x => x.SequenceEqual(_knownBlobNames)),
            _knownSubscriptionId,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithMissingRequiredParameter_ReturnsError()
    {
        // Arrange
        var parseResult = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--account-name", _knownAccountName
            // Missing container-name and blob-names
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotEqual(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_WithServiceException_ReturnsError()
    {
        // Arrange
        _storageService.DeleteBlobsBatch(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string[]>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new InvalidOperationException("Storage service error"));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--account-name", _knownAccountName,
            "--container-name", _knownContainerName,
            "--blob-names", "blob1.txt", "blob2.txt"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotEqual(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_WithPartialSuccess_ReturnsSuccessWithDetails()
    {
        // Arrange
        var successfulBlobs = new List<string> { "blob1.txt" };
        var failedBlobs = new List<string> { "blob2.txt: HTTP 404" };

        _storageService.DeleteBlobsBatch(
            _knownAccountName,
            _knownContainerName,
            _knownBlobNames,
            _knownSubscriptionId,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns((successfulBlobs, failedBlobs));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--account-name", _knownAccountName,
            "--container-name", _knownContainerName,
            "--blob-names", "blob1.txt", "blob2.txt"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Verify the result structure contains both successful and failed blobs
        var jsonResponse = JsonSerializer.Serialize(response.Results.Data);
        Assert.Contains("blob1.txt", jsonResponse);
        Assert.Contains("blob2.txt", jsonResponse);
    }

    [Fact]
    public async Task ExecuteAsync_WithTenant_PassesTenantToService()
    {
        // Arrange
        var tenant = "tenant123";
        var successfulBlobs = new List<string> { "blob1.txt" };
        var failedBlobs = new List<string>();

        _storageService.DeleteBlobsBatch(
            _knownAccountName,
            _knownContainerName,
            _knownBlobNames,
            _knownSubscriptionId,
            tenant,
            Arg.Any<RetryPolicyOptions>())
            .Returns((successfulBlobs, failedBlobs));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--account-name", _knownAccountName,
            "--container-name", _knownContainerName,
            "--blob-names", "blob1.txt",
            "--tenant", tenant
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);

        await _storageService.Received(1).DeleteBlobsBatch(
            _knownAccountName,
            _knownContainerName,
            Arg.Any<string[]>(),
            _knownSubscriptionId,
            tenant,
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithRetryPolicy_PassesRetryPolicyToService()
    {
        // Arrange
        var successfulBlobs = new List<string> { "blob1.txt" };
        var failedBlobs = new List<string>();

        _storageService.DeleteBlobsBatch(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string[]>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns((successfulBlobs, failedBlobs));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--account-name", _knownAccountName,
            "--container-name", _knownContainerName,
            "--blob-names", "blob1.txt",
            "--retry-max-retries", "5",
            "--retry-delay", "2.5"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);

        await _storageService.Received(1).DeleteBlobsBatch(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string[]>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Is<RetryPolicyOptions>(r => r.MaxRetries == 5 && r.Delay == 2.5));
    }

    [Fact]
    public void BindOptions_WithValidParseResult_ReturnsBoundOptions()
    {
        // Arrange
        var parseResult = _parser.Parse([
            "--subscription", _knownSubscriptionId,
            "--account-name", _knownAccountName,
            "--container-name", _knownContainerName,
            "--blob-names", "blob1.txt", "blob2.txt"
        ]);

        // Act
        var options = _command.BindOptions(parseResult);

        // Assert
        Assert.Equal(_knownSubscriptionId, options.Subscription);
        Assert.Equal(_knownAccountName, options.Account);
        Assert.Equal(_knownContainerName, options.Container);
        Assert.NotNull(options.BlobNames);
        Assert.Equal(2, options.BlobNames.Length);
        Assert.Contains("blob1.txt", options.BlobNames);
        Assert.Contains("blob2.txt", options.BlobNames);
    }
}
