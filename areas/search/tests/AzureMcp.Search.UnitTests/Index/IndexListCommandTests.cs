// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Search.Commands.Index;
using AzureMcp.Search.Models;
using AzureMcp.Search.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Search.UnitTests.Index;

public class IndexListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISearchService _searchService;
    private readonly ILogger<IndexListCommand> _logger;
    private readonly IndexListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownService = "service123";

    public IndexListCommandTests()
    {
        _searchService = Substitute.For<ISearchService>();
        _logger = Substitute.For<ILogger<IndexListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_searchService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsIndexes_WhenIndexesExist()
    {
        var expectedIndexes = new List<IndexInfo> { new("index1", null), new("index2", "This is the second index") };
        _searchService.ListIndexes(Arg.Is(_knownService), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedIndexes);

        var args = _parser.Parse([
            "--service-name", _knownService
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        var result = JsonSerializer.Deserialize<IndexListResult>(json, options);

        Assert.NotNull(result);
        Assert.Equal(expectedIndexes, result.Indexes);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoIndexes()
    {
        _searchService.ListIndexes(Arg.Is(_knownService), Arg.Any<RetryPolicyOptions>())
            .Returns(new List<IndexInfo>());

        var args = _parser.Parse([
            "--service-name", _knownService
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<IndexListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Indexes);
        Assert.Empty(result.Indexes);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        var expectedError = "Test error";

        _searchService.ListIndexes(Arg.Is(_knownService), Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse([
            "--service-name", _knownService
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class IndexListResult
    {
        [JsonPropertyName("indexes")]
        public List<IndexInfo> Indexes { get; set; } = [];
    }
}
