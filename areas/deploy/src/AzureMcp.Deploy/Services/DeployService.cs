// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Areas.Deploy.Services.Util;
using Azure.Core;
using AzureMcp.Core.Services.Azure;

namespace AzureMcp.Deploy.Services;

public class DeployService() : BaseAzureService, IDeployService
{

    public async Task<string> GetAzdResourceLogsAsync(
         string workspaceFolder,
         string azdEnvName,
         string subscriptionId,
         int? limit = null)
    {
        TokenCredential credential = await GetCredential();
        string result = await AzdResourceLogService.GetAzdResourceLogsAsync(
            credential,
            workspaceFolder,
            azdEnvName,
            subscriptionId,
            limit);
        return result;
    }
}
