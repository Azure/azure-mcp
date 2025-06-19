// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.KeyVault.Options.Key;

public class KeyCreateOptions : BaseKeyVaultOptions
{
    public string? KeyName { get; set; }
    public string? KeyType { get; set; }
}
