// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;

namespace AzureMcp.CloudArchitect.Options;

public static class CloudArchitectOptionDefinitions
{
    public const string QuestionName = "question";
    public const string QuestionNumberName = "question-number";
    public const string TotalQuestionsName = "total-questions";
    public const string AnswerName = "answer";
    public const string NextQuestionNeededName = "next-question-needed";
    public const string ConfidenceScoreName = "confidence-score";
    public const string ArchitectureComponentName = "architecture-component";
    public const string ArchitectureTierName = "architecture-tier";

    public static readonly Option<string> Question = new(
        $"--{QuestionName}",
        "The question to ask during the architecture design process."
    )
    {
        IsRequired = false
    };

    public static readonly Option<int> QuestionNumber = new(
        $"--{QuestionNumberName}",
        "The current question number in the design process."
    )
    {
        IsRequired = false
    };

    public static readonly Option<int> TotalQuestions = new(
        $"--{TotalQuestionsName}",
        "The total number of questions in the design process."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> Answer = new(
        $"--{AnswerName}",
        "The answer to the current question in the design process."
    )
    {
        IsRequired = false
    };

    public static readonly Option<bool> NextQuestionNeeded = new(
        $"--{NextQuestionNeededName}",
        "Whether the next question is needed in the design process."
    )
    {
        IsRequired = false
    };

    public static readonly Option<double> ConfidenceScore = new(
        $"--{ConfidenceScoreName}",
        "The confidence score for the current architecture design."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> ArchitectureComponent = new(
        $"--{ArchitectureComponentName}",
        "The architecture component being designed."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> ArchitectureTier = new(
        $"--{ArchitectureTierName}",
        "The architecture tier being designed (e.g., presentation, business, data)."
    )
    {
        IsRequired = false
    };

    public static readonly Option<ArchitectureDesignToolOptions> ArchitectureDesignTool = new(
        "--architecture-design-tool",
        "The complete architecture design tool options for guided design flow."
    )
    {
        IsRequired = false
    };
}
