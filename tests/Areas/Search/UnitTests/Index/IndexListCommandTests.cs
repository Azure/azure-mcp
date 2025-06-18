// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Search;
using AzureMcp.Areas.Search.Commands.Index;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Search.UnitTests.Index;

public class IndexListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISearchService _searchService;
    private readonly ILogger<IndexListCommand> _logger;

    public IndexListCommandTests()
    {
        _searchService = Substitute.For<ISearchService>();
        _logger = Substitute.For<ILogger<IndexListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_searchService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsIndexes_WhenIndexesExist()
    {
        // Arrange
        var expectedIndexes = new List<string> { "index1", "index2" };
        _searchService.ListIndexes(Arg.Is("service123"), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedIndexes);

        var command = new IndexListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse("--service-name service123");
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<IndexListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedIndexes, result.Indexes);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNull_WhenNoIndexes()
    {
        // Arrange
        _searchService.ListIndexes(Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(new List<string>());

        var command = new IndexListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse("--service-name service123");
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Null(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";
        var serviceName = "service123";

        _searchService.ListIndexes(Arg.Is(serviceName), Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var command = new IndexListCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse($"--service-name {serviceName}");
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class IndexListResult
    {
        [JsonPropertyName("indexes")]
        public List<string> Indexes { get; set; } = [];
    }
}
