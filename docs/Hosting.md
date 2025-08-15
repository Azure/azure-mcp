<!--
	Azure MCP Server Hosting Specification
	Status: Draft (Targeting Remote Preview)
-->

# Azure MCP Server Hosting Specification

## 0. Document Metadata
| Field | Value |
|-------|-------|
| Title | Azure MCP Server Hosting Specification |
| Version | 0.2-draft |
| Status | Draft (for internal review) |
| Owners | TBD |
| Last Updated | 2025-08-15 |
| Target Milestone | Remote Preview |

All normative requirements use RFC 2119 keywords (MUST, SHOULD, MAY, MUST NOT, SHOULD NOT).

## 1. Scope
This specification defines the architecture, deployment targets, authentication model, transport strategy, configuration surface, security requirements, and rollout plan for hosting the Azure MCP Server in remote (Azure) environments. It also defines a constrained unauthenticated demo mode and enumerates open issues gating general availability (GA). Non-Azure hosting guidance is out of primary scope and treated as informational.

## 2. Goals & Non‑Goals

### 2.1 Goals (Preview)
1. Provide a secure default Azure hosting path with minimal operational burden.
2. Support Managed Identity (MI) for server-to-Azure interactions.
3. Enable future per-user delegated access (OBO) without refactoring core tool implementations.
4. Gate experimental transports behind explicit feature flags with visible risk warnings.
5. Favor stateless horizontally scalable deployment patterns.
6. Define clear configuration and security baselines (logging, rate limiting, identity, network).
7. Provide a least-privilege role guidance matrix (placeholder in this version).

### 2.2 Non-Goals (Preview)
1. Multi-tenant managed SaaS platform.
2. Full parity across all Azure compute offerings.
3. Advanced stateful collaboration or session migration.
4. Deep non-Azure cloud hardening guidance.
5. Immediate support for dynamic client registration (blocked by current Microsoft Entra platform capabilities).

## 3. Definitions
| Term | Definition |
|------|------------|
| MCP | Model Context Protocol. |
| PRM Document | Initial protocol registration metadata from the server. |
| MI | Managed Identity (system or user-assigned) in Azure. |
| OBO | OAuth 2.0 On-Behalf-Of flow for delegated downstream API access. |
| Demo Mode | Unauthenticated, strictly static, read-only operational mode for UX validation. |
| Streamable HTTP | Emerging HTTP-based transport with chunked/streamed responses (experimental). |

## 4. Azure Hosting / Compute Options

| Option | Primary Use Case | Pros | Cons / Risks | Managed Identity | Scale Model | Startup Latency | Networking / Ingress | Cost Profile |
|--------|------------------|------|--------------|------------------|-------------|-----------------|----------------------|--------------|
| **Azure Container Apps (ACA)** | Default recommended preview target | Simple deployment, DAPR option, revisions/blue‑green, built-in autoscale (HTTP, KEDA), secrets, MI support | Cold start (smaller revisions), streaming limits to validate, per‑app concurrency tuning needed | Yes | Horizontal w/ KEDA | Low‑Med | Env, custom domains, internal ingress | Consumption-ish (pay per vCPU/sec) |
| **Azure App Service (Linux Container)** | Traditional web workloads, enterprise familiarity | Mature platform, built-in auth integration, easy diagnostics, slots | Less granular scaling vs ACA, container memory limits, streaming perf to test | Yes | Instance scale out | Med | Built-in WAF/AppGW integration | Steady cost per instance |
| **Azure Kubernetes Service (AKS)** | Advanced customers needing custom networking or sidecars | Full control, can co-host additional infra (Redis, vector DB) | Operational overhead, cluster cost baseline, upgrade mgmt | Yes (pod MI / workload identity) | HPA/KEDA | Depends on node pool | Full Kubernetes ingress stack | Higher; node baseline |
| **Azure Functions (Container / .NET isolated)** | Event-driven, sporadic usage | Elastic scale, native identity, binding ecosystem | Durable streaming session semantics harder, cold start, execution time limits, local dev divergence | Yes | Per-function elastic | Potential cold start | HTTP trigger + APIM optional | Consumption (burst friendly) |
| **Azure Container Instances (ACI)** | Ephemeral dev / test | Fast spin-up, no orchestration overhead | Not ideal for always-on, limited autoscale, ephemeral networking complexity | Yes | Manual / scripted | Low | Basic; private vNet optional | Per-second ephemeral |
| **Azure VM Scale Sets** | Edge cases needing custom OS / long-running state | Full control, custom images | Highest ops burden, patching, scaling logic custom | Yes | Custom | Depends on image | Fully custom (NSG, LB) | Pay for full VM uptime |
| **Azure Spring Apps / Others** | Niche, Java-centric | Platform features for JVM | Not aligned with core target languages initially | Yes | Platform scale | Med | Standard ingress | Specialized pricing |

