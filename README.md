# 🌌 Gaia Project: Full-Stack Operations Platform  
**Project Code:** A34D  

Gaia Project is a robust full-stack solution for performing and managing mathematical and string-based operations.  
It combines a modern **Angular 19** frontend with a **.NET 8** backend built on **Clean Architecture**.

---

## 🏗️ Architecture & Features

### System Components
- **Frontend (GaiaClient):** Reactive Angular 19 app using **Signals** for state management, with a responsive purple-themed UI.
- **Backend (GaiaServer):** .NET 8 Web API using **Repository Pattern**, **Unit of Work**, and **Strategy Pattern** for extensible operations.

### Key Features
- **Dynamic Operations:** Execute arithmetic (Addition, Power, etc.) and string operations (Concatenation, Comparison).
- **Real-time Tracking:** View the **last 3 operations** and **monthly usage statistics** immediately after a calculation.
- **Operation Management:** Toggle the active status of operations via a dedicated management dashboard.
- **Data Integrity:** Soft-delete functionality and comprehensive logging.

---

## 🛠️ Tech Stack

| Layer | Technology |
|------|------------|
| Frontend | Angular 19.2.0, RxJS 7.8.0, Signals, ngx-toastr |
| Backend | .NET 8.0, EF Core, MS SQL Server |
| Patterns | Clean Architecture, Strategy Pattern, Repository / Unit of Work |
| API | RESTful API with Swagger UI documentation |

---

## 🏁 Installation & Setup

### 1) Backend (.NET)
1. Navigate to: `GaiaProject.API/appsettings.json`  
2. Update the **Connection String** to your local SQL Server instance.  
3. Run migrations (optional if DB is already created):

```bash
dotnet ef database update --project ../GaiaProject.Infrastructure
```

4. Start the API:

```bash
dotnet run
```

---

### 2) Frontend (Angular)
1. Navigate to the **GaiaClient** folder  
2. Install dependencies:

```bash
npm install
```

3. Configure API base URL in `src/environments/environment.ts` to point to your .NET API  
4. Launch the app:

```bash
npm start
```

---

## 📡 API Endpoints Summary

- `GET /api/operations/active` — Returns operations for the calculator dropdown  
- `POST /api/operations/execute` — Performs a calculation and records history  
- `GET /api/operations` — Administrative view of all operations  
- `DELETE /api/operations/{id}` — Soft-deletes a specific operation  

---

## 📁 Project Structure

### Backend
- `GaiaProject.Domain` — Core business entities and interfaces  
- `GaiaProject.Application` — DTOs, Services, Operation Executors  
- `GaiaProject.Infrastructure` — DB Context and Repository implementations  
- `GaiaProject.API` — Web API (Controllers, Swagger, DI setup)

### Frontend
- `src/app/pages/calculator` — Main UI for performing operations  
- `src/app/pages/manage-operations` — Management UI for toggling operation statuses  

---

**Project Code:** A34D  
Developed as part of the Gaia Project software development exercise.

https://youtu.be/Dj-lqh7Nqqw
