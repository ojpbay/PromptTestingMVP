# Research: Prompt Testing MVP

## Decisions & Rationale

### Authentication Method
- **Decision**: Use OIDC (OpenID Connect) with organizational IdP (e.g., Azure AD) issuing JWT access tokens.
- **Rationale**: Standard enterprise practice, supports SSO & RBAC claims, simplifies frontend via implicit PKCE flow.
- **Alternatives Considered**: Custom JWT (rejected: security & maintenance burden), Session cookies (rejected: SPA + API cross-origin complexity).

### Invalid Scope Combinations
- **Decision**: Treat any combination as valid if all four fields selected unless backend rules table flags invalid pairings.
- **Rationale**: Simplifies UX; allows future business rule injection without frontend changes.
- **Alternatives**: Hard-coded validation matrix (rejected: brittle, high-change risk).

### Error Feedback Mechanism
- **Decision**: Standard toast + inline card message for test execution failures.
- **Rationale**: Immediate visibility + contextual persistence.
- **Alternatives**: Modal dialogs (rejected: interruptive), Silent logging only (rejected: poor UX).

### Document Repository Unavailability
- **Decision**: Surface user-facing retry suggestion + log error; mark test run as failed with reason.
- **Rationale**: Transparency and audit; avoids silent partial results.
- **Alternatives**: Automatic retries (rejected MVP: complexity), Queueing (rejected MVP scope).

### Prompt Editing Scope (FR-017)
- **Decision**: Editing of stored prompt definitions excluded; only ephemeral context modifications allowed per test session.
- **Rationale**: Avoids versioning and persistence complexity.
- **Alternatives**: Limited field updating (rejected: ambiguous persistence semantics).

### Test Execution Engine Interface
- **Decision**: Synchronous command dispatch creating TestRun row with status (Pending→Running→Completed/Failed). Worker logic abstracted behind MediatR handler (in-process for MVP).
- **Rationale**: Enables later externalization to background processor without contract change.
- **Alternatives**: External queue now (rejected: over-engineering for MVP).

### Results Persistence
- **Decision**: `TestRun` table with summary metrics; no historical detail beyond last accuracy stored per prompt + historical rows for audit.
- **Rationale**: Supports simple UI + foundation for trend later.
- **Alternatives**: Only last result (rejected: no audit), Full per-document metrics (rejected: out of scope).

### Testcontainers for SQL Server
- **Decision**: Use Testcontainers for integration tests if CI runner supports Linux containers w/ SQL Server image; fallback to LocalDB script if unsupported.
- **Rationale**: Environment parity + reproducibility.
- **Alternatives**: In-memory provider (rejected: diverges from production behavior), Shared dev DB (rejected: test isolation issues).

### Performance Considerations
- **Decision**: Batch invocation executes sequential document evaluations; progress not exposed in MVP; status returned immediately as Running then polled.
- **Rationale**: Simplicity; allows future streaming upgrade.
- **Alternatives**: WebSocket progress (rejected MVP), Parallel fan-out (rejected complexity vs scale need).

## Resolved Clarifications
All prior NEEDS CLARIFICATION markers addressed; authentication method chosen; error handling defined.

## Open Risks
- OIDC integration timeline (IdP readiness)
- Potential large document volume edge case (execution time) — mitigation: cap at internal threshold (env var) and log truncation event.

## Follow-Up Items
- Confirm CI runner supports Testcontainers + SQL Server
- Provision OIDC client + redirect URIs
- Establish scope validation rule source (DB table vs config)
