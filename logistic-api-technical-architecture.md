# Logistic-api – Technical Architecture & Development Guidelines

> **Document Purpose**
>
> This document is the primary technical documentation and architectural contract for the `Logistic-api` backend application.
>
> Every development task in `Logistic-api` MUST read this document before implementation.
>
> After every development task, this document MUST be reviewed and updated if the architecture, domain model, folder structure, dependencies, persistence model, API contracts, business rules, integrations, conventions, testing strategy, or technical decisions change.
>
> This document describes the **current intended technical architecture** of `Logistic-api`. It is not a changelog.
>
> If this document conflicts with the actual source code, the source code is the technical source of truth and this document must be corrected.

---

# 1. Application Overview

`Logistic-api` is the backend application of the Logistic Automation platform.

It is responsible for:

- Core business rules
- Business workflows
- Domain model
- Application use cases
- Persistence
- API contracts
- Data validation
- Infrastructure integrations
- Audit behavior
- Authorization when introduced
- Consistency and integrity of system state

The frontend application `Logistic-web` communicates with this service through HTTP APIs.

---

# 2. Technology Stack

The backend MUST be developed with:

```text
.NET
ASP.NET Core
Entity Framework Core
Npgsql.EntityFrameworkCore.PostgreSQL
PostgreSQL
Swagger / OpenAPI
```

Architectural approach:

```text
Domain Driven Design
CQRS
Repository Pattern
Database First
SOLID
Clean Code
```

Persistence source of truth:

```text
PostgreSQL database schema
```

The project uses a **Database First** persistence workflow. Database schema changes are applied to PostgreSQL first, then EF Core persistence models are re-scaffolded from the database.

Current backend baseline:

```text
.NET SDK / Target Framework: .NET 10
ASP.NET Core: 10.x
Entity Framework Core: 10.0.0
Npgsql.EntityFrameworkCore.PostgreSQL: 10.0.0
Swashbuckle.AspNetCore: 10.2.3
dotnet-ef tool: 10.0.12
PostgreSQL (Docker): 18-alpine
```

---

# 3. Backend Projects

The backend consists of four main projects:

```text
Logistic.Api
Logistic.ApplicationService
Logistic.Infrastructure
Logistic.Domain
```

Each project has a strict responsibility.

Do not move code between layers merely for convenience.

---

# 4. Dependency Direction

Core dependency direction:

```text
                ┌──────────────────────────────┐
                │         Logistic.Api         │
                └──────────────┬───────────────┘
                               │
                               ▼
                ┌──────────────────────────────┐
                │ Logistic.ApplicationService  │
                └──────────────┬───────────────┘
                               │
                               ▼
                ┌──────────────────────────────┐
                │       Logistic.Domain        │
                └──────────────────────────────┘

Logistic.Infrastructure
        │
        ├── Logistic.Domain
        └── Application contracts where required
```

The Domain layer is the inner core.

The Domain layer must not depend on:

- ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger
- Controllers
- HTTP
- Infrastructure implementations
- Frontend concerns

---

# 5. Core Engineering Principles

All backend development must follow:

- SOLID
- Clean Code
- Domain Driven Design
- CQRS
- Separation of Concerns
- Dependency Inversion
- Explicit business intent
- High Cohesion
- Low Coupling
- Testability
- Maintainability
- Encapsulation
- Avoidance of unnecessary abstractions
- Avoidance of over-engineering

Architecture should protect business rules from framework details.

---

# 6. Logistic.Api

## Responsibility

`Logistic.Api` is the HTTP/presentation entry point.

It is responsible for:

- Controllers
- HTTP request/response
- Middleware
- Swagger/OpenAPI
- Authentication/authorization bootstrap when introduced
- Dependency injection bootstrap
- API-specific configuration
- Global exception pipeline wiring

Suggested structure:

```text
Logistic.Api/
│
├── Controllers/
├── Middlewares/
├── Extensions/
├── Configurations/
├── Program.cs
└── appsettings.json
```

Only create folders that are needed.

---

# 7. Controller Rules

Controllers MUST remain thin.

Preferred flow:

```text
HTTP Request
    ↓
Model Binding / Input Validation
    ↓
Command / Query
    ↓
Application Layer
    ↓
HTTP Response
```

Controllers must not contain:

