// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.


// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using AzureMcp.Areas.KeyVault.Options;

namespace AzureMcp.Areas.KeyVault.Options.Secret;

public class SecretGetOptions : BaseKeyVaultOptions
{
    public string? SecretName { get; set; }
}
