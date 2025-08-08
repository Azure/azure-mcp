// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Reflection;
using AzureMcp.CloudArchitect.Options;
using AzureMcp.Core.Commands;
using AzureMcp.Core.Helpers;
using AzureMcp.Core.Models;
using Microsoft.Extensions.Logging;

namespace AzureMcp.CloudArchitect.Commands.Design;

public sealed class DesignCommand(ILogger<DesignCommand> logger) : BaseCloudArchitectCommand<ArchitectureDesignToolOptions>
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


    private static readonly string s_designArchitectureText = LoadArchitectureDesignText();

    private static string GetArchitectureDesignText() => s_designArchitectureText;

    public override string Name => "design";

    public override string Description =>
        "A tool for designing Azure cloud architectures through guided questions.";

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
        return options;
    }

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        var designArchitecture = GetArchitectureDesignText();
        context.Response.Status = 200;
        context.Response.Results = ResponseResult.Create(new List<string> { designArchitecture }, CloudArchitectJsonContext.Default.ListString);
        context.Response.Message = string.Empty;
        return Task.FromResult(context.Response);
    }
}

// Strongly-typed result record
public record DesignCommandResult(AzureMcp.CloudArchitect.Models.ArchitectureDesign Design);
