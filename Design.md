# Sports Statistics

## Overview

I was hired by the Bytechester Rovers footbal club to build a software application that will keep track of their teams statistics. 
They want to use C# and Blazor to deliver the product.
They also need the ability to show data charts to help their coaches visualize the players performances.

### Requirements

- [ ] This is an application that will track and generate reports about a sports team's performance.
- [ ] The app will have a page divided two areas: The UI where the in-game data will be tracked and an area showing the current statistics.
- [ ] The app will have a reports area in a different page showing the players statistics across multiple games. Coaches should be able to see detailed players information per game and per season. This area should contain barcharts with the players performance.
- [ ] The UI needs to contain a list of players with 5 parameters that will be tracked (i.e. passes, shots, tackles, dribbles, headers).
- [ ] Data should be tracked with the click of a button. (i.e. a pass button clicked on John Smith's row will track a pass at a given time in the game).
- [ ] The reports area should be updated immediately upon a button being clicked.

### Additional Requirements

- [ ] Admin area to manage the team (current players and fixtures/games).
- [ ] Authentication and authorization (Roles: Administator, Viewer)

##  High-Level Design

### 1. **Frontend (Blazor Server)**
- Role-based UI rendering using [AuthorizeView] in Blazor  
- Secure routing (e.g. `/admin` only accessible to Admins)
**Pages/Components**
- **Match Tracker Page**  
  - Player list grid with live buttons (passes, shots, etc.)  
  - Real-time statistics panel  
- **Reports Page**  
  - Game and season selector  
  - Interactive charts (bar charts per player and game)  
  - Detailed player breakdown panel  
- **Admin Area**  
  - **Team Management**
    - Add/edit/remove players  
    - Assign player positions
  - **Fixture/Game Management**
    - Schedule fixtures  
    - Update game details (opponent, date, lineup)

### 2. **Backend Services (C# .NET Core)**
**Core Responsibilities**
- Handle tracking inputs and timestamp actions  
- Aggregate and serve player statistics  
- Sync real-time updates to the frontend  
- Save and retrieve historical data per match/season
- Admin-specific APIs for CRUD operations on Players and Fixtures  
- Ensure audit tracking if needed (who made changes, when)
- Integrate **ASP.NET Core Identity** for user management  
- Extend default `IdentityUser` with a linked profile or reference to `Coach` or `TeamStaff` if needed  
- Define roles:  
  - **Administrator**: Full access to tracker, reports, and admin tools  
  - **Viewer**: Read-only access to reports/statistics

### 3. **Database Layer**
** SQL Server
- `UserRoles` (tied to Identity)  
- Extend `Game` and `Player` entities to be admin-manageable  

**Data Models**
- `Player` (name, position, ID)  
- `MatchEvent` (player ID, match ID, type, timestamp)  
- `Game` (date, opponent, match ID)  
- `Statistics` (aggregated data by player and game)

### 4. **Charting Library**
**Recommended**: ChartJs, ApexCharts, or Telerik UI for Blazor

**Purpose**
- Render dynamic bar charts instantly upon stat changes  
- Support filtering by player, match, or stat type

### 5. **Data Sync Logic**
- Button clicks trigger async service calls  
- Server processes and stores data  
- Statistics UI and charts re-render via signalR

---

# Low-Level Design Document  
**Project**: Bytechester Rovers � Team Statistics Tracker  
**Technology Stack**: C#, ASP.NET Core, Blazor, Charting Library  
**Author**: Chris Carter
**Date**: 2025-07-04
**Version**: 0.1

---

## 1. Purpose  

This Low-Level Design (LLD) document provides a detailed blueprint for the implementation of a football statistics tracking application commissioned by Bytechester Rovers FC. The primary goal is to deliver a responsive, real-time system that captures match data and visualizes individual and team performance for coaches, analysts, and stakeholders.

The application will be developed using C# and Blazor, leveraging modern component-driven architecture and a clean, modular codebase. Key features include:

- Real-time in-game tracking of player statistics via an intuitive UI.
- Dynamic reporting and visualization of performance data across fixtures and seasons.
- Role-based authentication and authorization to support administrators and viewers.
- Administrative tools to manage players, fixtures, and system data.

This document outlines the internal structure, component responsibilities, data flows, and integration patterns required to achieve a scalable, maintainable, and user-centric solution.

---

## 2. System Overview

### Authentication

| Component | Description |
|-----------|-------------|
| **User Login Service** | Handles credential validation and token issuance upon successful login. Interfaces with the identity store and supports session persistence. |
| **Role Management** | Defines and maps user roles such as Admin, Coach, Analyst, and Viewer. Manages role assignment and retrieval logic. |
| **Authorization Middleware** | Intercepts navigation and data access requests to enforce role-based policies. Ensures restricted views and actions are properly protected. |
| **Identity Store (User DB)** | Stores user profiles, hashed credentials, role bindings, and login history. Backed by a secure data access layer. |
| **Password Recovery & Reset** | Supports recovery workflows via email or system admin reset. Ensures secure credential updates. |
| **Registration Workflow** | Allows onboarding of new users via controlled registration or invitation codes. May include email verification and role pre-assignment. |

### Administration

| Component | Description |
|-----------|-------------|
| **Player Management Service** | Enables creation, editing, and archival of player profiles, including stats, positions, and availability status. |
| **Fixture Scheduler & Editor** | Supports the planning and editing of fixtures with venue, time, opponent, and status tracking. Includes bulk upload/import capabilities. |
| **User Administration Panel** | Allows admins to manage system users, assign or modify roles, and deactivate accounts when needed. |
| **Data Integrity Validator** | Performs pre-submit checks to ensure input data for players, fixtures, and users adheres to system rules. |
| **Audit & History Tracker** | Logs administrative actions and changes for transparency and rollback capabilities. |
| **Bulk Operations Handler** | Optimizes processing for batch operations like importing player data or updating fixture statuses. |
| **Admin Dashboard UI** | A dedicated interface presenting key management tools, shortcuts, and system health indicators for ease of operation. |

### Match Tracker

| Component | Description |
|-----------|-------------|
| **Live Match Console UI** | An interactive interface for recording match events, player actions, and team metrics in real time. Optimized for speed and usability. |
| **Event Entry Service** | Handles the creation and categorization of match events (goals, assists, tackles, substitutions, etc.) with timestamp and context. |
| **Player Action Logger** | Tracks individual player contributions with positional and event metadata. Syncs directly with live stats view. |
| **Game Timeline Generator** | Builds and updates a visual timeline of match progress and key moments, accessible during and after the game. |
| **Real-Time Sync Engine** | Ensures updates propagate instantly across connected sessions; allowing coaches and analysts to view stats live from multiple devices. |
| **Undo & Correction Flow** | Supports quick edits and rollback of accidental entries with audit logs for transparency. |
| **Match Metadata Handler** | Captures fixture info, referee details, pitch condition, and kickoff parameters as contextual data for analysis. |
| **Session Save & Resume** | Allows sessions to be paused and resumed, preserving in-game progress even under connectivity interruptions. |

### Reports

| Component | Description |
|-----------|-------------|
| **Data Aggregation Engine** | Consolidates raw match data into structured player and team metrics, such as goals, assists, pass accuracy, and minutes played. |
| **Charting & Visualization Service** | Renders interactive graphs using libraries like Chart.js or Plotly for performance trends, comparisons, and KPIs. |
| **Filter & Segmentation Controls** | Allows users to drill down by player, position, fixture, or season; supporting targeted analysis and exploration. |
| **Performance Summary View** | Displays key stats in concise dashboards with rankings, averages, and benchmarks per player or team. |
| **Comparative Analysis Tool** | Enables side-by-side comparison between players, matches, or seasons, highlighting differences and progress. |
| **Export & Sharing Options** | Provides download formats for PDF, CSV, or image snapshots of reports for external use or printing. |
| **Access Control Hooks** | Ensures report visibility adheres to user roles (e.g. analysts vs. coaches vs. viewers). |
| **Historical Data Renderer** | Visualizes trends over time to assess improvement, consistency, or performance dips across the season. |

---

## 3. Component Design  
Describe each module's internal components:
- **Component Name**  
  - *Function*: Brief description.  
  - *Inputs*: Props, parameters, or data received.  
  - *Outputs*: Events, rendered data, or state changes.  
  - *Interactions*: Other components/services it communicates with.

Repeat per component.

---

## 4. Class Diagram & Data Models  
Include (or link to) detailed class definitions:
- **Player**  
  > Properties: ID, Name, Position, etc.  
- **MatchEvent**  
  > Records timestamped actions like passes, shots...

Use diagrams or nested bullet points for relationships.

---

## 5. Sequence Diagrams  
Describe system behavior for key operations:
- *Tracking a stat (e.g., a pass)*  
- *Rendering updated statistics*  
- *Loading a report*  
Use simplified flow diagrams or markdown sequences.

---

## 6. Authentication and Authorization  
Explain identity setup:
- Roles: Admin, Viewer  
- Role-based page access (e.g., `/admin` restricted to Admins)  
- Identity integration and linked user data

---

## 7. UI/UX Structure  
Define component layouts and logic:
- Component tree for each page  
- Key interactions and data bindings  
- Conditional rendering (e.g., roles, dynamic states)

---

## 8. Chart Integration  
Explain how data connects to charting:
- Chart types used (e.g., bar chart per player)  
- Trigger logic for updates  
- Handling filters and drill-downs

---

## 9. Database Schema  
Outline major tables and relationships:
- ER diagram (if available)  
- Table: Players, Matches, Events, Stats, Users  
Include foreign key links and indexing strategy if applicable

---

## 10. API Endpoints  
List internal service endpoints:
- `POST /api/event` � Tracks a new match action  
- `GET /api/player/{id}/stats` � Fetch player report  
Document inputs, outputs, status codes.

---

## 11. Error Handling & Logging  
Define failure scenarios and recovery logic:
- Retry policies  
- Logging strategy  
- UI feedback for failed actions

---

## 12. Testing Strategy  
How modules/components will be tested:
- Unit tests  
- Integration tests  
- Mocked data for frontend rendering  

---

## 13. Environment & Deployment  
Mention environment setup:
- Configuration structure  
- Hosting model (e.g., Blazor Server vs WASM)  
- Deployment steps

---

## 14. Future Extensions  
Optional section for scalability, new feature ideas, or roadmap notes.


NOTES:
- Business Rules
  - Import a match from JSON/CSV
    - Only one match per day
    - If match exists and no game data - add, if game data - error.
    - Create match and add game data.

---

## 🏗️ Project Kickoff & Setup

### **Create Solution & Core Projects**

#### Create the Root Solution

- Open **Visual Studio**.
- Create a new **Blank Solution** named: `SportsStatistics`.
- Add existing files to the solution:
  - `.editorconfig`
  - `gitignore`
  - `DESIGN.md`
  - `LICENSE`    
  - `README.md`
  - `REQUIREMENTS.md`
- Create the following files:
  - `Directory.Build.props`
  - `Directory.Package.props`
- Create the following folders:
  - `_resources`
  - `src` (also solution folder)
  - `tests` (also solution folder)

#### Add Core Layered Projects

- Add the following projects to the `src` folder:

| Layer           | Project Name                     | Project Type                  | Purpose |
|----------------|----------------------------------|-------------------------------|---------|
| **Domain**      | `SportsStatistics.Domain`        | .NET Class Library            | Entities, value objects, domain events |
| **Application** | `SportsStatistics.Application`   | .NET Class Library            | Use cases, interfaces, DTOs |
| **Infrastructure** | `SportsStatistics.Infrastructure` | .NET Class Library            | EF Core, external services, persistence |
| **Web (UI)**    | `SportsStatistics.Web`           | Blazor Server App             | Frontend UI, DI setup, routing |
| **Shared**      | `SportsStatistics.Shared`        | .NET Class Library (optional) | Common utilities, enums, constants |

#### Add Aspire Host Projects
- Add a new **Aspire AppHost** project:
  - Name: `SportsStatistics.AppHost`
- Add a new **Aspire ServiceDefaults** project:
  - Name: `SportsStatistics.ServiceDefaults`

#### Set Up Project References

| From → To                             | Reason |
|--------------------------------------|--------|
| `Application` → `Domain`             | Use domain models in use cases |
| `Infrastructure` → `Application`     | Implement interfaces |
| `Web` → `Application`, `ServiceDefaults` | Inject services |
| `AppHost` → All other projects       | Aspire orchestration |
| `Shared` → All other projects        | If using shared utilities |

#### Setup SQL Server and EF Core

- Add the following packages in AppHost:
  - `Aspire.Hosting.SqlServer`

- Add the following packages in Infrastructure:
  - `Aspire.Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore`
  - `Microsoft.EntityFrameworkCore.Design`
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Tools`

- Create `SportsStatisticsDbContext` in `Persistence/`

- Configure DbContext in `Web` via DI:
  ```csharp
  builder.AddSqlServerDbContext<SportsStatisticsDbContext>("sportsstatistics-db");
  ```

#### Integrate with Aspire
- In `AppHost`, register services:
  ```csharp
  var sqlServer = builder.AddSqlServer("sql");
  var database = sqlServer.AddDatabase("sportsstatistics-db")
  builder.AddProject<Projects.SportsStatistics_Web>("sportsstatistics-web");
  ```

### **Configure Basic Routing & Layout**

- Establish main pages: Home, Match Tracker, Reports, Admin
- Add navigation and layout components
- TODO:
  - Layout
  - Signin, Register, Logout

#### Define Page Components

Create Razor pages for each main route in `SportsStatistics.Web/Pages/`:

| Page Name        | File Name                   | Route             | Purpose |
|------------------|-----------------------------|-------------------|---------|
| Home             | `Home/Index.razor`          | `/`               | Landing page, welcome message, quick links |
| Match Tracker    | `MatchTracker/Index.razor`  | `/match-tracker`  | Live in-game stat tracking UI |
| Reports          | `Reports/Index.razor`       | `/reports`        | Player and team performance charts |
| Admin            | `Admin/Index.razor`         | `/admin`          | Player and fixture management (role-restricted) |

Each page should include:
- `@page` directive for routing
- Basic layout placeholder
- Role-based access logic (for Admin)

#### Add Identity NuGet Packages

Add the following packages to the `SportsStatistics.Web` project:

- `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `Microsoft.AspNetCore.Identity.UI`

#### Create Identity Data Context

Create `ApplicationUser.cs` to extend Identity:

```csharp
public class ApplicationUser : IdentityUser
{
}
```

Update the existing `DbContext` to inherit from `IdentityDbContext`.

```csharp
public class SportsStatisticsDbContext : IdentityDbContext<ApplicationUser>
{
    public SportsStatisticsIdentityDbContext(DbContextOptions<SportsStatisticsDbContext> options)
        : base(options) { }
}
```

#### Register Identity Services

In `InfrastructureServiceRegistration.cs` of `SportsStatistics.Infrastructure`:

```csharp

builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
  options.Password.RequiredLength = 6;
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.SignIn.RequireConfirmedAccount = false;
  options.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppIdentityDbContext>();
```

#### Add Identity Routing

In `SportsStatistics.Web`.

- Create a `Signin.razor` component to `Components\Signin\`.
  - This component will be a placeholder for now with `@page "/signin"`.
- Create a `RedirectToSignin.razor` component in `Components\Shared\`.
  - This component will navigate to the `signin` page and force a reload.
- Update `_Imports.razor`, to include:
  - `Microsoft.AspNetCore.Components.Authorization`.
  - `SportsStatistics.Web.Components.Shared`.
- Update `Routes.razor`, to change the `RouteView` element to an `AuthorizeRouteView` element.

##### Add Identity Links to NavMenu

In `SportsStatistics.Web`.

- Update the `Components/Layout/NavMenu.razor` component to include:
  - AuthorizeView.
    - Authorized element for logout links.
    - NotAuthorized element for login and register links.

#### Apply Role-Based Access

In `SportsStatistics.Shared`.

- Create `Security/Roles.razor` component to include:
  - `public const string Administrator = nameof(Administrator);`

- Create `Security/Polices.razor` component to include:
  - `public const string RequireAdministratorRole = nameof(RequireAdministratorRole)`

In `SportsStatistics.Web`.

- Create `Admin/Pages/_Imports.razor`, and include:
  - `@attribute [Authorize(Policies.RequireAdministratorRole)]`

- Update `WebServiceRegistration.cs`, to include:
  - An `AddAuthorizationBuilder()` call on `builder.Service`, and then chain a call to `AddPolicy` to add the `RequireAdministratorRole` policy.

### **Add DatabaseMigrator Project**

#### Create the DatabaseMigrator Project

- Add a new **Worker Service** project to the `src` folder:
  - Name: `SportsStatistics.Tools.DatabaseMigrator`

- Add project references to:
  - `SportsStatistics.Application`
  - `SportsStatistics.Infrastructure`
  - `SportsStatistics.ServiceDefaults`
  - `SportsStatistics.Shared`

- Update `Program.cs` to include:
  - Call to `AddServiceDefaults`.
  - Add open telementry with tracing to `Services`.
  - Add the `ApplicationDbContext` to `Services`.
  - Make `Main` async.

#### Configure DatabaseMigrator for Aspire

In `SportsStatistics.Shared`.

- Update `Aspire\ProjectResourceConstants`, to include:
  - `public const string DatabaseMigrator = "sportsstatistics-databasemigrator";`

In `SportsStatistics.AppHost`.

- Update `AppHost.cs`, to include:
  - Declare a migrator project variable.
  - Add the project `SportsStatistics_Tools_DatabaseMigrator`.
  - With the name `ProjectResourceConstants.DatabaseMigrator`.
  - With a reference to the `database` project variable.
  - With a wait for `database` to be available.
  - Update the `web` project varibale to wait for `migrator` instead of `database`.

#### Implement DatabaseMigrator

In `SportsStatistics.Application`.

- Create a new record `MigrationResult.cs` in `Models/`
- Add the following properties:
  - `bool Success`
  - `string Message`
  - `int MigrationsApplied = 0`
  - `TimeSpan Duration = default`
  - `Exception? Exception = null`
- Add the following contructors:
  - `Success(int migrationsApplied, TimeSpan duration, string? message = null)`s
  - `Failure(string message, Exception? exception = null)`
  - `UpToDate(TimeSpan duration)`

- Create an interface `IDatabaseMigrationService` in `Interfaces/`
- Define the following contracts:
  - `Task<MigrationResult> MigrateAsync(CancellationToken cancellationToken = default);`

In `SportsStatistics.Infrastructure`.

- Create a new class `DatabaseMigrationService` in `Persistence/Services/`, that implements `IDatabaseMigrationService`.
- Implement the methods using `SportsStatisticsDbContext` to:
  - Use ExecutionStategy for resiliency.
  - Check connection.
  - Get pending and applied migrations.
  - Apply migrations and return a `MigrationResult`.

In `SportsStatistics.Tools.DatabaseMigrator`.

- Implement the `ExecuteAsync` method in the `Worker` class to:
  - Start a new `Activity` for tracing.
  - Call the `MigrateAsync` method on `IDatabaseMigrationService`.
  

#### Create Initial Migration and Update Database

In `Package Manager Console`

- Create the initial migration:
  - `Add-Migration InitialCreate -project SportsStatistics.Infrastructure -startupproject SportsStatistics.Web`.
- Run the `SportsStatistics.AppHost` application to trigger the migration via `SportsStatistics.Tools.DatabaseMigrator`.

---

## 🔐 Authentication & Identity

### **Integrate ASP.NET Core Identity**

#### Create Authentication Service

- Add user login, registration, and role management
- Seed initial roles: Administrator, Viewer

- In `SportsStatistics.Application`

- Create class `Models/ApplicationUserDto`:
  - `string Id`
  - `string Username`
  - `string Email`

- Create interface `Interfaces/Infrastruture/IAuthenticationService`:
  - `Task<ApplicationUserDto?> GetCurrentUserAsync();`
  - `Task<Result> PasswordSignInAsync(string email, string password, bool isPersistant);`
  - `Task SignOutAsync();`

### **Secure Page Access**

- Apply role-based authorization to routes and components
- Add login/logout UI and session handling

---

## 🛠️ Admin Tools

### **Add Player Management UI**

#### `Player` Entity

In `SportsStatistics.Domain`.

- Create a new class `Player.cs` in `Entities/`
  - `Guid Id`
  - `string Name`
  - `string Role`
  - `int SquadNumber`
  - `string Nationality`
  - `DateOnly DateOfBirth`
  - `int Age`

- Create forms to add/edit/delete players
- Connect to backend CRUD services

### 10. **Add Fixture Management UI**
- Build interface for scheduling and editing games
- Link fixtures to match tracking and reports

---

## ⚽ Match Tracking Core

### 5. **Build Player Grid & Action Buttons**
- Create UI for listing players and tracking actions (passes, shots, etc.)

### 6. **Implement Match Event Logging**
- Capture and store player actions with timestamps
- Wire up backend service to persist events

---

## 📊 Reports & Visualization

### 7. **Design Reports Page Layout**
- Add filters for player, match, and season
- Create placeholder charts and summary panels

### 8. **Connect Aggregated Data to Charts**
- Build backend logic to compute player stats
- Render charts dynamically based on selected filters

---

## 🔄 Real-Time Sync (Optional Enhancement)

### 11. **Enable Multi-User Live Sync**
- Set up SignalR hub for broadcasting match events
- Update UI components to respond to live data

---

## 🧪 Testing & Polish

### 12. **Add Unit & Integration Tests**
- Cover core services and data logic
- Validate key workflows (tracking, reporting, admin)

### 13. **Refine UX & Error Handling**
- Add loading states, validation messages, and fallback flows
- Polish layout and responsiveness

---

## 🚀 Deployment Prep

### 14. **Configure Hosting & Environment Settings**
- Set up appsettings, connection strings, and deployment profiles
- Prepare for hosting on IIS, Azure, or Linux server
