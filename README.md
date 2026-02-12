# LuftBornTask - ABP Framework Application

A full-stack application built with ABP Framework, .NET 10, Angular, and PostgreSQL, featuring Product CRUD operations with role-based access control.

## 🏗️ Architecture

- **Backend**: ASP.NET Core 10 with ABP Framework
- **Frontend**: Angular (latest version)
- **Database**: PostgreSQL 16
- **Authentication**: OpenIddict (OAuth 2.0 / OpenID Connect)
- **Containerization**: Docker & Docker Compose

## 📋 Prerequisites

Before running this application, ensure you have the following installed:

- **Docker Desktop** (version 20.10 or higher)
  - [Download for Windows](https://www.docker.com/products/docker-desktop)
  - Ensure Docker is running before executing commands
- **Git** (for cloning the repository)
- **Ports Available**: 
  - `5432` - PostgreSQL
  - `5000` - Backend API
  - `4200` - Frontend (Angular)

> **Note for Windows Users**: Port 80 is often blocked by IIS. This application uses port 4200 for the frontend to avoid conflicts.

## 🚀 Quick Start

### 1. Clone the Repository

```bash
git clone <repository-url>
cd LuftBornTask
```

### 2. Start the Application

Run the following command to build and start all services:

```powershell
docker-compose up --build
```

This will:
1. Build the backend, frontend, and database migrator Docker images
2. Start PostgreSQL database
3. Run database migrations and seed initial data
4. Start the backend API server
5. Start the frontend Angular application

### 3. Access the Application

Once all containers are running:

- **Frontend**: [http://localhost:4200](http://localhost:4200)
- **Backend API**: [http://localhost:5000](http://localhost:5000)
- **Swagger UI**: [http://localhost:5000/swagger](http://localhost:5000/swagger)

## 🔐 Default Credentials

The application is seeded with a default admin user:

| Field    | Value      |
|----------|------------|
| Username | `admin`    |
| Email    | `admin@abp.io`    |
| Password | `1q2w3E*`  |

## 👥 Roles and Permissions

### Admin Role

The `admin` role has full system access with all permissions, including:

- **User Management**: Create, read, update, delete users
- **Role Management**: Manage roles and permissions
- **Product Management**: Full CRUD operations on products
  - `LuftBornTask.Products` - View products list
  - `LuftBornTask.Products.Create` - Create new products
  - `LuftBornTask.Products.Edit` - Edit existing products
  - `LuftBornTask.Products.Delete` - Delete products

## 🧪 Testing the Application

### 1. Login

1. Navigate to [http://localhost:4200](http://localhost:4200)
2. Click on "Login"
3. Enter credentials: `admin` / `1q2w3E*`
4. You should be redirected to the dashboard

### 2. Verify Roles

1. After logging in, click on your username in the top-right corner
2. Select "My Account" or "Profile"
3. Verify that you have the `admin` role assigned

### 3. Test CRUD Operations

1. Navigate to "Products" in the main menu
2. **Create**: Click "New Product" and fill in the form
3. **Read**: View the products list
4. **Update**: Click edit icon on any product
5. **Delete**: Click delete icon on any product

## 🛠️ Development

### Running Locally (Without Docker)

#### Backend

```bash
cd aspnet-core/src/LuftBornTask.HttpApi.Host
dotnet run
```

#### Frontend

```bash
cd angular
npm install
npm start
```

#### Database

Ensure PostgreSQL is running and update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Host=localhost;Port=5432;Database=LuftBornTask;User ID=root;Password=myPassword;"
}
```

### Stopping the Application

To stop all Docker containers:

```powershell
docker-compose down
```

To stop and remove all data (including database):

```powershell
docker-compose down -v
```

## 📁 Project Structure

```
LuftBornTask/
├── aspnet-core/              # Backend .NET solution
│   ├── src/
│   │   ├── LuftBornTask.Domain/
│   │   ├── LuftBornTask.Application/
│   │   ├── LuftBornTask.HttpApi.Host/
│   │   └── ...
│   └── Dockerfile
├── angular/                  # Frontend Angular application
│   ├── src/
│   └── Dockerfile
├── docker-compose.yml        # Docker orchestration
└── README.md
```

## 🐛 Troubleshooting

### Port Already in Use

If you encounter port conflicts:

- **Port 5432**: Stop any local PostgreSQL instances
- **Port 5000**: Stop any other .NET applications
- **Port 4200**: Stop any Angular dev servers

### Backend Fails to Start

Check the logs:

```powershell
docker-compose logs backend
```

Common issues:
- Database not ready: Wait for PostgreSQL to be healthy
- Migration errors: Ensure db-migrator completed successfully

### Frontend Cannot Connect to Backend

Verify:
1. Backend is running: `docker ps`
2. CORS settings in `docker-compose.yml` include `http://localhost:4200`
3. Angular environment points to `http://localhost:5000`

## 📝 API Documentation

Once the backend is running, access the Swagger documentation at:

[http://localhost:5000/swagger](http://localhost:5000/swagger)

To test authenticated endpoints:
1. Click "Authorize" in Swagger UI
2. Use the OAuth flow with credentials: `admin` / `1q2w3E*`

## 🔄 Database Migrations

The database is automatically migrated on startup via the `db-migrator` container. To manually run migrations:

```bash
cd aspnet-core/src/LuftBornTask.DbMigrator
dotnet run
```

## 📞 Support

For issues or questions:
- Check the logs: `docker-compose logs`
- Review ABP documentation: [https://docs.abp.io](https://docs.abp.io)
- Check Docker status: `docker ps -a`

## 📄 License

[Add your license information here]
