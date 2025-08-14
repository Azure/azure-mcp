// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.AzureManagedLustre.Commands.FileSystem;
using AzureMcp.AzureManagedLustre.Models;
using AzureMcp.AzureManagedLustre.Services;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace AzureMcp.AzureManagedLustre.UnitTests.FileSystem;

public class FileSystemListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAzureManagedLustreService _amlfsService;
    private readonly ILogger<FileSystemListCommand> _logger;
    private readonly FileSystemListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "sub123";

    public FileSystemListCommandTests()
    {
        _amlfsService = Substitute.For<IAzureManagedLustreService>();
        _logger = Substitute.For<ILogger<FileSystemListCommand>>();

        var services = new ServiceCollection().AddSingleton(_amlfsService);
        _serviceProvider = services.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFileSystems()
    {
        // Arrange
        var expected = new List<LustreFileSystem>
        {
            new LustreFileSystem() { Name = "fs1", SubscriptionId = _knownSubscriptionId, ResourceGroupName = "rg1" },
            new LustreFileSystem() { Name = "fs2", SubscriptionId = _knownSubscriptionId, ResourceGroupName = "rg2" }
        };

        _amlfsService.ListFileSystemsAsync(
            Arg.Is(_knownSubscriptionId),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(expected);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<FileSystemListResultJson>(json);

        Assert.NotNull(result);
        Assert.NotNull(result!.FileSystems);
        Assert.Equal(2, result.FileSystems.Count);
        Assert.Equal("fs1", result.FileSystems[0].Name);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNull_WhenNoItems()
    {
        // Arrange
        _amlfsService.ListFileSystemsAsync(
            Arg.Is(_knownSubscriptionId),
            Arg.Is<string?>(x => x == null),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns([]);

        var args = _parser.Parse([
            "--subscription", _knownSubscriptionId
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesRequestFailedExceptions()
    {
        // Arrange - 404 Not Found
        _amlfsService.ListFileSystemsAsync(
            Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Azure.RequestFailedException(404, "not found"));

        var pr1 = _parser.Parse(["--subscription", _knownSubscriptionId]);
        var resp1 = await _command.ExecuteAsync(_context, pr1);
        Assert.Equal(404, resp1.Status);
        Assert.Contains("not found", resp1.Message, StringComparison.OrdinalIgnoreCase);

        // Arrange - 403 Forbidden
        _amlfsService.ListFileSystemsAsync(
            Arg.Any<string>(), Arg.Any<string?>(), Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .ThrowsAsync(new Azure.RequestFailedException(403, "forbidden"));

        var pr2 = _parser.Parse(["--subscription", _knownSubscriptionId]);
        var resp2 = await _command.ExecuteAsync(_context, pr2);
        Assert.Equal(403, resp2.Status);
        Assert.Contains("authorization", resp2.Message, StringComparison.OrdinalIgnoreCase);
    }

    private class FileSystemListResultJson
    {
        [JsonPropertyName("fileSystems")]
        public List<LustreFileSystemJson> FileSystems { get; set; } = [];
    }

    private class LustreFileSystemJson
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("resourceGroupName")]
        public string ResourceGroupName { get; set; } = string.Empty;

        [JsonPropertyName("subscriptionId")]
        public string SubscriptionId { get; set; } = string.Empty;
    }
}
