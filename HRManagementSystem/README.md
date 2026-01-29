# HR Management System

ASP.NET Core MVC HR management app with CRUD screens for **Positions**, **Employees**, **Departments**, **Jobs**, and **Job Groups**.  
Data is stored in a local **SQLite** database and the UI uses **Telerik/Kendo UI for ASP.NET Core** components.

## Tech stack

- **.NET**: `net8.0`
- **Web**: ASP.NET Core MVC (Controllers + Razor Views)
- **Data**: EF Core (`Microsoft.EntityFrameworkCore.Sqlite`)
- **Database**: SQLite file `HRManagementSystem/hr_database.db`
- **UI**: `Telerik.UI.for.AspNet.Core` (Kendo UI)

## Prerequisites

- **.NET SDK 8** (see `global.json`)
- (Optional) **Node.js + npm** (only needed if you run the licensing helper in `HRManagementSystem/package.json`)

## Quick start (CLI)

From the repo root:

```bash
dotnet restore
dotnet run --project HRManagementSystem/HRManagementSystem.csproj
```

Then open:

- `http://localhost:5147`
- `https://localhost:7211`

Default route is configured to:

- `/Positions/Index`

## Database behavior (important)

On startup the app will:

- **Create the SQLite database if missing** (`EnsureCreated`)
- **Seed demo data once** (if `Departments` already exist, seeding is skipped)

The database file lives at:

- `HRManagementSystem/hr_database.db`

### Reset the database

Stop the app, delete the DB file, then run again:

```bash
rm -f HRManagementSystem/hr_database.db
dotnet run --project HRManagementSystem/HRManagementSystem.csproj
```

## Running from Rider / Visual Studio

- Open `HRManagementSystem.sln`
- Run the `HRManagementSystem` project
- Launch profiles are defined in `HRManagementSystem/Properties/launchSettings.json` (`http` / `https`)

## Entity Framework migrations

This repo already contains an initial migration in `HRManagementSystem/Migrations/`.

If you want to create/apply migrations via CLI, install the EF tool and run commands like:

```bash
dotnet tool install --global dotnet-ef

# Add a migration
dotnet ef migrations add <MigrationName> \
  --project HRManagementSystem/HRManagementSystem.csproj \
  --startup-project HRManagementSystem/HRManagementSystem.csproj

# Apply migrations
dotnet ef database update \
  --project HRManagementSystem/HRManagementSystem.csproj \
  --startup-project HRManagementSystem/HRManagementSystem.csproj
```

## Telerik / Kendo licensing notes

The project references `Telerik.UI.for.AspNet.Core`. Depending on your environment, you may need to configure a valid Telerik/Kendo license to run without warnings.

There is also an npm dependency in `HRManagementSystem/package.json`:

- `@progress/kendo-licensing`

If you run into licensing messages during build/run, follow your organization’s Telerik/Kendo licensing setup.

## Troubleshooting

### `dotnet restore` fails (NuGet / Telerik feed)

- **Network/proxy**: make sure you can reach `nuget.org` and (if configured) `nuget.telerik.com`.
- **Telerik feed auth**: if your environment requires credentials for Telerik packages, ensure your `NuGet.config` is set up correctly for your machine/CI.
- **Offline environments**: restore/build requires packages unless they’re already cached locally.

## Project structure

```
.
├─ HRManagementSystem.sln
└─ HRManagementSystem/
   ├─ Controllers/          # MVC controllers (CRUD endpoints for Kendo grids)
   ├─ Data/                 # EF DbContext + DatabaseSeeder
   ├─ Models/               # Entity models
   ├─ Views/                # Razor pages (Kendo UI grids)
   ├─ wwwroot/              # Static assets
   ├─ appsettings.json       # Connection string (SQLite)
   └─ hr_database.db         # Local SQLite DB (created/seeded at runtime)
```

## Repo hygiene (recommended)

Build outputs and dependencies typically shouldn’t be committed:

- `**/bin/`, `**/obj/`
- `**/node_modules/`
- `.idea/`

If you see these in `git status`, it’s usually safe to add them to `.gitignore` for a cleaner repo.

