# Data Model: Prompt Testing MVP

## Entities

### ScopeSelection
Represents a user's active scope context for a session.
- id (GUID)
- team (string)
- brokingSegment (string)
- globalLineOfBusiness (string)
- product (string)
- createdAt (datetime)
- userId (string)

### Prompt
Data extraction prompt definition (read-only in MVP).
- id (GUID)
- name (string)
- version (string)
- dataPoint (string)
- status (enum: Active, Draft, Archived)
- baseContext (text)
- lastAccuracy (decimal 0-100, nullable)
- lastRunAt (datetime, nullable)

### PromptContext (Ephemeral)
Not persisted; represents user-edited context variant used for a single test run.
- promptId (GUID)
- modifiedContent (text)

### TestRun
Represents a single execution attempt of a prompt against scope documents.
- id (GUID)
- promptId (GUID, FK Prompt)
- scopeTeam (string)
- scopeBrokingSegment (string)
- scopeGlobalLineOfBusiness (string)
- scopeProduct (string)
- status (enum: Pending, Running, Completed, Failed)
- accuracy (decimal 0-100, nullable until completion)
- startedAt (datetime)
- completedAt (datetime, nullable)
- failureReason (string, nullable)
- userId (string)
- contextSnapshot (text) // ephemeral context used

### TestResultMetric (Optional future, stub for extension)
Simple extension point for granular metrics (not exposed in MVP UI).
- id (GUID)
- testRunId (GUID, FK TestRun)
- metricName (string)
- metricValue (decimal/string)

## Relationships
- Prompt 1..* TestRun
- TestRun 1..* TestResultMetric (future usage)

## Derived / Business Rules
- Only one Running TestRun per (userId, promptId) concurrently
- lastAccuracy & lastRunAt on Prompt updated transactionally upon TestRun completion (Completed status)
- Accuracy must be between 0 and 100 inclusive

## Validation Rules
- Scope fields required for TestRun creation
- ContextSnapshot required and non-empty for TestRun creation
- Status transitions allowed:
  - Pending → Running
  - Running → Completed | Failed
  - Completed/Failed → (terminal)

## Indexing
- Prompt: (status), (dataPoint)
- TestRun: (promptId), (status), (startedAt DESC)
- Scope composite index: (scopeTeam, scopeBrokingSegment, scopeGlobalLineOfBusiness, scopeProduct)

## Retention
- No deletion in MVP; audit value retained

## Open Questions
- Do we need to cap number of stored TestRuns per prompt? (Future)
