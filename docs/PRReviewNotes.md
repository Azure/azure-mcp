# Code Review Report: PR #626 — Deploy and Quota Commands

This report reviews PR #626 against the guidance in `docs/PR-626-Final-Recommendations.md` and repository standards. It summarizes what’s aligned, what’s missing, risks, and concrete next steps to reach compliance and improve maintainability.

## Review checklist

- [ ] Command groups follow hierarchical structure and consistent naming
- [ ] Command names use <object> <verb> pattern (no hyphens)
- [ ] Files/namespaces reorganized per target structure
- [ ] Integration leverages existing extension services (az/azd) where applicable
- [ ] AOT/trimming safety validated (no RequiresDynamicCode violations)
- [ ] Uses System.Text.Json (no Newtonsoft)
- [ ] Tests updated and sufficient coverage
- [ ] Documentation updated (commands and prompts)
  Note: No migration-from-legacy section is required; we only document the new hierarchical structure.
- [ ] CHANGELOG and spelling checks updated

## Findings

### 1) Command structure and naming

- Deploy area now uses hierarchical groups and verbs:
  - deploy app logs get
  - deploy infrastructure rules get
  - deploy pipeline guidance get
  - deploy plan get
  - deploy architecture diagram generate
- Quota area uses hierarchical groups and verbs:
  - quota usage check
  - quota region availability list
- Registration uses CommandGroup with nested subgroups for both areas. Good.
- Minor issue: the top-level deploy CommandGroup description string still references legacy hyphenated names (plan_get, iac_rules_get, etc.). This is cosmetic but inconsistent with the new pattern.

Status: Mostly PASS (fix description text).

### 2) File/folder organization and namespaces

- Deploy: Commands and Options are placed under App/Infrastructure/Pipeline/Plan/Architecture subfolders. Services and Templates folders added. JsonSourceGeneration context present. Good.
- Quota: Commands moved under Usage/ and Region/, Options split accordingly. Good.

Status: PASS.

### 3) Integration with existing extension commands

- Guidance recommends reusing the existing extension (Az/Azd) services to avoid duplication and clarify ownership. Current DeployService calls a local AzdResourceLogService to fetch logs via workspace parsing + Monitor Query. There’s no explicit reuse of the extension Az/Azd command surface.
- The PR’s intent acknowledges that azd-app-logs is temporary until azd exposes a native command. That’s fine short-term, but we should still abstract this behind interfaces and consider delegating through the extension service to reduce duplication and future migrations.

Status: PARTIAL. Needs refactor to consume extension services (IAzService/IAzdService) or document/ticket the temporary approach with deprecation plan.

### 4) AOT/trimming safety

- Projects declare <IsAotCompatible>true</IsAotCompatible>.
- Uses System.Text.Json source generation in Deploy (DeployJsonContext) and Quota (QuotaJsonContext). Good.
- YamlDotNet is used, but via low-level parser (events) rather than POCO deserialization. This reduces reflection/AOT risk, but we should still run the AOT analyzer script to confirm there are no warnings and consider linker descriptors if needed.
- Reflection is used to load embedded resources (GetManifestResourceStream). That’s typically safe with embedded resources; ensure resources are included (they are) and names are stable.

Status: LIKELY PASS (verify with analyzer). Action item to run eng/scripts/Analyze-AOT-Compact.ps1 and address any findings.

### 5) JSON library choice

- System.Text.Json is used across new code. No Newtonsoft dependencies in these areas. Good.

Status: PASS.

### 6) Tests

- Unit tests added for Quota commands (AvailabilityListCommandTests, CheckCommandTests) and Deploy command tests exist for the reorganized classes (LogsGetCommandTests, RulesGetCommandTests, GuidanceGetCommandTests, etc.).
- Tests validate parsing, happy paths, errors, and output shapes. Nice.

Status: PASS (keep adding targeted edge cases; see gaps below).

### 7) Documentation

- docs/azmcp-commands.md updated to include Deploy/Quota sections.
- Issue: one example contains a duplicated group token: “azmcp quota quota region availability list”.
- E2E prompts updated; several questions remain unresolved in PR comments (e.g., value-add vs existing tools, pipeline setup guidance). Document the new hierarchical structure explicitly; no migration-from-legacy content is needed.

Status: PARTIAL. Fix command typos and document the new hierarchical structure (no migration section).

### 8) CHANGELOG and spelling

- PR checklist flags CHANGELOG and spelling as incomplete. These need to be completed before merge.

Status: FAIL (to be addressed).

## Gaps and risks

