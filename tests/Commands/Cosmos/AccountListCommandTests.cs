// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Commands.Cosmos;
using AzureMcp.Models.Argument;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using Xunit;

namespace AzureMcp.Tests.Commands.Cosmos
{
    public class AccountListCommandTests
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICosmosService _cosmosService;

        public AccountListCommandTests()
        {
            _cosmosService = Substitute.For<ICosmosService>();

            var collection = new ServiceCollection();
            collection.AddSingleton(_cosmosService);

            _serviceProvider = collection.BuildServiceProvider();
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsAccounts_WhenAccountsExist()
        {
            // Arrange
            var expectedAccounts = new List<string> { "account1", "account2" };
            _cosmosService.GetCosmosAccounts(Arg.Is("sub123"), Arg.Any<string>(), Arg.Any<RetryPolicyArguments>())
                .Returns(expectedAccounts);

            var command = new AccountListCommand();
            var args = command.GetCommand().Parse(["--subscription", "sub123"]);
            var context = new CommandContext(_serviceProvider);

            // Act
            var response = await command.ExecuteAsync(context, args);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Results);

            var json = JsonSerializer.Serialize(response.Results);
            var result = JsonSerializer.Deserialize<AccountListResult>(json);

            Assert.NotNull(result);
            Assert.Equal(expectedAccounts, result.Accounts);
        }

        [Fact]
        public async Task ExecuteAsync_ReturnsNull_WhenNoAccounts()
        {
            // Arrange
            _cosmosService.GetCosmosAccounts("sub123", null, null)
                .Returns([]);

            var command = new AccountListCommand();
            var args = command.GetCommand().Parse(["--subscription", "sub123"]);
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
            var subscriptionId = "sub123";
            var defaultRetryPolicy = new RetryPolicyArguments
            {
                DelaySeconds = ArgumentDefinitions.RetryPolicy.Delay.DefaultValue,
                MaxDelaySeconds = ArgumentDefinitions.RetryPolicy.MaxDelay.DefaultValue,
                MaxRetries = ArgumentDefinitions.RetryPolicy.MaxRetries.DefaultValue,
                Mode = ArgumentDefinitions.RetryPolicy.Mode.DefaultValue,
                NetworkTimeoutSeconds = ArgumentDefinitions.RetryPolicy.NetworkTimeout.DefaultValue
            };

            _cosmosService.GetCosmosAccounts(subscriptionId, null, defaultRetryPolicy)
                .ThrowsAsync(new Exception(expectedError));

            var command = new AccountListCommand();
            var args = command.GetCommand().Parse(["--subscription", subscriptionId]);
            var context = new CommandContext(_serviceProvider);

            // Act
            var response = await command.ExecuteAsync(context, args);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(500, response.Status);
            Assert.Equal(expectedError, response.Message);
        }

        private class AccountListResult
        {
            [JsonPropertyName("accounts")]
            public List<string> Accounts { get; set; } = [];
        }
    }
}