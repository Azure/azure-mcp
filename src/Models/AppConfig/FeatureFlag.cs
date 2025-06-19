// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Models.AppConfig;

public class FeatureFlag
{
    public string Id { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Enabled { get; set; }
    public IDictionary<string, object> Conditions { get; set; } = new Dictionary<string, object>();
}
