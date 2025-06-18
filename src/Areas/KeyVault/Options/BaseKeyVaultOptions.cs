// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.KeyVault;

public class BaseKeyVaultOptions : SubscriptionOptions
{
    public string? VaultName { get; set; }
}
