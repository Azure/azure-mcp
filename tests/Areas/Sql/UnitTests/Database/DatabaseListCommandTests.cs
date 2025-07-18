// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.Sql.Commands.Database;
using AzureMcp.Areas.Sql.Models;
using AzureMcp.Areas.Sql.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.Sql.UnitTests.Database;

[Trait("Area", "Sql")]
public class DatabaseListCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISqlService _sqlService;
    private readonly ILogger<DatabaseListCommand> _logger;
    private readonly DatabaseListCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public DatabaseListCommandTests()
    {
        _sqlService = Substitute.For<ISqlService>();
        _logger = Substitute.For<ILogger<DatabaseListCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_sqlService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("list", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("databases on an Azure SQL Server", command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidParameters_ReturnsDatabases()
    {
        // Arrange
        var mockDatabases = new List<SqlDatabase>
        {
            new SqlDatabase(
                Name: "testdb1",
                Id: "/subscriptions/sub/resourceGroups/rg/providers/Microsoft.Sql/servers/server1/databases/testdb1",
                Type: "Microsoft.Sql/servers/databases",
                Location: "East US",
                Sku: null,
                Status: "Online",
                Collation: "SQL_Latin1_General_CP1_CI_AS",
                CreationDate: DateTimeOffset.UtcNow,
                MaxSizeBytes: 1073741824,
                ServiceLevelObjective: "S0",
                Edition: "Standard",
                ElasticPoolName: null,
                EarliestRestoreDate: DateTimeOffset.UtcNow,
                ReadScale: "Disabled",
                ZoneRedundant: false
            ),
            new SqlDatabase(
                Name: "testdb2",
                Id: "/subscriptions/sub/resourceGroups/rg/providers/Microsoft.Sql/servers/server1/databases/testdb2",
                Type: "Microsoft.Sql/servers/databases",
                Location: "East US",
                Sku: null,
                Status: "Online",
                Collation: "SQL_Latin1_General_CP1_CI_AS",
                CreationDate: DateTimeOffset.UtcNow,
                MaxSizeBytes: 2147483648,
                ServiceLevelObjective: "S1",
                Edition: "Standard",
                ElasticPoolName: null,
                EarliestRestoreDate: DateTimeOffset.UtcNow,
                ReadScale: "Disabled",
                ZoneRedundant: false
            )
        };

        _sqlService.ListDatabasesAsync(
            Arg.Is("server1"),
            Arg.Is("rg"),
            Arg.Is("sub"),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<CancellationToken>())
            .Returns(mockDatabases);

        var args = _parser.Parse(["--subscription", "sub", "--resource-group", "rg", "--server", "server1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithEmptyDatabaseList_ReturnsEmptyResults()
    {
        // Arrange
        var emptyDatabases = new List<SqlDatabase>();

        _sqlService.ListDatabasesAsync(
            Arg.Is("server1"),
            Arg.Is("rg"),
            Arg.Is("sub"),
            Arg.Any<RetryPolicyOptions>(),
            Arg.Any<CancellationToken>())
            .Returns(emptyDatabases);

        var args = _parser.Parse(["--subscription", "sub", "--resource-group", "rg", "--server", "server1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Null(response.Results);
        Assert.Equal("Success", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesServiceErrors()
    {
        // Arrange
        _sqlService.ListDatabasesAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsync(new Exception("Test error"));

        var args = _parser.Parse(["--subscription", "sub", "--resource-group", "rg", "--server", "server1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(500, response.Status);
        Assert.Contains("Test error", response.Message);
        Assert.Contains("troubleshooting", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesRequestFailedException()
    {
        // Arrange
        var requestException = new Azure.RequestFailedException(404, "SQL server not found");
        _sqlService.ListDatabasesAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsync(requestException);

        var args = _parser.Parse(["--subscription", "sub", "--resource-group", "rg", "--server", "server1"]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.Equal(404, response.Status);
        Assert.Contains("SQL server not found", response.Message);
    }

    [Theory]
    [InlineData("--subscription sub", false)]
    [InlineData("--resource-group rg", false)]
    [InlineData("--server server1", false)]
    [InlineData("--subscription sub --resource-group rg", false)]
    [InlineData("--subscription sub --server server1", false)]
    [InlineData("--resource-group rg --server server1", false)]
    [InlineData("--subscription sub --resource-group rg --server server1", true)]
    public async Task ExecuteAsync_ValidatesRequiredOptions(string args, bool shouldSucceed)
    {
        // Arrange
        if (shouldSucceed)
        {
            _sqlService.ListDatabasesAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>(),
                Arg.Any<CancellationToken>())
                .Returns(new List<SqlDatabase>());
        }

        var parseResult = _parser.Parse(args.Split(' '));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(shouldSucceed ? 200 : 400, response.Status);
        if (!shouldSucceed)
        {
            Assert.Contains("required", response.Message.ToLower());
        }
    }
}
