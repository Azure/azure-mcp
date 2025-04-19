// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Arguments;
using System.Text.Json;

namespace AzureMcp.Services.Interfaces;

public interface IAppServiceService
{

    Task<List<string>> ListAppServicePlans(
        string subscriptionId,
        string? tenant = null,
        RetryPolicyArguments? retryPolicy = null);

}