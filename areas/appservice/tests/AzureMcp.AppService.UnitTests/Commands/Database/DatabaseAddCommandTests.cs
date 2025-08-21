// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using AzureMcp.AppService.Commands.Database;
using AzureMcp.AppService.Models;
using AzureMcp.AppService.Services;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.AppService.UnitTests.Commands.Database;

[Trait("Area", "AppService")]
[Trait("Command", "DatabaseAdd")]
public class DatabaseAddCommandTests
{
    private readonly IAppServiceService _appServiceService;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseAddCommand> _logger;

    public DatabaseAddCommandTests()
    {
        _appServiceService = Substitute.For<IAppServiceService>();
        _logger = Substitute.For<ILogger<DatabaseAddCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_appServiceService);
        collection.AddSingleton(_logger);
        _serviceProvider = collection.BuildServiceProvider();
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
            ConnectionString = "Server=test-server.database.windows.net;Database=test-db;Trusted_Connection=True;TrustServerCertificate=True;",
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
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedConnection);

        var command = new DatabaseAddCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app", appName,
            "--database-type", databaseType,
            "--database-server", databaseServer,
            "--database", databaseName
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_WithConnectionString_PassesConnectionStringToService()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var connectionString = "Server=custom-server;Database=custom-db;";

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
            Arg.Is(appName),
            Arg.Is(resourceGroup),
            Arg.Is(databaseType),
            Arg.Is(databaseServer),
            Arg.Is(databaseName),
            Arg.Is(connectionString),
            Arg.Is(subscription),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedConnection);

        var command = new DatabaseAddCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app", appName,
            "--database-type", databaseType,
            "--database-server", databaseServer,
            "--database", databaseName,
            "--connection-string", connectionString
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);

        await _appServiceService.Received(1).AddDatabaseAsync(
            appName,
            resourceGroup,
            databaseType,
            databaseServer,
            databaseName,
            connectionString,
            subscription,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_WithTenant_PassesTenantToService()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";
        var tenant = "tenant123";

        var expectedConnection = new DatabaseConnectionInfo
        {
            DatabaseType = databaseType,
            DatabaseServer = databaseServer,
            DatabaseName = databaseName,
            ConnectionString = "Server=test-server.database.windows.net;Database=test-db;Trusted_Connection=True;TrustServerCertificate=True;",
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
            Arg.Is(tenant),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedConnection);

        var command = new DatabaseAddCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app", appName,
            "--database-type", databaseType,
            "--database-server", databaseServer,
            "--database", databaseName,
            "--tenant", tenant
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);

        await _appServiceService.Received(1).AddDatabaseAsync(
            appName,
            resourceGroup,
            databaseType,
            databaseServer,
            databaseName,
            Arg.Any<string>(),
            subscription,
            tenant,
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_MissingRequiredParameter_ReturnsErrorResponse()
    {
        // Arrange
        var command = new DatabaseAddCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", "sub123",
            "--resource-group", "rg1"
            // Missing required parameters
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);

        await _appServiceService.DidNotReceive().AddDatabaseAsync(
            Arg.Any<string>(),
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
    public async Task ExecuteAsync_ServiceThrowsException_ReturnsErrorResponse()
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";
        var databaseType = "SqlServer";
        var databaseServer = "test-server.database.windows.net";
        var databaseName = "test-db";

        _appServiceService
            .When(x => x.AddDatabaseAsync(
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<string>(),
                Arg.Any<RetryPolicyOptions>()))
            .Do(x => throw new InvalidOperationException("Service error"));

        var command = new DatabaseAddCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app", appName,
            "--database-type", databaseType,
            "--database-server", databaseServer,
            "--database", databaseName
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.NotEqual(200, response.Status);
    }

    [Theory]
    [InlineData("MySQL")]
    [InlineData("PostgreSQL")]
    [InlineData("CosmosDB")]
    public async Task ExecuteAsync_WithDifferentDatabaseTypes_CallsServiceCorrectly(string databaseType)
    {
        // Arrange
        var subscription = "sub123";
        var resourceGroup = "rg1";
        var appName = "test-app";
        var databaseServer = "test-server";
        var databaseName = "test-db";

        var expectedConnection = new DatabaseConnectionInfo
        {
            DatabaseType = databaseType,
            DatabaseServer = databaseServer,
            DatabaseName = databaseName,
            ConnectionString = "test-connection-string",
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
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(expectedConnection);

        var command = new DatabaseAddCommand(_logger);
        var args = command.GetCommand().Parse([
            "--subscription", subscription,
            "--resource-group", resourceGroup,
            "--app", appName,
            "--database-type", databaseType,
            "--database-server", databaseServer,
            "--database", databaseName
        ]);
        var context = new CommandContext(_serviceProvider);

        // Act
        var response = await command.ExecuteAsync(context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);

        await _appServiceService.Received(1).AddDatabaseAsync(
            appName,
            resourceGroup,
            databaseType,
            databaseServer,
            databaseName,
            Arg.Any<string>(),
            subscription,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }
}
