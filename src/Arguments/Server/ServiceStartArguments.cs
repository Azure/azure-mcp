// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;

namespace AzureMcp.Arguments.Server;

public class ServiceStartArguments
{
    [JsonPropertyName("transport")]
    public string Transport { get; set; } = TransportTypes.StdIo;

    [JsonPropertyName("port")]
    public int Port { get; set; }
}
