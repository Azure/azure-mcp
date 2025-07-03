// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

namespace AzureMcp.Areas.KeyVault.Options.Certificate;

public class CertificateGetOptions : BaseKeyVaultOptions
{
    public string? CertificateName { get; set; }
}
