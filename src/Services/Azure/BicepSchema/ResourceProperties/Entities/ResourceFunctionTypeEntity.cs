﻿// --------------------------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation. All rights reserved.
// --------------------------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace AzureMcp.Services.Azure.BicepSchema.ResourceProperties.Entities;

public class ResourceFunctionTypeEntity : ComplexType
{
    [JsonPropertyName("resourceType")]
    public required string ResourceType { get; init; }

    [JsonPropertyName("apiVersion")]
    public required string ApiVersion { get; init; }

    [JsonPropertyName("inputType")]
    public string? InputType { get; init; }

    [JsonPropertyName("outputType")]
    public required string OutputType { get; init; }

    public override bool Equals(object? obj) =>
        obj is ResourceFunctionTypeEntity other &&
        Name == other.Name &&
        ResourceType == other.ResourceType &&
        ApiVersion == other.ApiVersion &&
        InputType == other.InputType &&
        OutputType == other.OutputType;

    public override int GetHashCode() =>
        HashCode.Combine(Name, ResourceType, ApiVersion, InputType, OutputType);
}
