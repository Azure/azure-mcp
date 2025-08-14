// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.FunctionApp.Options;

public static class FunctionAppOptionDefinitions
{
    public const string FunctionAppName = "functionapp";

    public static readonly Option<string> FunctionApp = new(
        $"--{FunctionAppName}",
        "Function App name.")
    {
        IsRequired = true
    };
}
