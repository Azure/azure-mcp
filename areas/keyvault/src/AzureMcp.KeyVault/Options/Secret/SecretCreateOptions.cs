// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.KeyVault.Options.Secret;

public class SecretCreateOptions : BaseKeyVaultOptions
{
    public string? SecretName { get; set; }
    public string? SecretValue { get; set; }
}