- Core business rules
- Direct EF Core queries
- Complex calculations
- Entity state-transition logic
- Repository implementations
- Large mapping blocks
- Infrastructure-specific decisions

A Controller should primarily translate HTTP into an application use case.

---

# 8. Swagger / OpenAPI

Swagger/OpenAPI integration is mandatory.

Swagger must document:

- Endpoints
- HTTP methods
- Request contracts
- Response contracts
- HTTP status codes
- Authentication requirements when introduced

Swagger configuration belongs to `Logistic.Api`.

Business/Application/Domain layers must not depend on Swagger-specific types.

When authentication is introduced, Swagger authorization configuration must be updated accordingly.

Any important public API contract change must also be reflected in this document.

---

# 9. Logistic.ApplicationService

## Responsibility

`Logistic.ApplicationService` is the application use-case and orchestration layer.

It is responsible for:

- Commands
- Queries
- Handlers
- Application Services
- Assemblers
- Request/response application models where appropriate
- Application interfaces
- Transaction/use-case coordination
- Domain orchestration

Feature-based organization is preferred.

Example:

```text
Logistic.ApplicationService/
│
├── Features/
│   ├── Shipments/
│   │   ├── Commands/
│   │   ├── Queries/
│   │   ├── Handlers/
│   │   ├── Services/
│   │   ├── Models/
│   │   └── Assemblers/
│   └── ...
│
├── Common/
├── Interfaces/
└── Extensions/
```

Do not create generic folders that mix unrelated business capabilities.

---

# 10. CQRS

The application layer uses CQRS.

## Commands

Commands change application state.

Examples:

```text
CreateShipmentCommand
UpdateShipmentCommand
CancelShipmentCommand
AssignVehicleCommand
```

Each command should have a focused handler.

```text
CreateShipmentCommand
        ↓
CreateShipmentCommandHandler
```

## Queries

Queries read application state.

Examples:

```text
GetShipmentByIdQuery
GetShipmentListQuery
GetCustomerByIdQuery
```

```text
GetShipmentByIdQuery
        ↓
GetShipmentByIdQueryHandler
```

Do not place write-side behavior inside queries.

---

# 11. Handler Rules

Handlers should represent one application use case.

A handler may:

- Load entities through repositories
- Call domain behavior
- Call Domain Services
- Coordinate Application Services
- Persist changes
- Map results via Assemblers
- Return an application response

Handlers should not become large procedural scripts.

If behavior is reusable or domain-oriented, move it to the correct abstraction.

---

# 12. Application Services

Application Services may be used when a use case requires orchestration beyond a focused handler.

Possible responsibilities:

- Coordinating multiple repositories
- Coordinating multiple domain objects
- Integrating external application contracts
- Managing application workflow
- Coordinating transaction boundaries

Do not move domain logic into an Application Service merely because it is easier.

---

# 13. Assemblers

Assemblers belong to `Logistic.ApplicationService`.

They are responsible for model transformations such as:

```text
Entity → Response
DTO → Response
Request → Command
```

Example:

```text
ShipmentAssembler
```

Example method:

```csharp
ShipmentResponse ToResponse(Shipment entity)
```

Non-trivial mapping logic should not be duplicated across Controllers and Handlers.

---

# 14. Logistic.Domain

## Responsibility

`Logistic.Domain` is the core business layer.

It contains:

- Entities
- BaseEntity
- Value Objects
- Repository interfaces
- Domain Services
- Enums
- Domain exceptions
- Domain constants where appropriate
- Business invariants

Possible structure:

```text
Logistic.Domain/
│
├── Common/
├── Entities/
├── ValueObjects/
├── Repositories/
├── Services/
├── Enums/
├── Exceptions/
└── Constants/
```

Only create structures needed by the actual domain.

---

# 15. BaseEntity

All persistent Domain entities MUST inherit from:

```text
BaseEntity
```

`BaseEntity` contains common identity and audit fields:

```csharp
public abstract class BaseEntity
{
    public Guid Id { get; protected set; }

    public DateTime CreatedDate { get; protected set; }

    public DateTime? ModifiedDate { get; protected set; }

    public Guid? CreatedById { get; protected set; }

    public Guid? ModifiedById { get; protected set; }
}
```

Canonical field names:

```text
Id
CreatedDate
ModifiedDate
CreatedById
ModifiedById
```

Do not introduce alternative spellings such as `CratedDate`.

