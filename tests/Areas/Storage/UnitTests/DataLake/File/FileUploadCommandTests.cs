// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Storage.Commands.DataLake.File;
using AzureMcp.Areas.Storage.Models;
using AzureMcp.Areas.Storage.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Storage.UnitTests.DataLake.File;

[Trait("Area", "Storage")]
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
    private readonly string _knownFilePath = "data/logs/app.log";
    private readonly string _knownLocalFilePath = "/tmp/testfile.txt";
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
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("upload", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidParameters_UploadsFile()
    {
        // Arrange
        var expectedFile = new DataLakePathInfo(
            _knownFilePath,
            "file",
            1024,
            DateTimeOffset.UtcNow,
            "\"0x8D123456789ABCDEF\"");

        _storageService.UploadFile(
                _knownAccountName,
                _knownFileSystemName,
                _knownFilePath,
                _knownLocalFilePath,
                _knownSubscriptionId,
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
            .Returns(expectedFile);

        var parseResult = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--file-path", _knownFilePath,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<JsonElement>(json);
        var file = result.GetProperty("file");
        Assert.Equal(_knownFilePath, file.GetProperty("name").GetString());
        Assert.Equal("file", file.GetProperty("type").GetString());
        Assert.Equal(1024, file.GetProperty("size").GetInt64());
    }

    [Theory]
    [InlineData("--account-name test", false)]
    [InlineData("--account-name test --file-system-name fs", false)]
    [InlineData("--account-name test --file-system-name fs --file-path path", false)]
    [InlineData("--account-name test --file-system-name fs --file-path path --local-file-path local", false)]
    [InlineData("--account-name test --file-system-name fs --file-path path --local-file-path local --subscription sub", true)]
    public async Task ExecuteAsync_ValidatesInputCorrectly(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _storageService.UploadFile(
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<string>(),
                    Arg.Any<RetryPolicyOptions>())
                .Returns(new DataLakePathInfo("test", "file", 1024, DateTimeOffset.UtcNow, "etag"));
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
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception("Test error"));

        var parseResult = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--file-path", _knownFilePath,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithFileNotFoundException_Returns500()
    {
        // Arrange
        _storageService.UploadFile(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new FileNotFoundException("Local file not found"));

        var parseResult = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--file-path", _knownFilePath,
            "--local-file-path", _knownLocalFilePath,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Local file not found", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }
}
