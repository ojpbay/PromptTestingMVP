# Prompt Testing MVP - System Specification

## Overview

The Prompt Testing MVP is a simplified interface that allows users to quickly test and validate data extraction prompts against insurance documents within specific business scopes. This specification defines the minimum viable product for prompt testing functionality.

## Functional Requirements

### FR-1: Scope Selection
- **Description**: Users must select a business scope before accessing prompts
- **Components**: Team, Broking Segment, Global Line of Business, Product
- **Behavior**: 
  - All four scope fields must be selected to proceed
  - Scope can be pre-populated via URL parameters
  - Selected scope persists during the session
  - Users can clear and reselect scope

### FR-2: Prompt Management
- **Description**: Display and manage available data extraction prompts
- **Requirements**:
  - List all prompts available for the selected scope
  - Display prompt metadata: name, version, data point, status, accuracy
  - Allow selection of a single prompt for testing
  - Support prompt statuses: active, draft, archived

### FR-3: Context Editing
- **Description**: Edit prompt context and instructions
- **Requirements**:
  - Single active version per prompt (no version management)
  - Text area for editing prompt context
  - Auto-populate with existing context when prompt is selected
  - Context must be provided before test execution
  - No save functionality - changes are temporary for testing

### FR-4: Test Execution
- **Description**: Execute prompt tests against documents
- **Requirements**:
  - Run against ALL documents in the selected scope
  - No document selection options
  - Single test execution per prompt
  - Execution status indication
  - Prevent multiple simultaneous executions

### FR-5: Results Display
- **Description**: Show basic test results
- **Requirements**:
  - Display accuracy percentage
  - Show last run date
  - No detailed analysis or expandable sections
  - No result comparison features

## User Workflows

### Primary Workflow: Test a Prompt
1. **Select Scope**
   - Choose Team from dropdown
   - Choose Broking Segment from dropdown
   - Choose Global Line of Business from dropdown
   - Choose Product from dropdown
   - Apply scope selection

2. **Select Prompt**
   - View list of available prompts in table format
   - Select a prompt to test

3. **Edit Context**
   - Review auto-populated context
   - Modify prompt instructions as needed
   - Ensure context is not empty

4. **Execute Test**
   - Click "Run Test" button
   - Wait for execution to complete
   - View simple results summary

### Secondary Workflow: Change Scope or Prompt
1. **Return to Prompt List**
   - Navigate back from prompt details
   - Select different prompt

2. **Change Scope**
   - Clear current scope
   - Select new scope parameters
   - View prompts for new scope

## Data Models

### Scope
```
{
  team: string
  brokingSegment: string
  globalLineOfBusiness: string
  product: string
}
```

### Prompt
```
{
  id: string
  name: string
  version: string
  dataPoint: string
  status: "active" | "draft" | "archived"
  accuracy: number (0-100)
  lastRun: string (date)
}
```

### Context
```
{
  promptId: string
  content: string (multiline text)
}
```

## User Interface Requirements

### Layout Structure
- **Header Section**
  - Title: "Prompt Testing MVP"
  - Subtitle: "Simplified prompt testing interface for quick validation"
  - Settings button

- **Main Content Area**
  - **Left Column (1/3 width)**: Scope Selection
  - **Right Column (2/3 width)**: Dynamic content area

### Content States

#### State 1: No Scope Selected
- Display placeholder message
- Instruction to select scope
- Centered layout with icon

#### State 2: Scope Selected, No Prompt Selected
- Table of available prompts
- Columns: Name, Data Point, Version, Status, Accuracy, Actions
- Select button for each prompt

#### State 3: Prompt Selected
- **Prompt Details Card**
  - Prompt name and status badge
  - Data point and version information
  - Back to list navigation

- **Context Editor Card**
  - Text area for prompt context
  - Minimum 200px height
  - Placeholder text

- **Test Execution Card**
  - Information about test scope
  - Run test button (disabled if no context)
  - Execution status

- **Results Card** (if previous results exist)
  - Accuracy percentage
  - Last run date
  - Simple metrics layout

### Interactive Elements

#### Dropdown Menus (Scope Selection)
- Team options: ["Lloyd's", "London Market", "International"]
- Broking Segment options: ["Property", "Casualty", "Marine", "Aviation", "Energy"]
- Global Line of Business options: ["Property", "Casualty", "Specialty", "Reinsurance"]
- Product options: ["Commercial Property", "Professional Indemnity", "Marine Cargo", "Energy Liability"]

