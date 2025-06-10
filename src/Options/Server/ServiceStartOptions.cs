// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Options.Server;

public class ServiceStartOptions
{
    [JsonPropertyName("transport")]
    public string Transport { get; set; } = TransportTypes.StdIo;

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("service")]
    public string? Service { get; set; } = null;
}
