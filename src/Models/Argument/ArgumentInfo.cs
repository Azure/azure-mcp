// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Models.Argument;

public class ArgumentInfo(
    string name,
    string description,
    string? value = "",
    string? defaultValue = default,
    bool required = false) :
    ArgumentDefinition<string>(name, description, value, defaultValue, required);
