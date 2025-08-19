// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.AzureSignalR.Models;

/// <summary>
/// Represents a SignalR service access key.
/// </summary>
public class SignalRKeyModel
{
    /// <summary>
    /// The type of the key (Primary or Secondary).
    /// </summary>
    public string KeyType { get; set; } = string.Empty;

    /// <summary>
    /// The connection string for this key.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// The primary key value.
    /// </summary>
    public string PrimaryKey { get; set; } = string.Empty;

    /// <summary>
    /// The secondary key value.
    /// </summary>
    public string SecondaryKey { get; set; } = string.Empty;

    /// <summary>
    /// The primary connection string.
    /// </summary>
    public string PrimaryConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// The secondary connection string.
    /// </summary>
    public string SecondaryConnectionString { get; set; } = string.Empty;
}
