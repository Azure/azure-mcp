// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using AzureMcp.Areas.AppService.Commands.Database;
using AzureMcp.Areas.AppService.Models;
using AzureMcp.Areas.AppService.Services;
using AzureMcp.Models.Command;
using AzureMcp.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Areas.AppService.UnitTests.Database;

[Trait("Area", "AppService")]
public sealed class DatabaseAddCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAppServiceService _appServiceService;
    private readonly ILogger<DatabaseAddCommand> _logger;
    private readonly DatabaseAddCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public DatabaseAddCommandTests()
    {
        _appServiceService = Substitute.For<IAppServiceService>();
        _logger = Substitute.For<ILogger<DatabaseAddCommand>>();

        var collection = new ServiceCollection().AddSingleton(_appServiceService);
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_WithValidParameters_ReturnsSuccess()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";

        var expectedConnection = new DatabaseConnectionInfo
        {
            DatabaseType = databaseType,
            DatabaseServer = databaseServer,
            DatabaseName = databaseName,
            ConnectionString = "Server=test-server.database.windows.net;Database=test-db;Trusted_Connection=True;",
            ConnectionStringName = "test-dbConnection",
            IsConfigured = true,
            ConfiguredAt = DateTime.UtcNow
        };

        _appServiceService.AddDatabaseAsync(
            Arg.Is(appName),
            Arg.Is(resourceGroup),
            Arg.Is(databaseType),
            Arg.Is(databaseServer),
            Arg.Is(databaseName),
            Arg.Any<string>(),
            Arg.Is(subscription),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedConnection);

        var args = _parser.Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app-name", appName,
            "--database-type", databaseType,
            "--database-server", databaseServer,
            "--database-name", databaseName
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        // For this test, we'll just verify the response structure
        // The actual Results validation would be done in integration tests
    }

    [Fact]
    public async Task Service_IsCalledWithCorrectParameters()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var connectionString = "custom-connection-string";

        var expectedConnection = new DatabaseConnectionInfo
        {
            DatabaseType = databaseType,
            DatabaseServer = databaseServer,
            DatabaseName = databaseName,
            ConnectionString = connectionString,
            ConnectionStringName = "test-dbConnection",
            IsConfigured = true,
            ConfiguredAt = DateTime.UtcNow
        };

        _appServiceService.AddDatabaseAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedConnection);

        var args = _parser.Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app-name", appName,
            "--database-type", databaseType,
            "--database-server", databaseServer,
            "--database-name", databaseName,
            "--connection-string", connectionString
        ]);

        // Act
        await _command.ExecuteAsync(_context, args);

        // Assert - Verify service was called with correct parameters
        await _appServiceService.Received(1).AddDatabaseAsync(
            Arg.Is(appName),
            Arg.Is(resourceGroup),
            Arg.Is(databaseType),
            Arg.Is(databaseServer),
            Arg.Is(databaseName),
            Arg.Is(connectionString),
            Arg.Is(subscription),
            Arg.Any<RetryPolicyOptions>());
    }

    [Theory]
    [InlineData("SqlServer", "server.database.windows.net", "TestDB")]
    [InlineData("MySql", "server.mysql.database.azure.com", "TestDB")]
    [InlineData("PostgreSql", "server.postgres.database.azure.com", "TestDB")]
    [InlineData("CosmosDb", "testcosmosdb", "TestDB")]
    public async Task ExecuteAsync_WithDifferentDatabaseTypes_CallsService(string databaseType, string server, string database)
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";

        var expectedConnection = new DatabaseConnectionInfo
        {
            DatabaseType = databaseType,
            DatabaseServer = server,
            DatabaseName = database,
            ConnectionString = $"TestConnectionString-{databaseType}",
            ConnectionStringName = $"{database}Connection",
            IsConfigured = true,
            ConfiguredAt = DateTime.UtcNow
        };

        _appServiceService.AddDatabaseAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedConnection);

        var args = _parser.Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app-name", appName,
            "--database-type", databaseType,
            "--database-server", server,
            "--database-name", database
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        await _appServiceService.Received(1).AddDatabaseAsync(
            Arg.Is(appName),
            Arg.Is(resourceGroup),
            Arg.Is(databaseType),
            Arg.Is(server),
            Arg.Is(database),
            Arg.Any<string>(),
            Arg.Is(subscription),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithInvalidParameters_DoesNotCallService()
    {
        // Arrange - missing required parameters
        var args = _parser.Parse([
            "--subscription", "sub123"
            // Missing required parameters
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        // Should not call service when validation fails
        await _appServiceService.DidNotReceive().AddDatabaseAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WhenServiceThrowsUnauthorizedException_Returns403()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";

        _appServiceService.AddDatabaseAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new UnauthorizedAccessException("Access denied"));

        var args = _parser.Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app-name", appName,
            "--database-type", "SqlServer",
            "--database-server", "test-server",
            "--database-name", "test-db"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(403, response.Status);
        Assert.Contains("Access denied", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenServiceThrowsArgumentException_Returns400()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";

        _appServiceService.AddDatabaseAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new ArgumentException("Invalid database type"));

        var args = _parser.Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app-name", appName,
            "--database-type", "InvalidType",
            "--database-server", "test-server",
            "--database-name", "test-db"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("Invalid database type", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WhenServiceThrowsGenericException_Returns409()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";

        _appServiceService.AddDatabaseAsync(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new InvalidOperationException("Service unavailable"));

        var args = _parser.Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app-name", appName,
            "--database-type", "SqlServer",
            "--database-server", "test-server",
            "--database-name", "test-db"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(409, response.Status); // InvalidOperationException maps to 409
        Assert.Contains("The App Service is not in a valid state for this operation", response.Message);
    }
}
