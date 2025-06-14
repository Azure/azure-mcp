// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine.Parsing;
using System.Text.Json;
using System.Text.Json.Serialization;
using AzureMcp.Commands.Sql.Db;
using AzureMcp.Models.Command;
using AzureMcp.Models.Sql;
using AzureMcp.Options;
using AzureMcp.Services.Azure.Sql.Exceptions;
using AzureMcp.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace AzureMcp.Tests.Commands.Sql.Db;

public class DbAdviseCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ISqlService _sqlService;
    private readonly ILogger<DbAdviseCommand> _logger;
    private readonly DbAdviseCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public DbAdviseCommandTests()
    {
        _sqlService = Substitute.For<ISqlService>();
        _logger = Substitute.For<ILogger<DbAdviseCommand>>();

        var collection = new ServiceCollection().AddSingleton(_sqlService);

        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public async Task ExecuteAsync_WithRecommendations_ReturnsSuccess()
    {
        // Arrange
        var subscriptionId = "sub123";
        var database = "TestDB";
        var serverName = "TestServer";
        var resourceGroup = "TestRG";
        var recommendations = new List<SqlRecommendation>
        {
            new() { TableName = "Users", Name = "IX_Users_Email", Impact = 85, ExpectedImprovementPercent = 85.5 },
            new() { TableName = "Orders", Name = "IX_Orders_Date", Impact = 65, ExpectedImprovementPercent = 65.2 }
        };
        var analysisResult = new SqlAnalysisResult
        {
            AnalysisSuccessful = true,
            Recommendations = recommendations,
            AnalysisSummary = "Analysis completed successfully"
        };

        _sqlService.GetRecommendationsAsync(
            Arg.Is(database),
            Arg.Is(serverName),
            Arg.Is(resourceGroup),
            Arg.Any<string>(),
            Arg.Any<int?>(),
            Arg.Any<string>(),
            Arg.Is(subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(analysisResult);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--database", database,
            "--server", serverName,
            "--resource-group", resourceGroup
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Contains("Found 2 recommendation(s)", response.Message);
        Assert.NotNull(response.Results);

        var json = JsonSerializer.Serialize(response.Results);
        var result = JsonSerializer.Deserialize<DbAdviseResult>(json);

        Assert.NotNull(result);
        Assert.NotNull(result.Analysis);
        Assert.Equal(2, result.Analysis.TotalRecommendations);
        Assert.True(result.Analysis.AnalysisSuccessful);
        Assert.Equal(2, result.Recommendations.Count);
    }

    [Fact]
    public async Task ExecuteAsync_NoRecommendations_ReturnsSuccess()
    {
        // Arrange
        var subscriptionId = "sub123";
        var database = "TestDB";
        var serverName = "TestServer";
        var resourceGroup = "TestRG";
        var analysisResult = new SqlAnalysisResult
        {
            AnalysisSuccessful = true,
            Recommendations = [],
            AnalysisSummary = "No recommendations found"
        };

        _sqlService.GetRecommendationsAsync(
            Arg.Is(database),
            Arg.Is(serverName),
            Arg.Is(resourceGroup),
            Arg.Any<string>(),
            Arg.Any<int?>(),
            Arg.Any<string>(),
            Arg.Is(subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(analysisResult);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--database", database,
            "--server", serverName,
            "--resource-group", resourceGroup
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Contains("Analysis completed", response.Message);
        Assert.Contains("No recommendations found", response.Message);
        Assert.NotNull(response.Results);
    }

    [Fact]
    public async Task ExecuteAsync_WithOptionalParameters_PassesCorrectly()
    {
        // Arrange
        var subscriptionId = "sub123";
        var database = "TestDB";
        var serverName = "TestServer";
        var resourceGroup = "TestRG";
        var tableName = "Users";
        var minImpact = 50;
        var advisorType = "CreateIndex";

        var analysisResult = new SqlAnalysisResult
        {
            AnalysisSuccessful = true,
            Recommendations = [],
            AnalysisSummary = "Analysis completed"
        };

        _sqlService.GetRecommendationsAsync(
            Arg.Is(database),
            Arg.Is(serverName),
            Arg.Is(resourceGroup),
            Arg.Is(tableName),
            Arg.Is(minImpact),
            Arg.Is(advisorType),
            Arg.Is(subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(analysisResult);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--database", database,
            "--server", serverName,
            "--resource-group", resourceGroup,
            "--table", tableName,
            "--minimum-impact", minImpact.ToString(),
            "--advisor-type", advisorType
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(200, response.Status);
        Assert.Contains("CreateIndex advisor", response.Message);

        await _sqlService.Received(1).GetRecommendationsAsync(
            database,
            serverName,
            resourceGroup,
            tableName,
            minImpact,
            advisorType,
            subscriptionId,
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>());
    }

    [Fact]
    public async Task ExecuteAsync_AnalysisFailure_ReturnsError()
    {
        // Arrange
        var subscriptionId = "sub123";
        var database = "TestDB";
        var serverName = "TestServer";
        var resourceGroup = "TestRG";

        var analysisResult = new SqlAnalysisResult
        {
            AnalysisSuccessful = false,
            Recommendations = [],
            AnalysisSummary = "Analysis failed due to insufficient permissions"
        };

        _sqlService.GetRecommendationsAsync(
            Arg.Is(database),
            Arg.Is(serverName),
            Arg.Is(resourceGroup),
            Arg.Any<string>(),
            Arg.Any<int?>(),
            Arg.Any<string>(),
            Arg.Is(subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .Returns(analysisResult);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--database", database,
            "--server", serverName,
            "--resource-group", resourceGroup
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Equal("Analysis failed due to insufficient permissions", response.Message);
    }
    [Fact]
    public async Task ExecuteAsync_SqlException_ReturnsError()
    {
        // Arrange
        var subscriptionId = "sub123";
        var database = "TestDB";
        var serverName = "TestServer";
        var resourceGroup = "TestRG";
        var sqlException = new Exception("SQL error occurred");

        _sqlService.GetRecommendationsAsync(
            Arg.Is(database),
            Arg.Is(serverName),
            Arg.Is(resourceGroup),
            Arg.Any<string>(),
            Arg.Any<int?>(),
            Arg.Any<string>(),
            Arg.Is(subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(sqlException);

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--database", database,
            "--server", serverName,
            "--resource-group", resourceGroup
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("SQL error occurred", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_DatabaseNotFoundException_ReturnsError()
    {
        // Arrange
        var subscriptionId = "sub123";
        var database = "NonExistentDB";
        var serverName = "TestServer";
        var resourceGroup = "TestRG";

        _sqlService.GetRecommendationsAsync(
            Arg.Is(database),
            Arg.Is(serverName),
            Arg.Is(resourceGroup),
            Arg.Any<string>(),
            Arg.Any<int?>(),
            Arg.Any<string>(),
            Arg.Is(subscriptionId),
            Arg.Any<string>(),
            Arg.Any<RetryPolicyOptions>())
            .ThrowsAsync(new DatabaseNotFoundException("Database not found"));

        var args = _parser.Parse([
            "--subscription", subscriptionId,
            "--database", database,
            "--server", serverName,
            "--resource-group", resourceGroup
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Database not found", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_MissingRequiredParameter_ReturnsValidationError()
    {
        // Arrange
        var subscriptionId = "sub123";

        var args = _parser.Parse([
            "--subscription", subscriptionId
            // Missing required database, server-name, and resource-group
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, args);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(400, response.Status);
        Assert.Contains("Missing Required options:", response.Message);
        Assert.Contains("--server", response.Message);
        Assert.Contains("--database", response.Message);
        Assert.Contains("--resource-group", response.Message);
    }
    private class DbAdviseResult
    {
        [JsonPropertyName("analysis")]
        public SqlAnalysisResult Analysis { get; set; } = new();

        [JsonPropertyName("recommendations")]
        public List<SqlRecommendation> Recommendations { get; set; } = [];
    }
}