- Integration duplication: DeployService/AzdResourceLogService duplicates behavior that would better live behind extension services. Risk of divergence and confusion with azd MCP server effort. Mitigate by factoring through IAzService/IAzdService and marking the logs command as temporary.
- Documentation accuracy: Minor typos/conflicts can mislead users (e.g., duplicated “quota” token). Add migration notes to reduce confusion for users familiar with prior hyphenated commands.
- AOT/trimming: While likely safe, adding YamlDotNet warrants a quick AOT scan and consideration of linker configs if warnings appear.
- CI failures: Current PR pipeline shows failures for several platform builds (linux_x64, osx_x64, win_x64). Investigate before merge.

## Targeted recommendations and next steps

P0 (must do before merge)
- Fix deploy CommandGroup description to remove legacy hyphenated names and align with actual subcommands.
- Fix docs/azmcp-commands.md typos (“azmcp quota quota region availability list” → “azmcp quota region availability list”).
- Add CHANGELOG entry summarizing new areas (deploy/quota), command structure, and any breaking command name changes.
- Run spelling check: .\\eng\\common\\spelling\\Invoke-Cspell.ps1 and address findings.
- Investigate the CI failures on the PR (linux_x64, osx_x64, win_x64 jobs) and resolve.

P1 (integration and maintainability)
- Abstract DeployService’s log retrieval to use extension services (IAzdService) where possible, or encapsulate current logic behind an interface to ease migration when azd exposes native logs.
- Consider adding a light project reference to the extension area if needed for reuse (as recommended), or explicitly document why it’s deferred.
- Provide help examples for each command and ensure output shape/casing are documented.
- Expand tests for:
  - Architecture diagram: invalid/malformed raw-mcp-tool-input and large service graphs.
  - Quota parsing: empty/whitespace resource-types; mixed casing; extreme list lengths.

P2 (optional enhancements)
- Template system: Centralize all prompt content via TemplateService (you’ve started this); document template names and parameters; add unit tests for template retrieval.
- Performance: Consider caching region availability lookups and IaC rule templates where applicable.
- AOT verification: Add a short note to the PR description capturing AOT analysis results and any linker config changes (if needed).

## Tracking directive: Create issues for all open P1/P2

For every item labeled P1 or P2 in this report that is not completed in PR #626:

- Create a separate GitHub issue in the repository with:
  - Title: "[P1|P2] <short action>: <area>"
  - Labels: `area/deploy` or `area/quota` (and others as appropriate), `priority/P1` or `priority/P2`, and `PR/626-followup`
  - Body: Problem statement, acceptance criteria, links to the exact files/lines and to `docs/PR-626-Action-Plan.md`, plus owner and due date.
- Cross-link the issue in PR #626 and check the corresponding box in the "Exhaustive merge-readiness checklist" when done.

Issue template snippet:

