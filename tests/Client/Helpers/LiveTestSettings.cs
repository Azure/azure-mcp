// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Tests.Client.Helpers;

public class LiveTestSettings
{
    public string? TenantId { get; set; }
    public string? TenantName { get; set; }
    public string? SubscriptionId { get; set; }
    public string? SubscriptionName { get; set; }
    public string? ResourceGroupName { get; set; }
    public string? ResourceBaseName { get; set; }
    public string? CosmosAccountName { get; set; }
}