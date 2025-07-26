// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AppConfig.Models;

public class KeyValueSetting
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public ETag ETag { get; set; } = new();
    public DateTimeOffset? LastModified { get; set; }
    public bool? Locked { get; set; }
    internal record KeyValueResult(string Key, string? Label = null, string? Value = null, string? IsLocked = null);
    internal record KeyValueSettingsCollection(IReadOnlyList<KeyValueSetting> Settings);
    internal record KeyValueSettingsResult(KeyValueSetting Setting);
}
