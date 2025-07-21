// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.KeyVault.Options.Secret;

public class SecretCreateOptions : BaseKeyVaultOptions
{
    public string? SecretName { get; set; }
    public string? SecretValue { get; set; }
}
