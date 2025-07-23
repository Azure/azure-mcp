// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.AzureBestPractices.Options;

public static class BestPracticesOptionDefinitions
{
    public const string ResourceName = "resource";
    public const string ActionName = "action";

    public static readonly Option<string> Resource = new(
        $"--{ResourceName}",
        "The Azure resource type for which to get best practices. Options: 'general' (general Azure), 'azurefunctions' (Azure Functions)."
    )
    {
        IsRequired = true
    };

    public static readonly Option<string> Action = new(
        $"--{ActionName}",
        "The action type for the best practices. Options: 'all', 'code-generation', 'deployment'. Note: 'general' resource only supports 'all'."
    )
    {
        IsRequired = true
    };
}
