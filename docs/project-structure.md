# Logistic Automation – Project Structure & Technical Documentation

> **Document Role**
>
> This file is the primary technical documentation, architectural reference, and persistent technical context of the **Logistic Automation** project.
>
> Every development task MUST read this file before implementation.
>
> After every development task, this file MUST be reviewed and updated if the technical structure, architecture, dependencies, business modules, APIs, database model, integrations, conventions, or architectural decisions have changed.
>
> This document describes the **current state of the system**. It is not a changelog.
>
> If this document conflicts with the actual source code, the source code is the technical source of truth and this document must be corrected immediately.

---

# 1. Project Overview

**Project Name:** Logistic Automation

Logistic Automation is a logistics business automation platform consisting of two main applications:

```text
Logistic Automation
│
├── Logistic-web
└── Logistic-api
```

The system is designed to centralize logistics operational processes such as load/shipment management, customers, vehicles, drivers, subcontracted carriers, financial tracking, fuel tracking, and related operational workflows.

Business requirements are primarily defined in the project's business workflow documentation.

---

# 2. Applications

## 2.1 Logistic-web

`Logistic-web` is the frontend application.

### Technology

- React
- Modular / feature-based frontend architecture

### Responsibilities

- User interaction
- UI state management
- Collecting user input
- Building request models
- Communicating with `Logistic-api`
- Displaying API responses
- Client-side presentation logic

Business rules must not be implemented in the frontend unless they are strictly related to UI behavior.

Core business rules belong to the backend.

Preferred communication flow:

```text
React Component
       ↓
Hook / Module Logic
       ↓
API Service
       ↓
HTTP Client
       ↓
Logistic-api
```

React components should not directly contain HTTP implementation details where avoidable.

---

## 2.2 Logistic-api

`Logistic-api` is the backend application of the system.

### Technology

The backend MUST be developed with:

- **.NET**
- **ASP.NET Core**
- Entity Framework Core
- PostgreSQL
- Swagger / OpenAPI
- Domain Driven Design
- CQRS

The exact .NET target framework version must be documented in this file after it is selected or changed.

---

# 3. Core Engineering Principles

All development must follow these principles where applicable:

- SOLID
- Clean Code
- Domain Driven Design
- CQRS
- Separation of Concerns
- Dependency Inversion
- High Cohesion
- Low Coupling
- Testability
- Maintainability
- Explicit business intent
- Modular design
- Avoidance of unnecessary abstractions
- Avoidance of over-engineering

Architecture exists to control complexity, not to create complexity.

---

# 4. Repository Structure

High-level repository structure:

```text
/
├── logistic-web/
├── logistic-api/
├── database/
├── docker-compose.yml
├── project-structure.md
├── README.md
└── Lojistik Yönetim Sistemi – İş Tanımı ve Kapsam.docx
```

This section must reflect the actual repository.

Do not document folders or projects that do not exist.

---

# 5. Backend Solution Architecture

The backend consists of four main projects:

```text
Logistic.Api
Logistic.ApplicationService
Logistic.Infrastructure
Logistic.Domain
```

The architecture follows Domain Driven Design principles.

High-level dependency direction:

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
        └── Application contracts where technically required
```

The Domain layer must not depend on Infrastructure, ASP.NET Core, Entity Framework Core, PostgreSQL, HTTP, or frontend concerns.

---

# 6. Logistic.Api

## Responsibility

`Logistic.Api` is the presentation and HTTP entry-point layer.

Primary responsibilities:

- Controllers
- HTTP request/response handling
- API configuration
- Middleware
- Dependency injection bootstrap
- Authentication/authorization configuration when introduced
- Swagger / OpenAPI configuration

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

## Controller Rule

Controllers must remain thin.

Preferred flow:

```text
HTTP Request
    ↓
Model Binding / Request Validation
    ↓
Command / Query
    ↓
ApplicationService
    ↓
