# Developer Evaluation Project

## Overview
This project implements an API for managing sales records using ASP.NET Core and follows the principles of Domain-Driven Design (DDD). The API provides functionality for creating, retrieving, updating, and deleting sales records.

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or any preferred IDE
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or any compatible database

## Configuration
1. **Clone the Repository**
   ```bash
   git clone https://github.com/yourusername/DeveloperEvaluation.git
   cd DeveloperEvaluation
Setup Database

Update the connection string in appsettings.json within the Ambev.DeveloperEvaluation.WebApi project:
json
Copy code
"ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;User Id=your_user;Password=your_password;"
}
Migrate the Database

Open a terminal and navigate to the Ambev.DeveloperEvaluation.ORM project directory.
Run the following command to apply migrations:
bash
Copy code
dotnet ef database update
Running the Application
Run the API

Open the solution in Visual Studio or your preferred IDE.
Set Ambev.DeveloperEvaluation.WebApi as the startup project.
Run the application (F5 in Visual Studio).
API Endpoints

The API will be available at http://localhost:5000/api/sales.
Testing the Application
Unit Tests

Navigate to the tests directory.
Run the tests using the following command:
bash
Copy code
dotnet test
Integration Tests

Ensure that your database is running and the schema is up to date.
Run integration tests similarly to unit tests to verify the API endpoints.
Usage
Use tools like Postman or cURL to interact with the API.
Example of creating a sale:
http
Copy code
POST /api/sales
Content-Type: application/json

{
    "SaleNumber": 1,
    "SaleDate": "2023-01-01T00:00:00Z",
    "Customer": "John Doe",
    "TotalSaleAmount": 100.00,
    "Branch": "Main Branch",
    "Items": [
        {
            "Product": "Product A",
            "Quantity": 2,
            "UnitPrice": 50.00,
            "Discount": 0.00
        }
    ],
    "IsCancelled": false
}
Contributing
Feel free to fork the repository and submit pull requests for any improvements or features.

License
This project is licensed under the MIT License - see the LICENSE file for details.


### Notes:
- Customize the repository URL, database connection string, and any other project-specific details to fit your actual setup.
- Ensure that any necessary packages (like Entity Framework Core) are included in the project dependencies.

If you need any further modifications or specific details, feel free to ask!