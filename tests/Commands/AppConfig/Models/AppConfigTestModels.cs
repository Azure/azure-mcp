using AzureMcp.Models.AppConfig;
using System.Text.Json.Serialization;

namespace AzureMcp.Tests.Commands.AppConfig.Models;

/// <summary>
/// Represents the result of an account list operation.
/// </summary>
public sealed class AccountListResult
{
    [JsonPropertyName("accounts")]
    public List<AppConfigurationAccount> Accounts { get; set; } = [];
}

/// <summary>
/// Represents the result of a key-value delete operation.
/// </summary>
public sealed class KeyValueDeleteResult
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string? Label { get; set; }
}

/// <summary>
/// Represents the result of a key-value list operation.
/// </summary>
public sealed class KeyValueListResult
{
    [JsonPropertyName("settings")]
    public List<KeyValueSetting> Settings { get; set; } = [];
}

/// <summary>
/// Represents the result of a key-value lock operation.
/// </summary>
public sealed class KeyValueLockResult
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("isLocked")]
    public bool IsLocked { get; set; }
}

/// <summary>
/// Represents the result of a key-value set operation.
/// </summary>
public sealed class KeyValueSetResult
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string? Label { get; set; }
}

/// <summary>
/// Represents the result of a key-value show operation.
/// </summary>
public sealed class KeyValueShowResult
{
    [JsonPropertyName("setting")]
    public KeyValueSetting Setting { get; set; } = new();
}

/// <summary>
/// Represents the result of a key-value unlock operation.
/// </summary>
public sealed class KeyValueUnlockResult
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("isLocked")]
    public bool IsLocked { get; set; }
}
