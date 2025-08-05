// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Core.Models.Command;
using AzureMcp.MySql.Commands.Table;
using AzureMcp.MySql.Services;
using AzureMcp.Tests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.MySql.UnitTests.Table;

public class TableSchemaCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMySqlService _mysqlService;
    private readonly ILogger<TableSchemaCommand> _logger;

    public TableSchemaCommandTests()
    {
        _mysqlService = Substitute.For<IMySqlService>();
        _logger = Substitute.For<ILogger<TableSchemaCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_mysqlService);

        _serviceProvider = collection.BuildServiceProvider();
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsSchema_WhenSuccessful()
    {
        var expectedSchema = new List<string> { "id INT PRIMARY KEY", "name VARCHAR(100) NOT NULL", "email VARCHAR(255)" };
        _mysqlService.GetTableSchemaAsync("sub123", "rg1", "user1", "server1", "db1", "users").Returns(expectedSchema);

        var command = new TableSchemaCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "rg1",
            "--user-name", "user1",
            "--server", "server1",
            "--database", "db1",
            "--table", "users"
        ]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        
        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<TableSchemaResult>(json);
        Assert.NotNull(result);
        Assert.Equal(expectedSchema, result.Schema);
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsError_WhenTableNotFound()
    {
        _mysqlService.GetTableSchemaAsync("sub123", "rg1", "user1", "server1", "db1", "nonexistent").ThrowsAsync(new ArgumentException("Table not found"));

        var command = new TableSchemaCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "rg1",
            "--user-name", "user1",
            "--server", "server1",
            "--database", "db1",
            "--table", "nonexistent"
        ]);
        var context = new CommandContext(_serviceProvider);

        var response = await command.ExecuteAsync(context, args);

        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Table not found", response.Message);
    }

    [Fact]
    public void Metadata_IsConfiguredCorrectly()
    {
        var command = new TableSchemaCommand(_logger);
        
        Assert.Equal("schema", command.Name);
        Assert.Equal("Gets the schema of a MySQL table.", command.Description);
        Assert.Equal("Get MySQL Table Schema", command.Title);
        Assert.False(command.Metadata.Destructive);
        Assert.True(command.Metadata.ReadOnly);
    }

    private class TableSchemaResult
    {
        [JsonPropertyName("Schema")]
        public List<string> Schema { get; set; } = new List<string>();
    }
}
