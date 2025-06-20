// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Areas.Server.Options;

public class ServiceStartOptions
{
    [JsonPropertyName("transport")]
    public string Transport { get; set; } = TransportTypes.StdIo;

    [JsonPropertyName("port")]
    public int Port { get; set; }

    [JsonPropertyName("service")]
    public string? Service { get; set; } = null;

    [JsonPropertyName("readOnly")]
    public bool? ReadOnly { get; set; } = null;
}
