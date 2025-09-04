# Implementation Plan: Prompt Testing MVP

**Branch**: `001-the-prompt-testing` | **Date**: September 4, 2025 | **Spec**: `/home/oliba/PromptTestingMVP/specs/001-the-prompt-testing/spec.md`
**Input**: Feature specification from `/specs/001-the-prompt-testing/spec.md`

## Execution Flow (/plan command scope)
```
1. Load feature spec from Input path
   → If not found: ERROR "No feature spec at {path}"
2. Fill Technical Context (scan for NEEDS CLARIFICATION)
   → Detect Project Type from context (web=frontend+backend, mobile=app+api)
   → Set Structure Decision based on project type
3. Evaluate Constitution Check section below
   → If violations exist: Document in Complexity Tracking
   → If no justification possible: ERROR "Simplify approach first"
   → Update Progress Tracking: Initial Constitution Check
4. Execute Phase 0 → research.md
   → If NEEDS CLARIFICATION remain: ERROR "Resolve unknowns"
5. Execute Phase 1 → contracts, data-model.md, quickstart.md, agent-specific template file
6. Re-evaluate Constitution Check section
   → If new violations: Refactor design, return to Phase 1
   → Update Progress Tracking: Post-Design Constitution Check
7. Plan Phase 2 → Describe task generation approach (DO NOT create tasks.md)
8. STOP - Ready for /tasks command
```

**IMPORTANT**: The /plan command STOPS at step 7.

## Summary
Single-page style web feature enabling users to select a business scope (four fields), choose a prompt, temporarily edit its context, run a single batch test against all in-scope documents, and view basic accuracy metrics. MVP explicitly excludes prompt versioning, creation, detailed analytics, or document-level results. Core flow: Scope -> Prompt List -> Prompt Context/Edit -> Run Test -> View Results.

## Technical Context
**Language/Version**: Frontend: Angular 20 (standalone components, control flow syntax); Backend: .NET 9 (ASP.NET Core Web API)  
**Primary Dependencies**: Frontend: Angular Material, ag-grid, NgRx Signal Store; Backend: EF Core 9, MediatR, FluentValidation (proposed), Serilog (proposed), Swashbuckle (OpenAPI)  
**Storage**: SQL Server (Azure SQL or local) via EF Core  
**Testing**: Frontend: Jest + Testing Library; Backend: xUnit + FluentAssertions + WebApplicationFactory for integration  
**Target Platform**: Web (SPA) + REST API on Linux container  
**Project Type**: web (frontend + backend)  
**Performance Goals**: Page load <3s, prompt load <2s for ≤50 prompts, test execution status feedback <500ms from initiation  
**Constraints**: Single concurrent test per session; Minimal persistence (results + prompts); No prompt authoring; Authentication required (method unspecified)  
**Scale/Scope**: Initial user base small (internal teams); Data volume moderate (<10k documents per scope initially)  

NEEDS CLARIFICATION markers from spec (now resolved in research.md):
- Invalid scope combinations definition → dynamic backend rule table
- Error feedback mechanism for test execution failures → toast + inline card message
- Handling of document repository unavailability → failed TestRun w/ reason + retry guidance
- Whether prompt editing is fully excluded or limited (FR-017) → only ephemeral context allowed
- Authentication method → OIDC (Azure AD)

## Constitution Check (Initial & Post-Design)
**Simplicity**:
- Projects: 2 planned (frontend, backend) + tests within each (≤3 guideline OK)
- Framework usage direct: YES
- DTO minimization: Only boundary DTOs (justified)
- Avoided over-patterning: No repository abstraction

**Architecture**:
- Vertical slices + feature folders
- No CLI libraries (web justification)
- Docs generated (research, data-model, contracts, quickstart)

**Testing**:
- Strategy defined: contract → integration → component/unit
- Real DB via Testcontainers (fallback path defined)
- Failing contract tests to be created during implementation tasks (planned)

**Observability**:
- Structured logging plan (Serilog + correlation)
- Audit log via TestRun entries

**Versioning**:
- Starting at 0.1.0; build increments via CI

Status: PASS (no unjustified violations)

## Project Structure
```
backend/
  src/
    PromptTesting.Api/
    PromptTesting.Application/
    PromptTesting.Domain/
    PromptTesting.Infrastructure/
  tests/
    PromptTesting.ContractTests/
    PromptTesting.IntegrationTests/
    PromptTesting.UnitTests/
frontend/
  src/
    app/
      scope/
      prompts/
      context/
      testing/
      shared/
  tests/
```
**Structure Decision**: Option 2 (web application)

## Phase 0: Outline & Research
Research completed in `research.md` with decisions and alternatives.

## Phase 1: Design & Contracts
Generated artifacts:
- `data-model.md`
- `contracts/openapi.yaml`
- `quickstart.md`
(Contract tests deliberately deferred to task phase to ensure RED state first.)

## Phase 2: Task Planning Approach
Will map each contract and entity to tasks with TDD order: contract tests → handlers/endpoints → frontend services → components → integration tests → polishing. Estimate ~28 tasks. Parallelizable: independent frontend components after models & contracts.

## Complexity Tracking
| Violation | Why Needed | Simpler Alternative Rejected Because |
|-----------|------------|--------------------------------------|
| MediatR usage | Separation, pipeline behaviors | Controller logic less testable |
| DTO layer | Contract stability | Direct EF entities expose internals |

## Progress Tracking
**Phase Status**:
- [x] Phase 0: Research complete (/plan command)
- [x] Phase 1: Design complete (/plan command)
- [x] Phase 2: Task planning complete (/plan command - describe approach only)
- [ ] Phase 3: Tasks generated (/tasks command)
- [ ] Phase 4: Implementation complete
- [ ] Phase 5: Validation passed

**Gate Status**:
- [x] Initial Constitution Check: PASS
- [x] Post-Design Constitution Check: PASS
- [x] All NEEDS CLARIFICATION resolved
- [x] Complexity deviations documented

---
*Based on Constitution v2.1.1 - See `/memory/constitution.md`*
