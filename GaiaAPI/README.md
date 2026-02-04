# Gaia Project - .NET Core Clean Architecture

**Project Code: A34D**

A robust, scalable platform for performing various operations (arithmetic, string manipulation, etc.) on two input fields using Clean Architecture, Repository Pattern, Entity Framework Core, and MS SQL Server.

## 🏗️ Architecture Overview

This project implements **Clean Architecture** with clear separation of concerns:

```
GaiaProject/
├── GaiaProject.Domain/          # Core business entities and interfaces
├── GaiaProject.Application/     # Business logic and use cases
├── GaiaProject.Infrastructure/  # Data access and external services
└── GaiaProject.API/            # Presentation layer (REST API)
```

### Layer Dependencies
- **Domain**: No dependencies (core business rules)
- **Application**: Depends on Domain
- **Infrastructure**: Depends on Application
- **API**: Depends on Application and Infrastructure

## 🎯 Features

### Core Features (Part A - Required)
- ✅ Extensible operation system (add/remove operations without code changes)
- ✅ RESTful API for external applications
- ✅ Repository Pattern with Generic Repository
- ✅ Unit of Work for transaction management
- ✅ Entity Framework Core with MS SQL Server
- ✅ Swagger UI for API documentation
- ✅ CORS enabled for frontend integration

### Bonus Features (Part B)
- ✅ Operation history tracking in database
- ✅ Display last 3 operations of same type
- ✅ Monthly operation count statistics
- ✅ Comprehensive logging
- ✅ Soft delete functionality

### Supported Operations

#### Arithmetic Operations
1. **Addition** - Adds two numbers
2. **Subtraction** - Subtracts second number from first
3. **Multiplication** - Multiplies two numbers
4. **Division** - Divides first number by second
5. **Modulo** - Returns remainder of division
6. **Power** - Raises first number to power of second

#### String Operations
7. **Concatenation** - Joins two strings
8. **Compare** - Compares two strings
9. **Contains** - Checks if first string contains second
10. **LengthCompare** - Compares lengths of two strings

## 🚀 Getting Started

### Prerequisites
- **.NET 8.0 SDK** or later
- **Microsoft SQL Server** (Express/Developer/Enterprise)
- **Visual Studio 2022** or **Visual Studio Code**
- **SQL Server Management Studio** (optional, for database management)

### Installation Steps

1. **Clone the repository**
   ```bash
   git clone <your-repo-url>
   cd GaiaProject
   ```

2. **Update Connection String**
   
   Edit `GaiaProject.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=GaiaProjectDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
   }
   ```

   **Connection String Examples:**
   - Local SQL Server: `Server=localhost;Database=GaiaProjectDb;Trusted_Connection=True;TrustServerCertificate=True;`
   - SQL Server with credentials: `Server=localhost;Database=GaiaProjectDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;`
   - SQL Server Express: `Server=localhost\\SQLEXPRESS;Database=GaiaProjectDb;Trusted_Connection=True;TrustServerCertificate=True;`

3. **Restore NuGet Packages**
   ```bash
   dotnet restore
   ```

4. **Build the Solution**
   ```bash
   dotnet build
   ```

5. **Run Database Migrations** (Optional - EnsureCreated is used by default)
   ```bash
   cd GaiaProject.API
   dotnet ef database update --project ../GaiaProject.Infrastructure
   ```

6. **Run the Application**
   ```bash
   cd GaiaProject.API
   dotnet run
   ```

7. **Access Swagger UI**
   
   Open browser: `https://localhost:5001` or `http://localhost:5000`

## 📡 API Endpoints

### Base URL
```
https://localhost:5001/api
```

### Operations Endpoints

#### 1. Get All Operations
```http
GET /api/operations
```
Returns all operations (including inactive).

#### 2. Get Active Operations
```http
GET /api/operations/active
```
Returns only active operations.

**Response Example:**
```json
[
  {
    "id": 1,
    "name": "Addition",
    "displayName": "Addition (+)",
    "description": "Adds two numbers together",
    "operationType": "Arithmetic",
    "isActive": true,
    "sortOrder": 1
  }
]
```

#### 3. Get Operation by ID
```http
GET /api/operations/{id}
```

#### 4. Execute Operation
```http
POST /api/operations/execute
Content-Type: application/json

{
  "operationName": "Addition",
  "fieldA": "10",
  "fieldB": "20"
}
```

**Response Example:**
```json
{
  "isSuccess": true,
  "result": "30",
  "errorMessage": null,
  "historyInfo": {
    "lastThreeOperations": [
      {
        "fieldA": "10",
        "fieldB": "20",
        "result": "30",
        "executedAt": "2026-02-02T10:30:00Z"
      }
    ],
    "monthlyOperationCount": 5
  }
}
```

#### 5. Create New Operation
```http
POST /api/operations
Content-Type: application/json

{
  "name": "CustomOperation",
  "displayName": "Custom Operation",
  "description": "Description of operation",
  "operationType": "Custom",
  "implementationClass": "CustomOperationExecutor",
  "sortOrder": 11
}
```

#### 6. Update Operation
```http
PUT /api/operations/{id}
Content-Type: application/json

{
  "name": "UpdatedOperation",
  "displayName": "Updated Display Name",
  "description": "Updated description",
  "operationType": "Arithmetic",
  "implementationClass": "UpdatedExecutor",
  "sortOrder": 1
}
```

#### 7. Delete Operation
```http
DELETE /api/operations/{id}
```
Performs soft delete (sets IsDeleted flag).

## 🗄️ Database Schema

### Operations Table
```sql
CREATE TABLE Operations (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    DisplayName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500),
    OperationType NVARCHAR(50) NOT NULL,
    ImplementationClass NVARCHAR(200) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    SortOrder INT NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2,
    IsDeleted BIT NOT NULL DEFAULT 0
)
```

