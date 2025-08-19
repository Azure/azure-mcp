// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;
using AzureMcp.Deploy.Models;

namespace AzureMcp.Deploy.Services;

public interface IDeployService
{
    Task<string> GetAzdResourceLogsAsync(
        string workspaceFolder,
        string azdEnvName,
        string subscriptionId,
        int? limit = null);
}
