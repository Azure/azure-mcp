// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Storage.Commands.DataLake.FileSystem;
using AzureMcp.Storage.Models;
using AzureMcp.Storage.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Storage.UnitTests.DataLake.FileSystem;

public class FileSystemListPathsCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStorageService _storageService;
    private readonly ILogger<FileSystemListPathsCommand> _logger;
    private readonly FileSystemListPathsCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownAccountName = "account123";
    private readonly string _knownFileSystemName = "filesystem123";
    private readonly string _knownSubscriptionId = "sub123";

    public FileSystemListPathsCommandTests()
    {
        _storageService = Substitute.For<IStorageService>();
        _logger = Substitute.For<ILogger<FileSystemListPathsCommand>>();

        var collection = new ServiceCollection().AddSingleton(_storageService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_WithValidParameters_ReturnsPaths()
    {
        // Arrange
        var expectedPaths = new List<DataLakePathInfo>
        {
            new("file1.txt", "file", 1024, DateTimeOffset.Now, "\"etag1\""),
            new("directory1", "directory", null, DateTimeOffset.Now, "\"etag2\"")
        };

        _storageService.ListDataLakePaths(Arg.Is(_knownAccountName), Arg.Is(_knownFileSystemName), Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns(expectedPaths);

        var args = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<FileSystemListPathsResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedPaths.Count, result.Paths.Count);
        Assert.Equal(expectedPaths[0].Name, result.Paths[0].Name);
        Assert.Equal(expectedPaths[0].Type, result.Paths[0].Type);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyArray_WhenNoPaths()
    {
        // Arrange
        _storageService.ListDataLakePaths(Arg.Is(_knownAccountName), Arg.Is(_knownFileSystemName), Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns([]);

        var args = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<FileSystemListPathsResult>(json);

        Assert.NotNull(result);
        Assert.Empty(result.Paths);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";

        _storageService.ListDataLakePaths(Arg.Is(_knownAccountName), Arg.Is(_knownFileSystemName), Arg.Is(_knownSubscriptionId),
            null, Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    [Theory]
    [InlineData("--file-system-name filesystem123 --subscription sub123", false)] // Missing account
    [InlineData("--account-name account123 --subscription sub123", false)] // Missing file-system
    [InlineData("--account-name account123 --file-system-name filesystem123", false)] // Missing subscription
    [InlineData("--account-name account123 --file-system-name filesystem123 --subscription sub123", true)] // Valid
    public async Task ExecuteAsync_ValidatesRequiredParameters(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _storageService.ListDataLakePaths(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns([]);
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

    private class FileSystemListPathsResult
    {
        [JsonPropertyName("paths")]
        public List<DataLakePathInfo> Paths { get; set; } = [];
    }
}
