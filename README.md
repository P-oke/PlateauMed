# PlateauMed Task

This repository contains the source code and documentation for PlateauMed Task, a .NET 8 API application that follows the Clean Architecture pattern. 
The application is organized into distinct layers: Application, Domain, Infrastructure, and API. It uses SQL Server as its database, Entity Framework Core as the Object-Relational Mapper (ORM),
and X-Unit for testing. The API is accessible via both HTTPS and HTTP protocols at the following endpoints:

- **HTTPS:** https://localhost:7036
- **HTTP:** http://localhost:5062

## Project Structure

The project follows the Clean Architecture pattern, which separates the application into distinct layers:

- **Application:** Contains application-specific logic and use cases.
- **Domain:** Contains domain entities, value objects, and business rules.
- **Infrastructure:** Contains implementation details such as database access, external services, and other infrastructure concerns.
- **API:** Contains the web API endpoints and presentation.

## Getting Started

### Prerequisites

Before you begin, ensure you have the following installed on your local development environment:

- .NET 8 SDK
- SQL Server
- Visual Studio or Visual Studio Code (or any other preferred code editor)
- Git (for version control)

### Installation and Setup

1. Clone the repository to your local machine using Git:

   ```
   git clone https://github.com/p-oke/PlateauMed.git
   ```

2. Open the project in your preferred code editor.

3. Configure the database connection string in `appsettings.json` to point to your SQL Server instance:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=YourDatabaseName;Encrypt=false;TrustServerCertificate=true;"
     },
   }
   ```

4. Create a new migration to represent your changes to the data model:

   ```
   dotnet ef migrations add YourMigrationName
   ```

5. Apply the new migration to update the database schema:

   ```
   dotnet ef database update
   ```

   Alternatively, if you are using Visual Studio or Visual Studio Code, you can use the Entity Framework Core Package Manager Console commands:

   ```
   Add-Migration YourMigrationName
   Update-Database
   ```

### Running the Application

To run the application, use the following command:

```
dotnet run
```

The API will be accessible at the specified endpoints (https://localhost:7036 and http://localhost:5062).

### API Documentation

API documentation is available via Swagger. Once the application is running, navigate to the following URL in your web browser:

- **Swagger Documentation:** https://localhost:7036/swagger/index.html

Here you will find detailed information about the API endpoints, request parameters, and responses.

### Running Tests

X-Unit tests have been included in this project. To run the tests, use the following command:

```
dotnet test
```