HTTP Response
```

Controllers must not contain:

- Business calculations
- Complex decision logic
- Direct persistence logic
- Entity manipulation rules
- Infrastructure-specific business decisions

---

# 7. Swagger / OpenAPI

`Logistic.Api` MUST provide API documentation using **Swagger / OpenAPI**.

Swagger integration is part of the standard backend setup.

Swagger documentation should provide visibility into:

- Available endpoints
- HTTP methods
- Request models
- Response models
- Status codes
- Authentication requirements when introduced
- API contracts

Swagger configuration belongs to the API layer.

Business logic must never depend on Swagger-specific types or configuration.

When authentication is implemented, Swagger configuration should be updated to support the relevant authorization mechanism where appropriate.

Any important change to the public API contract must also be reflected in this technical documentation.

---

# 8. Logistic.ApplicationService

## Responsibility

`Logistic.ApplicationService` represents the application's use-case and orchestration layer.

It is responsible for:

- Application workflows
- CQRS Commands
- CQRS Queries
- Handlers
- Application Services
- Assemblers
- Response models
- Application-level interfaces
- Coordination of repositories and domain operations

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

Do not create unnecessary empty folders.

The real project structure must be reflected in this document after implementation.

---

# 9. CQRS

CQRS is used in the ApplicationService layer.

## Commands

Commands represent operations that change application state.

Examples:

```text
CreateShipmentCommand
UpdateShipmentCommand
CancelShipmentCommand
```

Preferred handler convention:

```text
CreateShipmentCommand
        ↓
CreateShipmentCommandHandler
```

## Queries

Queries represent read operations.

Examples:

```text
GetShipmentByIdQuery
GetShipmentListQuery
```

Preferred handler convention:

```text
GetShipmentByIdQuery
        ↓
GetShipmentByIdQueryHandler
```

Handlers should remain focused on a single use case.

Reusable or complex domain rules must not be duplicated across handlers.

---

# 10. Application Services

Application Services may be used for use-case orchestration when the workflow is too complex to remain cleanly inside a single handler.

They may:

- Coordinate repository calls
- Coordinate multiple domain operations
- Use external service abstractions
- Manage application-level workflow
- Participate in transaction coordination

Domain rules must not be moved into Application Services merely for convenience.

Rules that are inherent to the business domain should be represented in Domain entities, Value Objects, or Domain Services.

---

# 11. Assemblers

Assemblers belong to `Logistic.ApplicationService`.

Their primary responsibility is mapping between models.

Typical mappings:

```text
Entity → Response
Request → Command
DTO → Response
```

Example:

```text
ShipmentAssembler
```

Example method:

```csharp
ShipmentResponse ToResponse(Shipment entity)
```

Mapping logic should not be scattered across Controllers, Handlers, or repositories.

---

# 12. Logistic.Domain

## Responsibility

`Logistic.Domain` is the core business layer.

It contains the business model and business rules independently from technical infrastructure.

Possible structures:

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

Only structures actually needed by the domain should be created.

---

# 13. BaseEntity

All persistent Domain entities MUST inherit from a common base class named:

```text
BaseEntity
```

`BaseEntity` contains the common identity and audit fields of all entities.

Required fields:

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

> Note: The canonical field name is **CreatedDate**.

## Field Responsibilities

### Id

Unique identifier of the entity.

Default identifier type:

```text
Guid
```

### CreatedDate

Date/time when the entity was created.

### ModifiedDate

Date/time of the latest modification.

Can be null for entities that have never been modified after creation.

### CreatedById

Identifier of the user/system actor that created the entity.

### ModifiedById

Identifier of the user/system actor that last modified the entity.

May be null until an entity is modified.

## Audit Management

Audit field management must be centralized rather than duplicated across business handlers.

The concrete strategy may be implemented through the EF Core DbContext, SaveChanges interception, an application-level current-user abstraction, or another architecture-compatible mechanism.

When the concrete audit strategy is implemented, this section must be updated with the exact behavior.

Business entities should not manually duplicate audit assignment logic unless a specific domain requirement requires it.

---

# 14. Domain Entities

Entities:

- Inherit from `BaseEntity`
- Represent business concepts
- Protect their business invariants
- Should contain meaningful domain behavior where appropriate

Avoid purely anemic models when business behavior naturally belongs to the entity.

Current business entities:

- `Company` (aggregate root — firma kartı)
- `CompanyRole`
- `CompanyContact`
- `CompanyNote`
- `CompanyBankAccount`

Whenever an entity is introduced, document:

- Purpose
- Important fields
- Relationships
- Business invariants
- Repository
- Related Commands/Queries where important

---

# 15. Value Objects

Value Objects should be used when a concept has domain meaning beyond being a primitive value.

Potential examples:

```text
Address
Money
PhoneNumber
EmailAddress
VehiclePlate
Coordinate
```

Do not create Value Objects for every primitive property.

Use them only when they improve domain correctness, validation, equality semantics, or expressiveness.

Current Value Objects:

_None documented yet._

---

# 16. Domain Services

A Domain Service may be introduced when:

- A business rule belongs to the domain
- The behavior does not naturally belong to one Entity or Value Object
- Multiple domain objects participate in the rule

Potential examples:

```text
ShipmentAssignmentService
PricingDomainService
RouteCalculationService
```

Domain Services must not become generic helper classes.

Current Domain Services:

_None documented yet._

---

# 17. Repository Pattern

Repository abstractions belong to `Logistic.Domain`.

Example:

```text
Logistic.Domain/
└── Repositories/
    └── IShipmentRepository.cs
