# PR #626 — Deploy and Quota: Action Plan and Next Steps

This plan consolidates concrete follow-ups to take PR #626 from “draft” to “merge-ready,” based on the repository standards and the findings in PR review notes.

## Goals and success criteria

- Consistent, hierarchical command UX reflected across code, tests, and docs
- AOT/trimming and cloud-agnostic compliance preserved (no RequiresDynamicCode; sovereign cloud aware)
- Clean docs (no legacy names), CHANGELOG complete, spelling/build checks clean
- Clear path to reuse az/azd extension services; current temporary logic abstracted

## P0 (must complete before merge)

- Naming, descriptions, and docs
  - [x] CHANGELOG: switch examples to hierarchical CLI usage (e.g., `azmcp deploy plan get`, `azmcp quota region availability list`), optionally keep MCP tool ids in a separate subsection
  - [x] Remove any legacy/hyphenated names from command group descriptions and help
    - Files:
      - DeploySetup.cs: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/deploy/src/AzureMcp.Deploy/DeploySetup.cs#L26-L31
      - QuotaSetup.cs: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/quota/src/AzureMcp.Quota/QuotaSetup.cs#L23-L27
  - [x] docs/azmcp-commands.md: ensure examples match current hierarchy; fix duplicated token cases.
    - Quota usage example (approx lines): https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/docs/azmcp-commands.md#L897-L905
    - Quota region availability example (approx lines): https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/docs/azmcp-commands.md#L902-L908
    - Deploy section header (approx line): https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/docs/azmcp-commands.md#L909
    - Deploy app logs example (approx lines): https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/docs/azmcp-commands.md#L924-L929

- Repo hygiene and gates
  - [x] Run spelling: `./eng/common/spelling/Invoke-Cspell.ps1` and fix findings
  - [x] Run local verification: `./eng/scripts/Build-Local.ps1 -UsePaths -VerifyNpx` and fix issues
  - [x] Ensure `dotnet build AzureMcp.sln` passes cleanly (warnings-as-errors respected)

- Robustness and platform compliance
  - [x] Add `CancellationToken` plumbing to long-running operations (logs, region/usage queries) and propagate from commands to services
  - [x] Replace `Console.WriteLine` usages with `ILogger` (structured, leveled logging)
  - [x] Replace static `HttpClient` with `IHttpClientFactory` via DI for direct REST calls
  - [x] Ensure sovereign cloud support: avoid hard-coded `https://management.azure.com`; prefer ARM SDK or derive authority from `ArmEnvironment` for PostgreSQL usage checker
  - [x] Verify consistent exit codes and clear, actionable error messages

## P1 (should complete for maintainability)

- Integration and abstraction
  - [x] Introduce an interface (e.g., `IAppLogService`/`IAzdLogService`) and put current logs implementation behind it
  **The command will be replaced by azd extension command so no need to refactor to call azd**
  - [x] Add a short deprecation note in code/docs indicating intent to delegate to `azd` native logs when available; plan to route via extension service (e.g., `IAzdService`)

- Tests and schemas
  - [x] Diagram command: add tests for malformed/invalid `raw-mcp-tool-input` and oversize payload handling (safe URL limits)
  - [x] Quota commands: add tests for empty/whitespace resource types, mixed casing, and very long lists
  - [x] Add JSON round-trip tests to prove STJ source-gen coverage (no reflection fallback)

- Documentation polish
  - [x] Per-command help examples (include one example with `raw-mcp-tool-input`)
  **Schema is defined in the description of the command**
  - [x] Troubleshooting notes (auth, timeouts, diagram URL length)

## P2 (nice to have)

- Performance and caching
  - [x] Cache region availability results per subscription/provider (short TTL) to reduce redundant queries
  **As this is a local tool, caching is not needed**
  - [x] Cache embedded templates in `TemplateService`
  **As this is a local tool, caching is not needed**

- UX and contracts
  - [x] Optional `--verbose` flag following repo logging conventions
  - [x] Document output contracts (shape, casing) and link JSON schemas in docs

## File-level edits (suggested targets)

- Descriptions and registration
  - `areas/deploy/src/AzureMcp.Deploy/DeploySetup.cs` – group description/help text cleanup; ensure hierarchical verbs in help
  - `areas/quota/src/AzureMcp.Quota/QuotaSetup.cs` – confirm concise help and subgroup descriptions

