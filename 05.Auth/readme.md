## ASP.NET Core User Management API

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
│   ├── AuthController.cs          # Handles login & registration
│   ├── UsersController.cs         # Handles user CRUD endpoints
│
├── Models
│   ├── ApplicationUser.cs         # Extends IdentityUser with custom fields
│
├── DTOs
│   ├── RegisterDto.cs             # DTO for user registration
│   ├── LoginDto.cs                # DTO for login requests
│   ├── UserDto.cs                 # DTO for user data transfer
│   ├── UserUpdateDto.cs           # DTO for updating user info
│
├── Middleware
│   ├── ActiveUserMiddleware.cs    # Checks user status & enforces role rules
│
├── Data
│   ├── AppDbContext.cs            # EF Core DbContext
│   ├── AppDbContextFactory.cs     # Factory for EF migrations
│   ├── SeedData.cs                # Seeds default roles & admin user
│
├── Mapping
│   ├── MappingProfile.cs          # AutoMapper configuration
│
├── wwwroot
│   ├── index.html                 # Optional static landing page
│   ├── app.js                     # Optional static JavaScript
│   ├── styles.css                 # Optional static CSS
│
├── Program.cs                     # Startup configuration
├── appsettings.json               # App configs & secrets

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
  "name": "John",
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

# Database: `UserManagementDb` (SQL Server Management Studio)

<img width="1365" height="700" alt="1" src="https://github.com/user-attachments/assets/c2134dd5-df02-4306-bbf9-46394a49e803" />

<img width="1366" height="577" alt="2" src="https://github.com/user-attachments/assets/b881df4d-4093-4bb7-a9a8-f5d7e67ee2ec" />

<img width="1366" height="673" alt="3" src="https://github.com/user-attachments/assets/8fe72bf8-cce9-4d18-b936-303b8c7ec732" />

---

# Swagger UI — Interactive API Documentation

<img width="1353" height="689" alt="4" src="https://github.com/user-attachments/assets/ef650fe2-7a70-445d-bfd3-735de2b2ec53" />

---

# Registering a New User

<img width="942" height="520" alt="5" src="https://github.com/user-attachments/assets/b2be28f0-a0fd-4f0a-be37-9580f8f92068" />

---

# User Login — Generate JWT Token

<img width="939" height="585" alt="6" src="https://github.com/user-attachments/assets/aac88328-b170-40eb-b3ab-7c9c445373e3" />

---

# Active User — Retrieving Own Profile

<img width="938" height="552" alt="7" src="https://github.com/user-attachments/assets/363d2c42-3463-43b5-9650-c14077b3e3c0" />

---

# Active User — Updating Own Profile

<img width="941" height="566" alt="8" src="https://github.com/user-attachments/assets/bc35805c-cc39-461e-b79a-969f116904a0" />

---

# User Access Denied — Restricted Endpoint

<img width="937" height="455" alt="9" src="https://github.com/user-attachments/assets/627406c4-598b-4208-8fd6-2bcb931bd6e9" />

---

# User Access Denied — Restricted Action

<img width="939" height="512" alt="10" src="https://github.com/user-attachments/assets/d4d0e869-8fc7-485e-83ee-4cc55ad09433" />

---

# Admin Login — Generate Admin JWT

<img width="937" height="560" alt="11" src="https://github.com/user-attachments/assets/3f19fed8-01dd-48bc-8c36-19211b2bd432" />

---

# Admin — Get All Users

<img width="934" height="586" alt="12" src="https://github.com/user-attachments/assets/49ad3ab7-f6c4-438f-8d6c-53ff86f10f63" />

---

# Admin — Create New User

<img width="935" height="582" alt="13" src="https://github.com/user-attachments/assets/0c263033-2697-40f4-b1f3-d6e8c227606e" />

---

# Admin — Update Any User by ID

<img width="935" height="588" alt="14" src="https://github.com/user-attachments/assets/40e0e530-4d52-4d1f-8a1f-76a395fb1454" />

---

# Admin — Soft Delete User by ID

<img width="938" height="412" alt="15" src="https://github.com/user-attachments/assets/4e71cbfe-50f9-444c-af19-02538f05802f" />

<img width="940" height="437" alt="16" src="https://github.com/user-attachments/assets/60cc7c85-06c3-45ff-8d6f-f9588130b2ff" />

---

# Admin — Restore Deleted User

<img width="935" height="545" alt="17" src="https://github.com/user-attachments/assets/4ce13f9f-542c-4f0f-8dda-c460fdd95bb7" />

---

# Inactive User — Login Blocked

<img width="933" height="443" alt="20" src="https://github.com/user-attachments/assets/944d16ce-e2ab-4d66-8a80-6b09e2f7420b" />

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


