using System.Text.Json;
using System.Text.Json.Serialization;

namespace ToolSelection.Models;

// Constants
public static class McpConstants
{
    public const string LatestProtocolVersion = "2025-06-18";
    public const string JsonRpcVersion = "2.0";
}

// Standard JSON-RPC error codes
public static class ErrorCodes
{
    public const int ParseError = -32700;
    public const int InvalidRequest = -32600;
    public const int MethodNotFound = -32601;
    public const int InvalidParams = -32602;
    public const int InternalError = -32603;
}

// Basic types
public enum Role
{
    [JsonPropertyName("user")]
    User,
    [JsonPropertyName("assistant")]
    Assistant
}

public enum LoggingLevel
{
    [JsonPropertyName("debug")]
    Debug,
    [JsonPropertyName("info")]
    Info,
    [JsonPropertyName("notice")]
    Notice,
    [JsonPropertyName("warning")]
    Warning,
    [JsonPropertyName("error")]
    Error,
    [JsonPropertyName("critical")]
    Critical,
    [JsonPropertyName("alert")]
    Alert,
    [JsonPropertyName("emergency")]
    Emergency
}

// Base metadata
public class BaseMetadata
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }
}

// Tool annotations
public class ToolAnnotations
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("readOnlyHint")]
    public bool? ReadOnlyHint { get; set; }

    [JsonPropertyName("destructiveHint")]
    public bool? DestructiveHint { get; set; }

    [JsonPropertyName("idempotentHint")]
    public bool? IdempotentHint { get; set; }

    [JsonPropertyName("openWorldHint")]
    public bool? OpenWorldHint { get; set; }
}

// Tool definition
public class Tool : BaseMetadata
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("inputSchema")]
    public required JsonElement InputSchema { get; set; }

    [JsonPropertyName("outputSchema")]
    public JsonElement? OutputSchema { get; set; }

    [JsonPropertyName("annotations")]
    public ToolAnnotations? Annotations { get; set; }

    [JsonPropertyName("_meta")]
    public Dictionary<string, object>? Meta { get; set; }
}

// List tools result
public class ListToolsResult
{
    [JsonPropertyName("tools")]
    public required List<Tool> Tools { get; set; }

    [JsonPropertyName("nextCursor")]
    public string? NextCursor { get; set; }

    [JsonPropertyName("_meta")]
    public Dictionary<string, object>? Meta { get; set; }
}
