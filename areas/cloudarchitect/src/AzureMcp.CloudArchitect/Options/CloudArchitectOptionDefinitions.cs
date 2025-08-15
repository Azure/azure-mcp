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
    public const string StateName = "state";

    public static readonly Option<string> Question = new(
        $"--{QuestionName}",
         "The current question being asked"
    )
    {
        IsRequired = false
    };

    public static readonly Option<int> QuestionNumber = new(
        $"--{QuestionNumberName}",
        "Current question number"
    )
    {
        IsRequired = false
    };

    public static readonly Option<int> TotalQuestions = new(
        $"--{TotalQuestionsName}",
        "Estimated total questions needed"
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> Answer = new(
        $"--{AnswerName}",
         "The user's response to the question"
    )
    {
        IsRequired = false
    };

    public static readonly Option<bool> NextQuestionNeeded = new(
        $"--{NextQuestionNeededName}",
        "Whether another question is needed"
    )
    {
        IsRequired = false
    };

    public static readonly Option<double> ConfidenceScore = new(
        $"--{ConfidenceScoreName}",
        "A value between 0.0 and 1.0 representing confidence in understanding requirements. When this reaches 0.7 or higher, nextQuestionNeeded should be set to false."
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> ArchitectureComponent = new(
        $"--{ArchitectureComponentName}",
        "The specific Azure component being suggested. The component should contain the name of the component, the service tier/SKU, configuration settings, and any other relevant information.\""
    )
    {
        IsRequired = false
    };

    public static readonly Option<ArchitectureTier> ArchitectureTier = new(
        $"--{ArchitectureTierName}",
        "Which architectural tier this component belongs to"
    )
    {
        IsRequired = false
    };

    public static readonly Option<string> State = new(
        $"--{StateName}",
        "The complete architecture state from the previous request as JSON"
    )
    {
        IsRequired = false
    };
}
