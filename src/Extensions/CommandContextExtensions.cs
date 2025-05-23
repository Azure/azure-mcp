// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Extensions;

public static class CommandContextExtensions
{
    /// <summary>
    /// Validates required parameters and updates the response if validation fails
    /// </summary>
    /// <returns>True if validation passed, false if it failed</returns>
    public static bool Validate(this CommandContext context, ParseResult parseResult)
    {
        var missingParameters = parseResult.CommandResult.Command.Options
            .Where(o => o.IsRequired && parseResult.GetValueForOption(o) == null)
            .Select(o => $"--{o.Name}")
            .ToList();

        if (missingParameters.Count > 0)
        {
            context.Response.Status = 400;
            context.Response.Message = $"Missing required arguments: {string.Join(", ", missingParameters)}";
            return false;
        }

        if (!string.IsNullOrEmpty(parseResult.CommandResult.ErrorMessage))
        {
            context.Response.Status = 400;
            context.Response.Message = parseResult.CommandResult.ErrorMessage;
            return false;
        }

        return true;
    }
}
