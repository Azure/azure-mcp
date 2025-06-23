﻿// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AzureMcp.Services.Azure.BicepSchema.ResourceProperties.Entities;

public class ObjectTypeEntity : ComplexType
{
    [JsonPropertyName("properties")]
    public List<PropertyInfo> Properties { get; init; } = [];

    [JsonPropertyName("additionalPropertiesType")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? AdditionalPropertiesType { get; init; }

    [JsonPropertyName("sensitive")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Sensitive { get; init; }

    public override bool Equals(object? obj) =>
        obj is ObjectTypeEntity other &&
        Name == other.Name &&
        Properties.SequenceEqual(other.Properties) &&
        AdditionalPropertiesType == other.AdditionalPropertiesType &&
        Sensitive == other.Sensitive;

    public override int GetHashCode() =>
        HashCode.Combine(
            Name,
            Properties.Aggregate(0, (hash, prop) => HashCode.Combine(hash, prop)),
            AdditionalPropertiesType,
            Sensitive
        );
}
