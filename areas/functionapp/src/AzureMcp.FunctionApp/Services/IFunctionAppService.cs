// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.FunctionApp.Models;

namespace AzureMcp.FunctionApp.Services;

public interface IFunctionAppService
{
    Task<List<FunctionAppModel>?> ListFunctionApps(
        string subscription,
        string? tenant = null,
        RetryPolicyOptions? retryPolicy = null);
}
