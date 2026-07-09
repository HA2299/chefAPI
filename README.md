# ChefAPI - Culinary System Backend

A robust and secure Full-Stack Culinary Web API built with .NET, designed for sharing, managing, and automatically rating recipes, featuring an advanced AI-powered recipe generator.

## Key Features
* **Advanced AI Integration:** Integrates with the Gemini API to dynamically generate custom recipes based on user-provided ingredients.
* **Secure Authentication:** Implements secure JWT (JSON Web Tokens) authentication and role-based authorization for protected endpoints.
* **Asynchronous Architecture:** Full utilize of Async/Await patterns to optimize server performance and prevent thread blocking.
* **Relational Database Design:** Efficient database schema managed via Entity Framework Core with complex entity relations and automated rating calculations.

## Tech Stack
* **Language & Framework:** C# / .NET Web API
* **ORM:** Entity Framework Core (EF Core)
* **Database:** SQL Server
* **Security:** JWT Authentication (`System.IdentityModel.Tokens.Jwt`)
* **AI Tool:** Gemini API
* **Development Tools:** Git, GitHub, Swagger

## Architecture Patterns
* Layered Architecture (Client-Server Separation)
* Dependency Injection (DI)
* Repository Pattern
* Data Transfer Objects (DTO)

## Getting Started
1. Clone the repository.
2. Update the database connection string and API keys in `appsettings.json`.
3. Run `dotnet ef database update` to apply migrations.
4. Run `dotnet run` to start the server.
