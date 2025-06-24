// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Areas.Postgres.Commands.Table;
using AzureMcp.Areas.Postgres.Services;
using AzureMcp.Models.Command;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Tests.Areas.Postgres.UnitTests.Table;

[Trait("Area", "Postgres")]
public class GetSchemaCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IPostgresService _postgresService;
    private readonly ILogger<GetSchemaCommand> _logger;

    public GetSchemaCommandTests()
    {
        _postgresService = Substitute.For<IPostgresService>();
        _logger = Substitute.For<ILogger<GetSchemaCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_postgresService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSchema_WhenSchemaExists()
    {
        var expectedSchema = new List<string>(["CREATE TABLE test (id INT);"]);
        _postgresService.GetTableSchemaAsync("sub123", "rg1", "user1", "server1", "db123", "table123").Returns(expectedSchema);

        var command = new GetSchemaCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--resource-group", "rg1", "--user-name", "user1", "--server", "server1", "--database", "db123", "--table", "table123"]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<GetSchemaResult>(json);
        Assert.NotNull(result);
        Assert.Equal(expectedSchema, result.Schema);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNull_WhenSchemaDoesNotExist()
    {
        _postgresService.GetTableSchemaAsync("sub123", "rg1", "user1", "server1", "db123", "table123").Returns([]);

        var command = new GetSchemaCommand(_logger);
        var args = command.GetCommand().Parse(["--subscription", "sub123", "--resource-group", "rg1", "--user-name", "user1", "--server", "server1", "--database", "db123", "--table", "table123"]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Equal("Success", response.Message);
        Assert.Null(response.Results);
    }

    [Theory]
    [InlineData("--subscription")]
    [InlineData("--resource-group")]
    [InlineData("--user-name")]
    [InlineData("--server")]
    [InlineData("--database")]
    [InlineData("--table")]
    public async Task ExecuteAsync_ReturnsError_WhenParameterIsMissing(string missingParameter)
    {
        var command = new GetSchemaCommand(_logger);
        var args = command.GetCommand().Parse(new string[]
        {
            missingParameter == "--subscription" ? "" : "--subscription", "sub123",
            missingParameter == "--resource-group" ? "" : "--resource-group", "rg1",
            missingParameter == "--user-name" ? "" : "--user-name", "user1",
            missingParameter == "--server" ? "" : "--server", "server123",
            missingParameter == "--database" ? "" : "--database", "db123",
            missingParameter == "--table" ? "" : "--table", "table123"
        });

        var context = new CommandContext(_serviceProvider);
        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Equal($"Missing Required options: {missingParameter}", response.Message);
    }

    private class GetSchemaResult
    {
        [JsonPropertyName("Schema")]
        public List<string> Schema { get; set; } = new List<string>();
    }
}
