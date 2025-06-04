# Task Management System

A simple web-based task management system that allows users to create, update, and delete tasks. This system is built with a .NET backend.

---

## 🔧 Technologies Used

- Backend: ASP.NET Core Web API
- Database: PostgreSQL
- Authentication: JWT
- Docker

---

## 🚀 Getting Started

### Backend

1. Clone the repository

git clone https://github.com/HaninSalhab/Tasks_Backend.git
cd TaskManagement.Backend

2. Build Docker Images and Run the Containers to use the PostgreSQL

docker-compose up -d

3. Set the connection string in your appsettings.Development.json

if you want to connect to the PostgreSQL docker in your machine use this connection: "DefaultConnection": "Host=localhost;Port=5433;Database=TaskManagementDB;Username=dev;Password=dev"
if you want to connect to the PostgreSQL instance from Render use this connection:   "DefaultConnection": "Host=dpg-d0vve8u3jp1c739frtd0-a.oregon-postgres.render.com;Port=5432;Database=taskdb_ie2z;Username=dev;Password=FhzwR7fksJuOUDaqVSWdxHS3I2BFbZf1;SSL Mode=Require;Trust Server Certificate=true",

4. Apply database migrations if you decide to use your desktop docker:

Oppen Nuget Console and run this: Update-Database -Context TaskManagementDbContext

5. Run the backend