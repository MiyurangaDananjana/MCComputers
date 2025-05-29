# Project Setup and Usage Guide

This guide will help you set up and run the backend (.NET) and frontend (Angular) parts of the project, including running database migrations and connecting the Angular app to the API. It also includes instructions for pushing your code to GitHub.

---

## 1. Running EF Core Code-First Migrations (.NET Backend)

### Prerequisites
- .NET SDK installed on your machine.
- SQL Server or your preferred database accessible.
- Connection string properly configured in `appsettings.json`.

### Steps to Run Migrations

1. Open a terminal or command prompt in the backend project folder.

2. Create a migration for the initial database schema (you can change the migration name as needed):

   ```bash
   dotnet ef migrations add InitialCreate
