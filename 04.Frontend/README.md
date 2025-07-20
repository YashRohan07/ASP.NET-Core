## Tech Stack:
- Frontend UI: Clean, responsive layout designed with plain HTML and CSS.
- Frontend Logic: Written in vanilla JavaScript to handle user interactions, API requests, and dynamic content updates.
- Backend API: ASP.NET Core Web API provides secure endpoints for user data management and persistence via Entity Framework Core with SQL Server.


## JavaScript Responsibilities
The app.js file handles all dynamic frontend logic:
•	API Communication:
o	Fetches user lists with optional search, filter, and sort query parameters.
o	Sends POST requests to create new users.
o	Sends PUT requests to update existing users.
o	Sends DELETE requests to remove users.
•	Dynamic Rendering:
o	Builds and updates the user table dynamically based on the data received from the API.
•	Form Handling:
o	Populates the form with existing user data when editing.
o	Resets the form state after a successful add or update.
o	Switches the form title between Add User and Update User modes automatically.
•	User Interaction:
o	Handles search, filter, and sort actions instantly.
o	Provides confirmation before deleting a user.
