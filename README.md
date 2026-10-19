# RaceDay RESTful API — Part 2


## System Overview
**RaceDay** is a full-stack web-based event management system built specifically for the South African road running, walking, and cycling community. Part 2 provides the core backend RESTful API built with **ASP.NET Core Web API** and **Entity Framework Core (Code-First)** connected to an Azure/Local **SQL Server** database. 

The API handles all data persistence, enforces role-based access control, provides Swagger/OpenAPI documentation, and includes automated unit test suites.

---

## User Roles & Access Control
The application enforces two distinct user roles throughout the system:

1. **Organizer**
   * Can create, update, and delete events.
   * Can create age/distance event categories.
   * Can view all participant enrolments across their managed events.
   * Can capture and publish finish times and finishing positions for participants.

2. **Participant**
   * Can create an account and log in.
   * Can browse and view event details and available categories.
   * Can enter/enrol in events by selecting a category.
   * Can view personal enrolments and track race results/performance history.

> **Security Note:** Passwords are hashed before storage. Authentication state and role management are maintained via server-side session management across API requests.

---

## Technical Stack
* **Framework:** ASP.NET Core Web API (.NET 8.0)
* **ORM:** Entity Framework Core (Code-First Migration)
* **Database:** Microsoft SQL Server
* **Documentation & Testing UI:** Swagger / OpenAPI
* **Unit Testing:** xUnit / Moq
* **CI/CD Pipeline:** GitHub Actions

---

## Getting Started & Setup Instructions

### Prerequisites
* [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
* [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) or SQL Server Express/LocalDB
* [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Database Setup
1. Ensure your SQL Server instance is running.
2. Update the database connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=RaceDayDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }