// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Arguments.KeyVault.Key;

using AzureMcp.Arguments.KeyVault;

public class KeyListArguments : BaseKeyVaultArguments
{
    public string? VaultName { get; set; }
}
