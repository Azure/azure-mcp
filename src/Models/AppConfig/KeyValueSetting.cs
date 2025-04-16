// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Models.AppConfig;

public class KeyValueSetting
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public ETag ETag { get; set; } = new();
    public DateTimeOffset? LastModified { get; set; }
    public bool? Locked { get; set; }
}