## Progress Overview

## Installed essential NuGet packages:

  * `Microsoft.EntityFrameworkCore.SqlServer` — SQL Server database provider.
  * `Microsoft.EntityFrameworkCore.Tools` — for migrations and design-time commands.
  * `Swashbuckle.AspNetCore` — Swagger/OpenAPI support.
  * `Microsoft.AspNetCore.OpenApi` — OpenAPI endpoint integration.

## User Model Implementation

* Defined the `User` class in the `UserManagement.Models` namespace with the following properties:

  * `Id` — Primary key.
  * `Name` — Required.
  * `Email` — Required, with `[EmailAddress]` validation attribute.
  * `PhoneNumber` — Required.
  * `Address` — Required.
  * `IsActive` — Boolean flag with a default value of `true` to indicate active/inactive status.

## Database Context Setup

* Created `AppDbContext` in the `UserManagement.Data` namespace:

  * Inherits from `DbContext`.
  * Contains a `DbSet<User>` named `Users`.
  * Registered via dependency injection in `Program.cs`.

## Database Connection Configuration

* Added the connection string in `appsettings.json`:
* Configured `AppDbContext` in `Program.cs` to use SQL Server provider with the above connection string.

## Entity Framework Core Migrations

Executed EF Core CLI commands:
- dotnet ef migrations add InitialCreate
- dotnet ef database update

* This process:
  * Created the `Migrations` folder.
  * Generated the SQL Server database `UserManagementDb`.
  * Created the `Users` table matching the defined schema.

 
<img width="1007" height="573" alt="Capture" src="https://github.com/user-attachments/assets/d2f6ae71-337f-476f-911a-2b6667e382be" />



