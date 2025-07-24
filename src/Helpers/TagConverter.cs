// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json;

namespace AzureMcp.Helpers;

/// <summary>
/// Utility class for converting Azure resource tags to string representations.
/// </summary>
public static class TagConverter
{
    /// <summary>
    /// Converts a JSON element containing tags to a comma-separated string representation.
    /// This helps keep the output flat for the model.
    /// </summary>
    public static string? ConvertTagsToString(JsonElement tagsElement)
    {
        if (tagsElement.ValueKind != JsonValueKind.Object)
            return null;

        var tags = new List<string>();
        foreach (var tag in tagsElement.EnumerateObject())
        {
            try
            {
                var value = tag.Value.ValueKind switch
                {
                    JsonValueKind.String => tag.Value.GetString() ?? "",
                    JsonValueKind.Number => tag.Value.GetDecimal().ToString(),
                    JsonValueKind.True => "true",
                    JsonValueKind.False => "false",
                    JsonValueKind.Null => "",
                    _ => tag.Value.ToString()
                };
                tags.Add($"{tag.Name}={value}");
            }
            catch
            {
                // Skip problematic tags rather than failing entirely
                continue;
            }
        }

        return tags.Count > 0 ? string.Join(", ", tags) : null;
    }

    /// <summary>
    /// Converts a dictionary of tags to a comma-separated string representation.
    /// This helps keep the output flat for the model.
    /// </summary>
    public static string? ConvertTagsToString(IDictionary<string, string>? tags)
    {
        return tags?.Count > 0 ? string.Join(", ", tags.Select(kvp => $"{kvp.Key}={kvp.Value}")) : null;
    }
}
