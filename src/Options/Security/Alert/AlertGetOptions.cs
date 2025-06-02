// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AzureMcp.Models.Option;

namespace AzureMcp.Options.Security.Alert;

/// <summary>
/// Options for getting a security alert.
/// </summary>
public class AlertGetOptions : SubscriptionOptions
{
    /// <summary>
    /// The ID of the alert to retrieve.
    /// </summary>
    [Required]
    [JsonPropertyName(OptionDefinitions.Security.SystemAlertIdName)]
    public string SystemAlertId { get; set; } = string.Empty;
}
