# DestinyAPI

A REST API built with ASP.NET Core and themed on the game *Destiny*. It provides full CRUD operations for **Factions**, **Weapons** and **Characters**, stored in a MySQL database through Entity Framework Core.

The API is implemented twice, once in each common ASP.NET Core style:

| Project | Framework | Style |
|---|---|---|
| [`DestinyAPI`](DestinyAPI/) | .NET 8 | Controller-based API |
| [`DestinyAPI.Minimal`](DestinyAPI.Minimal/) | .NET 9 | Minimal API (endpoints mapped in `Program.cs`) |

## Data model

```
Faction 1 ──── * Weapon 1 ──── * Character
```

| Entity | Fields |
|---|---|
| **Faction** | `Id`, `Name` (max 50, required), `Description` (max 256, optional) |
| **Weapon** | `Id`, `Name` (max 50, required), `Damage` (decimal, 0–999.99), `FactionId` (FK → Faction) |
| **Character** | `Id`, `Name` (max 50, required), `WeaponId` (FK → Weapon) |

## Architecture

- **Repository pattern.** Each entity has an interface (`IFactionRepository`, `IWeaponRepository`, `ICharacterRepository`) with two implementations:
  - `Ef*Repository` stores data in MySQL through Entity Framework Core.
  - `InMemory*Repository` stores data in an in-memory list, for testing without a database.
- **Dependency injection.** The `useInMemory` flag in `Program.cs` chooses which implementation is registered. It is `false` by default, so the database is used.
- **Swagger / OpenAPI.** Swashbuckle generates interactive API documentation.

## Tech stack

- ASP.NET Core (.NET 8 / .NET 9)
- Entity Framework Core 8 (code-first migrations)
- MySQL with [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- Swashbuckle (Swagger UI)

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download) (and the .NET 9 SDK for the Minimal project)
- A running MySQL server
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### 1. Configure the database

The connection string is in `appsettings.json` in each project:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=DestinyDb;user=root;password=root"
}
```

Change the user and password to match your MySQL setup.

### 2. Create the database

The migrations are in the `DestinyAPI` project. Run this from the repository root:

```bash
dotnet ef database update --project DestinyAPI
```

This creates the `DestinyDb` database with the `Factions`, `Weapons` and `Characters` tables. Both projects use the same database.

### 3. Run the API

```bash
# Controller-based version: Swagger at http://localhost:5015/swagger
dotnet run --project DestinyAPI

# Minimal API version: Swagger at http://localhost:5208/
dotnet run --project DestinyAPI.Minimal
```

You can also open `DestinyAPI.sln` in Visual Studio and press F5.

> To try the API without MySQL, set `useInMemory = true` in `Program.cs`. Data is then kept in memory and lost when the app stops.

## Endpoints

Each resource has the same set of endpoints:

| Method | Route | Description |
|---|---|---|
| `GET` | `/api/{resource}` | Get all items |
| `GET` | `/api/{resource}/{id}` | Get one item by id |
| `POST` | `/api/{resource}` | Create an item |
| `PUT` | `/api/{resource}/{id}` | Update an item (the id in the URL must match the body) |
| `DELETE` | `/api/{resource}/{id}` | Delete an item |

`{resource}` is `factions`, `weapons` or `characters`.

### Example: create a weapon

```http
POST /api/weapons
Content-Type: application/json

{
  "name": "Gjallarhorn",
  "damage": 450.00,
  "factionId": 1
}
```

Because of the foreign keys, create a **Faction** first, then a **Weapon** that references it, then a **Character** that references the weapon.

## Project structure

```
DestinyAPI/
├── DestinyAPI/                 # Controller-based API (.NET 8)
│   ├── Controllers/            # Factions, Weapons, Characters controllers
│   ├── Data/                   # DbContext + design-time factory
│   ├── Migrations/             # EF Core migrations
│   ├── Models/                 # Faction, Weapon, Character
│   └── Repositories/           # Interfaces + EF and in-memory implementations
├── DestinyAPI.Minimal/         # Minimal API (.NET 9)
│   ├── Data/
│   ├── Models/
│   ├── Repositories/
│   └── Program.cs              # All endpoints are defined here
└── DestinyAPI.sln
```

---

School project for **API ontwikkeling**, AP Hogeschool Antwerpen.
