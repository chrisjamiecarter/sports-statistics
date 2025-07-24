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

---

# Low-Level Design Document  
**Project**: Bytechester Rovers – Team Statistics Tracker  
**Technology Stack**: C#, ASP.NET Core, Blazor, Charting Library  
**Author**: Chris Carter
**Date**: 2025-07-04
**Version**: 0.1

---

## 1. Purpose  
Brief explanation of the document’s goal:  
> “This LLD outlines the internal architecture and component-level design for the football statistics tracking system, enabling real-time data capture and visualization through a Blazor-based web application.”

---

## 2. Module Overview  
A high-level listing of core modules with a 1-2 line description per module:
- **Authentication Module** – Manages user login, roles, and access rights.
- **Match Tracker Module** – Handles live in-game statistics entry.
- **Reports Module** – Displays aggregated player data with interactive charts.
- **Admin Module** – Allows management of players, fixtures, and users.
- **Data Sync Module** – Handles real-time UI updates via SignalR or state binding.

---

## 3. Component Design  
Describe each module’s internal components:
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
- `POST /api/event` – Tracks a new match action  
- `GET /api/player/{id}/stats` – Fetch player report  
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