---

# 16. Audit Strategy

Audit management must be centralized.

Do not manually assign audit properties inside every Command Handler.

Preferred implementation options include:

- DbContext SaveChanges override
- EF Core interceptor
- Current-user abstraction
- Application/infrastructure audit service

The concrete strategy must:

- Set creation fields for new entities
- Set modification fields for changed entities
- Preserve original creation metadata
- Use UTC or a clearly defined application-wide time strategy
- Avoid duplication
- Remain testable

Current implementation:

- `LogisticDbContext` partial class override in `Persistence/Context/LogisticDbContext.Audit.cs`
- On `Added`: sets `CreatedDate` to UTC when not already set
- On `Modified`: sets `ModifiedDate` to UTC
- Applies to scaffolded persistence models: `Company`, `CompanyRole`, `CompanyContact`, `CompanyNote`, `CompanyBankAccount`
- `CreatedById` / `ModifiedById` remain null until authentication/current-user abstraction is introduced

---

# 17. Entity Design

Entities represent domain concepts and should protect their invariants.

Avoid public setters everywhere.

Prefer controlled state transitions through domain methods where business behavior exists.

Conceptually:

```csharp
shipment.AssignVehicle(vehicleId);
shipment.Cancel(reason);
shipment.MarkDelivered(deliveredAt);
```

Do not make every entity rich artificially.

Behavior belongs inside an entity when it naturally represents that entity's responsibility.

---

# 18. Value Objects

Use Value Objects for domain concepts with meaningful validation, semantics, or equality.

Potential examples:

```text
Money
Address
PhoneNumber
EmailAddress
VehiclePlate
Coordinate
```

Do not create Value Objects for every string or number.

---

# 19. Domain Services

Use a Domain Service when:

- The behavior is a domain rule
- It does not naturally belong to a single Entity or Value Object
- It may coordinate multiple domain concepts

Potential examples:

```text
ShipmentAssignmentService
PricingDomainService
RouteCalculationService
```

Domain Services must not become miscellaneous helper classes.

---

# 20. Repository Pattern

Repository interfaces belong to `Logistic.Domain`.

Example:

```text
Logistic.Domain/
└── Repositories/
    └── IShipmentRepository.cs
```

Concrete implementations belong to `Logistic.Infrastructure`.

```text
Logistic.Infrastructure/
└── Persistence/
    └── Repositories/
        ├── Mapper/
        │   └── ShipmentEntityMap.cs
        └── ShipmentRepository.cs
```

Dependency model:

```text
ApplicationService
       ↓
IShipmentRepository
       ↑
ShipmentRepository
       ↓
EntityMap
       ↓
Scaffolded Persistence Model / DbContext
```

Application code must not depend on a concrete repository class.

## Repository Mapper Folder

Every repository that needs translation between database-scaffolded persistence models and Domain entities must use an `EntityMap` class located under:

```text
Logistic.Infrastructure/
└── Persistence/
    └── Repositories/
        └── Mapper/
```

Naming convention:

```text
<EntityName>EntityMap.cs
```

Examples:

```text
ShipmentEntityMap.cs
CustomerEntityMap.cs
VehicleEntityMap.cs
```

`EntityMap` classes are responsible for persistence-to-domain mapping such as:

```text
Scaffolded Persistence Model → Domain Entity
Domain Entity → Scaffolded Persistence Model
```

`EntityMap` classes are **not** API response assemblers. API/Application mappings remain the responsibility of Assemblers in `Logistic.ApplicationService`.

Repositories should coordinate persistence and delegate non-trivial model translation to their corresponding `EntityMap` instead of scattering mapping logic inside repository methods.

For simple mappings, `EntityMap` classes should be stateless and deterministic. Do not introduce dependency injection into mapping classes unless a real dependency exists.

---

# 21. Repository Design Rules

Repositories should expose business-meaningful persistence operations.

Do not expose `DbSet`, scaffolded EF models, `IQueryable`, or EF-specific query objects to Domain/Application layers.

Prefer explicit methods where they match actual use cases.

Do not create a generic repository abstraction automatically unless repeated needs justify it.

Repository implementations may depend on:

- `LogisticDbContext`
- Scaffolded persistence models
- Repository `Mapper/EntityMap` classes

Repository implementations must return Domain/Application-facing abstractions rather than leaking generated persistence models outside Infrastructure.

