// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics;
using System.Text.Json.Serialization;
using Azure.Bicep.Types.Concrete;
using AzureMcp.Areas.BicepSchema.Services.Support;

namespace AzureMcp.Areas.BicepSchema.Services.ResourceProperties.Entities;

public class ResourceTypeEntity : ComplexType
{
    [JsonPropertyName("bodyType")]
    public required ComplexType BodyType { get; init; }

    private string? _flags;
    [JsonPropertyName("flags")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Flags
    {
        get
        {
            Debug.Assert(_flags?.EqualsOrdinalInsensitively(nameof(ResourceFlags.None)) != true);
            return _flags;
        }
        init
        {
            _flags = value?.EqualsOrdinalInsensitively(nameof(ResourceFlags.None)) == true
                ? null
                : value.NullIfEmptyOrWhitespace();
        }
    }

    [JsonPropertyName("scopeType")]
    public string ScopeType { get; init; } = "Unknown";

    [JsonPropertyName("readOnlyScopes")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ReadOnlyScopes { get; init; }

    public override bool Equals(object? obj) =>
        obj is ResourceTypeEntity other &&
        Name == other.Name &&
        BodyType.Equals(other.BodyType) &&
        Flags == other.Flags &&
        ScopeType == other.ScopeType &&
        ReadOnlyScopes == other.ReadOnlyScopes;

    public override int GetHashCode() =>
        HashCode.Combine(Name, BodyType, Flags, ScopeType, ReadOnlyScopes);
}
