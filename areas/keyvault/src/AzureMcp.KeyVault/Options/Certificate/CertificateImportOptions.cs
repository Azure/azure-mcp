// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.KeyVault.Options.Certificate;

public class CertificateImportOptions : BaseKeyVaultOptions
{
    public string? CertificateName { get; set; }

    public string? CertificateData { get; set; }

    public string? Password { get; set; }
}