- Quota services (robustness/cloud)
  - `areas/quota/src/AzureMcp.Quota/Services/Util/*.cs`
    - Replace `Console.WriteLine` with `ILogger`
    - Introduce `CancellationToken` parameters
    - Switch static `HttpClient` to `IHttpClientFactory`
    - Remove hard-coded management endpoint; prefer ARM SDK or environment-derived authority
      - AzureUsageChecker.cs: hardcoded authority and static HttpClient
        - Token scope: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/quota/src/AzureMcp.Quota/Services/Util/AzureUsageChecker.cs#L68
        - Static HttpClient: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/quota/src/AzureMcp.Quota/Services/Util/AzureUsageChecker.cs#L53
        - Console.WriteLine usages: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/quota/src/AzureMcp.Quota/Services/Util/AzureUsageChecker.cs#L86-L169
      - PostgreSQLUsageChecker.cs: hardcoded https://management.azure.com
        - Request URL: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/quota/src/AzureMcp.Quota/Services/Util/Usage/PostgreSQLUsageChecker.cs#L14-L15

- Deploy logs (integration seam)
  - `areas/deploy/src/AzureMcp.Deploy/Services/*` – introduce `IAppLogService` (or `IAzdLogService`), adapt current implementation behind interface; prepare to delegate to extension service when available
    - AzdResourceLogService.cs
      - Entry method: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/deploy/src/AzureMcp.Deploy/Services/Util/AzdResourceLogService.cs#L12-L18
      - AzdAppLogRetriever usage: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/deploy/src/AzureMcp.Deploy/Services/Util/AzdResourceLogService.cs#L24
    - AzdAppLogRetriever.cs
      - Type and initializer: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/deploy/src/AzureMcp.Deploy/Services/Util/AzdAppLogRetriever.cs#L11-L31
      - QueryAppLogsAsync switch cases: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/deploy/src/AzureMcp.Deploy/Services/Util/AzdAppLogRetriever.cs#L66-L116
    - TemplateService.cs
      - Embedded template loader: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/deploy/src/AzureMcp.Deploy/Services/Templates/TemplateService.cs#L14-L48

- Documentation
  - `docs/azmcp-commands.md` – hierarchical examples + troubleshooting
  - `CHANGELOG.md` – hierarchical CLI usage; optionally list MCP tool ids separately
  - JSON source-gen contexts (AOT):
    - QuotaJsonContext.cs: https://github.com/qianwens/azure-mcp/blob/qianwen/deploy/areas/quota/src/AzureMcp.Quota/Commands/QuotaJsonContext.cs#L12-L28

## AOT/trimming and cloud checks

- [x] Run `eng/scripts/Analyze-AOT-Compact.ps1`; resolve any warnings (linker config if needed)
- [x] Ensure only System.Text.Json is used and DTOs are covered by source-gen contexts
- [x] Confirm usage of Azure SDK defaults (retries/timeouts) and respect cloud/authority from environment (sovereign-ready)

## Validation checklist (green-before-merge)

- Build: `dotnet build` and `./eng/scripts/Build-Local.ps1 -UsePaths -VerifyNpx` pass
- Spelling: `./eng/common/spelling/Invoke-Cspell.ps1` clean
- Tests: unit + live tests pass, including new edge cases
- Manual E2E: execute all prompts in `docs/PR-626-Manual-Testing-Plan.md` (Deploy + Quota features from PR #626 only). Record pass/fail notes; all must pass. Create issues for any failures and cross-link to this document and PR #626.
- Help/smoke: `azmcp deploy --help` and `azmcp quota --help` show expected hierarchy; examples are copyable and correct
- Docs: CHANGELOG and azmcp-commands updated

## Optional runbook (local)

> The following commands are optional references when validating locally on Windows PowerShell.

```powershell
# Build + verify
./eng/scripts/Build-Local.ps1 -UsePaths -VerifyNpx

# Spelling
./eng/common/spelling/Invoke-Cspell.ps1

# Dotnet build
dotnet build ./AzureMcp.sln
```

## Ownership and tracking

- Create or link tracking issues for:
  - Logs abstraction + future delegation to `azd`
  - Sovereign-cloud compliance in direct REST usage (if any remain after refactor)
  - New tests (diagram/edge cases, quota parsing, JSON round-trip)
- Convert the checklists above into PR tasks or repo issues as appropriate.

### Follow-up Issue Creation (P1/P2)

For every P1 or P2 item in this action plan that is not completed in PR #626:

- Create a GitHub issue titled: "[P1|P2] <short action>: <area>"
- Labels: `area/deploy` or `area/quota` (and others as appropriate), `priority/P1` or `priority/P2`, and `PR/626-followup`
- Body: problem statement, acceptance criteria, links to exact files/lines and to this document, owner, and due date
- Cross-link in PR #626 and check off the corresponding item here when done

---

Completion definition: All P0 items checked, validation checklist green, and at least the P1 “logs abstraction” in place with a deprecation note for the temporary implementation.
