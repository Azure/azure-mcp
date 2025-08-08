// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.CloudArchitect.Options;

/// <summary>
/// The set of parameters that the architecture design tool takes as input.
/// </summary>
public class ArchitectureDesignToolOptions : BaseCloudArchitectOptions
{
    [JsonPropertyName("question")]
    public string Question { get; set; } = string.Empty;

    [JsonPropertyName("questionNumber")]
    public int QuestionNumber { get; set; }

    [JsonPropertyName("totalQuestions")]
    public int TotalQuestions { get; set; }

    [JsonPropertyName("answer")]
    public string? Answer { get; set; }

    [JsonPropertyName("nextQuestionNeeded")]
    public bool NextQuestionNeeded { get; set; }

    [JsonPropertyName("confidenceScore")]
    public double? ConfidenceScore { get; set; }

    [JsonPropertyName("architectureComponent")]
    public string? ArchitectureComponent { get; set; }

    [JsonPropertyName("architectureTier")]
    public ArchitectureTier? ArchitectureTier { get; set; }

    [JsonPropertyName("state")]
    public ArchitectureDesignToolState State { get; set; } = new();
}
