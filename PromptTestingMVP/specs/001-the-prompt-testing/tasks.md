# Tasks: Prompt Testing MVP

**Input**: Design documents from `/specs/001-the-prompt-testing/`
**Prerequisites**: plan.md (required), research.md, data-model.md, contracts/

## Execution Flow (main)
```
1. Loaded plan.md, data-model.md, contracts/openapi.yaml, research.md, quickstart.md
2. Extracted entities (ScopeSelection, Prompt, TestRun, TestResultMetric (future stub))
3. Extracted endpoints (/prompts, /prompts/{id}/context, /prompts/{id}/test, /tests/{id}/results, /scope/validate)
4. Generated setup, test-first, core, integration, polish tasks
5. Ensured tests precede implementation & parallel markers only on independent files
6. Built dependency ordering and parallel execution examples
7. Validated completeness (all endpoints & entities represented)
```

## Phase 3.1: Setup
- [ ] T001 Create backend solution & projects (Api, Domain, Application, Infrastructure) at `backend/src/` with .NET 9 solution file `backend/PromptTesting.sln`.
- [ ] T002 Add project references per plan (Api→Application→Domain; Api→Infrastructure→Domain) and add NuGet packages (MediatR, FluentValidation, EF Core SQL Server & Design, Serilog.AspNetCore, Swashbuckle) editing corresponding `.csproj` files.
- [ ] T003 Initialize frontend Angular 20 app at `frontend/prompt-testing/` with standalone components, routing, SCSS, install Angular Material, ag-grid, NgRx signals, Jest + Testing Library config.
- [ ] T004 Setup root tooling: `.editorconfig`, Prettier (frontend), basic `.gitignore`, add placeholder README.
- [ ] T005 Configure OpenAPI generation in `PromptTesting.Api` (Swashbuckle) and enable Serilog structured logging with correlation ID middleware skeleton.
- [ ] T006 Add solution-wide test projects: `backend/tests/PromptTesting.ContractTests`, `backend/tests/PromptTesting.IntegrationTests`, `backend/tests/PromptTesting.UnitTests` referencing necessary projects.
- [ ] T007 [P] Add Testcontainers dependency (SQL Server) to IntegrationTests project and base fixture class.
- [ ] T008 Configure Jest in frontend: `jest.config.js`, setup file for Angular, add `test:watch` script.

## Phase 3.2: Tests First (TDD) ⚠️ MUST FAIL INITIALLY
Contract Tests (one per endpoint) – backend/tests/PromptTesting.ContractTests
- [ ] T009 [P] Contract test GET /prompts validates 200 and JSON array matches Prompt schema in `.../ContractTests/Prompts/GetPromptsContractTests.cs`
- [ ] T010 [P] Contract test GET /prompts/{id}/context validates 200 + schema & 404 missing in `.../ContractTests/Prompts/GetPromptContextContractTests.cs`
- [ ] T011 [P] Contract test POST /prompts/{id}/test returns 202 with testId & handles 409 in `.../ContractTests/Prompts/PostPromptTestContractTests.cs`
- [ ] T012 [P] Contract test GET /tests/{id}/results returns 200 & schema or 404 in `.../ContractTests/Tests/GetTestResultsContractTests.cs`
- [ ] T013 [P] Contract test POST /scope/validate returns 200 valid combo or 422 invalid in `.../ContractTests/Scope/PostScopeValidateContractTests.cs`

Integration Tests (user stories) – backend/tests/PromptTesting.IntegrationTests
- [ ] T014 [P] Integration: Full user flow scope→list prompts in `.../IntegrationTests/Flows/ScopeToPromptListFlowTests.cs`
- [ ] T015 [P] Integration: Prompt select → context retrieval in `.../IntegrationTests/Flows/PromptContextFlowTests.cs`
- [ ] T016 [P] Integration: Execute test run and poll result lifecycle in `.../IntegrationTests/Flows/TestRunLifecycleTests.cs`
- [ ] T017 [P] Integration: Prevent concurrent test run same prompt/user in `.../IntegrationTests/Flows/ConcurrencyPreventionTests.cs`
- [ ] T018 [P] Integration: Failure path document repo unavailable marks failed run in `.../IntegrationTests/Flows/FailurePathTests.cs`

Frontend Component & Store Tests (initial failing specs) – frontend/prompt-testing/tests
- [ ] T019 [P] Scope selector component spec ensuring disables apply until all fields set in `frontend/prompt-testing/tests/scope/scope-selector.component.spec.ts`
- [ ] T020 [P] Prompt list component spec verifying columns & status badges in `frontend/prompt-testing/tests/prompts/prompt-list.component.spec.ts`
- [ ] T021 [P] Context editor component spec ensuring run disabled when empty in `frontend/prompt-testing/tests/context/context-editor.component.spec.ts`
- [ ] T022 [P] Test execution panel spec verifying status transitions in `frontend/prompt-testing/tests/testing/test-execution.component.spec.ts`
- [ ] T023 [P] Results card component spec verifying accuracy & last run display in `frontend/prompt-testing/tests/testing/results-card.component.spec.ts`

