// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.AppService.Commands.Database;
using AzureMcp.Areas.AppService.Models;
using AzureMcp.Areas.AppService.Options.Database;
using AzureMcp.Areas.AppService.Services;
using AzureMcp.Commands;
using AzureMcp.Models;
using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;

namespace AzureMcp.Tests.Areas.AppService.UnitTests.Database;

public class DatabaseAddCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Mock<IAppServiceService> _mockService;
    private readonly Mock<ILogger<DatabaseAddCommand>> _mockLogger;
    private readonly DatabaseAddCommand _command;

    public DatabaseAddCommandTests()
    {
        _mockService = new Mock<IAppServiceService>();
        _mockLogger = new Mock<ILogger<DatabaseAddCommand>>();
        _command = new DatabaseAddCommand(_mockLogger.Object);

        var services = new ServiceCollection();
        services.AddSingleton(_mockService.Object);
        services.AddLogging();
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public void Constructor_SetsPropertiesCorrectly()
    {
        // Assert
        Assert.Equal("add", _command.Name);
        Assert.Equal("Add Database to App Service", _command.Title);
        Assert.Contains("Adds a database connection to an Azure App Service application", _command.Description);
    }

    [Fact]
    public async Task ExecuteAsync_WithValidOptions_ReturnsSuccess()
    {
        // Arrange
        var databaseConnection = new DatabaseConnectionInfo
        {
            DatabaseType = "SqlServer",
            DatabaseServer = "test-server",
            DatabaseName = "test-db",
            ConnectionString = "Server=test-server;Database=test-db;Trusted_Connection=True;",
            ConnectionStringName = "test-dbConnection",
            IsConfigured = true,
            ConfiguredAt = DateTime.UtcNow
        };

        _mockService.Setup(s => s.AddDatabaseAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string?>(),
            It.IsAny<string>(),
            It.IsAny<RetryPolicy>()))
            .ReturnsAsync(databaseConnection);

        var options = new DatabaseAddOptions
        {
            AppName = "test-app",
            ResourceGroup = "test-rg",
            DatabaseType = "SqlServer",
            DatabaseServer = "test-server",
            DatabaseName = "test-db",
            Subscription = "test-sub",
            RetryPolicy = RetryPolicy.Default
        };

        var context = new CommandContext(_serviceProvider);
        var parseResult = CreateParseResult(options);

        // Act
        var result = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(200, result.Status);
        Assert.NotNull(result.Results);
        _mockService.Verify(s => s.AddDatabaseAsync(
            "test-app",
            "test-rg",
            "SqlServer",
            "test-server",
            "test-db",
            null,
            "test-sub",
            It.IsAny<RetryPolicy>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_WithException_ReturnsError()
    {
        // Arrange
        _mockService.Setup(s => s.AddDatabaseAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string?>(),
            It.IsAny<string>(),
            It.IsAny<RetryPolicy>()))
            .ThrowsAsync(new UnauthorizedAccessException("Access denied"));

        var options = new DatabaseAddOptions
        {
            AppName = "test-app",
            ResourceGroup = "test-rg",
            DatabaseType = "SqlServer",
            DatabaseServer = "test-server",
            DatabaseName = "test-db",
            Subscription = "test-sub",
            RetryPolicy = RetryPolicy.Default
        };

        var context = new CommandContext(_serviceProvider);
        var parseResult = CreateParseResult(options);

        // Act
        var result = await _command.ExecuteAsync(context, parseResult);

        // Assert
        Assert.Equal(403, result.Status);
        Assert.Contains("Access denied", result.Message);
    }

    private static ParseResult CreateParseResult(DatabaseAddOptions options)
    {
        var rootCommand = new RootCommand();
        var command = new Command("add");
        
        command.AddOption(new Option<string>("--app-name"));
        command.AddOption(new Option<string>("--resource-group"));
        command.AddOption(new Option<string>("--database-type"));
        command.AddOption(new Option<string>("--database-server"));
        command.AddOption(new Option<string>("--database-name"));
        command.AddOption(new Option<string>("--subscription"));
        
        rootCommand.Add(command);

        var args = new List<string>
        {
            "add",
            "--app-name", options.AppName ?? "",
            "--resource-group", options.ResourceGroup ?? "",
            "--database-type", options.DatabaseType ?? "",
            "--database-server", options.DatabaseServer ?? "",
            "--database-name", options.DatabaseName ?? "",
            "--subscription", options.Subscription ?? ""
        };

        return rootCommand.Parse(args.ToArray());
    }
}
