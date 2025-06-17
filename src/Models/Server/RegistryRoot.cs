// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Models.Server;

public sealed class RegistryRoot
{
    [JsonPropertyName("servers")]
    public Dictionary<string, RegistryServerInfo>? Servers { get; init; }
}
