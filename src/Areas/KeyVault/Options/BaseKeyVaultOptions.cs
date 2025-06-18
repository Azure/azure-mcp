// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Options;

namespace AzureMcp.Areas.KeyVault.Options;

public class BaseKeyVaultOptions : SubscriptionOptions
{
    public string? VaultName { get; set; }
}
