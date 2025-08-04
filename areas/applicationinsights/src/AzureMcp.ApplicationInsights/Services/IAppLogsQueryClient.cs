// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.Diagnostics.CodeAnalysis;
using Azure.Core;
using Azure.Monitor.Query;
using AzureMcp.ApplicationInsights.Models;

namespace AzureMcp.ApplicationInsights.Services
{
    public interface IAppLogsQueryClient
    {
        Task<IReadOnlyList<AppLogsQueryRow<T>>> QueryResourceAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(ResourceIdentifier resourceId, string kql, QueryTimeRange timeRange) where T : new();
    }
}
