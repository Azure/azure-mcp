// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Arguments;
using AzureMcp.Commands.Kusto;
using AzureMcp.Models;
using AzureMcp.Models.Command;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Commands.Kusto;

public sealed class QueryCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IKustoService _kusto;
    private readonly ILogger<QueryCommand> _logger;

    public QueryCommandTests()
    {
        _kusto = Substitute.For<IKustoService>();
        _logger = Substitute.For<ILogger<QueryCommand>>();
        var collection = new ServiceCollection();
        collection.AddSingleton(_kusto);
        _serviceProvider = collection.BuildServiceProvider();
    }

    public static IEnumerable<object[]> QueryArgumentMatrix()
    {
        yield return new object[] { "--subscription sub1 --cluster-name mycluster --database-name db1 --query \"StormEvents | take 1\"", false };
        yield return new object[] { "--cluster-uri https://mycluster.kusto.windows.net --database-name db1 --query \"StormEvents | take 1\"", true };
    }

    [Theory]
    [MemberData(nameof(QueryArgumentMatrix))]
    public async Task ExecuteAsync_ReturnsQueryResults(string cliArgs, bool useClusterUri)
    {
        // Arrange
        var expectedJson = System.Text.Json.JsonDocument.Parse("[{\"foo\":42}]").RootElement.EnumerateArray().Select(e => e.Clone()).ToList();
        if (useClusterUri)
        {
            _kusto.QueryItems(
                "https://mycluster.kusto.windows.net",
                "db1",
                "StormEvents | take 1",
                Arg.Any<string>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyArguments>())
                .Returns(expectedJson);
        }
        else
        {
            _kusto.QueryItems(
                "sub1", "mycluster", "db1", "StormEvents | take 1",
                Arg.Any<string>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyArguments>())
                .Returns(expectedJson);
        }
        var command = new QueryCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(cliArgs);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<QueryResult>(json);
        Assert.NotNull(result);
        Assert.NotNull(result.Results);
        Assert.Single(result.Results);
        var actualJson = result.Results[0].ToString();
        var expectedJsonText = expectedJson[0].ToString();
        Assert.Equal(expectedJsonText, actualJson);
    }

    [Theory]
    [MemberData(nameof(QueryArgumentMatrix))]
    public async Task ExecuteAsync_ReturnsNull_WhenNoResults(string cliArgs, bool useClusterUri)
    {
        if (useClusterUri)
        {
            _kusto.QueryItems(
                "https://mycluster.kusto.windows.net",
                "db1",
                "StormEvents | take 1",
                Arg.Any<string>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyArguments>())
                .Returns(new List<JsonElement>());
        }
        else
        {
            _kusto.QueryItems(
                "sub1", "mycluster", "db1", "StormEvents | take 1",
                Arg.Any<string>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyArguments>())
                .Returns(new List<JsonElement>());
        }
        var command = new QueryCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(cliArgs);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Null(response.Results);
    }

    [Theory]
    [MemberData(nameof(QueryArgumentMatrix))]
    public async Task ExecuteAsync_HandlesException_AndSetsException(string cliArgs, bool useClusterUri)
    {
        var expectedError = "Test error. To mitigate this issue, please refer to the troubleshooting guidelines here at https://aka.ms/azmcp/troubleshooting.";
        if (useClusterUri)
        {
            _kusto.QueryItems(
                "https://mycluster.kusto.windows.net",
                "db1",
                "StormEvents | take 1",
                Arg.Any<string>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyArguments>())
                .Returns(Task.FromException<List<System.Text.Json.JsonElement>>(new Exception("Test error")));
        }
        else
        {
            _kusto.QueryItems(
                "sub1", "mycluster", "db1", "StormEvents | take 1",
                Arg.Any<string>(), Arg.Any<AuthMethod?>(), Arg.Any<RetryPolicyArguments>())
                .Returns(Task.FromException<List<System.Text.Json.JsonElement>>(new Exception("Test error")));
        }
        var command = new QueryCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(cliArgs);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal(expectedError, response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsBadRequest_WhenMissingRequiredArguments()
    {
        var command = new QueryCommand(_logger);
        var parser = new Parser(command.GetCommand());
        var args = parser.Parse(""); // No arguments
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("Missing required", response.Message, System.StringComparison.OrdinalIgnoreCase);
    }

    private sealed class QueryResult
    {
        [JsonPropertyName("results")]
        public List<System.Text.Json.JsonElement> Results { get; set; } = new();
    }
}
