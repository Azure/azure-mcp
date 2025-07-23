// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.Server.Commands;
using AzureMcp.Areas.Server.Commands.ToolLoading;
using AzureMcp.Options;

namespace AzureMcp.Areas.Deploy.Options;

public class RawMcpToolInputOptions : GlobalOptions
{
    [JsonPropertyName(CommandFactoryToolLoader.RawMcpToolInputOptionName)]
    public string? RawMcpToolInput { get; set; }
}