### OperationHistories Table
```sql
CREATE TABLE OperationHistories (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OperationId INT NOT NULL,
    FieldA NVARCHAR(1000) NOT NULL,
    FieldB NVARCHAR(1000) NOT NULL,
    Result NVARCHAR(2000),
    IsSuccess BIT NOT NULL,
    ErrorMessage NVARCHAR(2000),
    ExecutedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ClientInfo NVARCHAR(500),
    CreatedAt DATETIME2 NOT NULL,
    UpdatedAt DATETIME2,
    IsDeleted BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (OperationId) REFERENCES Operations(Id)
)
```

## 🔧 Design Patterns Used

### 1. Clean Architecture
- Separation of concerns across layers
- Dependency inversion (inner layers don't depend on outer layers)

### 2. Repository Pattern
- Abstraction over data access
- Generic repository for common operations
- Specific repositories for custom queries

### 3. Unit of Work Pattern
- Transaction management
- Coordinates work of multiple repositories
- Ensures atomic operations

### 4. Strategy Pattern
- `IOperationExecutor` interface for different operation types
- Easy to add new operations without modifying existing code

### 5. Dependency Injection
- All dependencies injected through constructor
- Loose coupling between components

### 6. Factory Pattern (Implicit)
- Service collection resolves `IEnumerable<IOperationExecutor>`
- Automatic discovery of all executor implementations

## 🔌 Adding New Operations

To add a new operation without modifying existing code:

### 1. Create Operation Executor Class

```csharp
// In GaiaProject.Application/Operations/
public class SquareRootOperationExecutor : IOperationExecutor
{
    public string OperationType => "SquareRoot";

    public string Execute(string fieldA, string fieldB)
    {
        if (double.TryParse(fieldA, out var num))
        {
            return Math.Sqrt(num).ToString();
        }
        throw new InvalidOperationException("Field must be a valid number");
    }
}
```

### 2. Register in Program.cs

```csharp
builder.Services.AddScoped<IOperationExecutor, SquareRootOperationExecutor>();
```

### 3. Add to Database

```http
POST /api/operations
{
  "name": "SquareRoot",
  "displayName": "Square Root (√)",
  "description": "Calculates square root of first number",
  "operationType": "Arithmetic",
  "implementationClass": "SquareRootOperationExecutor",
  "sortOrder": 11
}
```

## 🧪 Testing the API

### Using Swagger UI
1. Navigate to `https://localhost:5001`
2. Expand endpoint sections
3. Click "Try it out"
4. Fill in parameters
5. Click "Execute"

### Using cURL

```bash
# Get all active operations
curl -X GET "https://localhost:5001/api/operations/active"

# Execute addition operation
curl -X POST "https://localhost:5001/api/operations/execute" \
  -H "Content-Type: application/json" \
  -d '{"operationName":"Addition","fieldA":"10","fieldB":"20"}'
```

### Using Postman
1. Import endpoints from Swagger JSON
2. Set base URL to `https://localhost:5001/api`
3. Test each endpoint

## 📊 Project Structure Details

```
GaiaProject/
│
├── GaiaProject.Domain/
│   ├── Entities/
│   │   ├── BaseEntity.cs              # Base entity with common properties
│   │   ├── Operation.cs               # Operation entity
│   │   └── OperationHistory.cs        # History tracking entity
│   └── Interfaces/
│       ├── IGenericRepository.cs      # Generic repo interface
│       ├── IOperationRepository.cs    # Operation-specific repo
│       ├── IOperationHistoryRepository.cs
│       
│
├── GaiaProject.Application/
│   ├── DTOs/
│   │   └── OperationDtos.cs           # Data transfer objects
│   ├── Interfaces/
│   │   ├── IOperationExecutor.cs      # Strategy pattern interface
│   │   └── IOperationService.cs       # Service interface
│   ├── Operations/
│   │   ├── ArithmeticOperations.cs    # Arithmetic executors
│   │   └── StringOperations.cs        # String executors
│   └── Services/
│       └── OperationService.cs        # Business logic implementation
│
├── GaiaProject.Infrastructure/
│   ├── Data/
│   │   └── GaiaDbContext.cs           # EF Core DbContext
│   └── Repositories/
│       ├── GenericRepository.cs       # Generic repo implementation
│       ├── OperationRepository.cs     # Operation repo
│       ├── OperationHistoryRepository.cs
│       
│
└── GaiaProject.API/
    ├── Controllers/
    │   └── OperationsController.cs    # REST API controller
    ├── Program.cs                      # Entry point & DI config
    ├── appsettings.json               # Configuration
    └── appsettings.Development.json
```

## 🎨 Best Practices Implemented

### Code Quality
- ✅ XML documentation comments
- ✅ Async/await throughout
- ✅ CancellationToken support
- ✅ Proper exception handling
- ✅ Logging at appropriate levels

### Security
- ✅ Soft delete instead of hard delete
- ✅ Input validation
- ✅ Error messages don't expose sensitive data
- ✅ CORS configuration

### Performance
- ✅ Async operations for I/O
- ✅ Database indexes on frequently queried fields
- ✅ Query filters for soft-deleted entities
- ✅ Efficient LINQ queries

### Maintainability
- ✅ Clear separation of concerns
- ✅ Single Responsibility Principle
- ✅ Open/Closed Principle (extensible operations)
- ✅ Dependency Injection
- ✅ Generic base classes to reduce code duplication

## 🔍 Code Comments
All classes and methods include the **A34D** comment as required by the project specifications.

## 📝 License
This is a development exercise project.

## 👤 Author
Developed as part of the Gaia Project software development exercise.

---

**Project Code: A34D** | Built with ❤️ using Clean Architecture
