// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.KeyVault.Options.Certificate;

public class CertificateCreateOptions : BaseKeyVaultOptions
{
    public string? CertificateName { get; set; }
}
