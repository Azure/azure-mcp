// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.CommandLine;
using System.Text.Json.Serialization;

namespace AzureMcp.Models.Argument;

public class ArgumentDefinition<T>(string name, string description, string? value = "", T? defaultValue = default, List<ArgumentOption>? suggestedValues = null, bool required = false)
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = name;

    [JsonPropertyName("description")]
    public string Description { get; set; } = description;

    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Value { get; set; } = value!;

    [JsonPropertyName("default")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T? DefaultValue { get; set; } = defaultValue;

    [JsonPropertyName("suggestedValues")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ArgumentOption>? SuggestedValues { get; set; } = suggestedValues;

    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Type { get; set; } = typeof(T).Name.ToLowerInvariant();

    [JsonPropertyName("required")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool Required { get; set; } = required;

    public bool ShouldSerializeValues() => SuggestedValues?.Count > 0;

    public Option<T> ToOption()
    {
        return DefaultValue != null
            ? new Option<T>($"--{Name}", () => DefaultValue, Description)
            : new Option<T>($"--{Name}", Description);
    }

    public JsonPropertyNameAttribute ToJsonAttribute()
    {
        return new JsonPropertyNameAttribute(Name);
    }
}