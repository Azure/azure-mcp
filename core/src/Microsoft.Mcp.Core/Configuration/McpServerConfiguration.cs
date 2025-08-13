// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.Mcp.Core.Configuration;

public sealed class McpServerConfiguration
{
    public const string DefaultName = "Microsoft.Mcp.Server";

    public string Name { get; set; } = DefaultName;
    public string? Version { get; set; }
    public string? UserAgent { get; set; }
    public bool EnableTelemetry { get; set; } = true;
}