#### Buttons
- **Apply Scope**: Enabled only when all scope fields selected
- **Clear Scope**: Always enabled when scope exists
- **Select Prompt**: One per prompt row
- **Back to List**: Navigate from prompt details
- **Run Test**: Disabled when context empty or test running

#### Status Indicators
- **Prompt Status Badges**: Visual distinction for active/draft/archived
- **Test Execution**: Loading state during test runs
- **Results**: Color-coded accuracy metrics

## Business Rules

### BR-1: Scope Requirement
- Prompts are only accessible after scope selection
- All four scope parameters must be provided
- Scope can be pre-populated via URL parameters

### BR-2: Context Validation
- Context must contain text before test execution
- No character limits enforced
- No format validation required

### BR-3: Test Execution
- Only one test can run at a time per user session
- Tests run against all documents in scope automatically
- No partial test execution allowed

### BR-4: Results Persistence
- Results are displayed immediately after test completion
- Historical results shown if available
- No result storage requirements for MVP

## Integration Requirements

### URL Parameters Support
- `team`: Pre-populate team selection
- `brokingSegment`: Pre-populate broking segment
- `globalLineOfBusiness`: Pre-populate line of business
- `product`: Pre-populate product selection

### External Dependencies
- **Document Repository**: Access to documents within scope
- **Prompt Repository**: CRUD operations for prompts
- **Test Execution Engine**: Async test execution capability
- **Results Storage**: Basic metrics storage

### API Requirements

#### Get Prompts by Scope
```
GET /api/prompts?scope={team,segment,lob,product}
Response: Array of Prompt objects
```

#### Get Prompt Context
```
GET /api/prompts/{id}/context
Response: Context object
```

#### Execute Test
```
POST /api/prompts/{id}/test
Body: { context: string, scope: Scope }
Response: { testId: string, status: "running" }
```

#### Get Test Results
```
GET /api/tests/{testId}/results
Response: { accuracy: number, completedAt: string }
```

## Performance Requirements

- Page load time: < 3 seconds
- Test execution feedback: Immediate status update
- Scope selection: < 1 second response
- Prompt loading: < 2 seconds for up to 50 prompts

## Security Requirements

- Scope-based access control
- User authentication required
- Audit logging for test executions
- No sensitive data in client-side storage

## Limitations (MVP Scope)

### Excluded Features
- Multiple prompt versions
- Detailed test result analysis
- Document selection for testing
- Batch prompt testing
- Result comparison
- Prompt creation/editing
- Advanced context management
- Test scheduling
- Result export
- Collaborative features

### Future Enhancements
- Advanced analytics integration
- Multi-version prompt management
- Document-level result analysis
- Batch testing capabilities
- Result history and trends
- Prompt performance optimization
- Integration with full analytics platform

## Success Criteria

1. **Usability**: Users can complete end-to-end prompt testing in < 5 minutes
2. **Reliability**: 99% uptime during business hours
3. **Performance**: Sub-3-second page loads and immediate UI feedback
4. **Accuracy**: Test results match full system within 1% variance
5. **Adoption**: 80% of users prefer MVP for quick testing scenarios

## Acceptance Criteria

### AC-1: Scope Selection
- [ ] All four scope fields must be selected before proceeding
- [ ] URL parameters pre-populate scope fields correctly
- [ ] Clear scope functionality resets all selections
- [ ] Invalid scope combinations show appropriate messaging

### AC-2: Prompt Display
- [ ] Prompts display correctly in table format
- [ ] All required metadata is visible
- [ ] Status badges show correct colors
- [ ] Selection works for all prompt types

### AC-3: Context Editing
- [ ] Context auto-populates when prompt selected
- [ ] Text area accepts multi-line input
- [ ] Context persists during session
- [ ] Empty context prevents test execution

### AC-4: Test Execution
- [ ] Run button disabled when context empty
- [ ] Loading state shows during execution
- [ ] Only one test runs at a time
- [ ] Results update immediately upon completion

### AC-5: Navigation
- [ ] Back navigation preserves scope selection
- [ ] Scope changes reset prompt selection
- [ ] All navigation is intuitive and consistent