# LuftBornTask - Backend (ASP.NET Core)

This is the backend solution for LuftBornTask, built with ASP.NET Core 10, ABP Framework 10, and Entity Framework Core with PostgreSQL.

## Project Structure

- **LuftBornTask.HttpApi.Host**: The API host project.
- **LuftBornTask.DbMigrator**: Console application to apply migrations and seed data.
- **LuftBornTask.Domain**: Domain layer (Entities, Domain Services).
- **LuftBornTask.EntityFrameworkCore**: EF Core implementation.

## Docker Setup

You can run the backend services (Database, Migrator, API) using Docker Compose from the root directory.

### Prerequisites

- **Docker Desktop** installed.

### Services

The backend subsystem consists of three Docker services defined in `../docker-compose.yml`:

1.  **PostgreSQL Database** (`postgres`)
    -   Port: `5432`
    -   Data Volume: `postgres_data`
    -   Credentials: `root` / `myPassword`

2.  **Database Migrator** (`db-migrator`)
    -   Runs on startup to apply pending migrations and seed initial data.
    -   Exits automatically after completion.

3.  **Backend API** (`backend`)
    -   Port: `5000`
    -   URL: `http://localhost:5000`
    -   Swagger: `http://localhost:5000/swagger`
    -   Health Check: `http://localhost:5000/health`

### Running the Backend

To start the backend stack (database + migrator + api):

```bash
# From the solution root
docker-compose up -d backend
```

*Note: The `backend` service depends on `db-migrator` and `postgres`, so they will start automatically.*

### Configuration

Environment variables are configured in `../docker-compose.yml`:

-   `ConnectionStrings__Default`: Connection string for PostgreSQL.
-   `App__SelfUrl`: The URL of the backend itself.
-   `App__ClientUrl`: The URL of the Angular frontend (for CORS).
-   `AuthServer__Authority`: The OIDC authority URL (backend URL).

## Development

If you want to run locally without Docker:
1. Update `appsettings.json` in `LuftBornTask.HttpApi.Host` with your local PostgreSQL connection string.
2. Run `LuftBornTask.DbMigrator` to create/seed the database.
3. Run `LuftBornTask.HttpApi.Host`.
