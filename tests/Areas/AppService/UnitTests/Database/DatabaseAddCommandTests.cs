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

    [Fact]
    public void CommandDocumentation_ExistsInDocumentationFile()
    {
        // Arrange
        var documentationPath = Path.Combine(GetProjectRoot(), "docs", "azmcp-commands.md");
        
        // Act
        var documentationContent = File.ReadAllText(documentationPath);
        
        // Assert
        Assert.Contains("### Azure App Service Operations", documentationContent);
        Assert.Contains("azmcp appservice database add", documentationContent);
        Assert.Contains("--app-name", documentationContent);
        Assert.Contains("--resource-group", documentationContent);
        Assert.Contains("--database-type", documentationContent);
        Assert.Contains("--database-server", documentationContent);
        Assert.Contains("--database-name", documentationContent);
        Assert.Contains("--connection-string", documentationContent);
        
        // Verify examples are present
        Assert.Contains("SqlServer", documentationContent);
        Assert.Contains("PostgreSql", documentationContent);
        Assert.Contains("MySql", documentationContent);
        Assert.Contains("CosmosDb", documentationContent);
    }

    [Fact]
    public void CommandDocumentation_HasCorrectExamples()
    {
        // Arrange
        var documentationPath = Path.Combine(GetProjectRoot(), "docs", "azmcp-commands.md");
        var documentationContent = File.ReadAllText(documentationPath);
        
        // Assert that documentation includes practical examples
        Assert.Contains("# Add a SQL Server database connection", documentationContent);
        Assert.Contains("# Add a PostgreSQL database connection with custom connection string", documentationContent);
        Assert.Contains("myserver.database.windows.net", documentationContent);
        Assert.Contains("myserver.postgres.database.azure.com", documentationContent);
    }

    [Fact]
    public void CommandDocumentation_FollowsEstablishedFormat()
    {
        // Arrange
        var documentationPath = Path.Combine(GetProjectRoot(), "docs", "azmcp-commands.md");
        var documentationContent = File.ReadAllText(documentationPath);
        
        // Extract the App Service section
        var appServiceSectionStart = documentationContent.IndexOf("### Azure App Service Operations");
        var nextSectionStart = documentationContent.IndexOf("## Response Format", appServiceSectionStart);
        var appServiceSection = documentationContent.Substring(appServiceSectionStart, nextSectionStart - appServiceSectionStart);
        
        // Assert format consistency
        Assert.Contains("**Parameters:**", appServiceSection);
        Assert.Contains("**Example:**", appServiceSection);
        Assert.Contains("```bash", appServiceSection);
        Assert.Contains("--subscription <subscription>", appServiceSection);
        
        // Verify parameter descriptions follow the pattern
        Assert.Contains("- `--app-name`: Name of the Azure App Service application", appServiceSection);
        Assert.Contains("- `--resource-group`: Resource group containing the App Service", appServiceSection);
        Assert.Contains("- `--database-type`: Type of database", appServiceSection);
    }

    private static string GetProjectRoot()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        while (currentDirectory != null && !File.Exists(Path.Combine(currentDirectory, "AzureMcp.sln")))
        {
            currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        }
        return currentDirectory ?? throw new DirectoryNotFoundException("Could not find project root");
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
