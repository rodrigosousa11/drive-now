# DriveNow

DriveNow is a simple car rental management application built with ASP.NET Core MVC. It allows users to manage customers, vehicles, and rental contracts, including creating, editing, and deleting contracts while tracking vehicle availability.

## Features

- Manage Customers and Vehicles
- Create, edit, and delete rental contracts
- Ensure vehicle availability when creating rentals
- Validate rental dates and initial mileage
- Paginated listing of rental contracts
- Localized UI support

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server (or any EF-supported database)
- PagedList.Core for pagination
- Bootstrap for frontend styling

## Prerequisites

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (or other EF Core supported database)

## Getting Started

1. **Clone the repository**
    ```bash
    git clone https://github.com/rodrigosousa11/drive-now.git
    cd drive-now
    ```

2. **Update connection string**
    Open `appsettings.json` and update the connection string to point to your database:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DriveNow;Trusted_Connection=True;"
    }
    ```

3. **Apply migrations and create database**
    ```bash
    dotnet ef database update
    ```

4. **Run the application**
    ```bash
    dotnet watch
    ```

5. **Access the application**
    Open a browser and navigate to `http://localhost:5175/` (or the port shown in your console).

## Project Structure

- `Controllers/` – Controllers for handling requests.
- `Models/` – EF Core entity classes.
- `Views/` – Razor views for UI.
- `AppDbContext.cs` – Database context.
- `wwwroot/` – Static assets (CSS, JS, images).
- `Resources`- Translations for every view.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE) for details.
