// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace Microsoft.Mcp.Core.Models.Option;

/// <summary>
/// Information about a command option.
/// </summary>
public sealed class OptionInfo
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsRequired { get; set; }
    public object? DefaultValue { get; set; }
}