# UsersCrud API

A RESTful API for user management built with ASP.NET Core, Entity Framework Core, and JWT authentication.

## Technologies

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core + SQLite
- BCrypt.Net — password hashing
- JWT Bearer Authentication
- Swagger / Swashbuckle
- xUnit + FluentAssertions — unit testing

## Features

- Full user CRUD
- Automatic data validation on DTOs
- Passwords hashed with BCrypt
- JWT-based authentication
- Protected endpoints with `[Authorize]`
- Interactive API documentation with Swagger
- Unit tests for the service layer

## Project Structure

```
UsersCrud/
├── Controllers/
│   ├── AuthController.cs
│   └── UsuarioController.cs
├── Data/
│   └── AppDbContext.cs
├── DTOs/
│   └── UsuarioDto.cs
├── Migrations/
├── Models/
│   └── Usuario.cs
├── Services/
│   ├── AuthService.cs
│   └── UsuarioService.cs
└── Program.cs

UsersCrud.Tests/
└── UsuarioServiceTests.cs
```

## Getting Started

### Prerequisites

- .NET 10 SDK
- dotnet-ef installed globally

```bash
dotnet tool install --global dotnet-ef
```

### Installation

```bash
git clone https://github.com/your-username/UsersCrud.git
cd UsersCrud
dotnet restore
dotnet ef database update
dotnet run
```

The API will be available at `http://localhost:5017`.

The Swagger documentation will be available at `http://localhost:5017/swagger`.

## Endpoints

### Authentication

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/api/auth/login` | Authenticates user and returns JWT token |

### Users (requires authentication)

| Method | Route | Description |
|--------|-------|-------------|
| POST | `/api/usuario` | Creates a new user |
| GET | `/api/usuario` | Lists all users |
| GET | `/api/usuario/{id}` | Finds user by ID |
| PUT | `/api/usuario/{id}` | Updates user |
| DELETE | `/api/usuario/{id}` | Deletes user |

## Authentication

To access protected endpoints, first log in:

```bash
curl -X POST http://localhost:5017/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email": "user@email.com", "senha": "123456"}'
```

Then use the returned token in subsequent requests:

```bash
curl http://localhost:5017/api/usuario \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## Running Tests

```bash
cd UsersCrud.Tests
dotnet test
```

## Configuration

JWT settings are defined in `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "UsersCrud",
    "Audience": "UsersCrud"
  }
}
```

> In production, the JWT key should be stored in environment variables, never in source code.
