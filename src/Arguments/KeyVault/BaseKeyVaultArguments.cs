// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Arguments.KeyVault;

public class BaseKeyVaultArguments : SubscriptionArguments
{
    public string? VaultName { get; set; }
}
