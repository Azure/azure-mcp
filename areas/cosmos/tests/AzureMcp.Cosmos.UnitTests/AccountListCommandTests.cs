// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Cosmos.Commands;
using AzureMcp.Cosmos.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Cosmos.UnitTests;

public class AccountListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICosmosService _cosmosService;
    private readonly ILogger<AccountListCommand> _logger;
    private readonly AccountListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;
    private readonly string _knownSubscriptionId = "00000000-0000-0000-0000-000000000001";

    public AccountListCommandTests()
    {
        _cosmosService = Substitute.For<ICosmosService>();
        _logger = Substitute.For<ILogger<AccountListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_cosmosService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsAccounts_WhenAccountsExist()
    {
        // Arrange
        var expectedAccounts = new List<string> { "account1", "account2" };
        _cosmosService.GetCosmosAccounts(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedAccounts);

        var args = _parser.Parse(["--subscription", _knownSubscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<AccountListResult>(json);

        Assert.NotNull(result);
        Assert.Equal(expectedAccounts, result.Accounts);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoAccounts()
    {
        // Arrange
        _cosmosService.GetCosmosAccounts(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns([]);

        var args = _parser.Parse(["--subscription", _knownSubscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<AccountListResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Accounts);
        Assert.Empty(result.Accounts);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesException()
    {
        // Arrange
        var expectedError = "Test error";

        _cosmosService.GetCosmosAccounts(Arg.Is(_knownSubscriptionId), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new Exception(expectedError));

        var args = _parser.Parse(["--subscription", _knownSubscriptionId]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.StartsWith(expectedError, response.Message);
    }

    private class AccountListResult
    {
        [JsonPropertyName("accounts")]
        public List<string> Accounts { get; set; } = [];
    }
}