```

Concrete repository implementations belong to `Logistic.Infrastructure`.

```text
Logistic.Infrastructure/
└── Persistence/
    └── Repositories/
        ├── Mapper/
        │   └── ShipmentEntityMap.cs
        └── ShipmentRepository.cs
```

Dependency direction:

```text
ApplicationService
       ↓
IShipmentRepository
       ↑
ShipmentRepository
       ↓
EntityMap
       ↓
Scaffolded EF Persistence Model / DbContext
```

Repository mapping between scaffold-generated persistence models and Domain entities is maintained in:

```text
Logistic.Infrastructure/Persistence/Repositories/Mapper/
```

Naming convention:

```text
<Entity>EntityMap.cs
```

`EntityMap` is a persistence/domain mapper and is separate from ApplicationService `Assembler` classes.

Application code must not depend directly on concrete repository implementations or scaffolded EF models.

---

# 18. Logistic.Infrastructure

## Responsibility

`Logistic.Infrastructure` contains technical implementations required by the system.

Responsibilities include:

- Entity Framework Core
- PostgreSQL
- Database First reverse engineering
- DbContext
- Scaffolded persistence models
- Repository implementations
- Repository `EntityMap` classes
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

Generated scaffold files are isolated in Infrastructure and must not become the Domain model.

---

# 19. Persistence Strategy

Database:

```text
PostgreSQL
```

ORM:

```text
Entity Framework Core
```

Database strategy:

```text
Database First
```

Local development database:

```text
PostgreSQL running in Docker
```

Persistence source of truth:

```text
PostgreSQL database schema
```

Persistence rules:

- Database schema changes happen first in PostgreSQL/versioned SQL.
- EF Core persistence models and DbContext are then re-scaffolded.
- EF Core migrations are not the default schema-authoring workflow.
- Scaffolded persistence models stay inside Infrastructure.
- Domain entities remain explicit business models.
- Repository interfaces belong to Domain.
- Repository implementations belong to Infrastructure.
- Persistence/domain mapping is handled by `<Entity>EntityMap` classes under `Persistence/Repositories/Mapper`.
- Application Entity-to-Response mapping continues to use Assemblers.
- EF-specific implementation details must not leak into Domain or frontend code.

## DbContext

`LogisticDbContext` is generated/re-generated from the PostgreSQL schema as part of the Database First workflow.

## Scaffolded Persistence Models

Recommended location:

```text
Logistic.Infrastructure/Persistence/Scaffolded/Models/
```

Generated files should not be manually edited when the change belongs in the database schema.

## Repository Implementations

Repository implementations use DbContext/scaffolded persistence models and return Domain-facing abstractions.

## Repository Mappers

```text
Logistic.Infrastructure/Persistence/Repositories/Mapper/
```

Contains `<Entity>EntityMap` classes for scaffolded persistence model ↔ Domain entity mapping.

## Schema Evolution

Recommended database source-control structure:

```text
database/
├── init/
└── migrations/
```

After a schema change:

```text
Database SQL change → PostgreSQL → EF scaffold → EntityMap review → code/tests/docs
```

The exact setup and scaffold commands are documented in `README.md`.

---

# 20. Frontend Architecture

`Logistic-web` follows a modular, feature-based architecture.

High-level target structure:

```text
src/
│
├── app/
│   ├── router/
│   ├── providers/
│   └── config/
│
├── modules/
│   └── <business-feature>/
│
├── shared/
│   ├── components/
│   ├── hooks/
│   ├── services/
│   ├── utils/
│   ├── constants/
│   └── types/
│
└── main.*
```

Only existing folders must be listed as current implementation.

## app

Contains application-wide configuration such as:

- Routing
- Providers
- Global configuration

## modules

Contains business-oriented frontend modules.

Current modules:

- `customers`

## shared

Contains reusable structures not owned by one specific business feature.

`shared` must not become a dumping ground for unrelated functionality.

---

# 21. API Communication

Frontend/backend communication must be isolated from presentation code.

Preferred flow:

```text
Component
   ↓
