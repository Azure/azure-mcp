// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;

namespace AzureMcp.Extensions;

public static class ArgumentExtensions
{
    public static void SortArguments(this List<ArgumentInfo> arguments)
    {
        arguments.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
    }
}