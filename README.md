# README

## Manual

Seed the database by running:

```bash
Update-Database -Project Infrastructure -StartupProject App
```

---

## Recruitment Tasks

### Task 1

Implementation and tests for the employee-superior hierarchy:

- Service implementation:  
  `src/Domain/Services/EmployeeStructure.cs`

- Unit tests:  
  `tests/Unit.Tests/Domain/Services/EmployeeStructureTests.cs`

### Task 2–4

You can execute and verify these tasks using the HTTP requests defined in:

```
src/App/App.http
```

### Task 5

Unit tests for the “submit vacation request” handler are located at:

```
tests/Unit.Tests/Application/CanSubmitVacationRequestHandlerTests.cs
```

### Task 6

Introduce caching (in memory or distributed) for rarely changing data for exaple vacation packages.

---