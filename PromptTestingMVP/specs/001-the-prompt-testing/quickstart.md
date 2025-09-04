# Quickstart: Prompt Testing MVP

## Prerequisites
- Node.js 20+
- Angular CLI 20+
- .NET 9 SDK
- Docker (for SQL Server + Testcontainers)

## Backend Setup
```
cd backend
# Create solution + projects (commands to be executed during implementation phase)
# dotnet new sln -n PromptTesting
# dotnet new webapi -n PromptTesting.Api
# dotnet new classlib -n PromptTesting.Domain
# dotnet new classlib -n PromptTesting.Application
# dotnet new classlib -n PromptTesting.Infrastructure
# dotnet sln add PromptTesting.*/*.csproj
# Add references appropriately
# dotnet add PromptTesting.Api/PromptTesting.Api.csproj reference PromptTesting.Application/PromptTesting.Application.csproj
# dotnet add PromptTesting.Application/PromptTesting.Application.csproj reference ../PromptTesting.Domain/PromptTesting.Domain.csproj
# dotnet add PromptTesting.Infrastructure/PromptTesting.Infrastructure.csproj reference ../PromptTesting.Domain/PromptTesting.Domain.csproj
# dotnet add PromptTesting.Api/PromptTesting.Api.csproj reference PromptTesting.Infrastructure/PromptTesting.Infrastructure.csproj

# Add packages
# dotnet add PromptTesting.Application package MediatR
# dotnet add PromptTesting.Application package FluentValidation
# dotnet add PromptTesting.Infrastructure package Microsoft.EntityFrameworkCore
# dotnet add PromptTesting.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
# dotnet add PromptTesting.Infrastructure package Microsoft.EntityFrameworkCore.Design
# dotnet add PromptTesting.Api package Serilog.AspNetCore
# dotnet add PromptTesting.Api package Swashbuckle.AspNetCore

# Run tests (later) - placeholder
# dotnet test
```

## Frontend Setup
```
cd frontend
# ng new prompt-testing --standalone --routing --style=scss
# cd prompt-testing
# npm install @angular/material ag-grid-community ag-grid-angular @ngrx/signals @ngrx/store-devtools
# npm install --save-dev jest @types/jest jest-preset-angular @testing-library/angular
# npx jest --init (configure preset)
```

## Running Locally (Planned)
1. Start SQL Server via Docker
   ```
   docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Your_strong_password123' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
   ```
2. Apply EF Core migrations (after creation)
   ```
   dotnet ef database update -p backend/src/PromptTesting.Infrastructure -s backend/src/PromptTesting.Api
   ```
3. Run backend API
   ```
   dotnet run --project backend/src/PromptTesting.Api
   ```
4. Run frontend dev server
   ```
   cd frontend/prompt-testing
   ng serve
   ```
5. Navigate to http://localhost:4200

## Test Strategy Summary
- Contract tests hit in-memory or containerized API verifying OpenAPI conformance
- Integration tests execute real EF Core operations against Testcontainers SQL Server
- Frontend component tests use Jest + Testing Library
- End-to-end tests (future) could be added with Playwright (not in MVP plan)

## Key Commands (Planned)
- `dotnet test`
- `npm run test`
- `npm run lint`

## Next Steps After Quickstart
- Implement tasks from `tasks.md` once generated
- Ensure failing tests exist before writing implementation code
