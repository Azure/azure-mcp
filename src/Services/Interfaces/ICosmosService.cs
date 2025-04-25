// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using AzureMcp.Models;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace AzureMcp.Services.Interfaces;

public interface ICosmosService : IDisposable
{
    Task<List<string>> GetCosmosAccounts(
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<string>> ListDatabases(
        string accountName,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<string>> ListContainers(
        string accountName,
        string databaseName,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

    Task<List<JsonNode>> QueryItems(
        string accountName,
        string databaseName,
        string containerName,
        string? query,
        string subscriptionId,
        AuthMethod authMethod = AuthMethod.Credential,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);
}