### 4.1 Prioritization (Preview)
| Tier | Targets | Rationale |
|------|---------|-----------|
| 1 | Azure Container Apps (primary), Azure App Service | Low operational overhead, MI support, rapid rollout |
| 2 | AKS, Azure Functions (HTTP subset) | Advanced control (AKS) / burst (Functions); higher complexity |
| 3 | ACI (dev), VMSS, others | Specialized / legacy or higher ops burden |

The implementation MUST initially deliver Tier 1. Tier 2 MAY be documented. Tier 3 MUST NOT block preview.

## 5. Deployment Artifacts & Packaging
| Artifact | Purpose | Required for Preview? | Notes |
|----------|---------|-----------------------|-------|
| Container Image (`mcr.microsoft.com/azure-sdk/azure-mcp`) | Primary deployment unit | Yes | Must publish versioned & `:latest` tags. Consider slim image variant. |
| Bicep / azd template | Turnkey infra + app | Yes | Provide `infra/` example with ACA + Log Analytics + MI assignment. |
| Helm Chart (AKS) | Optional advanced path | No (Doc only) | Provide later; ensure OPA/gatekeeper disclaimers. |
| GitHub Actions & Azure DevOps snippets | CI/CD guidance | Yes | Show build, scan, push, deploy. |
| `az containerapp up` quickstart | Fast start | Yes | Lean path for POC. |

## 6. Authentication & Authorization Model

### 6.1 Server → Azure
Uses Azure Identity with Managed Identity when running inside Azure. Fallback to existing credential chain for local dev. The environment variable to enable production credentials (`AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS=true`) remains valid.

### 6.2 End User → Server (Remote Mode)
Authentication strategies under evaluation:
1. **Bearer token (User Delegation / OBO)**: User signs in locally (InteractiveBrowserCredential). Client sends an access token to server; server validates and performs On-Behalf-Of (OBO) to acquire downstream tokens. Pros: preserves user RBAC. Cons: complexity + token forwarding surface.
2. **Server-side MI only (Impersonation-lite)**: Server executes with MI; user requests are authorized at application layer (scopes / allowlists). Pros: simpler. Cons: loses per-user RBAC fidelity; potential over‑privilege risk if MI has broad roles.
3. **Hybrid (Preview)**: Default to server MI for read-only operations with limited scope + optional user-token mode behind feature flag for sensitive operations.

Preview Requirement: The implementation MUST start with MI-only mode. OBO MUST be pluggable without modifying tool business logic.

### 6.3 Token Exchange / Flow Considerations
| Concern | OBO Flow | MI-only Flow |
|---------|----------|--------------|
| Per-user auditing | Strong | Weak (aggregate) |
| Complexity | High | Low |
| Attack surface (token replay) | Higher (needs PoP or audience checks) | Lower |
| RBAC fidelity | Exact | Coarse |

