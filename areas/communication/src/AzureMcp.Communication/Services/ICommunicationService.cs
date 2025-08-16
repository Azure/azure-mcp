// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Communication.Models;
using AzureMcp.Core.Options;

namespace AzureMcp.Communication.Services;

public interface ICommunicationService
{
    Task<List<SmsResult>> SendSmsAsync(
        string connectionString,
        string from,
        string[] to,
        string message,
        bool enableDeliveryReport = false,
        string? tag = null,
        RetryPolicyOptions? retryPolicy = null);
}