---

# 22. Logistic.Infrastructure

## Responsibility

`Logistic.Infrastructure` contains technical implementations.

Responsibilities include:

- Entity Framework Core
- PostgreSQL
- Database First scaffolding
- DbContext
- Scaffolded persistence models
- Repository implementations
- Repository `EntityMap` classes
- Database connectivity
- External service implementations
- Infrastructure configuration

Suggested structure:

```text
Logistic.Infrastructure/
│
├── Persistence/
│   ├── Context/
│   │   └── LogisticDbContext.cs
│   ├── Scaffolded/
│   │   └── Models/
│   └── Repositories/
│       ├── Mapper/
│       │   └── <Entity>EntityMap.cs
│       └── <Entity>Repository.cs
│
├── Services/
├── Integrations/
└── Extensions/
```

`Persistence/Scaffolded` is generated from the PostgreSQL database and must be treated as generated persistence code.

Repository `Mapper` files are maintained manually and must not be overwritten by the scaffold command.

Only create folders when needed.

---

# 23. Entity Framework Core & Database First

Entity Framework Core is the ORM, but the project uses a **Database First** model.

The PostgreSQL schema is the persistence source of truth.

Expected workflow:

```text
1. Change PostgreSQL schema
2. Apply/version the database SQL change
3. Re-scaffold DbContext and persistence models
4. Review generated changes
5. Update repository EntityMap classes if required
6. Update repositories/application/domain code as required
7. Run build and tests
8. Update technical documentation
```

Scaffold-generated files should be isolated under Infrastructure and should not become Domain entities.

Do not manually edit generated persistence model files when the same change can be produced from the database schema. Manual edits to generated files can be lost on the next scaffold.

Domain entities remain explicitly designed business models and are protected from EF/database-generation concerns.

### Recommended Scaffold Target

```text
Logistic.Infrastructure/
└── Persistence/
    ├── Context/
    │   └── LogisticDbContext.cs
    └── Scaffolded/
        └── Models/
            └── *.cs
```

The exact scaffold command must be kept in `README.md` and updated whenever paths, context names, provider versions, or database configuration change.

---

# 24. PostgreSQL

PostgreSQL is the primary database.

For local development, PostgreSQL runs in Docker.

Database-specific concerns must stay inside Infrastructure.

The local container configuration, credentials convention, ports, and startup steps are documented in `README.md`.

Production credentials and secrets must never be committed to source control.

Table, column, index, and constraint names use **singular PascalCase** and must be quoted in SQL (for example `"Company"`, `"SchemaVersion"`, `"CompanyId"`, `"UqCompanyTaxNumber"`, `"IxCompanyName"`).

---

# 25. DbContext

The application DbContext belongs to Infrastructure.

The DbContext is generated/re-generated from the PostgreSQL schema as part of the Database First workflow.

Its responsibilities include:

- EF Core unit-of-work boundary
- Change tracking
- Persistence
- Database relationship metadata generated from the schema
- Audit field coordination if that strategy is selected

The DbContext must not contain business rules.

Custom behavior that must survive re-scaffolding should be placed in safe extension points, partial classes, interceptors, separate configuration/services, or other non-generated files rather than directly editing generated code.

---

# 26. Database Schema Evolution & Re-Scaffolding

**EF Core Migrations are not the primary schema-authoring mechanism for this project.**

This application is Database First.

Schema changes must follow this direction:

```text
Versioned SQL / Database Change
            ↓
PostgreSQL
            ↓
EF Core Scaffold
            ↓
Infrastructure Persistence Models
            ↓
Repository EntityMap
            ↓
Domain / Application
```

Database schema changes should be stored as versioned SQL scripts or another explicitly approved database-change mechanism in source control.

Recommended repository location:

```text
database/
├── init/
└── migrations/
```

Rules:

- Never treat generated EF classes as the database source of truth.
- Do not use `dotnet ef migrations add` as the default schema-change workflow.
- Do not hand-edit scaffolded models to represent a schema change.
- Re-scaffold after relevant database schema changes.
- Review generated diffs before committing.
- Update affected `EntityMap` classes after re-scaffolding.
- Update this technical document when persistence structure or workflow changes.

If the project later switches to Code First or adopts EF migrations, that is an architectural change and requires an explicit ADR/documentation update.

---

# 27. Transactions

