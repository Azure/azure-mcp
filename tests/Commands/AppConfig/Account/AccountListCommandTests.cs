// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Commands.AppConfig.Account;
using AzureMcp.Models.AppConfig;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Commands.AppConfig.Account;

public class AccountListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppConfigService _appConfigService;
    private readonly ILogger<AccountListCommand> _logger;

    public AccountListCommandTests()
    {
        _appConfigService = Substitute.For<IAppConfigService>();
        _logger = Substitute.For<ILogger<AccountListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appConfigService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsAccounts_WhenAccountsExist()
    {
        // Arrange
        var expectedAccounts = new List<AppConfigurationAccount>
        {
            new() { Name = "account1", Location = "East US", Endpoint = "https://account1.azconfig.io" },
            new() { Name = "account2", Location = "West US", Endpoint = "https://account2.azconfig.io" }
        };        _appConfigService.GetAppConfigAccounts("sub123", Arg.Any<string?>(), Arg.Any<RetryPolicyOptions?>())
            .Returns(expectedAccounts);

        var command = new AccountListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<AccountListResult>(json);
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Accounts.Count);
        Assert.Equal("account1", result.Accounts[0].Name);
        Assert.Equal("account2", result.Accounts[1].Name);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsEmptyList_WhenNoAccountsExist()
    {
        // Arrange
        var expectedAccounts = new List<AppConfigurationAccount>();
        
        _appConfigService.GetAppConfigAccounts(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(expectedAccounts);

        var command = new AccountListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);        // Assert
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
    }    [Fact]
    public async Task ExecuteAsync_Returns500_WhenServiceThrowsException()
    {
        // Arrange
        _appConfigService.GetAppConfigAccounts(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
            .Returns(Task.FromException<List<AppConfigurationAccount>>(new Exception("Service error")));

        var command = new AccountListCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123"]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Service error", response.Message);
    }    [Fact]
    public async Task ExecuteAsync_Returns400_WhenSubscriptionIsMissing()
    {
        // Arrange
        var command = new AccountListCommand(_logger);
        var parseResult = command.GetCommand().Parse([]); // No arguments at all
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(400, response.Status);
        Assert.Contains("required", response.Message.ToLower());
    }

    private sealed class AccountListResult
    {
        [JsonPropertyName("accounts")]
        public List<AppConfigurationAccount> Accounts { get; set; } = [];
    }
}
