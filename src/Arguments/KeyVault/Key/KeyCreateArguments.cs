// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Arguments.KeyVault.Key;

public class KeyCreateArguments : BaseKeyVaultArguments
{
    public string? KeyName { get; set; }
    public string? KeyType { get; set; }
}