### 6.4 Local Development Requirements
The local server MUST NOT expose unauthenticated public endpoints. The following controls are REQUIRED:
1. MUST bind to `127.0.0.1` (loopback) by default.
2. MUST require an explicit opt-in flag to listen on any non-loopback interface (e.g., `--listen-any`), and MUST display a critical warning when used.
3. Streamable HTTP transport: **NOT SUPPORTED** in local mode for the initial remote preview (hard disabled; no override flags or environment variables to re-enable).
4. Demo mode (if present) MUST refuse streamable HTTP (implicit by #3).
5. MUST emit a high-visibility banner (stderr) on startup summarizing: bind address, enabled transports, auth mode, demo mode status.
6. SHOULD periodically (e.g., every 15 minutes) log a security heartbeat summarizing the same (to aid forensic review).

### 6.5 Managed Identity Scope Strategy
* Principle of least privilege: Create MI at resource group scope where possible.
* Prefer data-plane read roles first (e.g., Storage Blob Data Reader) vs full contributor roles.
* Document escalation path if write capabilities required.

### 6.6 Future: Per-user Delegation
* Evaluate OAuth 2.0 OBO with confidential app (App Registration + audience validation).
* Consider Proof-of-Possession (TLS-bound tokens) if MCP spec adds channel binding.

### 6.7 Unauthenticated Read-Only Demo Mode
An initial remote-hosted experience MAY be shipped without user authentication for evaluation. This mode MUST be strictly constrained.

| Aspect | Draft Approach |
|--------|----------------|
| Purpose | Fast preview of remote transport + UX without completing auth/OBO design. |
| Enable Mechanism | Explicit flag `--enable-demo-mode` OR env `AZURE_MCP_ENABLE_DEMO_MODE=true`; NEVER default. |
| Tool Surface | Extremely limited, whitelisted safe operations returning static / synthetic data only. No subscription / tenant enumeration. |
| Data Sources | Prefer baked-in static JSON snapshots (e.g., example storage account schema, generic RBAC role list) to avoid hitting real Azure APIs without auth. |
| Branding | Responses clearly include `"mode": "demo"` field and warning banner on server start + first N responses. |
| Security Controls | Hard rate limit (e.g., 30 req/min/IP), no write/destructive commands, no network egress except telemetry (respect opt-out). |
| Telemetry | Emit `demo_mode_enabled` event (anonymized). Track tool usage to refine safe set. |
| Abuse Mitigation | Auto-shutdown after configurable idle period (e.g., 30 minutes). Optional CAPTCHA / signed nonce for public endpoints (defer). |
| Sunset Plan | Marked experimental; removed or replaced once authenticated remote mode reaches stability. |
| Documentation | Prominent disclaimer: "Demo mode does NOT access your Azure resources; outputs are illustrative only." |

"Safe" candidate commands (illustrative; subject to review):
* Bicep schema lookup (served from embedded manifest, not live ARM call).
* Best practices guidance (static text file resource already present).
* Tool listing / capabilities (subset) for UI validation.

NOT allowed in demo mode (initial):
* Any command requiring subscription, resource group, or tenant parameters.
* Commands with network calls to Azure management/data plane.
* Destructive or mutation operations.

Implementation Sketch:
1. Add mode detector early in startup; when active, replace tool registry with curated static tool set.
2. Inject a response decorator adding demo warning metadata.
3. Block any attempt to pass subscription/resource parameters (validation layer).
4. Enforce rate limiting middleware (in-process token bucket) scoped to remote transport only.
5. Auto-disable if an auth credential becomes available (optional; could prompt to restart in full mode).

Risk Considerations:
* Users may believe they are interacting with real Azure resources. → Mitigation: explicit labeling, banner, distinct tool names (e.g., `demo-storage-example-list`).
* Attackers could scrape static data for amplification. → Mitigation: minimal static set, rate limiting.
* Accidental production enablement. → Mitigation: require BOTH flag and env var (defense in depth) OR build-time compile symbol for distribution images.

### 6.8 Ideal Entra ID (Authorization Code + PKCE + OBO) Flow & Gaps
Target future architecture for per-user delegated access:

**Target Sequence**
1. MCP server advertises (in its Protocol Registration / PRM document) that it uses Microsoft Entra ID for auth (includes issuer / audience metadata pointers or discovery hints).
2. Client connects to MCP server and retrieves the PRM document.
3. Client discovers Entra as the Authorization Server (AS) from PRM metadata.
4. Client constructs Entra OpenID Connect / OAuth2 metadata URLs (well-known discovery) and fetches endpoints.
5. Client registers with the Authorization Server (dynamic client registration) OR already possesses a suitable client ID.
6. Client initiates Authorization Code Flow with PKCE directly against Entra (user authenticates + consents).
7. Entra issues an access token whose `aud` (audience) is the MCP server (plus optional ID token for client state).
8. Client sends that access token to the MCP server on each request (e.g., Bearer token header / agreed channel field).
9. MCP server validates token (issuer, signature, expiry, audience, scopes / roles, nonce / c_hash as needed).
10. MCP server performs OAuth 2.0 On-Behalf-Of (OBO) flow to exchange the user token for downstream resource tokens (ARM, Storage, etc.).
11. MCP server maps/associates downstream tokens to the inbound user principal (cache keyed by user object ID + scopes) and executes tool operations.
12. Responses returned; auditing & telemetry capture per-user context.

**Current Blocking Gap**: No dynamic client registration for arbitrary third-party native MCP clients.

**Interim Alternatives**
| Approach | Description | Pros | Cons / Risks |
|----------|-------------|------|--------------|
| Pre-registered multi-tenant Public Client | Publish a client ID (no secret) that all MCP clients can use for auth code + PKCE | Simple for ecosystem, no secret distribution | Hard to apply client-specific policies, revocation affects all, potential misuse rate limits |
| Per-vendor Client IDs (manual registration) | Each MCP client vendor maintains an Entra app registration | Stronger isolation/accountability | Friction for new clients; coordination overhead |
| Device Code Flow (interactive fallback) | Clients invoke device code to let user authenticate in browser | Works without embedded browser, simpler UX for CLIs | Less seamless, slower, not ideal for high-frequency re-auth scenarios |
| Managed Identity Only (current preview baseline) | No per-user token; server uses MI for downstream calls | Simplifies backend, no OBO complexity | Loses per-user RBAC & auditing granularity; potential over-privileged MI |
| Brokered Auth (VS Code / WAM) | Leverage existing signed-in account in host environment | Smooth UX for first-party tools | Not portable to arbitrary MCP clients |

Per-user OBO is required for precise RBAC enforcement and auditability. MI-only mode aggregates actions under a single principal.

**Interim Mitigations**
1. Document MI-only mode as explicitly *coarse-grained*; urge least-privilege role assignment & regular access review.
2. Provide configuration switch to **require** per-user token once feature is available (fail fast if absent).
3. Abstract auth provider layer so OBO insertion later does not refactor tool implementations.
4. Normalize internal "principal context" object now (fields: auth_mode, user_oid?, upn?, token_scopes?, tenant_id) so logs & telemetry are forward-compatible.
5. Prepare security review for pre-registered public client fallback (threat: token phishing, misuse in other contexts) – consider audience restrictions & incremental consent scopes.

**Design Considerations (Future OBO)**
* Token Validation: Cache JWKS, enforce clock skew tolerance, validate `tid`, `iss`, `aud`, `exp`, `nbf`, `scp`/`roles`.
* OBO Caching: Key by (user_oid, tenant_id, resource, scope set) with TTL shorter than underlying token expiry; handle `invalid_grant` and `interaction_required` retries gracefully.
* Downstream Scope Minimization: Map tool → required resource scopes; request minimal union per operation.
* Privacy: Avoid persisting raw tokens; store only hashes / expiry for correlation; support explicit purge.
* Revocation Handling: Consider short token lifetimes + proactive refresh vs heavy introspection (Entra introspection limited).

**Risks**
| Risk | Impact | Mitigation Candidate |
|------|--------|----------------------|
| Public client ID abuse | Rate limiting / reputational blocking | Publish usage guidance + telemetry anomaly detection |
| Token replay (no PoP) | Unauthorized downstream calls if stolen | Evaluate channel binding when MCP spec supports PoP; enforce TLS; minimize token lifetime |
| Scope creep in MI fallback | Privilege escalation | Ship RBAC least-privilege templates; warn if broad roles detected |
| Mixed-mode complexity | Maintenance overhead | Clear capability negotiation; versioned auth block in PRM |

**Short-Term Directive**: Proceed with MI-only plus optional demo mode. OBO activation is contingent on (a) dynamic registration support OR (b) governance acceptance of a public multi-tenant client model.

## 7. Protocol & Transport Strategy

### 7.1 Current Protocols
* MCP standard request/response over stdio / local transports.
* Streamable HTTP (emerging) – flagged as experimental; security review pending.

### 7.2 Streamable HTTP Policy (Preview)
| Aspect | Policy |
|--------|--------|
| Default State | Disabled |
| Enable Mechanism | Explicit CLI flag (`--enable-stream-http`) OR env var (`AZURE_MCP_ENABLE_STREAM_HTTP=true`) |
| Azure Hosted Detection | If in Azure + flag set ⇒ allow with warning banner first run |
| Local Environment | Not supported (feature hard disabled). Any enable attempt MUST return an error. |
| Telemetry | Emit anonymized enable event (opt-out respects global telemetry setting) |

### 7.3 Alternative / Future Transports (Tracked)
* WebSocket (full-duplex, firewall-friendly) – potential for richer streaming.
* gRPC / gRPC-Web – performance & schema evolution advantages; requires client ecosystem support.
* QUIC-based (HTTP/3) – long-term possibility; deprioritized until MCP spec guidance emerges.

Directive: Transport abstraction MUST allow future addition without modifying tool handlers.

## 8. Stateless vs Stateful Architecture
| Dimension | Stateless Approach (Preferred) | Stateful Considerations |
|-----------|-------------------------------|-------------------------|
| Horizontal Scale | Simple; any instance handles any request | Requires affinity / session store |
| Caching | External (Redis, App Cache) optional | In-memory complexity |
| Streaming Sessions | Map session ID to in-memory stream; if lost, client reconnects | Persistent multiplexed streams |
| Failure Recovery | Retry to any instance | Complex migration |

Requirement: Implementation MUST remain stateless for preview (no server-side sticky sessions). Streaming sessions MUST be recoverable by re-issue from client.

## 9. Networking & Security
| Area | Considerations (Preview) | Future |
|------|--------------------------|--------|
| Ingress | ACA/App Service managed ingress; HTTPS only; enforce TLS 1.2+ | Custom domain w/ cert automation |
| Egress | Restrict with network rules; document required endpoints (ARM, Entra) | Private Link patterns |
| Private Networking | Offer vNet integration example (ACA) | Full private endpoint matrix |
| Rate Limiting | Basic per-IP / per-user logical throttles | Adaptive, dynamic quota system |
| Secrets | Azure Key Vault integration or ACA secrets | Key rotation automation |
| Observability | App Insights / OpenTelemetry exporter | Sampling policies, trace correlation with client session IDs |
| Supply Chain | Image signing (Notation / cosign), vulnerability scanning in CI | Continuous SBOM diff alerts |

## 10. Logging, Telemetry & Monitoring
Minimal preview baseline:
* Structured JSON logs (level, correlation_id, request_id, tool_name, latency_ms, auth_mode).
* Emit security-relevant events (auth failure, forbidden tool access, transport enable).
* Optional integration: Application Insights (centralized queries), Log Analytics workspace.
* Latency SLO: Document target P50 (<500ms) and P95 (<2s) for standard tool operations (subject to service latency).

## 11. Configuration Matrix (Initial)
| Variable | Purpose | Default | Scope |
|----------|---------|---------|-------|
| `AZURE_MCP_ENABLE_STREAM_HTTP` | Enables experimental streamable HTTP transport | `false` | Transport |
| `AZURE_MCP_STREAM_HTTP_UNSAFE_CONFIRM` | Suppresses interactive risk prompt | `false` | Transport |
| `AZURE_MCP_INCLUDE_PRODUCTION_CREDENTIALS` | Enables managed/workload identity creds | `false` | Auth |
| `AZURE_MCP_ONLY_USE_BROKER_CREDENTIAL` | Force broker interactive flow | `false` | Auth |
| `AZURE_MCP_COLLECT_TELEMETRY` | Telemetry opt-in/out | `true` | Telemetry |
| (Proposed) `AZURE_MCP_BIND_ADDRESS` | Bind address for server | `127.0.0.1` | Network |
| (Proposed) `AZURE_MCP_ALLOWED_ORIGINS` | CORS / origin allowlist (if browser clients later) | Empty (deny all) | Security |
| (Proposed) `AZURE_MCP_ENABLE_DEMO_MODE` | Enables unauthenticated static demo mode (see 4.7) | `false` | Mode |

## 12. Local vs Cloud Behavior
| Behavior | Local (Default) | Azure Hosted |
|----------|-----------------|--------------|
| Bind Address | `localhost` only | Public/ingress endpoint |
| Managed Identity | Not used (absent) | Used if available and enabled |
| Streaming Transport | Disabled; explicit double confirmation | Disabled by default; single flag to enable |
| Token Mode | Developer credential chain | Managed Identity primary |

## 13. Non-Azure Hosting (Informational)
* Supported but not optimized in this phase.
* Use service principal or environment credential chain.
* Risks: lack of MI, secret management responsibility shifts to operator, inconsistent network hardening.

## 14. Security & Threat Model (Preview Focus)
| Threat | Mitigation (Preview) | Follow-up |
|--------|----------------------|-----------|
| Unauthorized public access | Default localhost binding; explicit flag for public | mTLS / JWT enforcement |
| Token replay (if OBO later) | Defer OBO; scoped MI only for now | Audience validation + PoP tokens |
| Over-privileged MI | Document least privilege role set | Automated role analyzer |
| Transport downgrade attack | Only allow TLS ingress; explicit version logging | Security headers / compliance scans |
| Supply chain image tampering | CI scanning, digest pin examples | Image signing enforcement |
| Secret leakage in logs | Structured logging w/ value scrubbing | Static analyzers + runtime detectors |

## 15. Preview Rollout Plan
1. Author ACA + App Service quickstarts.
2. Release Bicep + azd template with MI and logging.
3. Add feature flags + warning banners for streamable HTTP.
4. Add structured telemetry events.
5. Document least privilege RBAC bundles for common scenarios.
6. Collect early adopter feedback; iterate.

## 16. Open Questions / Decisions Needed
| Area | Question | Status |
|------|----------|--------|
| OBO Flow | Need per-user RBAC day 1? | Pending (leaning NO) |
| Transport | Should we allow WebSocket early? | Investigate feasibility |
| ACA vs App Service | Publish both or only ACA in first article? | Decide |
| AKS Chart | Include in preview? | Likely defer |
| Rate Limiting | Implement simple in-process throttle? | TBD |
| Logging Correlation | Standard header or custom field for client correlation? | TBD |
| Demo Mode | Does unauthenticated static demo mode justify engineering & security review cost? | TBD |
| Demo Mode Scope | Which (if any) commands safe enough to expose statically? | TBD |
| OBO Client Registration | Public multi-tenant client ID vs per-vendor registrations vs wait for dynamic registration? | TBD |
| OBO Enable Criteria | What minimum safeguards (logging, rate limiting, token caching policy) gate turning OBO on? | TBD |

## 17. Future Enhancements (Backlog)
* OBO / delegated identity mode.
* Policy-driven tool exposure per-user / per-group.
* WebSocket transport implementation (if/when MCP spec endorses alternative streaming transport).
* Multi-region failover template (Front Door / Traffic Manager).
* Automated compliance baseline (Defender for Cloud integration).
* Pluggable authorization evaluators (custom policies).
* Enterprise secret rotation automation.

## 18. Potential Supplemental Appendices (Optional)
1. Architecture diagrams (logical + network flows).
2. Capacity planning worksheet.
3. Cost estimation scenarios (ACA vs App Service vs AKS).
4. Disaster recovery & resilience strategy (RPO/RTO baseline targets).
5. Observability playbook (common queries & dashboards).
6. Incident response runbook (e.g., transport vulnerability mitigation steps).
7. Governance & compliance mapping (SDL checkpoints, data handling statement).
8. Performance test plan (baseline workloads, thresholds, methodology).
9. Data classification & PII logging policy.
10. Change management procedure for enabling new auth/transport features.

## 19. Actionable Tasks
| # | Task | Priority | Owner (TBD) |
|---|------|----------|-------------|
| 1 | Finalize compute prioritization & publish decision record | High | |
| 2 | Implement feature flags + warnings for streamable HTTP | High | |
| 3 | Produce ACA Bicep template + azd example | High | |
| 4 | Draft least-privilege RBAC role matrix | High | |
| 5 | Add structured logging schema & docs | Medium | |
| 6 | Add local binding safety checks | Medium | |
| 7 | Write App Service deployment guide | Medium | |
| 8 | Investigate WebSocket feasibility (spike) | Low | |
| 9 | Threat model review w/ security team | High | |
| 10 | Decide OBO timeline & prerequisites | Medium | |
| 11 | Evaluate and decide on unauthenticated demo mode viability | High (timeboxed) | |
| 12 | If approved: implement demo mode tool whitelist & static data layer | Medium | |
| 13 | Add rate limiting middleware (covers demo + future transports) | Medium | |
| 14 | Author user-facing docs & warnings for demo mode | Medium | |

## 20. Unauthenticated-Safe Command Surface (Initial Assessment)
This section enumerates commands (tools) assessed as not requiring access to tenant- or subscription-scoped Azure resources and therefore candidates for exposure without authentication in either Demo Mode or a future constrained anonymous mode. Inclusion here DOES NOT automatically grant exposure; each item MUST pass final security review.

### 20.1 Criteria
Commands MAY be considered unauthenticated-safe if ALL of the following are true:
1. Do not accept subscription, tenant, resource group, or resource identifiers as required parameters.
2. Operate exclusively on static, embedded, or pre-fetched public reference data bundled with the server (no live Azure control/data plane calls).
3. Produce output free of secrets, tokens, or environment-specific identifiers.
4. Are read-only and non-mutative.
5. Cannot be combined with other unauthenticated commands to infer environment topology.

### 20.2 Candidate Command Groups
| Area | Example Tool (Namespace Mode) | Notes |
|------|-------------------------------|-------|
| Best Practices (general / azurefunctions / static-web-app) | `bestpractices-get` | Static curated guidance text. |
| Terraform Best Practices | `azureterraformbestpractices-get` | Static guidance file. |
| Bicep Schema | `bicepschema-get` (subset) | MUST serve only bundled schemas; NO live ARM discovery when unauthenticated. |
| Azure Foundry (Model Catalog Listing) | `foundry-models-list`, `foundry-models-deployments-list` (catalog listing ONLY) | Listing public model catalog metadata deemed non-sensitive (see 20.3). Deploy operations EXCLUDED. |
| Tool Discovery | `tools-list` (constrained) | Filtered to only show safe subset; hides resource-centric tools. |
| Static Version / Capability Info | (Proposed) `server-info` | Returns build/version, feature flags (non-sensitive). |

### 20.3 Azure Foundry Catalog Listing
Foundry model and catalog listing endpoints MAY be exposed without authentication provided:
1. Only public/global catalog entries (no tenant-scoped custom models) are returned.
2. No subscription, deployment identifiers, or tenant IDs are included.
3. Response is rate limited and size-limited (e.g., top N models) to mitigate enumeration abuse.
4. Output includes a disclaimer: "No authenticated Azure context; data may be partial or cached." (spec language only; implementation detail separate).

### 20.4 Exclusions (Not Safe Without Auth)
| Category | Reason |
|----------|--------|
| Any command accepting `--subscription`, `--resource-group`, or resource IDs | Potential enumeration / access scope inference |
| Storage / Data plane operations | Risk of surfacing tenant data paths even with read-only meta queries |
| Key Vault, Secrets, Certificates, Keys | High sensitivity |
| Service Bus, Cosmos DB, PostgreSQL, SQL, Redis | Potential metadata leakage |
| Workbooks, Monitor (Logs/Metrics), Load Testing | Environment topology + performance characteristics |
| Deployment / Create / Update / Delete commands | Mutative operations |

### 20.5 Governance & Review
1. Security review MUST sign off each candidate before activation in Demo Mode.
2. A regression test suite SHOULD assert that unauthenticated activation of any excluded tool returns an authorization error.
3. Telemetry SHOULD capture tool invocation counts to reassess risk and utility.
4. A documented process MUST exist to remove a tool from the safe list if post-release risk emerges.

### 20.6 Configuration Interaction
* Demo Mode MUST automatically restrict tool registry to the enumerated safe set.
* Attempting to invoke a non-safe tool while unauthenticated MUST yield a standardized error (e.g., `{"error":"auth_required"}`) without disclosing whether the tool exists outside safe mode.
* Safe tools MUST tag responses with a context attribute (e.g., `"authMode":"none"`).

### 20.7 Future Expansion Guardrails
Any proposal to add more unauthenticated tools MUST provide: threat analysis, data classification, abuse scenarios, rate limit plan, rollback plan.

### 20.8 Open Questions (Linked to Section 16)
| Question | Link |
|----------|------|
| Should Foundry deployment listing remain excluded despite being non-mutative? | Section 16 (Demo Mode Scope) |
| Do we need a cryptographic integrity stamp for static guidance assets? | Section 16 (Rate Limiting / Security) |
| Should we implement per-IP soft quotas distinct from global throttles for public tools? | Section 16 (Rate Limiting) |

END OF SPEC
