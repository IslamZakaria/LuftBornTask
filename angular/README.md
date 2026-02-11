# LuftBornTask - Frontend (Angular)

This is the frontend solution for LuftBornTask, built with Angular 19 and ABP Framework Angular UI.

## Docker Setup

You can run the frontend application using Docker Compose from the root directory.

### Prerequisites

- **Docker Desktop** installed.

### Services

The frontend subsystem consists of one Docker service defined in `../docker-compose.yml`:

1.  **Frontend** (`frontend`)
    -   Serves the Angular app via Nginx.
    -   Port: `80`
    -   URL: `http://localhost`

### Running the Frontend

To start the frontend (and the required backend services):

```bash
# From the solution root
docker-compose up -d frontend
```

*Note: The `frontend` service depends on the `backend` service, which depends on the database. Docker Compose will start the entire stack.*

### Configuration

The frontend Dockerfile uses Nginx. The API URL is configured via environment variables passed to the container or baked into `environment.prod.ts` depending on the build strategy (currently standard ABP environment management).

In specific to `docker-compose.yml`:
- `API_URL`: Pointing to the backend service (`http://backend:5000` internally).

## Development

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

### Authenticated Flow
Since the backend runs on port 5000 (usually), ensure your `src/environments/environment.ts` points to the running backend API.
