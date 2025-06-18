// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.KeyVault.Options;

namespace AzureMcp.Areas.KeyVault.Options.Key
{
    public class KeyListOptions : BaseKeyVaultOptions
    {
        public bool IncludeManagedKeys { get; set; } = false;
    }
}
