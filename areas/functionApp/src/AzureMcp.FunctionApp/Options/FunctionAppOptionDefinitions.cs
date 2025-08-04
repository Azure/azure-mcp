// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.FunctionApp.Options;

public static class FunctionAppOptionDefinitions
{
    public const string FunctionAppName = "name";

    public static readonly Option<string> FunctionApp = new(
        name: FunctionAppName,
        description: "Name of the Function App")
    {
        IsRequired = false
    };
}
