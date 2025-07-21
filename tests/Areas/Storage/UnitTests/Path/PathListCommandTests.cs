// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Storage.Commands.DataLake.Path;
using AzureMcp.Areas.Storage.Models;
using AzureMcp.Areas.Storage.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Storage.UnitTests.Path;

[Trait("Area", "Storage")]
public class PathListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStorageService _storageService;
    private readonly ILogger<PathListCommand> _logger;
    private readonly PathListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownAccountName = "account123";
    private readonly string _knownFileSystemName = "filesystem123";
    private readonly string _knownSubscriptionId = "sub123";

    public PathListCommandTests()
    {
        _storageService = Substitute.For<IStorageService>();
        _logger = Substitute.For<ILogger<PathListCommand>>();

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

        _storageService.ListDataLakePathsWithPrefix(Arg.Is(_knownAccountName), Arg.Is(_knownFileSystemName), 
            Arg.Any<string>(), Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns(expectedPaths);

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
        var result = JsonSerializer.Deserialize<PathListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedPaths.Count, result.Paths.Count);
        Assert.Equal(expectedPaths[0].Name, result.Paths[0].Name);
        Assert.Equal(expectedPaths[0].Type, result.Paths[0].Type);
    }

    [Fact]
    public async Task ExecuteAsync_WithPathPrefix_FiltersPaths()
    {
        // Arrange
        var expectedPaths = new List<DataLakePathInfo>
        {
            new("logs/app.log", "file", 2048, DateTimeOffset.Now, "\"etag1\""),
            new("logs/error.log", "file", 512, DateTimeOffset.Now, "\"etag2\"")
        };

        _storageService.ListDataLakePathsWithPrefix(Arg.Is(_knownAccountName), Arg.Is(_knownFileSystemName), 
            Arg.Is("logs/"), Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns(expectedPaths);

        var args = _parser.Parse([
            "--account-name", _knownAccountName,
            "--file-system-name", _knownFileSystemName,
            "--subscription", _knownSubscriptionId,
            "--path-prefix", "logs/"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<PathListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedPaths.Count, result.Paths.Count);
        Assert.All(result.Paths, path => Assert.StartsWith("logs/", path.Name));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyArray_WhenNoPaths()
    {
        // Arrange
        _storageService.ListDataLakePathsWithPrefix(Arg.Is(_knownAccountName), Arg.Is(_knownFileSystemName), 
            Arg.Any<string>(), Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns([]);

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
        var result = JsonSerializer.Deserialize<PathListResult>(json);

        Assert.NotNull(result);
        Assert.Empty(result.Paths);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";

        _storageService.ListDataLakePathsWithPrefix(Arg.Is(_knownAccountName), Arg.Is(_knownFileSystemName), 
            Arg.Any<string>(), Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).ThrowsAsync(new Exception(expectedError));

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
    [InlineData("--account-name account123 --file-system-name filesystem123 --subscription sub123 --path-prefix logs/", true)] // Valid with prefix
    public async Task ExecuteAsync_ValidatesRequiredParameters(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _storageService.ListDataLakePathsWithPrefix(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(),
                Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>()).Returns([]);
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

    private class PathListResult
    {
        [JsonPropertyName("paths")]
        public List<DataLakePathInfo> Paths { get; set; } = [];
    }
}