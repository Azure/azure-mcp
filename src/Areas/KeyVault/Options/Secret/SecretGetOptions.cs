// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.KeyVault.Options.Secret;

public class SecretGetOptions : BaseKeyVaultOptions
{
    public string? SecretName { get; set; }
}
