// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Models.Command;
using AzureMcp.Storage.Commands.DataLake.File;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Storage.UnitTests.DataLake.File;

public class FileUploadCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStorageService _storageService;
    private readonly ILogger<FileUploadCommand> _logger;
    private readonly FileUploadCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public FileUploadCommandTests()
    {
        _storageService = Substitute.For<IStorageService>();
        _logger = Substitute.For<ILogger<FileUploadCommand>>();

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
        Assert.Equal("upload", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("Upload a file to Azure Data Lake Storage Gen2", command.Description);
    }

    [Theory]
    [InlineData("--subscription sub --account-name account --file-system-name fs --file-path data/file.txt --source-file-path /local/file.txt", true)]
    [InlineData("--subscription sub --account-name account --file-system-name fs --file-path data/file.txt --source-file-path /local/file.txt --overwrite", true)]
    [InlineData("--subscription sub --account-name account --file-system-name fs --file-path data/file.txt", false)] // Missing source-file-path
    [InlineData("--subscription sub --account-name account --file-system-name fs --source-file-path /local/file.txt", false)] // Missing file-path
    [InlineData("--subscription sub --account-name account --file-path data/file.txt --source-file-path /local/file.txt", false)] // Missing file-system-name
    [InlineData("--subscription sub --file-system-name fs --file-path data/file.txt --source-file-path /local/file.txt", false)] // Missing account-name
    [InlineData("", false)] // Missing all required
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var mockResult = new FileUploadResult(
                "data/file.txt",
                1024,
                "mock-md5-hash",
                DateTimeOffset.UtcNow,
                false);

            _storageService.UploadFile(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<bool>(),
                Arg.Any<string>(),
                Arg.Any<string?>(),
                Arg.Any<Core.Options.RetryPolicyOptions?>())
                .Returns(mockResult);
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
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _storageService.UploadFile(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<bool>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(Task.FromException<FileUploadResult>(new Exception("Test error")));

        var parseResult = _parser.Parse([
            "--subscription", "sub",
            "--account-name", "account",
            "--file-system-name", "fs",
            "--file-path", "data/file.txt",
            "--source-file-path", "/local/file.txt"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesFileNotFoundError()
    {
        // Arrange
        _storageService.UploadFile(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<bool>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(Task.FromException<FileUploadResult>(new FileNotFoundException("Source file not found")));

        var parseResult = _parser.Parse([
            "--subscription", "sub",
            "--account-name", "account",
            "--file-system-name", "fs",
            "--file-path", "data/file.txt",
            "--source-file-path", "/local/file.txt"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(404, response.Status);
        Assert.Contains("Source file not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesFileExistsError()
    {
        // Arrange
        var requestException = new Azure.RequestFailedException(409, "File already exists and overwrite is not enabled.");
        _storageService.UploadFile(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<bool>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(Task.FromException<FileUploadResult>(requestException));

        var parseResult = _parser.Parse([
            "--subscription", "sub",
            "--account-name", "account",
            "--file-system-name", "fs",
            "--file-path", "data/file.txt",
            "--source-file-path", "/local/file.txt"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(409, response.Status);
        Assert.Contains("File already exists", response.Message);
        Assert.Contains("overwrite", response.Message.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_SuccessfulUpload_ReturnsExpectedResult()
    {
        // Arrange
        var expectedResult = new FileUploadResult(
            "data/test-file.txt",
            2048,
            "abcd1234hash",
            DateTimeOffset.UtcNow,
            false);

        _storageService.UploadFile(
            "test-account",
            "test-fs",
            "data/test-file.txt",
            "/local/test-file.txt",
            false,
            "test-subscription",
            null,
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(expectedResult);

        var parseResult = _parser.Parse([
            "--subscription", "test-subscription",
            "--account-name", "test-account",
            "--file-system-name", "test-fs",
            "--file-path", "data/test-file.txt",
            "--source-file-path", "/local/test-file.txt"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);

        // Verify the service was called with correct parameters
        await _storageService.Received(1).UploadFile(
            "test-account",
            "test-fs",
            "data/test-file.txt",
            "/local/test-file.txt",
            false,
            "test-subscription",
            null,
            Arg.Any<Core.Options.RetryPolicyOptions?>());
    }

    [Fact]
    public async Task ExecuteAsync_WithOverwriteOption_PassesCorrectParameters()
    {
        // Arrange
        var expectedResult = new FileUploadResult(
            "data/test-file.txt",
            2048,
            "abcd1234hash",
            DateTimeOffset.UtcNow,
            true);

        _storageService.UploadFile(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            true,
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>())
            .Returns(expectedResult);

        var parseResult = _parser.Parse([
            "--subscription", "test-subscription",
            "--account-name", "account123",
            "--file-system-name", "filesystem123",
            "--file-path", "data/test-file.txt",
            "--source-file-path", "/local/test-file.txt",
            "--overwrite"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);

        // Verify the service was called with overwrite = true
        await _storageService.Received(1).UploadFile(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            true,
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<Core.Options.RetryPolicyOptions?>());
    }
}
