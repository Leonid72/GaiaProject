# Gaia Project API Documentation (A34D)

## Base URL
```
Development: https://localhost:5001/api
Production: https://your-domain.com/api
```

## Authentication
Currently, the API does not require authentication. In production, consider implementing JWT authentication.

---

## Endpoints

### 1. Get All Operations

Retrieves all operations including inactive ones.

**Endpoint:** `GET /api/operations`

**Response:**
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

**Status Codes:**
- `200 OK`: Success

---

### 2. Get Active Operations

Retrieves only active operations, sorted by sortOrder.

**Endpoint:** `GET /api/operations/active`

**Response:**
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
  },
  {
    "id": 2,
    "name": "Subtraction",
    "displayName": "Subtraction (-)",
    "description": "Subtracts the second number from the first",
    "operationType": "Arithmetic",
    "isActive": true,
    "sortOrder": 2
  }
]
```

**Status Codes:**
- `200 OK`: Success

**Use Case:**
Use this endpoint to populate dropdown menus in client applications.

---

### 3. Get Operation by ID

Retrieves a specific operation by its ID.

**Endpoint:** `GET /api/operations/{id}`

**Parameters:**
- `id` (path, required): Operation ID

**Example:** `GET /api/operations/1`

**Response:**
```json
{
  "id": 1,
  "name": "Addition",
  "displayName": "Addition (+)",
  "description": "Adds two numbers together",
  "operationType": "Arithmetic",
  "isActive": true,
  "sortOrder": 1
}
```

**Status Codes:**
- `200 OK`: Success
- `404 Not Found`: Operation not found

---

### 4. Execute Operation ⭐ Main Feature

Executes an operation with given input values and returns the result with history information.

**Endpoint:** `POST /api/operations/execute`

**Request Body:**
```json
{
  "operationName": "Addition",
  "fieldA": "10",
  "fieldB": "20"
}
```

**Request Fields:**
- `operationName` (string, required): Name of the operation to execute
- `fieldA` (string, required): First input value
- `fieldB` (string, required): Second input value

**Response (Success):**
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
      },
      {
        "fieldA": "5",
        "fieldB": "15",
        "result": "20",
        "executedAt": "2026-02-02T10:25:00Z"
      }
    ],
    "monthlyOperationCount": 15
  }
}
```

**Response (Failure):**
```json
{
  "isSuccess": false,
  "result": "",
  "errorMessage": "Both fields must be valid numbers for addition",
  "historyInfo": null
}
```

**Status Codes:**
- `200 OK`: Success (check isSuccess field for operation result)
- `400 Bad Request`: Invalid request

**Operation-Specific Examples:**

#### Arithmetic Operations

**Addition:**
```json
Request: { "operationName": "Addition", "fieldA": "5", "fieldB": "3" }
Result: "8"
```

**Division:**
```json
Request: { "operationName": "Division", "fieldA": "10", "fieldB": "2" }
Result: "5"

Error Case: { "operationName": "Division", "fieldA": "10", "fieldB": "0" }
Error: "Cannot divide by zero"
```

**Power:**
```json
Request: { "operationName": "Power", "fieldA": "2", "fieldB": "3" }
Result: "8"
```

#### String Operations

**Concatenation:**
```json
Request: { "operationName": "Concatenation", "fieldA": "Hello", "fieldB": "World" }
Result: "HelloWorld"
```

**Contains:**
```json
Request: { "operationName": "Contains", "fieldA": "Hello World", "fieldB": "World" }
Result: "True"
```

**Replace:**
```json
Request: { "operationName": "Replace", "fieldA": "Hello World", "fieldB": "World|Universe" }
Result: "Hello Universe"
Note: FieldB format is "oldValue|newValue"
```

---

### 5. Create New Operation

Creates a new operation in the system.

**Endpoint:** `POST /api/operations`

**Request Body:**
```json
{
  "name": "SquareRoot",
  "displayName": "Square Root (√)",
  "description": "Calculates the square root of the first number",
  "operationType": "Arithmetic",
  "implementationClass": "SquareRootOperationExecutor",
  "sortOrder": 11
}
```

**Request Fields:**
- `name` (string, required): Unique operation name (no spaces)
- `displayName` (string, required): User-friendly name
- `description` (string, optional): Operation description
- `operationType` (string, required): Category (Arithmetic, String, Logical, etc.)
- `implementationClass` (string, required): Executor class name
- `sortOrder` (int, required): Display order

**Response:**
```json
{
  "id": 11,
  "name": "SquareRoot",
  "displayName": "Square Root (√)",
  "description": "Calculates the square root of the first number",
  "operationType": "Arithmetic",
  "isActive": true,
  "sortOrder": 11
}
```

**Status Codes:**
- `201 Created`: Success
- `400 Bad Request`: Validation error or duplicate name

**Note:** The implementation class must exist and be registered in the DI container.

---

### 6. Update Operation

Updates an existing operation.

**Endpoint:** `PUT /api/operations/{id}`

**Parameters:**
- `id` (path, required): Operation ID to update

