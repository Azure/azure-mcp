// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Areas.Server.Commands;
using AzureMcp.Core.Areas.Server.Commands.ToolLoading;
using AzureMcp.Core.Options;

namespace AzureMcp.Deploy.Options.Architecture;

public class DiagramGenerateOptions : GlobalOptions
{
    [JsonPropertyName(CommandFactoryToolLoader.RawMcpToolInputOptionName)]
    public string? RawMcpToolInput { get; set; }
}
