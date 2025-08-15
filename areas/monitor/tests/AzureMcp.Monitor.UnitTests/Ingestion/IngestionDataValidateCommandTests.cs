// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text.Json.Nodes;
using AzureMcp.Core.Models.Command;
using AzureMcp.Core.Options;
using AzureMcp.Monitor.Commands.Ingestion;
using AzureMcp.Monitor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.Monitor.UnitTests.Ingestion;

[Trait("Area", "Monitor")]
public sealed class IngestionDataValidateCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMonitorService _monitorService;
    private readonly ILogger<IngestionDataValidateCommand> _logger;
    private readonly IngestionDataValidateCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    private const string _knownDataCollectionRule = "/subscriptions/12345678-1234-1234-1234-123456789012/resourceGroups/rg-test/providers/Microsoft.Insights/dataCollectionRules/dcr-test";
    private const string _knownLogData = @"[{""TimeGenerated"": ""2023-01-01T12:00:00Z"", ""Message"": ""Test log entry"", ""Level"": ""Info""}]";
    private const string _knownTenant = "knownTenant";
    private const string _knownSubscription = "12345678-1234-1234-1234-123456789012";

    public IngestionDataValidateCommandTests()
    {
        _monitorService = Substitute.For<IMonitorService>();
        _logger = Substitute.For<ILogger<IngestionDataValidateCommand>>();

        var collection = new ServiceCollection();
        collection.AddSingleton(_monitorService);
        _serviceProvider = collection.BuildServiceProvider();

        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void ParseArguments_WithAllRequiredOptions_ReturnsValid()
    {
        // Arrange
        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", _knownLogData
        ]);

        // Act & Assert
        Assert.Empty(parseResult.Errors);
    }

    [Fact]
    public void ParseArguments_WithAllRequiredOptionsAndTenant_ReturnsValid()
    {
        // Arrange
        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", _knownLogData,
            "--tenant", _knownTenant
        ]);

        // Act & Assert
        Assert.Empty(parseResult.Errors);
    }

    [Fact]
    public void ParseArguments_MissingLogData_ReturnsError()
    {
        // Arrange
        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule
        ]);

        // Act & Assert
        Assert.NotEmpty(parseResult.Errors);
    }

    [Fact]
    public void ParseArguments_MissingDataCollectionRule_ReturnsError()
    {
        // Arrange
        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--log-data", _knownLogData
        ]);

        // Act & Assert
        Assert.NotEmpty(parseResult.Errors);
    }

    [Fact]
    public void ParseArguments_MissingAllRequired_ReturnsError()
    {
        // Arrange
        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription
        ]);

        // Act & Assert
        Assert.NotEmpty(parseResult.Errors);
    }

    [Fact]
    public async Task ExecuteAsync_ValidInput_ReturnsValidResult()
    {
        // Arrange
        var validationResults = new JsonObject
        {
            ["summary"] = new JsonObject
            {
                ["dataCollectionRule"] = _knownDataCollectionRule,
                ["recordCount"] = 1,
                ["errorCount"] = 0,
                ["warningCount"] = 0,
                ["isValid"] = true
            },
            ["validationResults"] = new JsonArray()
        };

        _monitorService.ValidateLogData(
            Arg.Is(_knownDataCollectionRule),
            Arg.Is(_knownLogData),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(("Valid", "Validation passed successfully for 1 record(s)", validationResults));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", _knownLogData
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);
    }

    [Fact]
    public async Task ExecuteAsync_InvalidJson_ReturnsInvalidResult()
    {
        // Arrange
        var invalidLogData = "invalid json";
        var validationResults = new JsonObject
        {
            ["summary"] = new JsonObject
            {
                ["dataCollectionRule"] = _knownDataCollectionRule,
                ["recordCount"] = 0,
                ["errorCount"] = 1,
                ["warningCount"] = 0,
                ["isValid"] = false
            },
            ["validationResults"] = new JsonArray([
                new JsonObject
                {
                    ["type"] = "error",
                    ["field"] = "logData",
                    ["message"] = "Invalid JSON format",
                    ["code"] = "INVALID_JSON"
                }
            ])
        };

        _monitorService.ValidateLogData(
            Arg.Is(_knownDataCollectionRule),
            Arg.Is(invalidLogData),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(("Invalid", "Validation failed with 1 error(s)", validationResults));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", invalidLogData
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status); // The command succeeds, but validation fails
        Assert.Contains("failed", response.Message.ToLowerInvariant());
    }

    [Fact]
    public async Task ExecuteAsync_ValidDataWithWarnings_ReturnsValidWithWarningsResult()
    {
        // Arrange
        var logDataWithoutTimeGenerated = @"[{""Message"": ""Test log entry"", ""Level"": ""Info""}]";
        var validationResults = new JsonObject
        {
            ["summary"] = new JsonObject
            {
                ["dataCollectionRule"] = _knownDataCollectionRule,
                ["recordCount"] = 1,
                ["errorCount"] = 0,
                ["warningCount"] = 1,
                ["isValid"] = true
            },
            ["validationResults"] = new JsonArray([
                new JsonObject
                {
                    ["type"] = "warning",
                    ["field"] = "logData[0].TimeGenerated",
                    ["message"] = "TimeGenerated field is recommended for custom logs",
                    ["code"] = "MISSING_TIMEGENERATED"
                }
            ])
        };

        _monitorService.ValidateLogData(
            Arg.Is(_knownDataCollectionRule),
            Arg.Is(logDataWithoutTimeGenerated),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(("Valid with warnings", "Validation passed with 1 warning(s)", validationResults));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", logDataWithoutTimeGenerated
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.NotNull(response.Results);
        Assert.Equal(200, response.Status);
        Assert.Contains("warning", response.Message.ToLowerInvariant());
    }

    [Fact]
    public async Task ExecuteAsync_ServiceException_HandlesError()
    {
        // Arrange
        _monitorService.ValidateLogData(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(Task.FromException<(string, string, JsonNode?)>(new InvalidOperationException("Service error")));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", _knownLogData
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(500, response.Status);
        Assert.Contains("Service error", response.Message);
    }

    [Fact]
    public async Task ExecuteAsync_WithRetryPolicy_PassesRetryPolicyToService()
    {
        // Arrange
        var validationResults = new JsonObject
        {
            ["summary"] = new JsonObject
            {
                ["dataCollectionRule"] = _knownDataCollectionRule,
                ["recordCount"] = 1,
                ["errorCount"] = 0,
                ["warningCount"] = 0,
                ["isValid"] = true
            },
            ["validationResults"] = new JsonArray()
        };

        _monitorService.ValidateLogData(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(("Valid", "Validation passed successfully for 1 record(s)", validationResults));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", _knownLogData,
            "--retry-policy-mode", "exponential",
            "--retry-policy-max-retries", "5"
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        await _monitorService.Received(1).ValidateLogData(
            Arg.Is(_knownDataCollectionRule),
            Arg.Is(_knownLogData),
            Arg.Any<string?>(),
            Arg.Is<RetryPolicyOptions?>(r => r != null));
    }

    [Fact]
    public async Task ExecuteAsync_WithTenant_PassesTenantToService()
    {
        // Arrange
        var validationResults = new JsonObject
        {
            ["summary"] = new JsonObject
            {
                ["dataCollectionRule"] = _knownDataCollectionRule,
                ["recordCount"] = 1,
                ["errorCount"] = 0,
                ["warningCount"] = 0,
                ["isValid"] = true
            },
            ["validationResults"] = new JsonArray()
        };

        _monitorService.ValidateLogData(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<string?>(),
            Arg.Any<RetryPolicyOptions?>())
            .Returns(("Valid", "Validation passed successfully for 1 record(s)", validationResults));

        var parseResult = _parser.Parse([
            "--subscription", _knownSubscription,
            "--data-collection-rule", _knownDataCollectionRule,
            "--log-data", _knownLogData,
            "--tenant", _knownTenant
        ]);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.NotNull(response);
        await _monitorService.Received(1).ValidateLogData(
            Arg.Is(_knownDataCollectionRule),
            Arg.Is(_knownLogData),
            Arg.Is(_knownTenant),
            Arg.Any<RetryPolicyOptions?>());
    }

    [Fact]
    public void Name_ReturnsExpectedValue()
    {
        // Assert
        Assert.Equal("data-validate", _command.Name);
    }

    [Fact]
    public void Description_ContainsExpectedKeywords()
    {
        // Assert
        Assert.Contains("validate", _command.Description.ToLowerInvariant());
        Assert.Contains("custom log data", _command.Description.ToLowerInvariant());
        Assert.Contains("azure monitor", _command.Description.ToLowerInvariant());
    }

    [Fact]
    public void Title_ReturnsExpectedValue()
    {
        // Assert
        Assert.Equal("Validate Log Data for Azure Monitor Ingestion", _command.Title);
    }

    [Fact]
    public void Metadata_IsReadOnly()
    {
        // Assert
        Assert.False(_command.Metadata.Destructive);
        Assert.True(_command.Metadata.ReadOnly);
    }
}
