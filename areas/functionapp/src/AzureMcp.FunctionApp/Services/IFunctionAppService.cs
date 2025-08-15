// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.FunctionApp.Models;

namespace AzureMcp.FunctionApp.Services;

public interface IFunctionAppService
{
    Task<List<FunctionAppInfo>?> ListFunctionApps(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);

    Task<FunctionAppInfo> CreateFunctionApp(
        string subscription,
        string resourceGroup,
        string functionAppName,
        string location,
        string? appServicePlan = null,
        string? planType = null,
        string? planSku = null,
        string? containerAppName = null,
        string? runtime = null,
        string? runtimeVersion = null,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
