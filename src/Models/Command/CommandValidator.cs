// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Models.Command;

/// <summary>
/// Validator for command line parameters
/// </summary>
public static class CommandValidator
{
    /// <summary>
    /// Validates required parameters and updates the response if validation fails
    /// </summary>
    /// <returns>True if validation passed, false if it failed</returns>
    public static bool TryValidate(ParseResult parseResult, CommandResponse response)
    {
        var missingParameters = parseResult.CommandResult.Command.Options
            .Where(o => o.IsRequired && parseResult.GetValueForOption(o) == null)
            .Select(o => $"--{o.Name}")
            .ToList();

        if (missingParameters.Count == 0)
        {
            return true;
        }

        response.Status = 400;
        response.Message = $"Missing required arguments: {string.Join(", ", missingParameters)}";
        return false;
    }
}
