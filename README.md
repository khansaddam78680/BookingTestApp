# 🏷️ Booking System API

A .NET 6 Web API for managing bookings with CSV import capabilities.

## 🛠️ Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) (for API testing)
- Swagger is added so we can directly check API

## 🚀 Getting Started

```bash
# Clone the repository
https://github.com/khansaddam78680/BookingTestApp.git
cd BookingTestApp

# Build the application
dotnet build

# Run the API
dotnet run --project BookingTestApp

# Run the tests
dotnet test
```

## Notes
- In AutoSeedData folder we have members.csv and inventory.csv which is added to the solution as a part of creating tables and seeding data to created tables.
- After app runs we can directly check the API endpoint to get all pre-added members and inventory. We also have endpoint to check all the bookings.

```bash
# Endpoint to check all members
api/data/getAllMembers

# Endpoint to check all inventory items
api/data/getAllInventory

# Endpoint to check all members
api/data/getAllBookings
```

Use above endpoints to directly test the app.

## Deployment

```bash
# Publish the Application
dotnet publish -c Release -o ./publish
```

After the publish we can deploye it directly on IIS or we can also integrate docker or other cloud technologies for easier deployment.