Transactions should be applied at the appropriate application use-case boundary.

Do not start transactions deep inside unrelated repositories unless technically justified.

A transaction should represent one atomic business/application operation.

The concrete transaction pattern must be documented when implemented.

---

# 28. Validation Strategy

Validation is separated into:

## Input Validation

Examples:

```text
Required field
Invalid Guid
Invalid format
Out-of-range primitive value
Malformed request
```

## Domain / Business Validation

Examples:

```text
Delivered shipment cannot be cancelled
Unavailable vehicle cannot be assigned
Invalid state transition
Duplicate business identifier
```

Input validation may be performed before reaching domain behavior.

Business invariants must be enforced in Domain/Application according to responsibility.

---

# 29. Error Handling

The API must use centralized/global error handling.

Do not add repetitive Controller-level `try/catch` blocks.

Potential exception types may include:

```text
EntityNotFoundException
BusinessRuleException
ValidationException
ConflictException
```

Do not create a large exception hierarchy without need.

The exception pipeline should translate application failures into consistent HTTP responses.

---

# 30. Error Contract

API errors should be predictable for `Logistic-web`.

A consistent error contract should support:

- User-friendly messages
- Validation messages
- Business-rule messages
- Diagnostics/correlation where appropriate

Do not expose:

- Stack traces
- Database exception text
- Connection strings
- Internal implementation details

The exact error response model must be documented when finalized.

---

# 31. HTTP Status Codes

Use status codes consistently.

Typical examples:

```text
200 OK
201 Created
204 No Content
400 Bad Request
401 Unauthorized
403 Forbidden
404 Not Found
409 Conflict
500 Internal Server Error
```

Do not return `200 OK` for every failure.

---

# 32. Async Programming

I/O-bound operations should use asynchronous APIs.

Use `CancellationToken` where appropriate.

```csharp
Task<Shipment?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken);
```

Preferred propagation:

```text
Controller
    ↓
Handler / Application Service
    ↓
Repository / Integration
```

Avoid sync-over-async.

---

# 33. Dependency Injection

Use constructor injection.

Application and Infrastructure registrations should be grouped into extension methods.

Preferred pattern:

```csharp
services.AddApplicationServices();
services.AddInfrastructure(configuration);
```

`Program.cs` should remain focused on application composition.

Do not use service locator patterns.

---

# 34. Configuration

Environment configuration must remain outside business logic.

Examples:

- PostgreSQL connection string
- External service URL
- Authentication settings
- Feature flags
- Logging configuration

Secrets must never be committed to source control.

---

# 35. Logging

Use structured logging.

Never log:

- Passwords
- Tokens
- Credentials
- Secrets
- Full sensitive personal information
- Confidential financial details unless explicitly sanitized and required

Useful safe context may include identifiers such as:

```text
ShipmentId
CustomerId
VehicleId
CorrelationId
```

---

# 36. Correlation and Request Context

When a request-correlation strategy is introduced, document it here.

A correlation identifier is recommended for tracing operations across:

```text
Logistic-web
    ↓
Logistic-api
    ↓
Database / Integrations
```

Do not create complex distributed tracing infrastructure before it is needed.

---

# 37. Authentication and Authorization

Authentication/authorization strategy is not defined until a business requirement is provided.

When introduced, document:

- Authentication mechanism
- Token/session strategy
- User identity abstraction
- Roles
- Permissions
- Policy rules
- Swagger integration
- Relationship to audit fields

Authorization must be enforced in the backend.

---

# 38. Current User Abstraction

Because `BaseEntity` contains:

```text
CreatedById
ModifiedById
```

the application will require a consistent current-user/system-actor abstraction when authenticated users are introduced.

Application/Domain layers should not depend directly on raw ASP.NET `HttpContext`.

A dedicated abstraction should be introduced when needed.

---

# 39. Time Strategy

Time handling must be consistent.

Preferred backend storage strategy:

```text
UTC
```

Do not mix arbitrary server-local times with UTC.

If another strategy is selected, document it explicitly.

---

# 40. API Contract Design

API contracts must be explicit.

Do not expose EF Core entities directly as public API responses.

Use response models.

```text
Entity
   ↓
Assembler
   ↓
Response Model
```

Incoming HTTP contracts should not bind directly to mutable domain entities.

---

# 41. Request / Response Models

