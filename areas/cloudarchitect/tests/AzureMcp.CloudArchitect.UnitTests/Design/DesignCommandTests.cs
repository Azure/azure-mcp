// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Text;
using System.Text.Json;
using AzureMcp.CloudArchitect;
using AzureMcp.CloudArchitect.Commands.Design;
using AzureMcp.CloudArchitect.Options;
using AzureMcp.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace AzureMcp.CloudArchitect.UnitTests.Design;

public class DesignCommandTests
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DesignCommand> _logger;
    private readonly DesignCommand _command;
    private readonly CommandContext _context;
    private readonly Parser _parser;

    public DesignCommandTests()
    {
        _logger = Substitute.For<ILogger<DesignCommand>>();

        var collection = new ServiceCollection();
        _serviceProvider = collection.BuildServiceProvider();
        _command = new(_logger);
        _context = new(_serviceProvider);
        _parser = new(_command.GetCommand());
    }

    [Fact]
    public void Constructor_InitializesCommandCorrectly()
    {
        var command = _command.GetCommand();
        Assert.Equal("design", command.Name);
        Assert.NotNull(command.Description);
        Assert.NotEmpty(command.Description);
        Assert.Contains("Design and architect comprehensive Azure cloud solutions for applications and services. This interactive assistant helps create scalable cloud architectures for file upload systems, web applications, APIs, e-commerce platforms, financial services, transaction systems, data processing services, and enterprise solutions. Through guided questions, provides tailored Azure architecture recommendations covering storage, compute, networking, databases, security, and application services to create robust user-facing cloud services and applications.", command.Description);
    }

    [Fact]
    public void Command_HasCorrectOptions()
    {
        var command = _command.GetCommand();

        // Check that the command has the expected options
        var optionNames = command.Options.Select(o => o.Name).ToList();

        Assert.Contains("question", optionNames);
        Assert.Contains("question-number", optionNames);
        Assert.Contains("total-questions", optionNames);
        Assert.Contains("answer", optionNames);
        Assert.Contains("next-question-needed", optionNames);
        Assert.Contains("confidence-score", optionNames);
        Assert.Contains("architecture-component", optionNames);
        Assert.Contains("architecture-tier", optionNames);
        Assert.Contains("state", optionNames);
    }

    [Theory]
    [InlineData("")]
    [InlineData("--question \"What is your application type?\"")]
    [InlineData("--question-number 1")]
    [InlineData("--total-questions 5")]
    [InlineData("--answer \"Web application\"")]
    [InlineData("--next-question-needed true")]
    [InlineData("--confidence-score 0.8")]
    [InlineData("--architecture-component \"Frontend\"")]
    [InlineData("--architecture-tier Infrastructure")]
    [InlineData("--question \"App type?\" --question-number 1 --total-questions 5")]
    [InlineData("--architecture-tier Platform --architecture-component \"AKS Cluster\"")]
    public async Task ExecuteAsync_ReturnsArchitectureDesignText(string args)
    {
        // Arrange
        var parseResult = _parser.Parse(args.Split(' ', StringSplitOptions.RemoveEmptyEntries));

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify that results contain the architecture design text by serializing it
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        response.Results.Write(writer);
        writer.Flush();

        var serializedResult = Encoding.UTF8.GetString(stream.ToArray());
        var resultList = JsonSerializer.Deserialize(serializedResult, CloudArchitectJsonContext.Default.ListString);

        Assert.NotNull(resultList);
        Assert.Single(resultList);
        Assert.NotEmpty(resultList[0]);

        // Verify it contains some expected architecture-related content
        var architectureText = resultList[0];
        Assert.Contains("architecture", architectureText.ToLower());
    }

    [Fact]
    public async Task ExecuteAsync_ConsistentResults()
    {
        // Arrange
        var parseResult1 = _parser.Parse(["--question", "test question 1"]);
        var parseResult2 = _parser.Parse(["--question", "test question 2"]);

        // Act
        var response1 = await _command.ExecuteAsync(_context, parseResult1);
        var response2 = await _command.ExecuteAsync(_context, parseResult2);

        // Assert - Both calls should return the same architecture design text
        Assert.Equal(200, response1.Status);
        Assert.Equal(200, response2.Status);

        // Serialize both results to compare them
        string serializedResult1 = SerializeResponseResult(response1.Results!);
        string serializedResult2 = SerializeResponseResult(response2.Results!);

        Assert.Equal(serializedResult1, serializedResult2);
    }

    [Fact]
    public async Task ExecuteAsync_WithAllOptionsSet()
    {
        // Arrange
        var args = new[]
        {
            "--question", "What is your application type?",
            "--question-number", "1",
            "--total-questions", "5",
            "--answer", "Web application",
            "--next-question-needed", "true",
            "--confidence-score", "0.8",
            "--architecture-component", "Frontend",
            "--architecture-tier", "Application"
        };

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify the command executed successfully regardless of the input options
        string serializedResult = SerializeResponseResult(response.Results);
        var resultList = JsonSerializer.Deserialize(serializedResult, CloudArchitectJsonContext.Default.ListString);
        Assert.NotNull(resultList);
        Assert.Single(resultList);
        Assert.NotEmpty(resultList[0]);
    }

    [Theory]
    [InlineData("What's your app type?", "What's your app type?")]
    [InlineData("How \"big\" is your app?", "How \"big\" is your app?")]
    [InlineData("Is it a \"web app\" or \"mobile app\"?", "Is it a \"web app\" or \"mobile app\"?")]
    [InlineData("What's the app's \"main purpose\"?", "What's the app's \"main purpose\"?")]
    [InlineData("Use 'single quotes' here", "Use 'single quotes' here")]
    [InlineData("Mixed \"quotes\" and 'apostrophes'", "Mixed \"quotes\" and 'apostrophes'")]
    public async Task ExecuteAsync_HandlesQuotesAndEscapingProperly(string questionWithQuotes, string expectedQuestion)
    {
        // Arrange
        var args = new[] { "--question", questionWithQuotes };
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify that the command executed successfully with the quoted input
        string serializedResult = SerializeResponseResult(response.Results);
        var resultList = JsonSerializer.Deserialize(serializedResult, CloudArchitectJsonContext.Default.ListString);
        Assert.NotNull(resultList);
        Assert.Single(resultList);
        Assert.NotEmpty(resultList[0]);

        // Verify the question was parsed correctly by checking if the command can access the option value
        var questionOption = parseResult.GetValueForOption(_command.GetCommand().Options.First(o => o.Name == "question"));
        Assert.Equal(expectedQuestion, questionOption);
    }

    [Fact]
    public async Task ExecuteAsync_HandlesComplexEscapingScenarios()
    {
        // Arrange - Test multiple options with various escaping scenarios
        var complexQuestion = "What is your \"primary\" application 'type' and how \"big\" will it be?";
        var complexAnswer = "It's a \"web application\" with 'high' scalability requirements";
        var complexComponent = "Frontend with \"React\" and 'TypeScript'";

        var args = new[]
        {
            "--question", complexQuestion,
            "--answer", complexAnswer,
            "--architecture-component", complexComponent,
            "--question-number", "2",
            "--total-questions", "10"
        };

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify all options were parsed correctly
        var questionValue = parseResult.GetValueForOption(_command.GetCommand().Options.First(o => o.Name == "question"));
        var answerValue = parseResult.GetValueForOption(_command.GetCommand().Options.First(o => o.Name == "answer"));
        var componentValue = parseResult.GetValueForOption(_command.GetCommand().Options.First(o => o.Name == "architecture-component"));

        Assert.Equal(complexQuestion, questionValue);
        Assert.Equal(complexAnswer, answerValue);
        Assert.Equal(complexComponent, componentValue);
    }

    [Fact]
    public void Metadata_IsConfiguredCorrectly()
    {
        // Arrange & Act
        var metadata = _command.Metadata;

        // Assert
        Assert.False(metadata.Destructive);
        Assert.True(metadata.ReadOnly);
    }

    [Theory]
    [InlineData("Infrastructure")]
    [InlineData("Platform")]
    [InlineData("Application")]
    [InlineData("Data")]
    [InlineData("Security")]
    [InlineData("Operations")]
    public async Task ExecuteAsync_WithArchitectureTierOptions(string tierValue)
    {
        // Arrange
        var args = new[] { "--architecture-tier", tierValue };
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify the architecture tier was parsed correctly
        var tierOption = parseResult.GetValueForOption(_command.GetCommand().Options.First(o => o.Name == "architecture-tier"));
        Assert.Equal(Enum.Parse<ArchitectureTier>(tierValue), tierOption);
    }

    [Fact]
    public async Task ExecuteAsync_WithStateOption()
    {
        // Arrange - Create a simple JSON state object
        var stateJson = "{\"architectureComponents\":[],\"architectureTiers\":{\"infrastructure\":[],\"platform\":[],\"application\":[],\"data\":[],\"security\":[],\"operations\":[]},\"requirements\":{\"explicit\":[],\"implicit\":[],\"assumed\":[]},\"confidenceFactors\":{\"explicitRequirementsCoverage\":0.5,\"implicitRequirementsCertainty\":0.7,\"assumptionRisk\":0.3}}";
        var args = new[] { "--state", stateJson };
        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify the command executed successfully with state option
        string serializedResult = SerializeResponseResult(response.Results);
        var resultList = JsonSerializer.Deserialize(serializedResult, CloudArchitectJsonContext.Default.ListString);
        Assert.NotNull(resultList);
        Assert.Single(resultList);
        Assert.NotEmpty(resultList[0]);
    }

    [Fact]
    public async Task ExecuteAsync_WithCompleteOptionSet()
    {
        // Arrange - Test all options together including the new ones
        var args = new[]
        {
            "--question", "What type of application are you building?",
            "--question-number", "3",
            "--total-questions", "8",
            "--answer", "A financial trading platform",
            "--next-question-needed", "false",
            "--confidence-score", "0.9",
            "--architecture-component", "Azure SQL Database",
            "--architecture-tier", "Data"
        };

        var parseResult = _parser.Parse(args);

        // Act
        var response = await _command.ExecuteAsync(_context, parseResult);

        // Assert
        Assert.Equal(200, response.Status);
        Assert.NotNull(response.Results);
        Assert.Empty(response.Message);

        // Verify all options were parsed correctly
        var command = _command.GetCommand();
        var questionValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "question"));
        var questionNumberValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "question-number"));
        var totalQuestionsValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "total-questions"));
        var answerValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "answer"));
        var nextQuestionNeededValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "next-question-needed"));
        var confidenceScoreValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "confidence-score"));
        var componentValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "architecture-component"));
        var tierValue = parseResult.GetValueForOption(command.Options.First(o => o.Name == "architecture-tier"));

        Assert.Equal("What type of application are you building?", questionValue);
        Assert.Equal(3, questionNumberValue);
        Assert.Equal(8, totalQuestionsValue);
        Assert.Equal("A financial trading platform", answerValue);
        Assert.Equal(false, nextQuestionNeededValue);
        Assert.Equal(0.9, confidenceScoreValue);
        Assert.Equal("Azure SQL Database", componentValue);
        Assert.Equal(ArchitectureTier.Data, tierValue);
    }

    private static string SerializeResponseResult(ResponseResult responseResult)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);
        responseResult.Write(writer);
        writer.Flush();
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    #region Validation Tests

    [Theory]
    [InlineData(-0.1)]
    [InlineData(1.1)]
    [InlineData(2.0)]
    [InlineData(-1.0)]
    public void Parse_InvalidConfidenceScore_ReturnsError(double invalidScore)
    {
        // Arrange
        var args = new[] { "--confidence-score", invalidScore.ToString() };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.NotEmpty(parseResult.Errors);
        Assert.Contains("Confidence score must be between 0.0 and 1.0", parseResult.Errors.Select(e => e.Message));
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    [InlineData(0.1)]
    [InlineData(0.9)]
    public void Parse_ValidConfidenceScore_NoErrors(double validScore)
    {
        // Arrange
        var args = new[] { "--confidence-score", validScore.ToString() };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.Empty(parseResult.Errors);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    [InlineData(-100)]
    public void Parse_NegativeQuestionNumber_ReturnsError(int invalidQuestionNumber)
    {
        // Arrange
        var args = new[] { "--question-number", invalidQuestionNumber.ToString() };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.NotEmpty(parseResult.Errors);
        Assert.Contains("Question number cannot be negative", parseResult.Errors.Select(e => e.Message));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void Parse_ValidQuestionNumber_NoErrors(int validQuestionNumber)
    {
        // Arrange
        var args = new[] { "--question-number", validQuestionNumber.ToString() };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.Empty(parseResult.Errors);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-5)]
    [InlineData(-100)]
    public void Parse_NegativeTotalQuestions_ReturnsError(int invalidTotalQuestions)
    {
        // Arrange
        var args = new[] { "--total-questions", invalidTotalQuestions.ToString() };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.NotEmpty(parseResult.Errors);
        Assert.Contains("Total questions cannot be negative", parseResult.Errors.Select(e => e.Message));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    public void Parse_ValidTotalQuestions_NoErrors(int validTotalQuestions)
    {
        // Arrange
        var args = new[] { "--total-questions", validTotalQuestions.ToString() };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.Empty(parseResult.Errors);
    }

    [Theory]
    [InlineData(1, 5)]
    [InlineData(5, 5)]
    [InlineData(3, 10)]
    [InlineData(0, 5)] // Zero is valid for question number
    public void Parse_QuestionNumberWithinTotalQuestions_NoErrors(int questionNumber, int totalQuestions)
    {
        // Arrange
        var args = new[]
        {
            "--question-number", questionNumber.ToString(),
            "--total-questions", totalQuestions.ToString()
        };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.Empty(parseResult.Errors);
    }

    [Fact]
    public void Parse_MultipleValidationErrors_ReturnsFirstError()
    {
        // Arrange - Set both invalid confidence score and negative question number
        var args = new[]
        {
            "--confidence-score", "1.5",
            "--question-number", "-1"
        };

        // Act
        var parseResult = _parser.Parse(args);

        // Assert
        Assert.NotEmpty(parseResult.Errors);
        // Should return the first validation error encountered
        Assert.Contains("Confidence score must be between 0.0 and 1.0", parseResult.Errors.Select(e => e.Message));
    }

    #endregion
}
