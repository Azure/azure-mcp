// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.Mcp.Core.Configuration;

public sealed class McpServerConfiguration
{
    public string Name { get; set; } = string.Empty;
    public string? Version { get; set; }
    public string? UserAgent { get; set; }
    public bool IsTelemetryEnabled { get; set; } = true;
}