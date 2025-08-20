// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Storage.Commands.Blob;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using static AzureMcp.Storage.Commands.Blob.BlobDownloadCommand;

namespace AzureMcp.Storage.UnitTests.Blob;

public class BlobDownloadCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStorageService _storageService;
    private readonly ILogger<BlobDownloadCommand> _logger;
    private readonly BlobDownloadCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownAccount = "account123";
    private readonly string _knownContainer = "container123";
    private readonly string _knownBlob = "test-blob.txt";
    private readonly string _knownLocalFilePath = @"C:\Downloads\test-blob.txt";
    private readonly string _knownSubscription = "sub123";

    public BlobDownloadCommandTests()
    {
        _storageService = Substitute.For<IStorageService>();
        _logger = Substitute.For<ILogger<BlobDownloadCommand>>();

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
        Assert.Equal("download", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsDownloadResult()
    {
        // Arrange
        var expectedResult = new BlobDownloadInfo(
            _knownBlob,
            _knownContainer,
            _knownLocalFilePath,
            1024,
            DateTimeOffset.UtcNow,
            "\"0x8D123456789ABCD\"",
            "abc123def456",
            false);

        _storageService.DownloadBlob(
            Arg.Is(_knownAccount),
            Arg.Is(_knownContainer),
            Arg.Is(_knownBlob),
            Arg.Is(_knownLocalFilePath),
            Arg.Is(false),
            Arg.Is(_knownSubscription),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedResult);

        var args = _parser.Parse([
            "--account", _knownAccount,
            "--container", _knownContainer,
            "--blob", _knownBlob,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscription
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);

        // Try to see what we get with JsonSerializerOptions using CamelCase
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var commandResult = JsonSerializer.Deserialize<BlobDownloadCommandResult>(json, options);

        Assert.NotNull(commandResult);
        Assert.NotNull(commandResult.DownloadInfo);

        var result = commandResult.DownloadInfo;
        Assert.Equal(_knownBlob, result.Blob);
        Assert.Equal(_knownContainer, result.Container);
        Assert.Equal(_knownLocalFilePath, result.DownloadLocation);
        Assert.Equal(1024, result.BlobSize);
        Assert.Equal("\"0x8D123456789ABCD\"", result.ETag);
        Assert.Equal("abc123def456", result.MD5Hash);
        Assert.False(result.WasLocalFileOverwritten);
    }

    [Fact]
    public async Task ExecuteAsync_WithOverwrite_ReturnsCorrectResult()
    {
        // Arrange
        var expectedResult = new BlobDownloadInfo(
            _knownBlob,
            _knownContainer,
            _knownLocalFilePath,
            1024,
            DateTimeOffset.UtcNow,
            "\"0x8D123456789ABCD\"",
            "abc123def456",
            true);

        _storageService.DownloadBlob(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Is(true),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedResult);

        var args = _parser.Parse([
            "--account", _knownAccount,
            "--container", _knownContainer,
            "--blob", _knownBlob,
            "--local-file-path", _knownLocalFilePath,
            "--overwrite",
            "--subscription", _knownSubscription
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var commandResult = JsonSerializer.Deserialize<BlobDownloadCommandResult>(json, options);

        Assert.NotNull(commandResult);
        Assert.NotNull(commandResult.DownloadInfo);
        Assert.True(commandResult.DownloadInfo.WasLocalFileOverwritten);
    }

    [Theory]
    [InlineData("", false, "required")]
    [InlineData("--account account --container container", false, "required")]
    [InlineData("--account account --container container --blob blob", false, "required")]
    [InlineData("--account account --container container --blob blob --local-file-path /path/to/file", false, "required")]
    [InlineData("--account account --container container --blob blob --local-file-path /path/to/file --subscription sub", true, null)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed, string? expectedErrorKeyword)
    {
        // Arrange
        if (shouldSucceed)
        {
            var expectedResult = new BlobDownloadInfo(
                "blob",
                "container",
                "/path/to/file",
                1024,
                DateTimeOffset.UtcNow,
                "\"0x8D123456789ABCD\"",
                "abc123def456",
                false);

            _storageService.DownloadBlob(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<bool>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
                .Returns(expectedResult);
        }

        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (shouldSucceed)
        {
            Assert.NotNull(response.Results);
            Assert.Equal("Success", response.Message);
        }
        else
        {
            Assert.Contains(expectedErrorKeyword!, response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _storageService.DownloadBlob(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<bool>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<BlobDownloadInfo>(new Exception("Test error")));

        var parseResult = _parser.Parse([
            "--account", _knownAccount,
            "--container", _knownContainer,
            "--blob", _knownBlob,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscription
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesFileExistsError()
    {
        // Arrange
        _storageService.DownloadBlob(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Is(false),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<BlobDownloadInfo>(new InvalidOperationException("File already exists")));

        var parseResult = _parser.Parse([
            "--account", _knownAccount,
            "--container", _knownContainer,
            "--blob", _knownBlob,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscription
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("File already exists", response.Message);
    }
}
