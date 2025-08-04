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

            if (value.ValueKind == JsonValueKind.True)
            {
                args.Add("true");
            }
            else if (value.ValueKind == JsonValueKind.False)
            {
                args.Add("false");
            }
            else if (value.ValueKind == JsonValueKind.Number)
            {
                args.Add(value.GetRawText());
            }
            else if (value.ValueKind == JsonValueKind.String)
            {
                var strValue = value.GetString();
                if (!string.IsNullOrEmpty(strValue))
                {
                    args.Add(strValue);
                }
            }
            else if (value.ValueKind == JsonValueKind.Object || value.ValueKind == JsonValueKind.Array)
            {
                if (value.EnumerateArray().All(t => t.ValueKind == JsonValueKind.String))
                {
                    foreach (var item in value.EnumerateArray())
                    {
                        var itemValue = item.GetString();
                        if (!string.IsNullOrEmpty(itemValue))
                        {
                            args.Add(itemValue);
                        }
                    }
                }
                else
                {
                    // For complex JSON objects or arrays, pass as JSON to preserve structure

                    args.Add("\'" + value.GetRawText() + "\'");
                }
            }
            else
            {
                var strValue = value.GetRawText();
                if (!string.IsNullOrEmpty(strValue))
                {
                    args.Add(strValue);
                }
            }
        }

        return command.Parse(args.ToArray());
    }
}
