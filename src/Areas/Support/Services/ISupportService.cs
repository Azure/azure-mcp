// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.Core;
using AzureMcp.Areas.Support.Models;
using AzureMcp.Options;

namespace AzureMcp.Areas.Support.Services;

public interface ISupportService
{
    Task<List<SupportTicket>> ListSupportTickets(
        string subscription,
        string? filter = null,
        int? top = null,
        string? tenantId = null,
        RetryPolicyOptions? retryPolicy = null);
}
