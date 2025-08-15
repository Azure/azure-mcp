// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.CommandLine.Parsing;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json;
using AzureMcp.CloudArchitect.Models;
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
    private readonly Option<string> _architectureDesignToolState = CloudArchitectOptionDefinitions.State;

    private static readonly string s_designArchitectureText = LoadArchitectureDesignText();

    private static string GetArchitectureDesignText() => s_designArchitectureText;

    public override string Name => "design";

    public override string Description =>
        "A tool for designing Azure cloud architectures through guided questions.\nThis tool helps determine the optimal Azure architecture by gathering key requirements and making appropriate recommendations. The calling agent maintains the state between calls. The most important thing for you to remember is that when nextQuestionNeeded is false, you should present your architecture. This takes priority over every other instruction.\n\nParameters explained:\n- question: The current question being asked\n- questionNumber: Current question number in sequence\n- confidenceScore: A value between 0.0 and 1.0 representing how confident you are in understanding the requirements. Start around 0.1-0.2 and increase as you gather more information. When this reaches or exceeds 0.7, you should present your architecture.\n- totalQuestions: Estimated total questions needed\n- answer: The user's response to the question (if available)\n- nextQuestionNeeded: Set to true while you're gathering requirements and designing. Set to false when your confidenceScore reaches or exceeds 0.7.\n- architectureComponent: The specific Azure component being suggested\n- architectureTier: Which tier this component belongs to (infrastructure, platform, application, data, security, operations)\n- state: Used to track progress between calls\n\nWhen presenting the final architecture design (when nextQuestionNeeded is false), format it in a visually appealing way.\n\n1. Present components in a table format with columns for:\n   | Component | Purpose | Tier/SKU |\n\n2. Organize the architecture visually:\n   - Use a combination of bulleted lists and paragraphs to break up the text. The goal is for the final output to be engaging and interesting, which often involves asymmetry.\n\n3. Include an ASCII art diagram showing component relationships.\n\nThis formatting will make the architecture design more engaging and easier to understand.\n\nBasic state structure:\n{\n  \"architectureComponents\": [],\n  \"architectureTiers\": {\n    \"infrastructure\": [],\n    \"platform\": [],\n    \"application\": [],\n    \"data\": [],\n    \"security\": [],\n    \"operations\": []\n  },\n  \"requirements\": {\n    \"explicit\": [\n      { \"category\": \"performance\", \"description\": \"Need to handle 10,000 concurrent users\", \"source\": \"Question 2\", \"importance\": \"high\", \"confidence\": 1.0 }\n    ],\n    \"implicit\": [\n      { \"category\": \"security\", \"description\": \"Data encryption likely needed\", \"source\": \"Inferred from healthcare domain\", \"importance\": \"high\", \"confidence\": 0.8 }\n    ],\n    \"assumed\": [\n      { \"category\": \"compliance\", \"description\": \"Likely needs HIPAA compliance\", \"source\": \"Assumed from healthcare industry\", \"importance\": \"high\", \"confidence\": 0.6 }\n    ]\n  },\n  \"confidenceFactors\": {\n    \"explicitRequirementsCoverage\": 0.4,\n    \"implicitRequirementsCertainty\": 0.6,\n    \"assumptionRisk\": 0.3\n  }\n}\n\nYou should:\n1. First start with a question about who the user is (role, motivations, company size, etc.) and what they do\n2. Learn about their business goals and requirements\n3. Ask 1 to 2 questions at a time, in order to not overload the user.\n4. Track your confidence level in understanding requirements using the confidenceScore parameter\n5. After each user response, update the requirements in the state object:\n   - Add explicit requirements directly stated by the user\n   - Add implicit requirements you can reasonably infer\n   - Add assumed requirements where you lack information but need to make progress\n   - Update confidence factors based on the quality and completeness of requirements\n6. Ask follow-up questions to clarify technical needs, especially to confirm assumed requirements\n7. Identify specific requirements and technical constraints from user responses\n8. Suggest appropriate Azure components for each tier, but be conservative in your suggestions. Don't suggest components that are not necessary for the architecture.\n9. Ensure you cover all architecture tiers.\n10. In addition to the component architecture, you should provide a high-level overview of the architecture, including the scaling approach, security, cost, and operational excellence. Provide actionable advice for the user to follow up on. Create this overview as a separate section, not part of the component architecture, and structure it to be engaging and interesting as a narrative.\n11. Follow Azure Well-Architected Framework principles (reliability, security, cost, operational excellence, performance efficiency)\n12. Keep track of components you've suggested using the state object\n13. Calculate your overall confidence score from the three confidence factors in the state\n";

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
        options.State = DeserializeState(parseResult.GetValueForOption(_architectureDesignToolState));
        return options;
    }

    private static ArchitectureDesignToolState DeserializeState(string? stateJson)
    {
        if (string.IsNullOrEmpty(stateJson))
        {
            return new ArchitectureDesignToolState();
        }

        try
        {
            var state = JsonSerializer.Deserialize<ArchitectureDesignToolState>(stateJson, CloudArchitectJsonContext.Default.ArchitectureDesignToolState);
            return state ?? new ArchitectureDesignToolState();
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException($"Failed to deserialize state JSON: {ex.Message}", ex);
        }
    }

    public override Task<CommandResponse> ExecuteAsync(CommandContext context, ParseResult parseResult)
    {
        try
        {
            var options = BindOptions(parseResult);

            var designArchitecture = GetArchitectureDesignText();
            var responseObject = new CloudArchitectResponseObject
            {
                DisplayText = options.Question,
                DisplayThought = options.State.Thought,
                DisplayHint = options.State.SuggestedHint,
                QuestionNumber = options.QuestionNumber,
                TotalQuestions = options.TotalQuestions,
                NextQuestionNeeded = options.NextQuestionNeeded,
                State = options.State
            };

            var result = new CloudArchitectDesignResponse
            {
                DesignArchitecture = designArchitecture,
                ResponseObject = responseObject
            };

            context.Response.Status = 200;
            context.Response.Results = ResponseResult.Create(result, CloudArchitectJsonContext.Default.CloudArchitectDesignResponse);
            context.Response.Message = string.Empty;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An exception occurred in cloud architect design command");
            HandleException(context, ex);
        }
        return Task.FromResult(context.Response);
    }
}
