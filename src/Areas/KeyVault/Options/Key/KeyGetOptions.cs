// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.KeyVault.Options;

namespace AzureMcp.Areas.KeyVault.Options.Key;

public class KeyGetOptions : BaseKeyVaultOptions
{
    public string? KeyName { get; set; }
}
