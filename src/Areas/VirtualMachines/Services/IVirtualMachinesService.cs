// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Azure.ResourceManager.Compute.Models;
using AzureMcp.Models;

namespace AzureMcp.Areas.VirtualMachines.Services;

public interface IVirtualMachinesService
{
    Task<List<VirtualMachineInfo>> ListVirtualMachinesAsync(
        string subscription,
        string? resourceGroup,
        string? tenant,
        RetryPolicyOptions? retryPolicy,
        CancellationToken cancellationToken = default);
}

public record VirtualMachineInfo(
    string Name,
    string? ResourceGroup,
    string? Location,
    string? Status,
    string? Size,
    string? OsType);