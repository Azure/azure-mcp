// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.Storage.Commands.Account;
using AzureMcp.Areas.Storage.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Subscription;

[Trait("Area", "Core")]
public class SubscriptionCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IStorageService _storageService;
    private readonly ILogger<AccountListCommand> _logger;
    private readonly AccountListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public SubscriptionCommandTests()
    {
        _storageService = Substitute.For<IStorageService>();
        _logger = Substitute.For<ILogger<AccountListCommand>>();

        var collection = new ServiceCollection().AddSingleton(_storageService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Validate_WithEnvironmentVariableOnly_PassesValidation()
    {
        // Arrange
        var originalValue = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        Environment.SetEnvironmentVariable("AZURE_SUBSCRIPTION_ID", "env-subscription");

        try
        {
            var parseResult = _parser.Parse([]);

            // Act & Assert
            Assert.Empty(parseResult.Errors);
        }
        finally
        {
            // Cleanup
            Environment.SetEnvironmentVariable("AZURE_SUBSCRIPTION_ID", originalValue);
        }
    }

    [Fact]
    public async Task ExecuteAsync_WithEnvironmentVariableOnly_CallsServiceWithCorrectSubscription()
    {
        // Arrange
        var originalValue = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        Environment.SetEnvironmentVariable("AZURE_SUBSCRIPTION_ID", "env-subscription");

        try
        {
            var expectedAccounts = new List<string> { "account1", "account2" };

            _storageService.GetStorageAccounts(Arg.Is("env-subscription"), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
                .Returns(expectedAccounts);

            var parseResult = _parser.Parse([]);

            // Act
            var response = await _command.ExecuteAsync(_context, parseResult);

            // Assert
            Assert.NotNull(response);

            // Verify the service was called with the environment variable subscription
            await _storageService.Received(1).GetStorageAccounts("env-subscription", Arg.Any<string>(), Arg.Any<RetryPolicyOptions>());
        }
        finally
        {
            // Cleanup
            Environment.SetEnvironmentVariable("AZURE_SUBSCRIPTION_ID", originalValue);
        }
    }

    [Fact]
    public async Task ExecuteAsync_WithBothOptionAndEnvironmentVariable_PrefersOption()
    {
        // Arrange
        var originalValue = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        Environment.SetEnvironmentVariable("AZURE_SUBSCRIPTION_ID", "env-subscription");

        try
        {
            var expectedAccounts = new List<string> { "account1", "account2" };

            _storageService.GetStorageAccounts(Arg.Is("option-subscription"), Arg.Any<string>(), Arg.Any<RetryPolicyOptions>())
                .Returns(expectedAccounts);

            var parseResult = _parser.Parse(["--subscription", "option-subscription"]);

            // Act
            var response = await _command.ExecuteAsync(_context, parseResult);

            // Assert
            Assert.NotNull(response);

            // Verify the service was called with the option subscription, not the environment variable
            await _storageService.Received(1).GetStorageAccounts("option-subscription", Arg.Any<string>(), Arg.Any<RetryPolicyOptions>());
            await _storageService.DidNotReceive().GetStorageAccounts("env-subscription", Arg.Any<string>(), Arg.Any<RetryPolicyOptions>());
        }
        finally
        {
            // Cleanup
            Environment.SetEnvironmentVariable("AZURE_SUBSCRIPTION_ID", originalValue);
        }
    }
}
