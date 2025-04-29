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

## Deployment

```bash
# Publish the Application
dotnet publish -c Release -o ./publish
```

After the publish we can deploye it directly on IIS or we can also integrate docker or other cloud technologies for easier deployment.
