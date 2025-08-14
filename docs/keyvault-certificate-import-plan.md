# Plan: Key Vault Certificate Import Command (azmcp-keyvault-certificate-import)

## Goal
Add a new Azure MCP Server tool / command to import an existing certificate (PFX or PEM with private key) into an Azure Key Vault.

Tool description (authoritative initial version):
"azmcp-keyvault-certificate-import - Import existing certificates and private keys into Key Vault securely. This tool uploads certificates from various formats (PFX, PEM) with password protection and access policy configuration. Returns import status and certificate details. Requires vault-name, certificate-name, and certificate-data."

## Scope
Adds one new command under existing Key Vault area: `azmcp keyvault certificate import` (tool name `azmcp-keyvault-certificate-import`).

## Requirements Checklist
- [ ] Add new options: `--certificate` (already exists), `--vault` (already exists), `--certificate-data` (new), `--password` (optional for PFX), future-proof optional `--policy` (OUT OF SCOPE NOW) & access policy configuration (description mentions it but actual access policies are managed outside certificate import; we will clarify in docs and defer advanced policy customization).
- [ ] Update `KeyVaultOptionDefinitions` with `CertificateData` and `CertificatePassword` options.
- [ ] Create `CertificateImportOptions : BaseKeyVaultOptions` with `CertificateName`, `CertificateData`, `Password`.
- [ ] Extend `IKeyVaultService` with `ImportCertificate` method.
- [ ] Implement `ImportCertificate` in `KeyVaultService` using `CertificateClient.ImportCertificateAsync`.
- [ ] Implement `CertificateImportCommand` (sealed, primary constructor) following existing pattern (similar to `CertificateCreateCommand`).
- [ ] Add JSON serialization entry for new result record in `KeyVaultJsonContext`.
- [ ] Register command in `KeyVaultSetup` under certificate group.
- [ ] Unit tests: parameter binding & validation, service invocation, exception handling (mock service). (Mocking return instance is complex; will assert call + error path.)
- [ ] Live test: generate a self-signed cert at runtime (PFX with password) and import it; assert returned properties (name, thumbprint, cer). (Uses existing deployed vault; no infra change.)
- [ ] e2e test prompts update: add prompts for import (position alphabetically between certificate-get and certificate-list).
- [ ] Documentation updates: `CHANGELOG.md`, `docs/azmcp-commands.md` (command reference), `README.md` (example prompt under Key Vault), VSIX README if applicable (SKIP – Key Vault section already exists; adding example not mandatory but can be included), `e2eTests/e2eTestPrompts.md` (done above), ensure alphabetical ordering maintained.
- [ ] Ensure AOT safety (pattern matches existing Key Vault code which is already AOT-included; no reflection heavy code; no additional packages).
- [ ] Run build & tests; spelling check; format.

## Design Details
### Options
New option names follow existing style (lowercase, hyphenated nouns):
- `--certificate-data` (required): Accepts either (a) path to PFX/PEM file or (b) base64 string of PFX bytes or (c) raw PEM text (starts with `-----BEGIN`). Implementation detection order:
  1. If file exists -> read bytes (binary or text). For PEM, pass entire raw text bytes.
  2. Else if starts with `-----BEGIN` treat as PEM text -> UTF8 bytes.
  3. Else attempt Base64 decode → bytes (if fails -> validation error 400).
- `--password` optional: only used if importing password-protected PFX. Passed to `ImportCertificateOptions.Password` if not null/empty.

### Service Method Contract
Signature:
```csharp
Task<KeyVaultCertificateWithPolicy> ImportCertificate(
    string vaultName,
    string certificateName,
    string certificateData,
    string? password,
    string subscription,
    string? tenantId = null,
    RetryPolicyOptions? retryPolicy = null);
```
Validation: vaultName, certificateName, certificateData, subscription all required. Throws informative exception wrapping Azure SDK errors.

### Command Response Shape
Matches create command fields for consistency:
```
name, id, keyId, secretId, cer (base64), thumbprint, enabled,
notBefore, expiresOn, createdOn, updatedOn, subject, issuerName
```

### Error Handling
Reuse base patterns; custom messages for:
- File not found
- Invalid base64 / unsupported format
Azure `RequestFailedException` mapped via base handler.

### Unit Tests
Scenarios:
1. Success path triggers service call (service throws controlled exception to avoid constructing complex return object) – verify parameters.
2. Missing certificate-data -> 400.
3. Invalid data (simulate parse failure) -> returns 400 with message containing `certificate-data`.
4. Service throws -> 500 with troubleshooting link.

### Live Test
1. Generate self-signed cert (RSA 2048, `CN=ImportTest`) and export PFX with random password.
2. Call tool with generated name, base64(pfx bytes) as `certificate-data`, provide `--password`.
3. Assert status 200 and expected properties present.

### e2e Prompts
Add at least two prompts:
| azmcp-keyvault-certificate-import | Import the certificate from file <file_path> into the key vault <key_vault_account_name> with name <certificate_name> |
| azmcp-keyvault-certificate-import | Import a PFX certificate into the key vault <key_vault_account_name> named <certificate_name> |

### Non-Goals / Deferred
- Advanced access policy configuration (requires ARM operations) – clarify in description docs as out-of-scope for initial release.
- Policy customization during import (using custom `CertificatePolicy`) – using server default for simplicity.
- PEM chain parsing / splitting – raw PEM content passed directly; Azure SDK interprets.

## Implementation Order
1. Add new options + options class + interface method.
2. Implement service method.
3. Add command + JSON context update + setup registration.
4. Unit tests.
5. Live test addition.
6. Docs + e2e prompts + changelog.
7. Build, format, tests, spelling.

## Risks & Mitigations
- Invalid user input (nonexistent file, bad base64) → explicit validation & user-friendly errors.
- Large certificates ( > ~20KB) – acceptable; in-memory handling fine.
- AOT concern (X509 operations) – standard BCL only; safe.

## Acceptance Criteria
- Tool discoverable with specified description.
- Passing valid PFX (file or base64) produces 200 and expected fields.
- Validation errors return 400.
- Listed in docs and prompts.

---
Plan approved for implementation steps above.
