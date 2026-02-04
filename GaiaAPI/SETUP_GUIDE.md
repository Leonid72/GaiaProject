# Gaia Project - Quick Setup Guide (A34D)

## Prerequisites Checklist
- [ ] .NET 8.0 SDK installed
- [ ] Visual Studio 2022 or VS Code installed
- [ ] SQL Server (Express/Developer/Enterprise) installed
- [ ] SQL Server Management Studio (optional)

## Step-by-Step Setup

### 1. Database Setup (Choose Option A or B)

#### Option A: Automatic (Recommended)
The application will automatically create the database on first run using `EnsureCreated()`.
Just make sure SQL Server is running and update the connection string.

#### Option B: Manual SQL Script
Run the `DatabaseInitialization.sql` script in SQL Server Management Studio:
1. Open SSMS
2. Connect to your SQL Server instance
3. Open `DatabaseInitialization.sql`
4. Execute the script (F5)

### 2. Update Connection String

Edit `GaiaProject.API/appsettings.json`:

**For Windows Authentication (Recommended for local development):**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GaiaProjectDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**For SQL Server Express:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=GaiaProjectDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**For SQL Authentication:**
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=GaiaProjectDb;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
```

### 3. Build and Run

**Using Visual Studio:**
1. Open `GaiaProject.sln`
2. Set `GaiaProject.API` as startup project
3. Press F5 to run

**Using Command Line:**
```bash
cd GaiaProject.API
dotnet restore
dotnet build
dotnet run
```

### 4. Test the Application

#### Option 1: Swagger UI
Open your browser to: `https://localhost:5001`

#### Option 2: Client UI
1. Open `ClientUI.html` in your browser
2. Make sure the API is running first
3. If you get CORS errors, the API might not be running or ports don't match

#### Option 3: Using cURL
```bash
# Get all operations
curl -k https://localhost:5001/api/operations/active

# Execute an operation
curl -k -X POST https://localhost:5001/api/operations/execute \
  -H "Content-Type: application/json" \
  -d '{"operationName":"Addition","fieldA":"10","fieldB":"20"}'
```

## Common Issues and Solutions

### Issue 1: Cannot connect to SQL Server
**Solution:**
- Verify SQL Server is running (Services → SQL Server)
- Check connection string
- Enable TCP/IP in SQL Server Configuration Manager
- Restart SQL Server service

### Issue 2: Trust Server Certificate Error
**Solution:**
Add `TrustServerCertificate=True;` to your connection string.

### Issue 3: CORS Error in Client UI
**Solution:**
- Ensure API is running
- Check that CORS is enabled in Program.cs (it is by default)
- Verify the API_BASE_URL in ClientUI.html matches your API port

### Issue 4: Port Already in Use
**Solution:**
Edit `GaiaProject.API/Properties/launchSettings.json` to change ports:
```json
"applicationUrl": "https://localhost:5002;http://localhost:5001"
```

### Issue 5: Migration Error
**Solution:**
The project uses `EnsureCreated()` instead of migrations by default. If you want to use migrations:
```bash
cd GaiaProject.API
dotnet ef migrations add InitialCreate --project ../GaiaProject.Infrastructure
dotnet ef database update
```

## Verifying Installation

After starting the API, you should see:
```
info: GaiaProject.API.Program[0]
      Database initialized successfully - A34D
```

### Test Basic Functionality

1. **Get Operations:**
   - Navigate to: `https://localhost:5001/api/operations/active`
   - You should see 10 operations (6 arithmetic + 4 string operations)

2. **Execute Operation:**
   - POST to: `https://localhost:5001/api/operations/execute`
   - Body:
     ```json
     {
       "operationName": "Addition",
       "fieldA": "5",
       "fieldB": "3"
     }
     ```
   - Expected result: `"result": "8"`

## Project Structure

```
GaiaProject/
├── GaiaProject.Domain/          # Entities and interfaces
├── GaiaProject.Application/     # Business logic
├── GaiaProject.Infrastructure/  # Data access (EF Core)
├── GaiaProject.API/            # Web API
├── ClientUI.html               # Test client
├── DatabaseInitialization.sql  # Manual DB setup
└── README.md                   # Full documentation
```

## Next Steps

1. ✅ Verify all operations work through Swagger
2. ✅ Test the Client UI
3. ✅ Add custom operations (see README.md)
4. ✅ Review operation history in database
5. ✅ Explore the code structure

## Support

For issues or questions:
1. Check the full README.md
2. Review code comments (all marked with A34D)
3. Check SQL Server error logs
4. Review API logs in console output

---

**Project Code: A34D**
Built with Clean Architecture + Repository Pattern + EF Core + MS SQL Server