- Problem: <what and why>
- Scope: <commands/files>
- Acceptance Criteria: <bullets>
- Links: <file:line anchors> · PR #626 · docs/PR-626-Action-Plan.md
- Owner: <name> · Priority: P1|P2 · Labels: area/*, priority/*, PR/626-followup

## Compliance matrix vs Final Recommendations

- Command Groups (quota, deploy subgroups): Done.
- Command Structure Changes (verbs): Done; minor description text cleanup pending.
- Integration Strategy (reuse az/azd): Partially done; not yet wired to extension services.
- File/Folder Reorg: Done.
- Namespace Updates: Done.
- Project File Updates (embedded resources): Done. Extension project reference: Not added (consider per integration plan).
- Registration Updates: Done (areas registered in core setup).
- Template System: Implemented; continue consolidating prompts.
- Plan command scope: Current implementation returns a plan template; it no longer writes files directly. Aligned with guidance.

## Quick quality gates snapshot

- Build: PR pipeline shows failures on multiple x64 jobs (linux/osx/win). Needs investigation.
- Lint/Spelling: Spelling unchecked; run script and fix.
- Tests: New unit tests present; ensure they run in CI and are green after any fixes.
- Smoke/help: Verify `azmcp deploy --help` and `azmcp quota --help` show the expected hierarchy post-changes.

## Appendix: Suggested documentation deltas

- docs/azmcp-commands.md
  - Fix: “azmcp quota quota region availability list” → “azmcp quota region availability list”.
  - Add “Migration from legacy hyphenated names” table mapping plan-get → plan get, iac-rules-get → infrastructure rules get, etc.
- docs/new-command.md
  - Include example of hierarchical registration via CommandGroup and guidance on naming.

---

Completion summary
- The PR largely meets the architectural reorg, naming, and testability goals. The biggest remaining items are integration reuse (az/azd), small docs fixes, CHANGELOG/spelling, and CI stabilization. Addressing the P0/P1 items above should make this PR ready to merge.

## Exhaustive merge-readiness checklist

Note: Priority tags — [P0] must before merge, [P1] should before merge, [P2] nice-to-have.

### Command design and UX
- [ ] [P0] Verify command hierarchy and verbs match guidance exactly:
  - deploy app logs get
  - deploy infrastructure rules get
  - deploy pipeline guidance get
  - deploy plan get
  - deploy architecture diagram generate
  - quota usage check
  - quota region availability list
- [ ] [P0] Remove legacy/hyphenated names and outdated descriptions (e.g., DeploySetup group description).
  - Files: areas/deploy/src/AzureMcp.Deploy/DeploySetup.cs; areas/quota/src/AzureMcp.Quota/QuotaSetup.cs (verify help text)
- [ ] [P0] Ensure --help text is concise and consistent; clearly mark required vs optional options.
- [ ] [P1] Provide help examples for each command (typical + edge-case for raw-mcp-tool-input).
- [ ] [P1] Confirm output shapes and property casing are consistent and documented.

### Options and input validation
- [ ] [P0] Validate raw-mcp-tool-input JSON: required fields present; unknown fields behavior defined; helpful errors.
- [ ] [P0] Validate quota inputs: region/resource types normalized; reject empty/invalid sets; friendly messages.
- [ ] [P0] Validate logs query time windows and limits; set sane defaults and max bounds.
- [ ] [P1] Add JSON schema snippets for raw-mcp-tool-input in docs and link from --help.
- [ ] [P2] Robust list parsers (comma/space/newline with trimming) + tests.
  - Files: areas/deploy/src/AzureMcp.Deploy/Options/DeployOptionDefinitions.cs; areas/deploy/src/AzureMcp.Deploy/Options/App/LogsGetOptions.cs; areas/quota/src/AzureMcp.Quota/Commands/Usage/CheckCommand.cs; areas/quota/src/AzureMcp.Quota/Commands/Region/AvailabilityListCommand.cs

### Code structure, AOT, and serialization
- [ ] [P0] Use primary constructors where applicable in new classes.
- [ ] [P0] Use System.Text.Json only; no Newtonsoft.
- [ ] [P0] Ensure all DTOs are covered by source-gen contexts (DeployJsonContext, QuotaJsonContext) in all (de)serializations.
- [ ] [P0] Prefer static members where possible; avoid reflection/dynamic not safe for AOT.
- [ ] [P0] Run AOT/trimming analysis; address warnings (preserve attributes/linker config if needed).
- [ ] [P1] Add JSON round-trip tests proving source-gen coverage.
- [ ] [P2] Enforce culture-invariant formatting/parsing (dates, numbers, casing).
  - Files: areas/deploy/src/AzureMcp.Deploy/Commands/DeployJsonContext.cs; areas/quota/src/AzureMcp.Quota/Commands/QuotaJsonContext.cs

### Services and Azure SDK usage
- [ ] [P0] Use IHttpClientFactory and reuse Azure SDK clients appropriately; avoid per-call instantiation.
- [ ] [P0] Use Azure SDK default retries/timeouts; avoid custom retries unless justified.
- [ ] [P0] Respect cloud/authority from environment; support sovereign clouds.
- [ ] [P0] Use TokenCredential correctly; do not accept/store secrets directly.
- [ ] [P1] Abstract log retrieval behind an interface and prefer routing via extension services (IAzService/IAzdService) to reduce duplication.
- [ ] [P2] Keep diagnostics minimal, opt-in, and scrub PII.
  - Files: areas/deploy/src/AzureMcp.Deploy/Services/DeployService.cs; areas/deploy/src/AzureMcp.Deploy/Services/Util/AzdResourceLogService.cs

### Templates, resources, and I/O
- [ ] [P0] Ensure embedded templates/rules are included and load via correct manifest names.
- [ ] [P0] Validate template outputs are non-null and meaningful; handle missing resource errors.
- [ ] [P1] Tests confirming presence and expected content of embedded resources.
- [ ] [P2] Cache static templates in-memory (thread-safe) to reduce I/O.
  - Files: areas/deploy/src/AzureMcp.Deploy/Services/Templates/TemplateService.cs; areas/deploy/src/AzureMcp.Deploy/Commands/Infrastructure/RulesGetCommand.cs; areas/deploy/src/AzureMcp.Deploy/Commands/Plan/GetCommand.cs

### Security and robustness
- [ ] [P0] Bound and sanitize inputs for diagram generation; warn or fail cleanly if payload exceeds safe URL length.
- [ ] [P0] Encode all URLs; avoid external calls based on untrusted input (Azure SDK excepted).
- [ ] [P1] Review YamlDotNet usage; handle malformed YAML with clear errors.
- [ ] [P1] Plumb CancellationToken through long-running operations (logs queries).
- [ ] [P2] Consider allowlist constraints for resource types/locations if applicable.
  - Files: areas/deploy/src/AzureMcp.Deploy/Commands/Architecture/DiagramGenerateCommand.cs; areas/deploy/src/AzureMcp.Deploy/Services/Util/AzdResourceLogService.cs

### Testing
- [ ] [P0] Per-command tests (success + 1–2 error cases):
  - deploy/app/logs/get (invalid YAML, empty resources, query timeout)
  - deploy/infrastructure/rules/get (resource presence)
  - deploy/plan/get (template present)
  - deploy/architecture/diagram/generate (bad/large JSON, valid graph)
  - quota/usage/check (invalid/empty resource types, mixed casing)
  - quota/region/availability/list (filters, cognitive services variants)
- [ ] [P0] Tests to ensure JsonSerializerContext is used (no reflection fallback at runtime).
- [ ] [P0] Tests asserting output contracts (shape, casing, required properties).
- [ ] [P1] Integration tests with recorded fixtures/test proxy where feasible.
- [ ] [P1] Update E2E prompt tests with new commands and sample payloads.
- [ ] [P2] Concurrency and large-input perf smoke tests.
  - Files (tests to extend/verify): areas/deploy/tests/AzureMcp.Deploy.UnitTests/**; areas/quota/tests/AzureMcp.Quota.UnitTests/**; core/tests/AzureMcp.Tests/**

### Documentation
- [ ] [P0] Fix typos (e.g., docs/azmcp-commands.md “azmcp quota quota …” → “azmcp quota …”).
- [ ] [P0] Document each command: synopsis, options, example inputs/outputs, error cases, JSON schemas.
- [ ] [P0] Update CHANGELOG.md summarizing new areas/commands and notable behavior.
- [ ] [P1] Troubleshooting notes (auth issues, timeouts, payload too large for diagrams).
- [ ] [P2] Link to architecture decisions for diagram/templates.
  - Files: docs/azmcp-commands.md; CHANGELOG.md; docs/new-command.md; docs/PR-626-Code-Review.md (this document)

### Repo hygiene and engineering system
- [ ] [P0] One class/interface per file; remove dead code/unused usings; consistent naming.
- [ ] [P0] Ensure copyright headers; run header script.
- [ ] [P0] Run local verifications:
  - ./eng/scripts/Build-Local.ps1 -UsePaths -VerifyNpx
  - .\eng\common\spelling\Invoke-Cspell.ps1
- [ ] [P0] dotnet build of AzureMcp.sln passes cleanly; address warnings-as-errors.
- [ ] [P1] Run Analyze-AOT-Compact.ps1 and Analyze-Code.ps1; address issues.
- [ ] [P2] Verify package versions pinned in Directory.Packages.props match standards.
  - Files: Directory.Packages.props; eng/scripts/*.ps1; eng/common/spelling/*; solution-wide

### CI and cross-platform readiness
- [ ] [P0] Investigate and fix failing CI jobs (linux_x64, osx_x64, win_x64); reproduce locally as needed.
- [ ] [P0] Ensure tests are stable (not time/date/region dependent); deflake if needed.
- [ ] [P1] Validate trimming/AOT publish on CI; ensure any publish profiles succeed.
- [ ] [P2] Add light smoke validation for each new command in CI (mock/dry-run).
  - Files/areas: eng/pipelines/**; failing job logs in GitHub Actions; areas/deploy/**; areas/quota/**

### User experience polish
- [ ] [P0] Consistent exit codes (0 success, non-zero error) and documented.
- [ ] [P0] Clear error messages with next-step guidance.
- [ ] [P1] --help output tidy with copyable examples; consistent option naming.
- [ ] [P2] Optional --verbose honoring repo logging conventions.
  - Files: core/src/AzureMcp.Cli/** (command wiring/help); areas/*/*/Commands/** (messages)

### Ownership and maintainability
- [ ] [P0] Interface-first services (IDeployService, IQuotaService) with explicit DI lifetimes.
- [ ] [P1] Reusable parsing/validation helpers with unit tests.
- [ ] [P2] Lightweight README per area (deploy, quota) describing purpose and extension points.
  - Files: areas/deploy/src/AzureMcp.Deploy/Services/**; areas/quota/src/AzureMcp.Quota/**; core/src/** (DI registration)

### Quick P0 punch list
- [ ] Fix command descriptions/names to remove legacy terms.
- [ ] Tighten validation and error messages for raw-mcp-tool-input and quota inputs.
- [ ] Ensure STJ source-gen contexts cover all (de)serialized types; remove reflection paths.
- [ ] Add/complete unit tests per command and output contract tests.
- [ ] Update docs/azmcp-commands.md, add examples, and fix typos.
- [ ] Update CHANGELOG.md for new commands/features.
- [ ] Run Build-Local verification and CSpell; fix findings.
- [ ] Address CI failures across platforms until all green.
