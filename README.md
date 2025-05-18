1. Register User
POST {{base_url}}/api/auth/register
Content-Type: application/json
{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Password123!"
}

2. Login and Store Token
POST {{base_url}}/api/auth/login
Content-Type: application/json
{
  "username": "testuser",
  "password": "Password123!"
}

3. Create Task (Authenticated)
POST {{base_url}}/api/tasks
Content-Type: application/json
Authorization: Bearer {{token}}
{
  "title": "First Task",
  "description": "This is my first task",
  "dueDate": "2024-12-31",
  "priority": "Medium"
}

4. Get All Tasks
GET {{base_url}}/api/tasks
Authorization: Bearer {{token}}

5. Update Task
PUT {{base_url}}/api/tasks
Content-Type: application/json
Authorization: Bearer {{token}}
{
  "title": "First Task 1",
  "description": "This is my first task 1",
  "dueDate": "2025-05-20",
  "priority": "High"
}

6 Delete Task
DELETE {{base_url}}/api/tasks
Authorization: Bearer {{token}}

Pass Task Id

----------------------- Table Structure -------------------------------------

CREATE TABLE TMTasks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500) NULL,
    DueDate DATETIME2 NOT NULL,
    Priority NVARCHAR(10) NOT NULL 
        CHECK (Priority IN ('Low', 'Medium', 'High')),
    Status NVARCHAR(15) NOT NULL 
        CHECK (Status IN ('Pending', 'InProgress', 'Completed')) 
        DEFAULT 'Pending',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UserId INT NOT NULL,
    CONSTRAINT FK_TMTasks_TMUsers FOREIGN KEY (UserId) 
        REFERENCES TMUsers(Id) ON DELETE CASCADE
);

CREATE TABLE TMUsers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
);