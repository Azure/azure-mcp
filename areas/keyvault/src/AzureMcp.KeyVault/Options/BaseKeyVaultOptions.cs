// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Core.Options;

namespace AzureMcp.KeyVault.Options;

public class BaseKeyVaultOptions : SubscriptionOptions
{
    public string? VaultName { get; set; }
}