Hook / Module Logic
   ↓
Service
   ↓
HTTP Client
   ↓
Logistic-api
```

API contract changes must be reviewed for both:

- Backend impact
- Frontend impact

Important API contract changes must also be reflected in this document.

---

# 22. Current Business Modules

Current modules:

## Customers

```text
Module: Customers
Purpose: Manage companies with the Customer role (firma/muhasebe kartı, contacts, notes, bank accounts)
Main Entities: Company, CompanyRole, CompanyContact, CompanyNote, CompanyBankAccount
Value Objects: None (MVP)
Commands: CreateCustomer, UpdateCustomer, DeactivateCustomer, AddCustomerContact, AddCustomerNote, AddCustomerBankAccount
Queries: GetCustomerById, GetCustomerList
Handlers: Matching *CommandHandler / *QueryHandler under Features/Customers/Handlers
Application Services: CustomerValidationService
Repositories: ICompanyRepository → CompanyRepository
API Endpoints:
  GET    /api/customers
  GET    /api/customers/{id}
  POST   /api/customers
  PUT    /api/customers/{id}
  PATCH  /api/customers/{id}/deactivate
  POST   /api/customers/{id}/contacts
  POST   /api/customers/{id}/notes
  POST   /api/customers/{id}/bank-accounts
Frontend Module: Logistic-web/src/modules/customers
Important Business Rules:
  - Customers API creates companies with Customer role
  - TaxNumber and NationalId are globally unique
  - Individual requires NationalId; corporate requires TaxNumber + TaxOffice
  - Only one primary contact and one default bank account
  - Inactive companies cannot be mutated
```

When adding a business module, document it with the following structure:

```text
Module:
Purpose:
Main Entities:
Value Objects:
Commands:
Queries:
Handlers:
Application Services:
Repositories:
API Endpoints:
Frontend Module:
Important Business Rules:
```

---

# 23. Important Business Rules

Current implemented business rules:

- Customer-role companies must include valid identity fields based on `CompanyType`
- Duplicate `TaxNumber` or `NationalId` is rejected with HTTP 409
- Customer list queries return only companies with the `Customer` role
- Primary contact and default bank account uniqueness is enforced in the domain aggregate

Only rules that are actually part of the implemented system should be listed.

This section must stay synchronized with:

- Business workflow documentation
- Domain model
- Application behavior
- Actual source code

---

# 24. Validation Strategy

Validation is separated into two categories.

## Input Validation

Examples:

- Required field validation
- Invalid Guid
- Invalid format
- Invalid request structure

## Domain / Business Validation

Examples:

- An operation is forbidden by current shipment status
- A vehicle cannot be assigned under a specific business condition
- A finalized financial record cannot be changed

Business validation belongs in the appropriate Domain/Application layer.

Core business rules must not exist only on the frontend.

---

# 25. Error Handling Strategy

The backend must use a centralized/global error handling mechanism.

Avoid repetitive Controller-level:

```csharp
try
{
}
catch
{
}
```

patterns.

Meaningful Application/Domain exceptions may be introduced when required.

Possible examples:

```text
EntityNotFoundException
BusinessRuleException
ValidationException
ConflictException
```

Avoid unnecessary exception hierarchies.

The concrete global exception implementation must be documented here after it is introduced.

---

# 26. Logging Strategy

Structured logging should be used.

Logs should:

- Be searchable
- Include useful context
- Help diagnose failures
- Avoid exposing sensitive information

Never log:

- Passwords
- Authentication tokens
- Credentials
- Secrets

The concrete logging provider and conventions must be added here after implementation.

---

# 27. Dependency Injection

Constructor Injection is the default dependency injection mechanism.

Layer registrations should be encapsulated where appropriate.

Preferred pattern:

```csharp
services.AddApplicationServices();
services.AddInfrastructure(configuration);
```

`Program.cs` should remain focused on application bootstrap rather than detailed registration logic.

---

# 28. Async Programming

I/O-bound operations should use asynchronous APIs.

`CancellationToken` should be propagated where appropriate.

Example:

```csharp
Task<Shipment?> GetByIdAsync(
    Guid id,
    CancellationToken cancellationToken);
