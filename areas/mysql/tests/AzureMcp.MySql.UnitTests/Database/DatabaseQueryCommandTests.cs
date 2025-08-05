// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.MySql.Commands.Database;
using AzureMcp.MySql.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.MySql.UnitTests.Database;

public class DatabaseQueryCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMySqlService _mysqlService;
    private readonly ILogger<DatabaseQueryCommand> _logger;

    public DatabaseQueryCommandTests()
    {
        _mysqlService = Substitute.For<IMySqlService>();
        _logger = Substitute.For<ILogger<DatabaseQueryCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_mysqlService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsResults_WhenQuerySucceeds()
    {
        var expectedResults = new List<string> { "id, name", "1, John", "2, Jane" };
        _mysqlService.ExecuteQueryAsync("sub123", "rg1", "user1", "server1", "db1", "SELECT * FROM users").Returns(expectedResults);

        var command = new DatabaseQueryCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123", 
            "--resource-group", "rg1", 
            "--user", "user1", 
            "--server", "server1", 
            "--database", "db1", 
            "--query", "SELECT * FROM users"
        ]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<DatabaseQueryResult>(json);
        Assert.NotNull(result);
        Assert.Equal(expectedResults, result.Results);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsError_WhenQueryFails()
    {
        _mysqlService.ExecuteQueryAsync("sub123", "rg1", "user1", "server1", "db1", "INVALID SQL").ThrowsAsync(new InvalidOperationException("Syntax error"));

        var command = new DatabaseQueryCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123", 
            "--resource-group", "rg1", 
            "--user", "user1", 
            "--server", "server1", 
            "--database", "db1", 
            "--query", "INVALID SQL"
        ]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Syntax error", response.Message);
    }

    [Fact]
    public void Metadata_IsConfiguredCorrectly()
    {
        var command = new DatabaseQueryCommand(_logger);
        
        Assert.Equal("query", command.Name);
        Assert.Equal("Executes a query against a MySQL database.", command.Description);
        Assert.Equal("Query MySQL Database", command.Title);
        Assert.False(command.Metadata.Destructive);
        Assert.True(command.Metadata.ReadOnly);
    }

    private class DatabaseQueryResult
    {
        [JsonPropertyName("Results")]
        public List<string> Results { get; set; } = new List<string>();
    }
}
