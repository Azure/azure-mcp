// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Models.Argument;
using System.CommandLine;

namespace AzureMcp.Extensions;

public static class ArgumentExtensions
{
    public static void SortArguments(this List<ArgumentInfo> arguments)
    {
        arguments.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.OrdinalIgnoreCase));
    }

    public static Option<T> ToOption<T>(this ArgumentDefinition<T> definition)
    {
        var option = new Option<T>(
            name: $"--{definition.Name}",
            description: definition.Description
        );
        if (definition.DefaultValue is not null)
            option.SetDefaultValue(definition.DefaultValue);
        option.IsRequired = definition.Required;
        return option;
    }
}