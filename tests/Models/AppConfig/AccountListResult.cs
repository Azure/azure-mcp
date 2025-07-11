// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Text.Json.Serialization;
using AzureMcp.Areas.AppConfig.Models;

namespace AzureMcp.Tests.Models.AppConfig;

/// <summary>
/// Represents the result of an account list operation.
/// </summary>
public sealed class AccountListResult
{
    [JsonPropertyName("accounts")]
    public List<AppConfigurationAccount> Accounts { get; set; } = [];
}