**Request Body:**
```json
{
  "name": "Addition",
  "displayName": "Addition Plus (+)",
  "description": "Adds two numbers together - Updated",
  "operationType": "Arithmetic",
  "implementationClass": "AdditionOperationExecutor",
  "sortOrder": 1
}
```

**Response:**
```json
{
  "id": 1,
  "name": "Addition",
  "displayName": "Addition Plus (+)",
  "description": "Adds two numbers together - Updated",
  "operationType": "Arithmetic",
  "isActive": true,
  "sortOrder": 1
}
```

**Status Codes:**
- `200 OK`: Success
- `404 Not Found`: Operation not found
- `400 Bad Request`: Validation error

---

### 7. ChangeStatus Operation

Change Status an operation (sets isActive flag).

**Endpoint:** `/api/Operations/{id}/status`

**Parameters:**
- `id` (path, required): Operation ID 

**Response:** No content

**Status Codes:**
- `204 No Content`: Success
- `404 Not Found`: Operation not found

**Note:** This is a change status.

---

## Error Handling

All endpoints return consistent error responses:

```json
{
  "message": "Error description here"
}
```

### Common Error Codes

| Status Code | Meaning |
|------------|---------|
| 200 | Success |
| 201 | Resource created |
| 204 | Success with no content |
| 400 | Bad request / Validation error |
| 404 | Resource not found |
| 500 | Internal server error |

---

## Usage Examples

### cURL Examples

**Get active operations:**
```bash
curl -X GET "https://localhost:5001/api/operations/active" \
  -H "accept: application/json"
```

**Execute operation:**
```bash
curl -X POST "https://localhost:5001/api/operations/execute" \
  -H "Content-Type: application/json" \
  -d '{
    "operationName": "Addition",
    "fieldA": "10",
    "fieldB": "20"
  }'
```

**Create operation:**
```bash
curl -X POST "https://localhost:5001/api/operations" \
  -H "Content-Type: application/json" \
  -d '{
    "name": "CustomOp",
    "displayName": "Custom Operation",
    "description": "A custom operation",
    "operationType": "Custom",
    "implementationClass": "CustomOperationExecutor",
    "sortOrder": 11
  }'
```

### JavaScript/Fetch Examples

**Get operations:**
```javascript
fetch('https://localhost:5001/api/operations/active')
  .then(response => response.json())
  .then(data => console.log(data));
```

**Execute operation:**
```javascript
fetch('https://localhost:5001/api/operations/execute', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    operationName: 'Addition',
    fieldA: '10',
    fieldB: '20'
  })
})
  .then(response => response.json())
  .then(data => {
    if (data.isSuccess) {
      console.log('Result:', data.result);
      console.log('Monthly count:', data.historyInfo.monthlyOperationCount);
    } else {
      console.error('Error:', data.errorMessage);
    }
  });
```

### C# HttpClient Example

```csharp
using System.Net.Http.Json;

var client = new HttpClient { BaseAddress = new Uri("https://localhost:5001") };

// Execute operation
var request = new {
    operationName = "Addition",
    fieldA = "10",
    fieldB = "20"
};

var response = await client.PostAsJsonAsync("/api/operations/execute", request);
var result = await response.Content.ReadFromJsonAsync<OperationExecuteResponseDto>();

if (result.IsSuccess)
{
    Console.WriteLine($"Result: {result.Result}");
}
```

---

## Rate Limiting

Currently, there is no rate limiting. Consider implementing rate limiting in production:
- Recommended: 100 requests per minute per IP
- Use AspNetCoreRateLimit NuGet package

---

## CORS Configuration

The API allows all origins in development. Update CORS policy for production:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://your-domain.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
```

---

## Testing with Swagger

1. Navigate to `https://localhost:5001`
2. Swagger UI will open automatically
3. Expand any endpoint
4. Click "Try it out"
5. Fill in parameters
6. Click "Execute"
7. View response

---

## History Information (Bonus Feature)

The execute operation endpoint returns history information:

**lastThreeOperations:**
- Shows the last 3 successful operations of the same type
- Ordered by execution time (newest first)
- Includes input values, result, and timestamp

**monthlyOperationCount:**
- Total count of operations of this type executed in the current month
- Resets at the start of each month
- Useful for analytics

---

## Data Types

### OperationDto
```typescript
{
  id: number,
  name: string,
  displayName: string,
  description: string,
  operationType: string,
  isActive: boolean,
  sortOrder: number
}
```

### OperationExecuteRequestDto
```typescript
{
  operationName: string,
  fieldA: string,
  fieldB: string
}
```

### OperationExecuteResponseDto
```typescript
{
  isSuccess: boolean,
  result: string,
  errorMessage: string | null,
  historyInfo: {
    lastThreeOperations: Array<{
      fieldA: string,
      fieldB: string,
      result: string,
      executedAt: string
    }>,
    monthlyOperationCount: number
  } | null
}
```

---

## Project Code: A34D
Complete API documentation for the Gaia Project operation platform.
