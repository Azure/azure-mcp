// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Core.Options;

namespace AzureMcp.Deploy.Options.App;

public class LogsGetOptions : SubscriptionOptions
{
    [JsonPropertyName("workspaceFolder")]
    public string WorkspaceFolder { get; set; } = string.Empty;

    [JsonPropertyName("azdEnvName")]
    public string AzdEnvName { get; set; } = string.Empty;

    [JsonPropertyName("limit")]
    public int? Limit { get; set; }
}
