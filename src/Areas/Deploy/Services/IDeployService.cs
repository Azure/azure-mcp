// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.Deploy.Models;
using AzureMcp.Options;

namespace AzureMcp.Areas.Deploy.Services;

public interface IDeployService
{
    Task<string> GetAzdResourceLogsAsync(
        string workspaceFolder,
        string azdEnvName,
        string subscriptionId,
        int? limit = null);
}