```

Preferred flow:

```text
Controller
    ↓
Handler / Application Service
    ↓
Repository / Integration
```

Avoid sync-over-async.

---

# 29. Naming Conventions

Current conventions:

```text
Entity Base:
BaseEntity

Command:
<CreateSomething>Command

Command Handler:
<CreateSomething>CommandHandler

Query:
<GetSomething>Query

Query Handler:
<GetSomething>QueryHandler

Repository Interface:
I<Entity>Repository

Repository Implementation:
<Entity>Repository

Assembler:
<Entity>Assembler

Response:
<Entity>Response

Request:
<Action><Entity>Request
```

Names should describe business intent whenever possible.

---

# 30. Architectural Decisions

## ADR-001 – Backend is developed with .NET

The backend application is developed using .NET and ASP.NET Core.

**Reason:** Provides the core runtime and web framework for the Logistic-api application.

---

## ADR-002 – Four-layer backend architecture

The backend is separated into:

```text
Logistic.Api
Logistic.ApplicationService
Logistic.Infrastructure
Logistic.Domain
```

**Reason:** Separates HTTP/presentation, application orchestration, technical infrastructure, and business domain concerns.

---

## ADR-003 – Domain Driven Design

The backend follows Domain Driven Design.

**Reason:** Business rules must remain explicit and protected from framework and infrastructure details.

---

## ADR-004 – CQRS

Commands and Queries are separated in `Logistic.ApplicationService`.

**Reason:** Provides explicit use-case boundaries and separates state-changing behavior from read behavior.

---

## ADR-005 – Repository abstractions belong to Domain

Repository interfaces are located in `Logistic.Domain`.

Concrete implementations are located in `Logistic.Infrastructure`.

**Reason:** The business/application layers must not depend on persistence implementations.

---

## ADR-006 – Entity mappings use Assemblers

Entity-to-response and related application mappings are handled through Assemblers in `Logistic.ApplicationService`.

**Reason:** Prevents mapping logic from being duplicated across Controllers and Handlers.

---

## ADR-007 – All entities inherit from BaseEntity

All persistent Domain entities inherit from `BaseEntity`.

Required common fields:

```text
Id
CreatedDate
ModifiedDate
CreatedById
ModifiedById
```

**Reason:** Provides a consistent identity and audit model across persistent domain entities.

---

## ADR-008 – Swagger / OpenAPI is mandatory

`Logistic.Api` integrates Swagger/OpenAPI.

**Reason:** API contracts must be discoverable and documented for development, testing, integration, and maintenance.

---

## ADR-009 – Frontend uses feature-based modular architecture

`Logistic-web` is organized around business features.

**Reason:** Related UI, state, API access, and feature-specific logic remain cohesive as the application grows.

---

## ADR-010 – project-structure.md is mandatory technical documentation

This file is the project's primary technical documentation and persistent architectural context.

**Reason:** Every developer or AI coding agent must have a single up-to-date technical reference describing the current system.

This document is part of the project's Definition of Done.

## ADR-011 – Persistence uses Database First

PostgreSQL is the persistence source of truth. DbContext and persistence models are re-scaffolded after schema changes. EF Core migrations are not the default schema-authoring mechanism.

## ADR-012 – Repository persistence mapping uses EntityMap classes

Persistence/domain mapping is maintained under `Logistic.Infrastructure/Persistence/Repositories/Mapper` using `<Entity>EntityMap` classes.

## ADR-013 – Local PostgreSQL runs in Docker

Local development uses a Dockerized PostgreSQL instance. Setup instructions and commands are maintained in `README.md`.

---

# 31. Testing Strategy

Current test projects:

_None documented yet._

When tests are introduced, document:

- Test project names
- Unit/integration scope
- Naming conventions
- Test dependencies
- Database strategy for integration tests
- External integration test strategy

---

# 32. Security

Authentication and authorization:

_Not defined yet._

Do not invent an authentication model without a requirement.

When introduced, document:

- Authentication mechanism
- Authorization model
- Roles/permissions if any
- Current-user abstraction
- Relationship with `CreatedById` / `ModifiedById`
- Swagger authorization configuration
- Security-related middleware

---

# 33. Configuration

Environment-specific configuration must remain outside business logic.

Secrets must never be committed to source control.

Configuration strategy:

_To be documented after project bootstrap._

At minimum, future documentation should describe:

- Database connection configuration
- Environment management
- Secret management
- External integration configuration

---

# 34. External Integrations

Current external integrations:

_None documented yet._

For every new integration document:

```text
Integration:
Purpose:
Interface:
Implementation:
Configuration:
Used By:
Failure Strategy:
```

---

# 35. Business Workflow Documentation

Business behavior must follow the project's business workflow `.md` documentation.

For every development task:

1. Read `project-structure.md`.
2. Read the relevant business workflow documentation.
3. Inspect the existing implementation.
4. Identify the affected business capability.
5. Determine the affected architectural layers.
6. Reuse existing conventions and abstractions.
7. Implement the requirement.
8. Run the relevant validation/tests/build.
9. Review `project-structure.md`.
10. Update `project-structure.md` if anything documented here has changed.

---

# 36. Source of Truth Priority

If information conflicts, use this priority:

```text
1. Explicit new requirement
2. Business workflow documentation
3. Actual source code and current system behavior
4. project-structure.md
5. General technical assumptions
```

If source code and this document conflict, inspect the implementation and update this document.

If a new requirement intentionally changes the architecture, implement the new requirement and then update this file.

---

# 37. Documentation Maintenance Protocol

`project-structure.md` is a living technical document.

It MUST be reviewed after every development task.

Update this document whenever any of the following changes:

- .NET version
- React version
- Framework or library
- Project dependency
- Folder structure
- Architectural layer
- Module
- Entity
- BaseEntity structure
- Value Object
- Enum
- Domain Service
- Repository interface
- Repository implementation
- DbContext
- Database First scaffold structure
- EntityMap mapping
- Database schema/scaffold strategy
- Docker PostgreSQL setup
- Command
- Query
- Handler
- Application Service
- Assembler
- Controller
- API endpoint
- Swagger/OpenAPI setup
- API contract
- Authentication
- Authorization
- Audit mechanism
- Logging
- Validation
- Error handling
- External integration
- Frontend/backend interaction
- Testing strategy
- Important business rule
- Architectural decision

Do not add every code edit as a chronological entry.

This document should always explain the **current architecture and current technical state** of the system.

---

# 38. Definition of Done

A development task is not considered complete until all applicable checks are complete:

```text
[ ] Requirement implemented
[ ] Relevant business workflow reviewed
[ ] Existing architecture respected
[ ] Business rules are in the correct layer
[ ] Controllers remain thin
[ ] Infrastructure details do not leak into Domain
[ ] CQRS separation is preserved
[ ] Repository abstractions are in the correct layer
[ ] Database First workflow was respected for schema changes
[ ] Scaffolded persistence models were refreshed when required
[ ] Repository EntityMap classes were reviewed/updated when required
[ ] Mapping follows the Assembler convention
[ ] Async/CancellationToken usage is correct where applicable
[ ] Duplicate logic was not introduced
[ ] Unnecessary abstraction was not introduced
[ ] Naming is clear
[ ] Error handling is appropriate
[ ] Swagger reflects API changes
[ ] Build succeeds
[ ] Relevant tests/checks succeed
[ ] project-structure.md was reviewed
[ ] project-structure.md was updated if required
```

---

# 39. Mandatory Rule for Future Prompts

Every future development prompt for this project must treat this document as mandatory context.

Before making code changes:

```text
READ project-structure.md
```

After making code changes:

```text
REVIEW project-structure.md
UPDATE project-structure.md IF REQUIRED
```

Updating technical documentation is not an optional follow-up task.

It is part of the development task itself.

The goal is:

> A Senior Software Engineer joining the Logistic Automation project should be able to read `project-structure.md` and quickly understand the system's current technologies, architecture, responsibilities, conventions, API approach, persistence strategy, domain model, integrations, and major technical decisions before inspecting the source code.
