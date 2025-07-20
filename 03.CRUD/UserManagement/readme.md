## Project Details

* **Framework:** ASP.NET Core Web API (.NET 8.0)
* **Database:** SQL Server
* **ORM:** Entity Framework Core

##
Here i have added `Age` column later, Then for updating the Database:
- dotnet ef migrations add AddAgeToUser
- dotnet ef database update



## **CRUD Endpoints**

| HTTP Method | Route             | Description          |
| ----------- | ----------------- | -------------------- |
| POST        | `/api/users`      | Create new user      |
| GET         | `/api/users`      | Get all users        |
| GET         | `/api/users/{id}` | Get user by ID       |
| PUT         | `/api/users/{id}` | Update existing user |
| DELETE      | `/api/users/{id}` | Delete user by ID    |



## **Search, Filter, Sort**

* **Search:** `?search=keyword` → search `Name` or `Email`.
* **Filter by Status:** `?status=active` or `?status=inactive`.
* **Sort by Age:** `?sort=age_asc` or `?sort=age_desc`.


## **API Response Format**

All endpoints return:

```json
{
  "message": "Descriptive message",
  "data": [ /* users list or single user */ ]
}
```

## **Tested With Postman**

