# Stock API

This project is a .NET Core-based API designed to manage stock data, user portfolios, and comments related to various stocks. The API provides a secure, scalable, and efficient solution to handle stock-related information, including user authentication and authorization using JWT.

## Features

- **User Authentication & Authorization:** Implements JWT (JSON Web Tokens) for secure user authentication and role-based access control.
- **Stock Management:** Allows CRUD operations on stock data, including symbol, company name, industry, market cap, and more.
- **Portfolio Management:** Enables users to manage their stock portfolios.
- **Comment System:** Users can leave comments on specific stocks.
- **Database Integration:** Utilizes **SQL Server** and **Entity Framework Core** for database management.
- **Swagger Integration:** API documentation and testing using Swagger with JWT token support.
- **JSON Serialization:** Uses Newtonsoft.Json for handling JSON serialization, **ensuring no reference loops**.

## Technologies Covered

- **.NET Core:** The core framework used to build the API.
- **Entity Framework Core:** ORM used to interact with SQL Server.
- **SQL Server:** Database management system for storing data.
- **ASP.NET Identity:** For handling user authentication and authorization.
- **JWT (JSON Web Tokens):** Used for secure user authentication and authorization.
- **Swagger:** API documentation and testing.
- **Newtonsoft.Json:** For JSON serialization and deserialization.


## Getting Started

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)


### Installation

1. Clone the repository:
   ```bash
   git clone git@github.com:mohamedhassan218/stock-api.git
   ```
2. Navigate to the project directory:
   ```bash
   cd API
   ```
3. Restore the dependencies:
   ```bash
   dotnet restore
   ```
4. Update the `appsettings.json` with your SQL Server connection string:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Your SQL Server connection string here"
   }
   ```
5. Apply the migrations to set up the database:
   ```bash
   dotnet ef database update
   ```
6. Run the application:
   ```bash
   dotnet run
   ```


### API Documentation

Once the application is running, you can access the Swagger UI for API documentation and testing at:

```
http://localhost:5111/swagger/index.html
```


### Thank You

Special thanks to *Teddy Smith* for his excellent **[playlist](https://www.youtube.com/playlist?list=PL82C6-O4XrHfrGOCPmKmwTO7M0avXyQKc)** which guided me throughout this project. His teachings on .NET Core and building RESTful APIs were invaluable.