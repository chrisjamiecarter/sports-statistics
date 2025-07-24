# Sports Statistics

## Overview

I was hired by a Bytechester Rovers footbal club to build a software application that will keep track of their teams statistics. 
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