Typical naming:

```text
CreateShipmentRequest
UpdateShipmentRequest
ShipmentResponse
ShipmentListItemResponse
```

Keep public contracts stable and intentional.

Do not expose fields only because they happen to exist in the database.

---

# 42. API Versioning

API versioning should only be introduced when needed.

When introduced, document:

- Versioning method
- Compatibility rules
- Deprecation strategy
- Swagger grouping

---

# 43. Pagination

List endpoints expected to grow should support a deliberate pagination strategy.

Avoid returning unbounded large datasets.

The concrete pagination contract must be documented when introduced.

---

# 44. Filtering and Sorting

Filtering/sorting should be explicit and validated.

Do not expose arbitrary database expressions directly to clients.

Query parameters should map to supported application query options.

---

# 45. Concurrency and Idempotency

Concurrency strategy should be introduced when a real use case requires it.

Potential concerns:

- Optimistic concurrency
- Duplicate processing
- Conflicting updates
- Idempotency

Operations that may be retried by clients or integrations should be evaluated for idempotency.

Do not add complexity before it is needed.

---

# 46. External Integrations

External integrations should be abstracted behind interfaces.

Preferred direction:

```text
Application
    ↓
Integration Interface
    ↑
Infrastructure Implementation
```

For every integration document:

```text
Integration:
Purpose:
Interface:
Implementation:
Configuration:
Used By:
Failure Strategy:
```

Do not let external SDK types spread through Domain logic.

---

# 47. Resilience

When external dependencies are introduced, evaluate:

- Timeout
- Retry
- Circuit breaker
- Cancellation
- Failure fallback
- Idempotency

Do not blindly retry non-idempotent operations.

---

# 48. Naming Conventions

Preferred conventions:

```text
Base Entity:
BaseEntity

Entity:
Shipment

Repository Interface:
IShipmentRepository

Repository Implementation:
ShipmentRepository

Command:
CreateShipmentCommand

Command Handler:
CreateShipmentCommandHandler

Query:
GetShipmentByIdQuery

Query Handler:
GetShipmentByIdQueryHandler

Domain Service:
ShipmentAssignmentService

Application Service:
ShipmentApplicationService

Assembler:
ShipmentAssembler

Request:
CreateShipmentRequest

Response:
ShipmentResponse
```

Names should express business intent.

Avoid vague names such as `Manager`, `Helper`, `CommonService`, `UtilityService`, or `DataHandler` unless the name truly describes the responsibility.

---

# 49. Testing Strategy

Recommended test scopes:

```text
Domain Unit Tests
Application Unit Tests
Infrastructure Integration Tests
API Integration Tests
```

## Domain Tests

Focus on:

- Entity invariants
- Value Objects
- Domain Services
- State transitions
- Business rules

## Application Tests

Focus on:

- Command/Query handlers
- Application workflows
- Repository interaction behavior
- Error scenarios
- Mapping where meaningful

## Infrastructure Tests

Focus on:

- EF Core mappings
- Repository implementations
- PostgreSQL behavior where relevant
- External integrations

## API Integration Tests

Focus on:

- Endpoint contracts
- HTTP status codes
- Validation
- Error mapping
- Authorization when introduced

Do not test framework internals.

---

# 50. Business Workflow Documentation

Business workflows are defined in the project's workflow documentation.

Before implementing any feature:

1. Read this document.
2. Read the relevant business workflow.
3. Inspect existing code.
4. Identify the domain capability.
5. Identify affected layers.
6. Reuse existing patterns.
7. Implement the smallest correct design.
8. Run build/tests.
9. Review this document.
10. Update this document if the technical reality changed.

Do not invent important business rules without requirement support.

---

# 51. Current Business Modules

Current modules:

## Customers

```text
Module: Customers
Purpose: CRUD and detail management for Customer-role companies
Entities: Company, CompanyRole, CompanyContact, CompanyNote, CompanyBankAccount
Repository Interfaces: ICompanyRepository
Repository Implementations: CompanyRepository
EntityMap: CompanyEntityMap
Commands: CreateCustomer, UpdateCustomer, DeactivateCustomer, AddCustomerContact, AddCustomerNote, AddCustomerBankAccount
Queries: GetCustomerById, GetCustomerList
Handlers: Features/Customers/Handlers/*
Application Services: CustomerValidationService
Assemblers: CustomerAssembler
API Endpoints: /api/customers/*
Important Business Rules: Customer role required; unique tax/national ID; inactive company mutation blocked
Database Tables: Company, CompanyRole, CompanyContact, CompanyNote, CompanyBankAccount
```

