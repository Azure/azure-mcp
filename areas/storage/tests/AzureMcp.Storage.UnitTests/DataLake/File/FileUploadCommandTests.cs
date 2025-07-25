// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Storage.Commands.DataLake.File;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
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
    private readonly string _knownAccountName = "account123";
    private readonly string _knownFileSystemName = "filesystem123";
    private readonly string _knownFilePath = "filesystem123/data/test.txt";
    private readonly string _knownLocalFilePath = "/tmp/test.txt";
    private readonly string _knownSubscriptionId = "sub123";

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
    public async Task ExecuteAsync_WithValidParameters_ReturnsUploadedFileInfo()
    {
        // Arrange
        var expectedFile = new DataLakePathInfo(
            _knownFilePath,
            "file",
            1024,
            DateTimeOffset.Now,
            "\"etag1\"");

        _storageService.UploadFile(
            Arg.Is(_knownAccountName),
            Arg.Is(_knownFilePath),
            Arg.Is(_knownLocalFilePath),
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()).Returns(expectedFile);

        var args = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--file-path", _knownFilePath,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<FileUploadResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedFile.Name, result.File.Name);
        Assert.Equal(expectedFile.Type, result.File.Type);
        Assert.Equal(expectedFile.ContentLength, result.File.ContentLength);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesFileNotFoundException()
    {
        // Arrange
        var expectedError = "Local file not found";

        _storageService.UploadFile(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new FileNotFoundException(expectedError));

        var args = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--file-path", _knownFilePath,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("Local file not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesGeneralException()
    {
        // Arrange
        var expectedError = "Test error";

        _storageService.UploadFile(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--file-path", _knownFilePath,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Theory]
    [InlineData("--file-system-name filesystem123 --file-path test.txt --local-file-path /tmp/test.txt --subscription sub123", false)] // Missing account
    [InlineData("--account-name account123 --file-path test.txt --local-file-path /tmp/test.txt --subscription sub123", false)] // Missing file-system
    [InlineData("--account-name account123 --file-system-name filesystem123 --local-file-path /tmp/test.txt --subscription sub123", false)] // Missing file-path
    [InlineData("--account-name account123 --file-system-name filesystem123 --file-path test.txt --subscription sub123", false)] // Missing local-file-path
    [InlineData("--account-name account123 --file-system-name filesystem123 --file-path test.txt --local-file-path /tmp/test.txt", false)] // Missing subscription
    [InlineData("--account-name account123 --file-system-name filesystem123 --file-path test.txt --local-file-path /tmp/test.txt --subscription sub123", true)] // Valid
    public async Task ExecuteAsync_ValidatesRequiredParameters(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            var mockFile = new DataLakePathInfo("test.txt", "file", 1024, DateTimeOffset.Now, "\"etag\"");
            _storageService.UploadFile(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>()).Returns(mockFile);
        }

        var parseResult = _parser.Parse(args.Split(' '));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (!shouldSucceed)
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("upload", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("Upload a local file", command.Description);
    }

    private class FileUploadResult
    {
        [JsonPropertyName("file")]
        public DataLakePathInfo File { get; set; } = new("", "", null, DateTimeOffset.MinValue, "");
    }
}
