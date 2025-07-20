
## Tech Stack

* **Frontend UI:** A clean, responsive layout built using **HTML** and **CSS**, ensuring easy maintenance and fast loading.
* **Frontend Logic:** Developed with **vanilla JavaScript** to manage user interactions, communicate with the backend API and update the UI dynamically without any external frameworks.
* **Backend API:** **ASP.NET Core Web API** that provides secure REST endpoints for managing user data, with **Entity Framework Core** and **SQL Server** handling data persistence and migrations.

---

## JavaScript Responsibilities

The `app.js` file handles all core dynamic behavior for the frontend:

**API Communication**

* Fetches the user list with support for search, filter, and sort via query parameters.
* Sends `POST` requests to add new users.
* Sends `PUT` requests to update existing users.
* Sends `DELETE` requests to remove users from the database.

**Dynamic Rendering**

* Builds and updates the user table dynamically based on API responses.
* Renders action buttons (Edit/Delete) for each user row.

**Form Handling**

* Automatically fills the form with user data when editing an existing user.
* Resets the form after adding or updating a user.
* Switches the form title between **Add User** and **Update User** modes for clarity.

**User Interaction**

* Handles live search, filtering by status, and sorting by age through dropdowns and input fields.
* Prompts a confirmation dialog before deleting any user to prevent accidental deletions.

<img width="937" height="653" alt="11" src="https://github.com/user-attachments/assets/47ab1412-db94-461b-80af-d4e1a44f01ce" />

<img width="936" height="653" alt="12" src="https://github.com/user-attachments/assets/ff528e77-7204-478e-bfee-6baa08024ebc" />

<img width="939" height="648" alt="13" src="https://github.com/user-attachments/assets/3e4e53d9-895b-40bf-b4e2-757a88f53746" />



## File Structure
UserManagement
 ├── Controllers/         # ASP.NET Core API controllers (User CRUD logic)
 ├── Data/                # Entity Framework Core database context and configuration
 ├── Migrations/          # EF Core migrations for database schema
 ├── Models/              # Data models (User entity, DTOs)
 ├── wwwroot/             # Static frontend files
 │   ├── index.html       # Main HTML page (form + search + table)
 │   ├── app.js           # All JavaScript logic (API calls, rendering, event handling)
 │   └── styles.css       # Custom styling for layout and components
 ├── appsettings.json     # Application configuration (DB connection, settings)
 ├── Program.cs           # ASP.NET Core application entry point