When adding a module, document:

```text
Module:
Purpose:
Entities:
Value Objects:
Domain Services:
Repository Interfaces:
Commands:
Queries:
Handlers:
Application Services:
Assemblers:
Repository Implementations:
API Endpoints:
Important Business Rules:
```

---

# 52. Architectural Decisions

## API-ADR-001 – Backend uses .NET and ASP.NET Core

`Logistic-api` is developed with .NET / ASP.NET Core.

## API-ADR-002 – Four-layer architecture

The backend consists of:

```text
Logistic.Api
Logistic.ApplicationService
Logistic.Infrastructure
Logistic.Domain
```

## API-ADR-003 – Domain Driven Design

Business concepts and invariants are represented in Domain.

## API-ADR-004 – CQRS in ApplicationService

Commands and Queries are separated.

## API-ADR-005 – Repository interfaces belong to Domain

Interfaces are defined in Domain and implemented in Infrastructure.

## API-ADR-006 – Entity/Response mapping uses Assemblers

Assemblers live in `Logistic.ApplicationService`.

## API-ADR-007 – All persistent entities inherit BaseEntity

`BaseEntity` contains:

```text
Id
CreatedDate
ModifiedDate
CreatedById
ModifiedById
```

## API-ADR-008 – PostgreSQL is the primary database

Persistence uses PostgreSQL.

## API-ADR-009 – Entity Framework Core is the ORM

EF Core is used for persistence implementation.

## API-ADR-010 – Swagger/OpenAPI is mandatory

Swagger/OpenAPI documents public API contracts.

## API-ADR-011 – Centralized error handling

API errors are handled through a global mechanism.

## API-ADR-012 – This file is mandatory technical documentation

This document is part of Definition of Done.

## API-ADR-013 – Persistence strategy is Database First

The PostgreSQL database schema is the persistence source of truth. EF Core persistence models and `LogisticDbContext` are reverse engineered from the database. EF Core migrations are not the default schema-authoring mechanism.

## API-ADR-014 – Repository mappings use EntityMap classes

Repository persistence/domain mapping is managed under `Logistic.Infrastructure/Persistence/Repositories/Mapper` using `<Entity>EntityMap` classes.

**Reason:** Keeps scaffolded database models isolated from the Domain layer and prevents mapping code from being duplicated inside repositories.

---

# 53. Definition of Done

A `Logistic-api` task is complete only when all applicable checks pass:

```text
[ ] Requirement implemented
[ ] Relevant business workflow reviewed
[ ] Correct architectural layer used
[ ] Domain invariants remain protected
[ ] Controllers remain thin
[ ] CQRS separation is respected
[ ] Repository abstraction is correct
[ ] Infrastructure does not leak into Domain
[ ] API/Application mapping follows Assembler conventions
[ ] Persistence/Domain mapping follows Repository EntityMap conventions
[ ] BaseEntity/audit rules are respected
[ ] Async/CancellationToken usage is correct
[ ] Validation is implemented at the correct level
[ ] Error contract is correct
[ ] Swagger reflects API changes
[ ] Database changes were applied DB-first and versioned appropriately
[ ] DbContext/scaffolded persistence models were re-generated when required
[ ] Repository EntityMap classes were reviewed/updated when required
[ ] No unnecessary abstraction was introduced
[ ] No duplicated business rule was introduced
[ ] Relevant tests pass
[ ] Solution builds successfully
[ ] This technical document was reviewed
[ ] This technical document was updated if required
```

---

# 54. Mandatory Maintenance Rule

Every future prompt and development task for `Logistic-api` must use this document as mandatory technical context.

Before development:

```text
READ THIS FILE
```

After development:

```text
REVIEW THIS FILE
UPDATE THIS FILE IF REQUIRED
```

Any development that changes the technical reality of the application but leaves this document outdated is incomplete.

> A Senior Software Engineer should be able to read this file and quickly understand the backend stack, architectural layers, dependency rules, domain model approach, CQRS structure, persistence strategy, API conventions, audit model, Swagger requirements, testing strategy, and engineering rules before inspecting the source code.
