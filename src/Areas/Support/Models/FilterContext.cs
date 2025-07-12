// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.Support.Models;

public record FilterContext(
    string? TenantId,
    RetryPolicyOptions? RetryPolicy);
