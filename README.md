# Logistic API

ASP.NET Core 10 backend for the Logistic platform (PostgreSQL, EF Core, Swagger).

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) and Docker Compose v2 (full stack)
- Optional sibling repo: [logistic-web](https://github.com/erhnglmz/logistic-web)

## Repository layout (full stack)

Clone both repositories **as siblings**:

```text
your-workspace/
├── logistic-api/    ← this repo (run compose from here)
└── logistic-web/
```

```bash
git clone https://github.com/erhnglmz/logistic-api.git
git clone https://github.com/erhnglmz/logistic-web.git
```

## Quick start — Docker (integration)

```bash
cd logistic-api
cp .env.example .env
docker compose up -d --build
```

| Service    | URL |
|-----------|-----|
| Swagger   | http://localhost:5111/swagger |
| Health    | http://localhost:5111/health/ready |
| Web UI    | http://localhost:5173 |
| PostgreSQL | localhost:5432 |

Default DB credentials are in `.env.example` (local/docker only, not for production).

## Quick start — Docker (development / hot reload)

```bash
docker compose -f compose.dev.yaml up --build
```

- API: `dotnet watch`
- Web: Vite dev server on port 5173

If `logistic-web` is not at `../logistic-web`, set `LOGISTIC_WEB_PATH` in `.env`.

## Local run (without full Docker stack)

1. Start PostgreSQL only:

   ```bash
   docker compose up -d postgres
   ```

2. Configure API:

   ```bash
   cp .env.example .env
   cp Logistic.Api/appsettings.Development.json.example Logistic.Api/appsettings.Development.json
   # Edit passwords if needed (matches POSTGRES_PASSWORD in .env)
   export $(grep -v '^#' .env | xargs)   # macOS/Linux — sets ConnectionStrings__*
   ```

3. Run API:

   ```bash
   dotnet run --project Logistic.Api/Logistic.Api.csproj --launch-profile http
   ```

4. Run web from sibling `logistic-web` (`npm install && npm run dev`).

## Secrets and configuration

- Never commit `.env` or `Logistic.Api/appsettings.Development.json`.
- Connection strings come from environment variables or local overrides (`ConnectionStrings__DefaultConnection`).
- Before pushing, scan for accidental secrets:

  ```bash
  rg -i 'password|api[_-]?key|secret' --glob '!**/bin/**' --glob '!**/obj/**' --glob '!**/node_modules/**'
  ```

## Documentation

Product and architecture docs are in [`docs/`](docs/).

## Solution

```bash
dotnet build Logistic.sln
dotnet test   # when tests exist
```