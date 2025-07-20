
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

## `index.html` file creation
<img width="937" height="653" alt="11" src="https://github.com/user-attachments/assets/47ab1412-db94-461b-80af-d4e1a44f01ce" />

## `app.js` file creation
<img width="936" height="653" alt="12" src="https://github.com/user-attachments/assets/ff528e77-7204-478e-bfee-6baa08024ebc" />

## `styles.css` file creation
<img width="939" height="648" alt="13" src="https://github.com/user-attachments/assets/3e4e53d9-895b-40bf-b4e2-757a88f53746" />



## User Management Dashboard
<img width="1366" height="732" alt="1" src="https://github.com/user-attachments/assets/5c214f99-3310-43bb-a4b8-e350f32437a8" />

## Search By Name
<img width="1366" height="674" alt="2" src="https://github.com/user-attachments/assets/c76f3236-1b00-46dc-adb7-86591db5885a" />

## Active Users
<img width="1366" height="661" alt="3" src="https://github.com/user-attachments/assets/0f972fba-caa5-4573-9560-67225ac0177a" />

## Inactive Users
<img width="1366" height="661" alt="4" src="https://github.com/user-attachments/assets/453be246-3513-46cf-a7a4-83c596f5304e" />

## Age Asc Order
<img width="1366" height="730" alt="5" src="https://github.com/user-attachments/assets/c7a9814e-942c-4c24-aba5-7094339f6a02" />

## Age Des Order
<img width="1357" height="722" alt="6" src="https://github.com/user-attachments/assets/186403a1-7c8d-4f45-8341-4d3797d24d75" />

## Update User
<img width="1366" height="768" alt="Screenshot (734)" src="https://github.com/user-attachments/assets/8d1403e0-7bc4-49ec-8ec8-2812179f7474" />

<img width="1366" height="768" alt="Screenshot (735)" src="https://github.com/user-attachments/assets/e900fd68-ac3c-48b4-99b5-af0d7b76fdae" />

## Add User
<img width="1366" height="768" alt="Screenshot (736)" src="https://github.com/user-attachments/assets/e7b80694-7f75-4dfa-89ab-3d98a59c698c" />

## Delete User
<img width="1366" height="768" alt="Screenshot (737)" src="https://github.com/user-attachments/assets/267edb17-eecc-4043-99ee-088c2fa44026" />

<img width="1366" height="768" alt="Screenshot (738)" src="https://github.com/user-attachments/assets/72d6b8f1-4abe-4400-8fe9-d29a4bb4c9d1" />




