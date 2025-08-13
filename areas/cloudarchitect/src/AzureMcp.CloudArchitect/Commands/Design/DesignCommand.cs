// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AzureMcp.CloudArchitect.Options;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Helpers;
using AzureMcp.Core.Models;
using Microsoft.Extensions.Logging;

namespace AzureMcp.CloudArchitect.Commands.Design;

public sealed class DesignCommand(ILogger<DesignCommand> logger) : GlobalCommand<ArchitectureDesignToolOptions>
{
    private const string CommandTitle = "Design Azure cloud architectures through guided questions";
    private readonly ILogger<DesignCommand> _logger = logger;

    private readonly Option<string> _questionOption = CloudArchitectOptionDefinitions.Question;
    private readonly Option<int> _questionNumberOption = CloudArchitectOptionDefinitions.QuestionNumber;
    private readonly Option<int> _questionTotalQuestions = CloudArchitectOptionDefinitions.TotalQuestions;
    private readonly Option<string> _answerOption = CloudArchitectOptionDefinitions.Answer;
    private readonly Option<bool> _nextQuestionNeededOption = CloudArchitectOptionDefinitions.NextQuestionNeeded;
    private readonly Option<double> _confidenceScoreOption = CloudArchitectOptionDefinitions.ConfidenceScore;
    private readonly Option<string> _architectureComponentOption = CloudArchitectOptionDefinitions.ArchitectureComponent;
    private readonly Option<ArchitectureTier> _architectureTierOption = CloudArchitectOptionDefinitions.ArchitectureTier;
    private readonly Option<ArchitectureDesignToolState> _architectureDesignToolState = CloudArchitectOptionDefinitions.State;

    private static readonly string s_designArchitectureText = LoadArchitectureDesignText();

    private static string GetArchitectureDesignText() => s_designArchitectureText;

    public override string Name => "design";

    public override string Description =>
        "Design and architect comprehensive Azure cloud solutions for applications and services. This interactive assistant helps create scalable cloud architectures for file upload systems, web applications, APIs, e-commerce platforms, financial services, transaction systems, data processing services, and enterprise solutions. Through guided questions, provides tailored Azure architecture recommendations covering storage, compute, networking, databases, security, and application services to create robust user-facing cloud services and applications.";

    public override string Title => CommandTitle;

    public override ToolMetadata Metadata => new()
    {
        Destructive = false,
        ReadOnly = true
    };

    private static string LoadArchitectureDesignText()
    {
        Assembly assembly = typeof(DesignCommand).Assembly;
        string resourceName = EmbeddedResourceHelper.FindEmbeddedResource(assembly, "azure-architecture-design.txt");
        return EmbeddedResourceHelper.ReadEmbeddedResource(assembly, resourceName);
    }

    protected override void RegisterOptions(Command command)
    {
        base.RegisterOptions(command);
        command.AddOption(_questionOption);
        command.AddOption(_questionNumberOption);
        command.AddOption(_questionTotalQuestions);
        command.AddOption(_answerOption);
        command.AddOption(_nextQuestionNeededOption);
        command.AddOption(_confidenceScoreOption);
        command.AddOption(_architectureComponentOption);
        command.AddOption(_architectureTierOption);
        command.AddOption(_architectureDesignToolState);

        command.AddValidator(result =>
        {
            // Validate confidence score is between 0.0 and 1.0
            var confidenceScore = result.GetValueForOption(_confidenceScoreOption);
            if (confidenceScore < 0.0 || confidenceScore > 1.0)
            {
                result.ErrorMessage = "Confidence score must be between 0.0 and 1.0";
                return;
            }

            // Validate question number is not negative
            var questionNumber = result.GetValueForOption(_questionNumberOption);
            if (questionNumber < 0)
            {
                result.ErrorMessage = "Question number cannot be negative";
                return;
            }

            // Validate total questions is not negative
            var totalQuestions = result.GetValueForOption(_questionTotalQuestions);
            if (totalQuestions < 0)
            {
                result.ErrorMessage = "Total questions cannot be negative";
                return;
            }
        });
    }

    protected override ArchitectureDesignToolOptions BindOptions(ParseResult parseResult)
    {
        var options = base.BindOptions(parseResult);
        options.Question = parseResult.GetValueForOption(_questionOption) ?? string.Empty;
        options.QuestionNumber = parseResult.GetValueForOption(_questionNumberOption);
        options.TotalQuestions = parseResult.GetValueForOption(_questionTotalQuestions);
        options.Answer = parseResult.GetValueForOption(_answerOption);
        options.NextQuestionNeeded = parseResult.GetValueForOption(_nextQuestionNeededOption);
        options.ConfidenceScore = parseResult.GetValueForOption(_confidenceScoreOption);
        options.ArchitectureComponent = parseResult.GetValueForOption(_architectureComponentOption);
        options.ArchitectureTier = parseResult.GetValueForOption(_architectureTierOption);
        options.State = parseResult.GetValueForOption(_architectureDesignToolState) ?? new ArchitectureDesignToolState();
        return options;
    }

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var designArchitecture = GetArchitectureDesignText();
            context.Response.Status = 200;
            context.Response.Results = ResponseResult.Create(new List<string> { designArchitecture }, CloudArchitectJsonContext.Default.ListString);
            context.Response.Message = string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred in cloud architec design command");
            HandleException(context, ex);
        }
        return Task.FromResult(context.Response);

    }
}
