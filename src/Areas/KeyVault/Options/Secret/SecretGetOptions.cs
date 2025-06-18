// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Options.KeyVault.Secret;

public class SecretGetOptions : BaseKeyVaultOptions
{
    public string? SecretName { get; set; }
}