## Phase 3.3: Core Implementation (ONLY after above tests are red)
Backend Domain & Application
- [ ] T024 [P] Create Domain entities (Prompt, TestRun, TestResultMetric) in `backend/src/PromptTesting.Domain/Entities/` with invariants.
- [ ] T025 [P] Create Value Objects / enums (PromptStatus, TestRunStatus) in `backend/src/PromptTesting.Domain/ValueObjects/`.
- [ ] T026 [P] DbContext + configurations (Prompt, TestRun) in `backend/src/PromptTesting.Infrastructure/Persistence/` with indices.
- [ ] T027 MediatR queries: GetPromptsQuery, GetPromptContextQuery in `...Application/Prompts/Queries/`.
- [ ] T028 MediatR commands: ExecutePromptTestCommand, ValidateScopeCommand in `...Application/Prompts/Commands/` & `...Application/Scope/Commands/`.
- [ ] T029 Concurrency guard logic (single running test) in ExecutePromptTestCommand handler.
- [ ] T030 Update Prompt on TestRun completion (accuracy, lastRunAt) within command handler transaction.
- [ ] T031 Validation (FluentValidation) for commands & queries in `...Application/Validation/`.

Backend API Endpoints
- [ ] T032 Implement GET /prompts in `PromptTesting.Api/Endpoints/Prompts/GetPromptsEndpoint.cs`.
- [ ] T033 Implement GET /prompts/{id}/context in `PromptTesting.Api/Endpoints/Prompts/GetPromptContextEndpoint.cs`.
- [ ] T034 Implement POST /prompts/{id}/test in `PromptTesting.Api/Endpoints/Prompts/PostPromptTestEndpoint.cs`.
- [ ] T035 Implement GET /tests/{id}/results in `PromptTesting.Api/Endpoints/Tests/GetTestResultsEndpoint.cs`.
- [ ] T036 Implement POST /scope/validate in `PromptTesting.Api/Endpoints/Scope/ValidateScopeEndpoint.cs`.
- [ ] T037 Wire Serilog + correlation ID middleware and minimal audit logging of TestRun events in `Program.cs`.

Frontend State & Services
- [ ] T038 API models & types in `frontend/prompt-testing/src/app/shared/models/`.
- [ ] T039 HTTP data service for prompts & tests in `frontend/prompt-testing/src/app/shared/services/prompt-testing-api.service.ts`.
- [ ] T040 NgRx signal stores: scopeStore, promptsStore, testRunStore in `frontend/prompt-testing/src/app/...` feature folders.
- [ ] T041 Routing & top-level layout shell with left scope column in `frontend/prompt-testing/src/app/app.routes.ts` and layout component.

Frontend Components
- [ ] T042 Scope selector standalone component in `scope/` folder with Angular Material selects.
- [ ] T043 Prompt list component using ag-grid with status cell renderer in `prompts/`.
- [ ] T044 Context editor component with textarea + validation in `context/`.
- [ ] T045 Test execution panel component with run button + status states in `testing/`.
- [ ] T046 Results card component with accuracy display & color coding in `testing/`.

## Phase 3.4: Integration
- [ ] T047 EF Core migrations initial create + Prompt seed data in `Infrastructure/Migrations/`.
- [ ] T048 Implement Testcontainers SQL Server setup & use in integration tests fixture.
- [ ] T049 OIDC authentication & JWT validation middleware configuration in `PromptTesting.Api`.
- [ ] T050 Scope validation rule source table + seeding + handler logic reuse.
- [ ] T051 Error handling middleware (problem details) & consistent error responses.
- [ ] T052 Frontend OIDC auth integration (silent login, token injection) service in `shared/services/auth.service.ts`.
- [ ] T053 Frontend toast notification service + material snackbar integration.
- [ ] T054 Polling logic for test run status in testRunStore with exponential backoff.

## Phase 3.5: Polish
- [ ] T055 [P] Backend unit tests for domain invariants & validation in `UnitTests/Domain/`.
- [ ] T056 [P] Backend unit tests for handlers edge cases in `UnitTests/Application/`.
- [ ] T057 [P] Frontend unit tests for stores (signals) in `frontend/prompt-testing/tests/stores/`.
- [ ] T058 Accessibility review & ARIA roles for interactive components.
- [ ] T059 Performance smoke test measuring prompt list load (<2s) script in `backend/tests/PromptTesting.IntegrationTests/Performance/`.
- [ ] T060 [P] Update `quickstart.md` with any new auth/env steps.
- [ ] T061 [P] Update OpenAPI doc & regenerate client types if drift.
- [ ] T062 Dependency audit & license check script.
- [ ] T063 Final documentation pass: root README + architecture notes.

## Dependencies
- T001→T002→T005 (logging needs project) → T006
- T003 independent after T001
- Contract tests (T009–T013) require T001–T006 setup
- Integration tests (T014–T018) require DB fixture (T007) & base solution
- Frontend tests (T019–T023) require T003
- Domain/entities (T024–T026) before MediatR handlers (T027–T030, T031)
- Handlers before endpoints (T032–T036)
- Endpoints & handlers before migrations seeding (T047) not strictly, but recommended
- Auth (T049) before protected endpoint tests pass fully
- Stores (T040) before components using them (T042–T046)

## Parallel Execution Examples
```
# Example batch 1 (after setup):
T009 T010 T011 T012 T013 (contract tests in parallel)

# Example batch 2 (frontend tests):
T019 T020 T021 T022 T023

# Example batch 3 (domain modeling):
T024 T025 T026

# Example batch 4 (polish stage):
T055 T056 T057 T060 T061
```

## Validation Checklist
- [x] All contracts have contract test tasks (T009–T013)
- [x] All entities have model tasks (Prompt, TestRun, TestResultMetric stub, Scope selection implicit via fields) (T024–T026)
- [x] Tests precede implementation (Phases 3.2 before 3.3)
- [x] Parallel tasks only touch separate files
- [x] Explicit file paths provided or folder targets clear
- [x] User story integration flows covered (T014–T018)

SUCCESS: Tasks ready for execution.
