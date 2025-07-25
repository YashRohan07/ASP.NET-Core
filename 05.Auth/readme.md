## ASP.NET Core 6 User Management API

- JWT-based **stateless authentication**
- **ASP.NET Core Identity** for user & role management
- **Admin** & **User** roles with RBAC
- `IsActive` flag — block users without deleting accounts
- **Custom Middleware** to enforce roles & status rules
- Full **CRUD** operations for user management (Admin only)
- Predictable `{ Message, Data }` JSON response format
- **Swagger UI** for interactive API docs
- Fully testable with **Postman**

---

## Tech Stack

| Technology | Purpose |
|----------------|---------|
| **ASP.NET Core 6** | Web API Framework |
| **Entity Framework Core** | ORM for SQL Server |
| **ASP.NET Core Identity** | User & Role management |
| **JWT Authentication** | Secure, stateless authentication |
| **AutoMapper** | DTO mapping |
| **Swagger** | API documentation |
| **Custom Middleware** | Enforce request pipeline rules |

---

## Project Structure (Simplified)

```
├── Controllers
│   ├── AuthController.cs       # Handles login/register
│   ├── UsersController.cs      # Handles user CRUD
├── Models
│   ├── ApplicationUser.cs      # Extends IdentityUser
├── DTOs
│   ├── RegisterDto.cs
│   ├── LoginDto.cs
│   ├── UserDto.cs
├── Middleware
│   ├── ActiveUserMiddleware.cs # Role/Status checks
├── Data
│   ├── SeedData.cs             # Seeds roles & admin user
├── Program.cs                  # Startup config
├── appsettings.json            # Configs & secrets
````

---

### Install Dependencies

Run in the project root:

```bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 6.*
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.*
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.*
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 6.*
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.*
dotnet add package Swashbuckle.AspNetCore --version 6.*
```

---

### Configure Database & JWT

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=UserManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true"
},
"Jwt": {
  "Key": "YourSuperSecretKey123!",
  "Issuer": "YourApp",
  "Audience": "YourAppUser"
}
```

> **Important**: For production, use `dotnet user-secrets` or environment variables for secrets.

---

### Apply EF Core Migrations

```bash
dotnet ef migrations add InitialIdentityMigration
dotnet ef database update
```

This will:

* Create **Identity tables** (`AspNetUsers`, `AspNetRoles`, etc.)
* Add custom `ApplicationUser` fields

---

### Seed Roles & Default Admin

The `SeedData.InitializeAsync` runs automatically on startup:

* Adds `Admin` & `User` roles
* Seeds default admin:

  * **Email**: `admin@example.com`
  * **Password**: `Admin@12345`

---

### Run the App

```bash
dotnet run
```

Visit **Swagger UI** at:

```
https://localhost:<port>/swagger
```

---

## Authentication Flow

1. **Register** → `/api/auth/register`
2. **Login** → `/api/auth/login` → receive a JWT
3. Use JWT as:

   ```
   Authorization: Bearer <jwt-token>
   ```

---

## API Endpoints

| Method | Route                     | Access          | Purpose              |
| ------ | ------------------------- | --------------- | -------------------- |
| POST   | `/api/auth/register`      | Public          | Register a new user  |
| POST   | `/api/auth/login`         | Public          | Login & receive JWT  |
| GET    | `/api/users`              | Admin only      | Get all users        |
| GET    | `/api/users/{id}`         | Admin only      | Get user by ID       |
| GET    | `/api/users/me`           | Any active user | Get own profile      |
| POST   | `/api/users`              | Admin only      | Create new user      |
| PUT    | `/api/users/{id}`         | Admin only      | Update any user      |
| PUT    | `/api/users/me`           | Any active user | Update own profile   |
| DELETE | `/api/users/{id}`         | Admin only      | Soft delete user     |
| POST   | `/api/users/{id}/restore` | Admin only      | Restore deleted user |

**Response Format:**

```json
{
  "Message": "Success",
  "Data": { }
}
```

---

## Custom Middleware Rules

* ❌ If `IsActive = false` → Block all requests → **403 Forbidden**
* ✅ If **non-Admin** → Only `GET` allowed (except `PUT /me`)
* ✅ **Admins** → Full CRUD access

---

## Scenarios

| Route                    | Token         | Outcome         |
| ------------------------ | ------------- | --------------- |
| `GET /api/users`         | Admin         | ✅ 200 OK        |
| `GET /api/users`         | User          | ❌ 403 Forbidden |
| `GET /api/users/me`      | Active User   | ✅ 200 OK        |
| `GET /api/users/me`      | Inactive User | ❌ 403 Forbidden |
| `PUT /api/users/me`      | Active User   | ✅ 200 OK        |
| `PUT /api/users/{id}`    | User          | ❌ 403 Forbidden |
| `DELETE /api/users/{id}` | Admin         | ✅ 200 OK        |

---

## Testing (Postman)

**Register**

```http
POST /api/auth/register
Body:
{
  "name": "John Doe",
  "email": "john@example.com",
  "password": "John123$",
  "age": 30,
  "address": "Dhaka"
}
```

**Login**

```http
POST /api/auth/login
Body:
{
  "email": "john@example.com",
  "password": "John123$"
}
```

**Set Authorization Header**

```
Authorization: Bearer <jwt-token>
```

**Try Requests**

* ✅ `GET /api/users/me` → works for active users
* ❌ `POST /api/users` → 403 if not Admin
* ❌ Set `IsActive = false` → Any request → 403 Forbidden

---

---

## Common Commands

| Command                           | Purpose                |
| --------------------------------- | ---------------------- |
| `dotnet restore`                  | Restore NuGet packages |
| `dotnet run`                      | Run the app            |
| `dotnet ef migrations add <Name>` | Add a new migration    |
| `dotnet ef database update`       | Apply migration to DB  |

---

