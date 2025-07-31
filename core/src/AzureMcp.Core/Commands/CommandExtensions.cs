// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Core.Commands;

/// <summary>
/// Extensions for parsing command options from dictionary arguments
/// </summary>
public static class CommandExtensions
{
    /// <summary>
    /// Parse command options directly from a dictionary of arguments
    /// </summary>
    /// <param name="command">The command to parse options for</param>
    /// <param name="arguments">Dictionary of argument name/value pairs</param>
    /// <returns>ParseResult containing the parsed arguments</returns>
    public static ParseResult ParseFromDictionary(this Command command, IReadOnlyDictionary<string, JsonElement>? arguments)
    {
        if (arguments == null || arguments.Count == 0)
        {
            return command.Parse(Array.Empty<string>());
        }
        var args = new List<string>();
        foreach (var (key, value) in arguments)
        {
            var option = command.Options.FirstOrDefault(o =>
                o.Name.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (option == null)
            {
                continue;
            }

            if (value.ValueKind == JsonValueKind.Null)
            {
                continue;
            }
            args.Add($"--{option.Name}"); // Use the actual option name for consistency            // Handle different value types
            var strValue = value.ValueKind switch
            {
                JsonValueKind.True => "true",
                JsonValueKind.False => "false",
                JsonValueKind.Number => value.GetRawText(),
                JsonValueKind.String => value.GetString(),
                JsonValueKind.Array => string.Join(" ", value.EnumerateArray().Select(e => e.GetString() ?? string.Empty)),
                _ => value.GetRawText()
            };

            if (strValue != null)
            {
                args.Add(strValue);
            }
        }

        return command.Parse(args.ToArray());
    }
}
