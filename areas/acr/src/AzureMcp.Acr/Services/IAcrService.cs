// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Acr.Services;

public interface IAcrService
{
    Task<List<string>> ListRegistries(
        string subscription,
        string? resourceGroup = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
