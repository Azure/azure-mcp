// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Deploy.Models.Templates;

/// <summary>
/// Parameters for generating pipeline templates.
/// </summary>
public sealed class PipelineTemplateParameters
{
    /// <summary>
    /// Environment name prompt text.
    /// </summary>
    public string EnvironmentNamePrompt { get; set; } = string.Empty;

    /// <summary>
    /// Subscription ID prompt text.
    /// </summary>
    public string SubscriptionIdPrompt { get; set; } = string.Empty;

    /// <summary>
    /// GitHub environment create command.
    /// </summary>
    public string EnvironmentCreateCommand { get; set; } = string.Empty;

    /// <summary>
    /// JSON parameters for federated credentials.
    /// </summary>
    public string JsonParameters { get; set; } = string.Empty;

    /// <summary>
    /// Environment argument for GitHub CLI commands.
    /// </summary>
    public string EnvironmentArg { get; set; } = string.Empty;

    /// <summary>
    /// Converts the parameters to a dictionary for template processing.
    /// </summary>
    /// <returns>A dictionary containing the parameter values.</returns>
    public Dictionary<string, string> ToDictionary()
    {
        return new Dictionary<string, string>
        {
            { nameof(EnvironmentNamePrompt), EnvironmentNamePrompt },
            { nameof(SubscriptionIdPrompt), SubscriptionIdPrompt },
            { nameof(EnvironmentCreateCommand), EnvironmentCreateCommand },
            { nameof(JsonParameters), JsonParameters },
            { nameof(EnvironmentArg), EnvironmentArg }
        };
    }
}
