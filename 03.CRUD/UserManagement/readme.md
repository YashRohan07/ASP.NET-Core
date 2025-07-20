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

<img width="940" height="612" alt="1" src="https://github.com/user-attachments/assets/ecd1f050-f72d-408e-aec1-68ccdfaf2964" />

<img width="934" height="594" alt="3" src="https://github.com/user-attachments/assets/fd9c9ca3-ebe1-448a-b837-31db37a98731" />

<img width="937" height="532" alt="4" src="https://github.com/user-attachments/assets/3a36713d-6f77-4ccc-9e05-9e6452dc2835" />

<img width="938" height="532" alt="5" src="https://github.com/user-attachments/assets/b5883e35-e316-42f3-b487-d1738e9c0d41" />

<img width="937" height="525" alt="7" src="https://github.com/user-attachments/assets/901c3e80-b6ca-4dec-9412-f79c53a79e35" />

<img width="1034" height="659" alt="14" src="https://github.com/user-attachments/assets/0fe47481-1046-49b2-89fa-37ffc607dc4a" />

<img width="936" height="467" alt="8" src="https://github.com/user-attachments/assets/9802afbe-8518-4efd-8c96-c66f376cc453" />

<img width="934" height="574" alt="9" src="https://github.com/user-attachments/assets/2ddb2dc4-c139-4306-aa7e-a31d12f0c8e2" />

<img width="940" height="580" alt="10" src="https://github.com/user-attachments/assets/06f094d1-7936-4033-84d8-f61497ee4c6d" />

<img width="940" height="588" alt="11" src="https://github.com/user-attachments/assets/a1434b36-e459-40bb-9a5b-d01dbd9312d9" />

<img width="940" height="620" alt="12" src="https://github.com/user-attachments/assets/41484c47-a3e6-4bff-9bd1-2df3c5229c32" />

<img width="935" height="515" alt="13" src="https://github.com/user-attachments/assets/bfbdb458-c53b-4d6c-a395-5b90bc6b4290" />









