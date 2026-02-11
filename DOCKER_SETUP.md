# LuftBornTask - Docker Setup

This guide explains how to run the entire LuftBornTask application (ASP.NET Core backend with ABP, Angular frontend, and PostgreSQL) using Docker and Docker Compose.

## Prerequisites

- **Docker**: [Install Docker Desktop](https://www.docker.com/products/docker-desktop)
- **Docker Compose**: Included with Docker Desktop

No need to install .NET, Node.js, PostgreSQL, or any other dependencies on your machine!

## Project Structure

```
LuftBornTask/
├── docker-compose.yml          # Orchestrates all services
├── aspnet-core/
│   ├── Dockerfile              # Backend build configuration
│   ├── .dockerignore            # Files to exclude from Docker build
│   └── src/                     # ASP.NET Core source code
└── angular/
    ├── Dockerfile              # Frontend build configuration
    ├── nginx.conf              # Nginx web server configuration
    ├── .dockerignore            # Files to exclude from Docker build
    └── src/                     # Angular source code
```

## Services

The `docker-compose.yml` defines three services:

### 1. **PostgreSQL Database** (`postgres`)
- Container: `luftborn-postgres`
- Port: `5432`
- Database: `LuftBornTask`
- Credentials: `root` / `myPassword`
- Data persists in volume `postgres_data`

### 2. **ASP.NET Core Backend** (`backend`)
- Container: `luftborn-backend`
- Port: `5000`
- URL: `http://localhost:5000`
- Automatically runs database migrations on startup
- Depends on PostgreSQL being healthy

### 3. **Angular Frontend** (`frontend`)
- Container: `luftborn-frontend`
- Port: `80`
- URL: `http://localhost` or `http://localhost:80`
- Serves optimized production build
- Depends on backend being healthy

## Quick Start

### 1. Start All Services

From the project root directory (where `docker-compose.yml` is located):

```bash
docker-compose up -d
```

This will:
- Build the ASP.NET Core backend image
- Build the Angular frontend image
- Start PostgreSQL
- Start the backend (waits for PostgreSQL)
- Start the frontend (waits for backend)

### 2. Wait for Services to be Ready

Monitor the services:

```bash
docker-compose logs -f
```

Wait until you see:
```
luftborn-backend | Now listening on: http://+:5000
luftborn-frontend | [notice] worker process started
```

### 3. Access the Application

- **Frontend**: http://localhost
- **Backend API**: http://localhost:5000
- **Swagger UI**: http://localhost:5000/swagger/index.html

## Common Commands

### View Logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f backend
docker-compose logs -f frontend
docker-compose logs -f postgres
```

### Stop Services

```bash
# Stop all services (keeps data)
docker-compose down

# Stop and remove volumes (deletes database)
docker-compose down -v
```

### Restart Services

```bash
# Restart all
docker-compose restart

# Restart specific service
docker-compose restart backend
```

### Rebuild Images

If you make changes to source code:

```bash
# Rebuild all
docker-compose up -d --build

# Rebuild specific service
docker-compose up -d --build backend
docker-compose up -d --build frontend
```

### Execute Commands in Container

```bash
# Access backend container shell
docker-compose exec backend /bin/bash

# Access frontend container shell
docker-compose exec frontend /bin/sh

# Access database container
docker-compose exec postgres psql -U root -d LuftBornTask
```

## Development Workflow

### When You Make Backend Changes

1. Make changes to C# code in `aspnet-core/src/`
2. Rebuild and restart:
   ```bash
   docker-compose up -d --build backend
   ```

### When You Make Frontend Changes

1. Make changes to TypeScript/Angular code in `angular/src/`
2. Rebuild and restart:
   ```bash
   docker-compose up -d --build frontend
   ```

### When You Add NuGet Packages

1. Edit `.csproj` file
2. Rebuild:
   ```bash
   docker-compose up -d --build backend
   ```

### When You Add NPM Packages

1. Edit `angular/package.json`
2. Rebuild:
   ```bash
   docker-compose up -d --build frontend
   ```

## Database

### Connection Details

- **Host**: `postgres` (from within containers) or `localhost` (from host machine)
- **Port**: `5432`
- **Database**: `LuftBornTask`
- **User**: `root`
- **Password**: `myPassword`

### Access PostgreSQL

```bash
docker-compose exec postgres psql -U root -d LuftBornTask
```

### Database Persistence

Database data is stored in the `postgres_data` Docker volume and persists across:
- Service restarts
- Container recreation

Data is deleted only when you run:
```bash
docker-compose down -v
```

### Database Migrations

The ASP.NET Core application handles EF Core migrations automatically on startup.

## Environment Configuration

### Backend Environment Variables

Configured in `docker-compose.yml`:

```yaml
ConnectionStrings__Default: "Host=postgres;Port=5432;Database=LuftBornTask;User ID=root;Password=myPassword;"
App__SelfUrl: "http://localhost:5000"
App__ClientUrl: "http://localhost:80"
App__CorsOrigins: "http://localhost:80"
AuthServer__Authority: "http://localhost:5000"
```

### Modify Configuration

Edit `docker-compose.yml` to change settings, then rebuild:

```bash
docker-compose up -d --build
```

## Health Checks

All services include health checks that Docker monitors:

- **PostgreSQL**: Checks `pg_isready`
- **Backend**: Checks `/health` endpoint
- **Frontend**: Checks HTTP 200 response

View health status:

```bash
docker-compose ps
```

## Troubleshooting

### Ports Already in Use

If ports 80, 5000, or 5432 are already in use:

1. Edit `docker-compose.yml`
2. Change port mappings (e.g., `8080:80` for frontend)
3. Rebuild: `docker-compose up -d --build`

### Backend Not Starting

```bash
# Check logs
docker-compose logs backend

# Common issues:
# - Database not ready: Wait a few seconds
# - Connection string wrong: Check docker-compose.yml
# - Port already in use: Change port mapping
```

### Frontend Shows 404

```bash
# Ensure backend is accessible from frontend
docker-compose logs frontend

# Clear browser cache and refresh
# Or use incognito/private window
```

### Permission Denied Errors

On Linux, you might need to use `sudo`:

```bash
sudo docker-compose up -d
```

### Volume Issues

```bash
# Clean all volumes and rebuild
docker-compose down -v
docker-compose up -d --build
```

## Network Communication

- Frontend connects to backend via `http://backend:5000` (internal Docker network)
- Backend connects to PostgreSQL via `postgres:5432` (internal Docker network)
- From your host machine:
  - Frontend: `http://localhost`
  - Backend: `http://localhost:5000`
  - Database: `localhost:5432`

## Production Considerations

For production deployment:

1. Update environment variables for security
2. Use external secret management
3. Configure proper CORS origins
4. Enable HTTPS (add reverse proxy like Traefik)
5. Use database backups
6. Monitor container logs and health
7. Use resource limits in compose file
8. Consider using Kubernetes instead

## Performance Tips

1. **Build once, use many times**: Images are cached
2. **Volume mounts for development**: For faster iteration
3. **Multi-stage builds**: Already implemented to minimize image size
4. **Layer caching**: Docker caches layers for faster rebuilds

## Cleanup

To completely remove all containers and volumes:

```bash
docker-compose down -v
```

To remove unused Docker resources:

```bash
docker system prune -a
```

## Support

- **ASP.NET Core ABP**: https://docs.abp.io/
- **Angular**: https://angular.dev/
- **Docker**: https://docs.docker.com/
- **PostgreSQL**: https://www.postgresql.org/docs/

## License

See individual project licenses in `aspnet-core/` and `angular/` directories.
